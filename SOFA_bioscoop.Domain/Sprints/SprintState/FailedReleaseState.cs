using System;
using System.Reflection.Metadata;

namespace SOFA_bioscoop.Domain
{
    public class FailedReleaseState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void CancelRelease(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void FinishSprint(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void HandlePostFinish(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void RetryRelease(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void StartPipeline(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void StartSprint(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void UploadReviewSummary(Sprint sprint, Document summary)
        {
            throw new NotImplementedException();
        }

        public void UploadReviewSummary(Sprint sprint, string summary)
        {
            throw new NotImplementedException();
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new NotImplementedException();
        }
    }
}
