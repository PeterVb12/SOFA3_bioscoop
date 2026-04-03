using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public interface ISprintTypeStrategy
    {
        ISprintState getPostFinishState(Sprint sprint);
        void cancel(Sprint sprint);
        void ExecutePostFinish(Sprint sprint);
    }
}
