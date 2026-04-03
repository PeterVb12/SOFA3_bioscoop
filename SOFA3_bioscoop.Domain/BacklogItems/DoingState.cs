using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA_bioscoop.Domain
{
    public class DoingState : IBacklogitemState
    {
        public void StartWork(BacklogItem item) =>
    throw new InvalidOperationException("Item is al in uitvoering.");

        public void ReadyForTesting(BacklogItem item)
        {
            item.SetState(item.GetReadyForTestingState());
            item.NotifyTesters();
        }

        public void StartTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item moet eerst Ready for Testing zijn.");

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