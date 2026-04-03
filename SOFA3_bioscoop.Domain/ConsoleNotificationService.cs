using System;

namespace SOFA_bioscoop.Domain
{
    public class ConsoleNotificationService : INotificationService
    {
        public void Send(Person recipient, string message)
        {
            Console.WriteLine("Notification sent to " + recipient.Name + ": " + message);
        }
    }
}
