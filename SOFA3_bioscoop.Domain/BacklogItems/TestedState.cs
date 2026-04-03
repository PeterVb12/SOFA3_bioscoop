using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA_bioscoop.Domain
{
    public class TestedState : IBacklogitemState
    {
        public void StartWork(BacklogItem item) =>
            throw new InvalidOperationException("Item is al getest.");

        public void ReadyForTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al getest.");

        public void StartTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al getest.");

        public void FinishTesting(BacklogItem item) =>
            throw new InvalidOperationException("Item is al getest.");

        public void Approve(BacklogItem item)
        {
            // lead developer keurt goed via definition of done
            item.SetState(item.GetDoneState());
        }

        public void Deny(BacklogItem item)
        {
            // lead developer keurt af: terug naar Ready for Testing
            item.SetState(item.GetReadyForTestingState());
            item.NotifyTesters();
        }
        public void Reject(BacklogItem item) =>
            throw new InvalidOperationException("Item is al getest, gebruik Deny voor definition of done afkeuring.");
        public void AddThread(BacklogItem item, Thread thread) =>
            item.threads.Add(thread);
    }
}