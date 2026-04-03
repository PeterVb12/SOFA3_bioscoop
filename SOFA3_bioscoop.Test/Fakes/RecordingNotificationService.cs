using System.Collections.Generic;
using SOFA_bioscoop.Domain;

namespace SOFA3_bioscoop.Test.Fakes
{
    public sealed class RecordingNotificationService : INotificationService
    {
        public List<(Person Recipient, string Message)> Sent { get; } = new();

        public void Send(Person recipient, string message)
        {
            Sent.Add((recipient, message));
        }
    }
}
