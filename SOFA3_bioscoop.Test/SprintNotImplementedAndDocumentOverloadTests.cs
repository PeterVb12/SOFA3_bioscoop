using System;
using System.Reflection.Metadata;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class SprintNotImplementedAndDocumentOverloadTests
    {
        [Fact]
        public void ReleasedState_AllSprintOperations_ThrowNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.SetState(sprint.GetReleasedState());
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<NotImplementedException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<NotImplementedException>(() => sprint.CancelRelease());
            Assert.Throws<NotImplementedException>(() => sprint.FinishSprint());
            Assert.Throws<NotImplementedException>(() => sprint.HandlePostFinish());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineFailure());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<NotImplementedException>(() => sprint.RetryRelease());
            Assert.Throws<NotImplementedException>(() => sprint.StartPipeline());
            Assert.Throws<NotImplementedException>(() => sprint.StartSprint());
            Assert.Throws<NotImplementedException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<NotImplementedException>(() => sprint.MarkAsReviewed());
            Assert.Throws<NotImplementedException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void FailedReleaseState_AllSprintOperations_ThrowNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.SetState(sprint.GetFailedReleaseState());
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<NotImplementedException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<NotImplementedException>(() => sprint.CancelRelease());
            Assert.Throws<NotImplementedException>(() => sprint.FinishSprint());
            Assert.Throws<NotImplementedException>(() => sprint.HandlePostFinish());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineFailure());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<NotImplementedException>(() => sprint.RetryRelease());
            Assert.Throws<NotImplementedException>(() => sprint.StartPipeline());
            Assert.Throws<NotImplementedException>(() => sprint.StartSprint());
            Assert.Throws<NotImplementedException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<NotImplementedException>(() => sprint.MarkAsReviewed());
            Assert.Throws<NotImplementedException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void ReviewedState_AllSprintOperations_ThrowNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.SetState(sprint.GetReviewedState());
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<NotImplementedException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<NotImplementedException>(() => sprint.CancelRelease());
            Assert.Throws<NotImplementedException>(() => sprint.FinishSprint());
            Assert.Throws<NotImplementedException>(() => sprint.HandlePostFinish());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineFailure());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<NotImplementedException>(() => sprint.RetryRelease());
            Assert.Throws<NotImplementedException>(() => sprint.StartPipeline());
            Assert.Throws<NotImplementedException>(() => sprint.StartSprint());
            Assert.Throws<NotImplementedException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<NotImplementedException>(() => sprint.MarkAsReviewed());
            Assert.Throws<NotImplementedException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void ClosedState_AllSprintOperations_ThrowNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.SetState(sprint.GetClosedState());
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<NotImplementedException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<NotImplementedException>(() => sprint.CancelRelease());
            Assert.Throws<NotImplementedException>(() => sprint.FinishSprint());
            Assert.Throws<NotImplementedException>(() => sprint.HandlePostFinish());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineFailure());
            Assert.Throws<NotImplementedException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<NotImplementedException>(() => sprint.RetryRelease());
            Assert.Throws<NotImplementedException>(() => sprint.StartPipeline());
            Assert.Throws<NotImplementedException>(() => sprint.StartSprint());
            Assert.Throws<NotImplementedException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<NotImplementedException>(() => sprint.MarkAsReviewed());
            Assert.Throws<NotImplementedException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void FinishedState_UploadReviewSummary_DocumentOverload_Executes()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            var finished = Assert.IsType<FinishedState>(PrivateStateAccessor.GetSprintState(sprint));
            finished.UploadReviewSummary(sprint, default(Document));
        }

        [Fact]
        public void InProgressState_UploadReviewSummary_DocumentOverload_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            var state = Assert.IsType<InProgressState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<InvalidOperationException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }

        [Fact]
        public void ReleasingState_UploadReviewSummary_DocumentOverload_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            var state = Assert.IsType<ReleasingState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<InvalidOperationException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }

        [Fact]
        public void FailedReleaseState_UploadReviewSummary_DocumentOverload_ThrowsNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.SetState(sprint.GetFailedReleaseState());
            var state = Assert.IsType<FailedReleaseState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<NotImplementedException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }

        [Fact]
        public void CancelledState_UploadReviewSummary_DocumentOverload_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            sprint.CancelRelease();
            var state = Assert.IsType<CancelledState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<InvalidOperationException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }

        [Fact]
        public void ClosedState_UploadReviewSummary_DocumentOverload_ThrowsNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.SetState(sprint.GetClosedState());
            var state = Assert.IsType<ClosedState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<NotImplementedException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }

        [Fact]
        public void ReviewedState_UploadReviewSummary_DocumentOverload_ThrowsNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.SetState(sprint.GetReviewedState());
            var state = Assert.IsType<ReviewedState>(PrivateStateAccessor.GetSprintState(sprint));
            Assert.Throws<NotImplementedException>(() => state.UploadReviewSummary(sprint, default(Document)));
        }
    }
}
