using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA_bioscoop.Domain
{
    public class DoneState : IBacklogitemState
    {
        public void StartWork(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void ReadyForTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void StartTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void FinishTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void Approve(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void Deny(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");

        public void Reject(BacklogItem item) =>
            throw new InvalidOperationException("Item is al Done.");
        public void AddThread(BacklogItem item, Thread thread) =>
            item.threads.Add(thread);
    }
}