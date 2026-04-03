using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class InProgressState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            sprint.AddBacklogItem(item);
        }

        public void CancelRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void FinishSprint(Sprint sprint)
        {
            sprint.SetState(sprint.GetFinishedState());
        }

        public void HandlePostFinish(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void RetryRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void StartPipeline(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void StartSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in progress");
        }

        public void UploadReviewSummary(Sprint sprint, Document summary)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void UploadReviewSummary(Sprint sprint, string summary)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is still in progress, finish first");
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in progress");
        }
    }
}
