using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public interface ISprintTypeStrategy
    {
        void executePostFinish(Sprint sprint);
        void cancel(Sprint sprint);
    }
}
