// NOTE: Legacy interface retained temporarily for compatibility but aligned to .NET event/delegate types.
namespace Moga.Windows.Phone
{
    public interface IControllerManagerPublicNonVirtuals
    {
        void Connect();
        ControllerAction GetKeyCode(KeyCode keyCode);
        float GetAxisValue(Axis axis);
        ControllerResult GetState(ControllerState info);
        void SetPacketMonitor(System.Action<object> packetMonitor);
        void SetListener(IControllerListener listener);
        void Suspending();
        void Resuming();
        void Close();
        void OpenBluetoothSettings();

        event System.EventHandler<KeyEvent> KeyChanged;
        event System.EventHandler<MotionEvent> AxisChanged;
        event System.EventHandler<StateEvent> StateChanged;
        event System.EventHandler ConnectionRetry;
        event System.EventHandler Connected;
        event System.EventHandler Disconnected;
        event System.EventHandler BluetoothUnavailable;

        bool EventsActive { get; }
    }
}
