using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class ReleasingState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void CancelRelease(Sprint sprint)
        {
            sprint.notificationService?.Send(sprint.GetScrumMaster(), "Results insufficient, release cancelled");
            sprint.notificationService?.Send(sprint.GetProductOwner(), "Results insufficient, release cancelled");
            sprint.SetState(sprint.GetCancelledState());
        }

        public void FinishSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void HandlePostFinish(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void RetryRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void StartPipeline(Sprint sprint)
        {
            try
            {
                if (sprint.developmentPipeline == null)
                    throw new InvalidOperationException("No development pipeline configured.");

                sprint.developmentPipeline.ReleasePipeline();
                sprint.notificationService?.Send(sprint.GetScrumMaster(), "Release succesful");
                sprint.SetState(sprint.GetReleasedState());
            }
            catch (Exception)
            {
                sprint.notificationService?.Send(sprint.GetScrumMaster(), "Release failed");
            }
        }

        public void StartSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void UploadReviewSummary(Sprint sprint, Document summary)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void UploadReviewSummary(Sprint sprint, string summary)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is waiting to be released");
        }
    }
}
