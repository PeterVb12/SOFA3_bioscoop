using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    class ReviewStrategy : ISprintTypeStrategy
    {
        public void cancel(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public ISprintState getPostFinishState(Sprint sprint)
        {
            return sprint.GetInReviewState();
        }
    }
}
