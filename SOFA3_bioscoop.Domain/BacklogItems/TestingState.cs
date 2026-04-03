using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA_bioscoop.Domain
{
    public class TestingState : IBacklogitemState
    {
        public void StartWork(BacklogItem item) =>
            throw new InvalidOperationException("Item is in Testing, kan niet terug naar Doing.");

        public void ReadyForTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al in Testing.");

        public void StartTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al in Testing.");

        public void FinishTesting(BacklogItem item)
        {
            item.SetState(item.GetTestedState());
        }

        public void Approve(BacklogItem item) =>
            throw new InvalidOperationException("Item moet eerst Tested zijn.");

        public void Deny(BacklogItem item) =>
            throw new InvalidOperationException("Item moet eerst Tested zijn.");

        public void Reject(BacklogItem item)
        {
            // tester keurt af: terug naar Todo, notificatie naar scrum master
            item.SetState(item.GetTodoState());
            item.NotifyScrumMaster();
        }
        public void AddThread(BacklogItem item, Thread thread) =>
            item.threads.Add(thread);
    }
}