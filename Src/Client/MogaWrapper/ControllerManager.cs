// Managed implementation of Moga ControllerManager using Windows.Gaming.Input
// Converted from P/Invoke to UWP-compatible implementation for Windows 10 Mobile SDK 15063

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Gaming.Input;
using Windows.System;
using Windows.System.Threading;

namespace Moga.Windows.Phone
{
    public sealed class ControllerManager
    {
        private static ControllerManager _instance;
        private Gamepad _currentGamepad;
        private GamepadReading _previousReading;
        private GamepadReading _currentReading;
        private IControllerListener _listener;
        private Action<object> _packetMonitor;
        private bool _isConnected;
        private bool _eventsActive;
        private ThreadPoolTimer _pollTimer;
        private bool _handlersRegistered;

        // Constructor
        public ControllerManager()
        {
            _eventsActive = false;
            _isConnected = false;
            _handlersRegistered = false;
        }

        // Static factory method for compatibility
        public static ControllerManager GetInstance()
        {
            if (_instance == null)
                _instance = new ControllerManager();
            return _instance;
        }

        // Public methods
        public void Connect()
        {
            try
            {
                if (!_handlersRegistered)
                {
                    Gamepad.GamepadAdded += OnGamepadAdded;
                    Gamepad.GamepadRemoved += OnGamepadRemoved;
                    _handlersRegistered = true;
                }
                var gamepads = Gamepad.Gamepads;
                if (gamepads.Count > 0)
                {
                    _currentGamepad = gamepads[0];
                    _isConnected = true;
                    _eventsActive = true;
                    StartGamepadMonitoring();
                    Connected?.Invoke(this, EventArgs.Empty);
                    StateChanged?.Invoke(this, new StateEvent(ControllerState.Connection, ControllerResult.Connected));
                }
                else
                {
                    ConnectionRetry?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                BluetoothUnavailable?.Invoke(this, EventArgs.Empty);
            }
        }

        public ControllerAction GetKeyCode(KeyCode keyCode)
        {
            if (!_isConnected || _currentGamepad == null)
                return ControllerAction.Unpressed;

            _currentReading = _currentGamepad.GetCurrentReading();

            switch (keyCode)
            {
                case KeyCode.A:
                    return (_currentReading.Buttons & GamepadButtons.A) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.B:
                    return (_currentReading.Buttons & GamepadButtons.B) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.X:
                    return (_currentReading.Buttons & GamepadButtons.X) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.Y:
                    return (_currentReading.Buttons & GamepadButtons.Y) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.Start:
                    return (_currentReading.Buttons & GamepadButtons.Menu) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.Select:
                    return (_currentReading.Buttons & GamepadButtons.View) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.L1:
                    return (_currentReading.Buttons & GamepadButtons.LeftShoulder) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.R1:
                    return (_currentReading.Buttons & GamepadButtons.RightShoulder) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.L2:
                    return _currentReading.LeftTrigger > 0.5 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.R2:
                    return _currentReading.RightTrigger > 0.5 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.ThumbLeft:
                    return (_currentReading.Buttons & GamepadButtons.LeftThumbstick) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.ThumbRight:
                    return (_currentReading.Buttons & GamepadButtons.RightThumbstick) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.DirectionUp:
                    return (_currentReading.Buttons & GamepadButtons.DPadUp) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.DirectionDown:
                    return (_currentReading.Buttons & GamepadButtons.DPadDown) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.DirectionLeft:
                    return (_currentReading.Buttons & GamepadButtons.DPadLeft) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                case KeyCode.DirectionRight:
                    return (_currentReading.Buttons & GamepadButtons.DPadRight) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                default:
                    return ControllerAction.Unpressed;
            }
        }

        public float GetAxisValue(Axis axis)
        {
            if (!_isConnected || _currentGamepad == null)
                return 0.0f;

            _currentReading = _currentGamepad.GetCurrentReading();

            switch (axis)
            {
                case Axis.X:
                    return (float)_currentReading.LeftThumbstickX;
                case Axis.Y:
                    return (float)_currentReading.LeftThumbstickY;
                case Axis.Z:
                    return (float)_currentReading.RightThumbstickX;
                case Axis.RZ:
                    return (float)_currentReading.RightThumbstickY;
                case Axis.LeftTrigger:
                    return (float)_currentReading.LeftTrigger;
                case Axis.RightTrigger:
                    return (float)_currentReading.RightTrigger;
                default:
                    return 0.0f;
            }
        }

        public ControllerResult GetState(ControllerState info)
        {
            switch (info)
            {
                case ControllerState.Connection:
                    return _isConnected ? ControllerResult.Connected : ControllerResult.Disconnected;
                case ControllerState.PowerLow:
                    // Battery info not available in Windows.Gaming.Input for SDK 15063
                    return ControllerResult.False;
                case ControllerState.SupportedVersion:
                    return ControllerResult.VersionMogaPro; // Assume Pro version for XInput compatibility
                case ControllerState.SelectedVersion:
                    return ControllerResult.VersionMogaPro;
                default:
                    return ControllerResult.False;
            }
        }

        public void SetPacketMonitor(Action<object> packetMonitor)
        {
            _packetMonitor = packetMonitor;
        }

        public void SetListener(IControllerListener listener)
        {
            _listener = listener;
        }

        public void Suspending()
        {
            _eventsActive = false;
            _pollTimer?.Cancel();
            _pollTimer = null;
        }

        public void Resuming()
        {
            if (_isConnected)
            {
                _eventsActive = true;
                if (_pollTimer == null)
                {
                    StartGamepadMonitoring();
                }
            }
        }

        public void Close()
        {
            _eventsActive = false;
            _isConnected = false;
            _pollTimer?.Cancel();
            _pollTimer = null;
            _currentGamepad = null;
            if (_handlersRegistered)
            {
                Gamepad.GamepadAdded -= OnGamepadAdded;
                Gamepad.GamepadRemoved -= OnGamepadRemoved;
                _handlersRegistered = false;
            }
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public async void OpenBluetoothSettings()
        {
            try
            {
                await Launcher.LaunchUriAsync(new Uri("ms-settings:bluetooth"));
            }
            catch (Exception)
            {
                // Fallback for older versions
                await Launcher.LaunchUriAsync(new Uri("ms-settings:network-bluetooth"));
            }
        }

        // Standard .NET events
        public event EventHandler<KeyEvent> KeyChanged;
        public event EventHandler<MotionEvent> AxisChanged;
        public event EventHandler<StateEvent> StateChanged;
        public event EventHandler ConnectionRetry;
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler BluetoothUnavailable;

        public bool EventsActive => _eventsActive;

        // Private helper methods
        private void StartGamepadMonitoring()
        {
            if (_currentGamepad == null) return;
            _previousReading = _currentGamepad.GetCurrentReading();
            _pollTimer?.Cancel();
            _pollTimer = ThreadPoolTimer.CreatePeriodicTimer(_ =>
            {
                try
                {
                    CheckGamepadInput();
                }
                catch { /* swallow to keep timer alive */ }
            }, TimeSpan.FromMilliseconds(33)); // ~30Hz
        }

        // This method should be called periodically to check for input changes
        private void CheckGamepadInput()
        {
            if (!_eventsActive || _currentGamepad == null) return;

            _currentReading = _currentGamepad.GetCurrentReading();

            // Check for button changes
            CheckButtonChanges();
            
            // Check for axis changes
            CheckAxisChanges();

            _previousReading = _currentReading;
        }

        private void CheckButtonChanges()
        {
            var previousButtons = _previousReading.Buttons;
            var currentButtons = _currentReading.Buttons;
            var changedButtons = previousButtons ^ currentButtons;

            if (changedButtons != GamepadButtons.None)
            {
                // Map gamepad buttons to KeyCode and fire events
                CheckButtonChange(GamepadButtons.A, KeyCode.A, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.B, KeyCode.B, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.X, KeyCode.X, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.Y, KeyCode.Y, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.Menu, KeyCode.Start, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.View, KeyCode.Select, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.LeftShoulder, KeyCode.L1, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.RightShoulder, KeyCode.R1, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.LeftThumbstick, KeyCode.ThumbLeft, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.RightThumbstick, KeyCode.ThumbRight, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.DPadUp, KeyCode.DirectionUp, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.DPadDown, KeyCode.DirectionDown, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.DPadLeft, KeyCode.DirectionLeft, changedButtons, currentButtons);
                CheckButtonChange(GamepadButtons.DPadRight, KeyCode.DirectionRight, changedButtons, currentButtons);
            }
        }

        private void CheckButtonChange(GamepadButtons gamepadButton, KeyCode keyCode, GamepadButtons changedButtons, GamepadButtons currentButtons)
        {
            if ((changedButtons & gamepadButton) != 0)
            {
                var action = (currentButtons & gamepadButton) != 0 ? ControllerAction.Pressed : ControllerAction.Unpressed;
                var keyEvent = new KeyEvent(keyCode, action);
                
                KeyChanged?.Invoke(this, keyEvent);
                _listener?.OnKeyEvent(keyEvent);
            }
        }

        private void CheckAxisChanges()
        {
            const double stickThreshold = 0.05;   // dead zone for sticks
            const double triggerThreshold = 0.05; // dead zone for triggers

            // Left stick
            CheckAxisChange(Axis.X, _previousReading.LeftThumbstickX, _currentReading.LeftThumbstickX, stickThreshold);
            CheckAxisChange(Axis.Y, _previousReading.LeftThumbstickY, _currentReading.LeftThumbstickY, stickThreshold);

            // Right stick
            CheckAxisChange(Axis.Z, _previousReading.RightThumbstickX, _currentReading.RightThumbstickX, stickThreshold);
            CheckAxisChange(Axis.RZ, _previousReading.RightThumbstickY, _currentReading.RightThumbstickY, stickThreshold);

            // Triggers
            CheckAxisChange(Axis.LeftTrigger, _previousReading.LeftTrigger, _currentReading.LeftTrigger, triggerThreshold);
            CheckAxisChange(Axis.RightTrigger, _previousReading.RightTrigger, _currentReading.RightTrigger, triggerThreshold);
        }

        private void CheckAxisChange(Axis axis, double previousValue, double currentValue, double threshold)
        {
            if (Math.Abs(currentValue - previousValue) > threshold)
            {
                var motionEvent = new MotionEvent(axis, (float)currentValue);
                
                AxisChanged?.Invoke(this, motionEvent);
                _listener?.OnMotionEvent(motionEvent);
            }
        }

        private void OnGamepadAdded(object sender, Gamepad gamepad)
        {
            if (_currentGamepad == null)
            {
                _currentGamepad = gamepad;
                _isConnected = true;
                if (_eventsActive)
                {
                    StartGamepadMonitoring();
                    Connected?.Invoke(this, EventArgs.Empty);
                    StateChanged?.Invoke(this, new StateEvent(ControllerState.Connection, ControllerResult.Connected));
                }
            }
        }

        private void OnGamepadRemoved(object sender, Gamepad gamepad)
        {
            if (_currentGamepad == gamepad)
            {
                _pollTimer?.Cancel();
                _pollTimer = null;
                _currentGamepad = null;
                _isConnected = false;
                StateChanged?.Invoke(this, new StateEvent(ControllerState.Connection, ControllerResult.Disconnected));
                Disconnected?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
