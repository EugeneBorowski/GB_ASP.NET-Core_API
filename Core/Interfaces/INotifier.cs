using System.Diagnostics;

namespace Core.Interfaces
{
    public interface INotifier
    {
        void Notify();
    }
    public class Notifier1 : INotifier
    {
        public void Notify()
        {
            Debug.WriteLine("Debugging from Notifier 1");
        }
    }
}
