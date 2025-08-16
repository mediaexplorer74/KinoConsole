namespace Moga.Windows.Phone
{
    public interface IControllerListener
    {
        void OnKeyEvent(KeyEvent e);
        void OnMotionEvent(MotionEvent e);
        void OnStateEvent(StateEvent e);
    }
}
