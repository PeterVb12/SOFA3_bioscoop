using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
    
namespace SOFA_bioscoop.Domain
{ 
    public class CreatedState : ISprintState
{
    public void AddBacklogItem(Sprint sprint, BacklogItem item)
    {
        throw new InvalidOperationException("Must start sprint first");    
    }

    public void CancelRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Must start sprint first");
    }

    public void ValidateEdit(Sprint sprint)
    {
        // hoeft niks te doen, want in de created state is het valid om te editen.
    }

    public void FinishSprint(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot finish sprint while in creation fase");
    }

    public void HandlePostFinish(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot finish sprint while in creation fase");
    }

    public void OnPipelineFailure(Sprint sprint)
    {
        throw new InvalidOperationException("Start sprint first to execute pipeline operations");
    }

    public void OnPipelineSuccess(Sprint sprint)
    {
        throw new InvalidOperationException("Start sprint first to execute pipeline operations");
    }

    public void RetryRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Start sprint first to retry release");
    }

    public void StartPipeline(Sprint sprint)
    {
        throw new InvalidOperationException("Start sprint first to execute pipeline operations");
    }

    public void StartSprint(Sprint sprint)
    {
        sprint.SetState(sprint.GetInProgressState());
    }

    public void UploadReviewSummary(Sprint sprint, Document summary)
    {
        throw new InvalidOperationException("Start sprint first to upload summary");
    }
}
}
