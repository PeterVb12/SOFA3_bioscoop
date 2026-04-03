using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA_bioscoop.Domain
{
    public class ReadyForTestingState : IBacklogitemState
    {
        public void StartWork(BacklogItem item) =>
            throw new InvalidOperationException("Item wacht op testing, kan niet terug naar Doing.");

        public void ReadyForTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Ready for Testing.");

        public void StartTesting(BacklogItem item)
        {
            item.SetState(item.GetTestingState());
        }

        public void FinishTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is nog niet in Testing.");

        public void Approve(BacklogItem item) =>
            throw new InvalidOperationException("Item is nog niet getest.");

        public void Deny(BacklogItem item) =>
            throw new InvalidOperationException("Item is nog niet getest.");

        public void Reject(BacklogItem item) =>
            throw new InvalidOperationException("Item is nog niet in Testing.");

        public void AddThread(BacklogItem item, Thread thread) =>
            item.threads.Add(thread);
    }
}