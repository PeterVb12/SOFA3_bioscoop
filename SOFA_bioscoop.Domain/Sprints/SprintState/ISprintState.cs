using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public interface ISprintState
    {

        void ValidateEdit(Sprint sprint);

        void AddBacklogItem(Sprint sprint, BacklogItem item);

        void StartSprint(Sprint sprint);

        void FinishSprint(Sprint sprint);
        void HandlePostFinish(Sprint sprint);
        void StartPipeline(Sprint sprint);
        void OnPipelineSuccess(Sprint sprint);
        void OnPipelineFailure(Sprint sprint);

        void RetryRelease(Sprint sprint);
        void CancelRelease(Sprint sprint);
        void UploadReviewSummary(Sprint sprint, string summary);
        void MarkAsReviewed(Sprint sprint);
    }
}
