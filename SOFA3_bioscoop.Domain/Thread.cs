using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class Thread
    {
        public string Title { get; }
        private readonly List<Message> messages = new List<Message>();
        private readonly List<IObserver> observers = new List<IObserver>();

        public Thread(string title)
        {
            Title = title;
        }

        public void AddMessage(Message message)
        {
            messages.Add(message);
            NotifyObservers(message);
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(Message message)
        {
            observers.ForEach(o => o.Update(message));
        }

        public IReadOnlyList<Message> GetMessages() => messages.AsReadOnly();
    }
}
