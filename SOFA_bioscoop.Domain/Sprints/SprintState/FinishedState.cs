using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class FinishedState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw new InvalidOperationException("Sprint is finished");
        }

        public void CancelRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished, start release first");
        }

        public void FinishSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished");
        }

        public void HandlePostFinish(Sprint sprint)
        {
            sprint.SetState(sprint.GetPostFinishedState());
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished, start pipeline first");
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished, start pipeline first");
        }

        public void RetryRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished, start release first");
        }

        public void StartPipeline(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished, start pipeline first");
        }

        public void StartSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished");
        }

        public void UploadReviewSummary(Sprint sprint, Document summary)
        {
            //generate review method call
        }

        public void UploadReviewSummary(Sprint sprint, string summary)
        {
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished");
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is finished");
        }
    }
}
