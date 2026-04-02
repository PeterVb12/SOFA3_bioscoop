using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class ReleaseStrategy : ISprintTypeStrategy
    {
        public void cancel(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void executePostFinish(Sprint sprint)
        {
            throw new NotImplementedException();
        }
    }
}
