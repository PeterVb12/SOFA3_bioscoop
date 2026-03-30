using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain.BacklogItems
{
    public interface IBacklogitemState
    {
            void StartWork(BacklogItem item);

            void ReadyForTesting(BacklogItem item);

            void StartTesting(BacklogItem item);

            void FinishTesting(BacklogItem item);

            void Reject(BacklogItem item);

            void Approve(BacklogItem item);

            void DenyDefinitionOfDone(BacklogItem item);
    }
}
