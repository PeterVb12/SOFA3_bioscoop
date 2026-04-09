using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.Pipelines;
using SOFA3_bioscoop.Test.Fakes;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class SprintStateExhaustiveTests
    {
        [Fact]
        public void Created_AddBacklogItem_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            var item = SprintTestFactories.MinimalBacklogItem();
            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
        }

        [Fact]
        public void Created_FinishSprint_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
        }

        [Fact]
        public void Created_HandlePostFinish_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.HandlePostFinish());
        }

        [Fact]
        public void Created_StartPipeline_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.StartPipeline());
        }

        [Fact]
        public void Created_OnPipelineSuccess_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
        }

        [Fact]
        public void Created_OnPipelineFailure_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
        }

        [Fact]
        public void Created_RetryRelease_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
        }

        [Fact]
        public void Created_CancelRelease_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.CancelRelease());
        }

        [Fact]
        public void Created_UploadReviewSummary_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.UploadReviewSummary("x"));
        }

        [Fact]
        public void Created_MarkAsReviewed_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
        }

        [Fact]
        public void InProgress_AddBacklogItem_AddsToBacklog()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            var item = SprintTestFactories.MinimalBacklogItem();
            sprint.AddBacklogItem(item);
            Assert.Equal(1, PrivateStateAccessor.GetSprintBacklogCount(sprint));
        }

        [Fact]
        public void InProgress_StartSprintAgain_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        [Fact]
        public void InProgress_EditName_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void InProgress_FinishSprint_MovesToFinished()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            Assert.Same(sprint.GetFinishedState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void InProgress_HandlePostFinish_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.HandlePostFinish());
        }

        [Fact]
        public void Finished_AddBacklogItem_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(SprintTestFactories.MinimalBacklogItem()));
        }

        [Fact]
        public void Finished_HandlePostFinish_ReleaseStrategy_GoesToReleasing()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            Assert.Same(sprint.GetReleasingState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void Finished_HandlePostFinish_ReviewStrategy_GoesToInReview()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            Assert.Same(sprint.GetInReviewState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void Finished_UploadReviewSummary_String_IsNoOp()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.UploadReviewSummary("ignored until in review");
            Assert.False(sprint.HasReviewSummary());
        }

        [Fact]
        public void Finished_StartSprint_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        [Fact]
        public void Releasing_StartPipeline_Success_MovesToReleased()
        {
            var recorder = new RecordingNotificationService();
            var sprint = SprintTestFactories.CreateReleaseSprint(
                new DeploymentPipeline(),
                recorder);
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.StartPipeline();

            Assert.Same(sprint.GetReleasedState(), PrivateStateAccessor.GetSprintState(sprint));
            Assert.Contains(recorder.Sent, m => m.Message.Contains("succes", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Releasing_StartPipeline_NoPipeline_ThrowsAndSendsFailure()
        {
            var recorder = new RecordingNotificationService();
            var sprint = SprintTestFactories.CreateReleaseSprint(notifications: recorder);
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.StartPipeline();

            Assert.Same(sprint.GetReleasingState(), PrivateStateAccessor.GetSprintState(sprint));
            Assert.Contains(recorder.Sent, m => m.Message.Contains("failed", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Releasing_StartPipeline_PipelineThrows_SendsFailureNotification()
        {
            var recorder = new RecordingNotificationService();
            var sprint = SprintTestFactories.CreateReleaseSprint(new FailingAtBuildPipeline(), recorder);
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.StartPipeline();

            Assert.Contains(recorder.Sent, m => m.Message.Contains("failed", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Releasing_CancelRelease_MovesToCancelled_AndNotifiesStakeholders()
        {
            var recorder = new RecordingNotificationService();
            var sprint = SprintTestFactories.CreateReleaseSprint(notifications: recorder);
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.CancelRelease();

            Assert.Same(sprint.GetCancelledState(), PrivateStateAccessor.GetSprintState(sprint));
            Assert.Contains(recorder.Sent, x => x.Recipient.Role == Role.ScrumMaster);
            Assert.Contains(recorder.Sent, x => x.Recipient.Role == Role.ProductOwner);
        }

        [Fact]
        public void Releasing_CancelRelease_WithoutNotificationService_StillCancels()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.CancelRelease();

            Assert.Same(sprint.GetCancelledState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void Cancelled_RetryRelease_ReturnsToReleasing()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            sprint.CancelRelease();

            sprint.RetryRelease();

            Assert.Same(sprint.GetReleasingState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void InReview_UploadThenMarkReviewed_MovesToReviewed()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.UploadReviewSummary("Summary text");
            Assert.True(sprint.HasReviewSummary());
            sprint.MarkAsReviewed();

            Assert.Same(sprint.GetReviewedState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void InReview_MarkAsReviewed_WithoutSummary_Throws()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
        }

        [Fact]
        public void InReview_EditName_ThrowsNotImplemented()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            Assert.Throws<NotImplementedException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void Releasing_MostOperationsStillThrow_InvalidOperation()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint(new DeploymentPipeline());
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.HandlePostFinish());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
        }

        [Fact]
        public void Cancelled_StartSprint_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            sprint.CancelRelease();

            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        [Fact]
        public void GetPostFinishedState_MatchesStrategy()
        {
            var release = SprintTestFactories.CreateReleaseSprint();
            var review = SprintTestFactories.CreateReviewSprint();
            Assert.Same(release.GetReleasingState(), release.GetPostFinishedState());
            Assert.Same(review.GetInReviewState(), review.GetPostFinishedState());
        }

        private sealed class FailingAtBuildPipeline : DevelopmentPipeline
        {
            protected override void FetchSources()
            {
            }

            protected override void InstallPackages()
            {
            }

            protected override void Build()
            {
                throw new InvalidOperationException("build fail");
            }

            protected override void Test()
            {
            }

            protected override void Analyse()
            {
            }

            protected override void Deploy()
            {
            }
        }
    }
}
