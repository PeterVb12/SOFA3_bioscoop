using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFA_bioscoop.Domain;

namespace SOFA3_bioscoop.DomainServices
{
    public interface INotificationService
    {
        void Send(Person recipient, string message);
    }
}
