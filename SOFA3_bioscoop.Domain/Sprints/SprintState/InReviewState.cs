using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SOFA_bioscoop.Domain
{
    public class InReviewState : ISprintState
    {
        public void AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void CancelRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void FinishSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void HandlePostFinish(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void OnPipelineFailure(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void OnPipelineSuccess(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void RetryRelease(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void StartPipeline(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        public void StartSprint(Sprint sprint)
        {
            throw new InvalidOperationException("Sprint is in review");
        }

        //"De
        //scrum master initieert de bijbehorende actie en kan deze alleen uitvoeren wanneer hij een
        //samenvatting van de review als document voor de sprint heeft geüpload."
        public void UploadReviewSummary(Sprint sprint, string summary)
        {
            sprint.SetReviewSummary(summary);
        }

        public void MarkAsReviewed(Sprint sprint)
        {
            if (!sprint.HasReviewSummary())
            {
                throw new InvalidOperationException("Upload review summary first");
            }

            sprint.SetState(sprint.GetReviewedState());
        }

        public void ValidateEdit(Sprint sprint)
        {
            throw new NotImplementedException();
        }
    }
}
