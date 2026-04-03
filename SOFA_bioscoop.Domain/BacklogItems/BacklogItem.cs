using SOFA_bioscoop.Domain.BacklogItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class BacklogItem : IBacklogitemState
    {
        private string title;
        private string description;
        private IBacklogitemState state;
        private List<Thread> threads = new List<Thread>();

        public BacklogItem(string title, string description)
        {
            this.title = title;
            this.description = description;
            //state = new TodoState();


        }
        public void AddThread(Thread thread)
        {
            threads.Add(thread);
        }

        public void SetState(IBacklogitemState state)
        {
            this.state = state;
        }
        public void Approve(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void DenyDefinitionOfDone(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void FinishTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void ReadyForTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void Reject(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void StartTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void StartWork(BacklogItem item)
        {
            throw new NotImplementedException();
        }
    }
}
