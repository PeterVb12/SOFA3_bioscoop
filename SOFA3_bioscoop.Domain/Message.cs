using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class Message
    {
        public Person Sender { get; }
        public string Content { get; }
        public DateTime Timestamp { get; }

        public Message(Person sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
        }
    }
}
