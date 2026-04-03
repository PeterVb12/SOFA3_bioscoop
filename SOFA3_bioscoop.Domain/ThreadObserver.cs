using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class ThreadObserver : IObserver
    {
        private Person person;
        private List<INotificationService> notificationServices;

        public ThreadObserver(Person person, List<INotificationService> notificationServices)
        {
            this.person = person;
            this.notificationServices = notificationServices;
        }
        //hoihoi
        public void Update(Message message)
        {
            if (message.Sender.Equals(person)) return;

            notificationServices.ForEach(s => s.Send(person, message.Content));
        }
    }
}
