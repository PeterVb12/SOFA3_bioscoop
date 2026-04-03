using System;
using System.Reflection.Metadata;

namespace SOFA_bioscoop.Domain
{
    public class CancelledState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void CancelRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void FinishSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void HandlePostFinish(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void RetryRelease(Sprint sprint)
        {
            sprint.SetState(sprint.GetReleasingState());
        }

        public void StartPipeline(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void StartSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void UploadReviewSummary(Sprint sprint, Document summary)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void UploadReviewSummary(Sprint sprint, string summary)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is cancelled");
        }
    }
}
