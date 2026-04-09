using System;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class SprintTypeStrategyTests
    {
        [Fact]
        public void ReleaseStrategy_GetPostFinishState_ReturnsReleasingState()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            var strategy = new ReleaseStrategy();
            Assert.Same(sprint.GetReleasingState(), strategy.getPostFinishState(sprint));
        }

        [Fact]
        public void ReviewStrategy_GetPostFinishState_ReturnsInReviewState()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            var strategy = new ReviewStrategy();
            Assert.Same(sprint.GetInReviewState(), strategy.getPostFinishState(sprint));
        }

        [Fact]
        public void ReleaseStrategy_Cancel_ThrowsNotImplemented()
        {
            var strategy = new ReleaseStrategy();
            Assert.Throws<NotImplementedException>(() => strategy.cancel(SprintTestFactories.CreateReleaseSprint()));
        }

        [Fact]
        public void ReviewStrategy_Cancel_ThrowsNotImplemented()
        {
            var strategy = new ReviewStrategy();
            Assert.Throws<NotImplementedException>(() => strategy.cancel(SprintTestFactories.CreateReviewSprint()));
        }
    }
}
