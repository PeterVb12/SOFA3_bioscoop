using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    // TC-06
    public class SprintPipelineWorkflowTests
    {
        // TC-06
        [Fact]
        public void Sprint_ShouldEnterReleasingState_WhenHandlePostFinish_AfterFinished()
        {
            Sprint sprint = CreateDeploymentSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            Assert.Same(sprint.GetFinishedState(), PrivateStateAccessor.GetSprintState(sprint));

            sprint.HandlePostFinish();

            Assert.Same(sprint.GetReleasingState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        private static Sprint CreateDeploymentSprint()
        {
            return new Sprint(
                "TC06-DeploymentSprint",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
        }
    }
}
