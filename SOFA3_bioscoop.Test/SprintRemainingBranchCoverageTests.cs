using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.Pipelines;
using SOFA3_bioscoop.Test.Fakes;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;
using DomainThread = SOFA_bioscoop.Domain.Thread;

namespace SOFA3_bioscoop.Test
{
    public class SprintRemainingBranchCoverageTests
    {
        [Fact]
        public void InProgress_InvalidOperations_Throw()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<InvalidOperationException>(() => sprint.CancelRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.StartPipeline());
            Assert.Throws<InvalidOperationException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
        }

        [Fact]
        public void Finished_InvalidOperations_Throw()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<InvalidOperationException>(() => sprint.CancelRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.StartPipeline());
            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
            Assert.Throws<InvalidOperationException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void InReview_InvalidOperations_Throw()
        {
            var sprint = SprintTestFactories.CreateReviewSprint();
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<InvalidOperationException>(() => sprint.CancelRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.HandlePostFinish());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.StartPipeline());
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        [Fact]
        public void Releasing_InvalidOperations_Throw()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint(new DeploymentPipeline());
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
            Assert.Throws<InvalidOperationException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void Releasing_ValidateEditViaEditStartDate_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint(new DeploymentPipeline());
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            Assert.Throws<InvalidOperationException>(() => sprint.EditStartDate(DateTime.UtcNow));
        }

        [Fact]
        public void Cancelled_InvalidOperations_ThrowExceptRetry()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("PO", Role.ProductOwner));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();
            sprint.CancelRelease();
            var item = SprintTestFactories.MinimalBacklogItem();

            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
            Assert.Throws<InvalidOperationException>(() => sprint.CancelRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.HandlePostFinish());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineFailure());
            Assert.Throws<InvalidOperationException>(() => sprint.OnPipelineSuccess());
            Assert.Throws<InvalidOperationException>(() => sprint.StartPipeline());
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.UploadReviewSummary("x"));
            Assert.Throws<InvalidOperationException>(() => sprint.MarkAsReviewed());
            Assert.Throws<InvalidOperationException>(() => sprint.EditName("x"));
        }

        [Fact]
        public void Releasing_StartPipeline_Success_WithoutNotificationService_StillReleases()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint(new DeploymentPipeline());
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.StartPipeline();

            Assert.Same(sprint.GetReleasedState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void Releasing_StartPipeline_Failure_WithoutNotificationService_DoesNotThrow()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint(new FailingAtInstallPipeline());
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.HandlePostFinish();

            sprint.StartPipeline();

            Assert.Same(sprint.GetReleasingState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void RunReleasePipeline_WhenPipelineSucceeds_DoesNotNotify()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.NotificationService = new RecordingNotificationService();
            sprint.Pipeline = new DeploymentPipeline();

            sprint.RunReleasePipeline();

            Assert.Empty(((RecordingNotificationService)sprint.NotificationService!).Sent);
        }

        [Fact]
        public void RunReleasePipeline_WhenPipelineFails_AndNotificationNull_DoesNotThrow()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            sprint.Pipeline = new FailingPipeline();

            sprint.RunReleasePipeline();
        }

        [Fact]
        public void RunReleasePipeline_WhenPipelineFails_AndNotificationSet_ButPipelineNull_DoesNotEnterNotifyBranch()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("SM", Role.ScrumMaster));
            var recorder = new RecordingNotificationService();
            sprint.NotificationService = recorder;
            sprint.Pipeline = null;

            sprint.RunReleasePipeline();

            Assert.Empty(recorder.Sent);
        }

        [Fact]
        public void RunReleasePipeline_WhenPipelineFails_GetScrumMasterFailure_Propagates()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.Pipeline = new FailingPipeline();
            sprint.NotificationService = new RecordingNotificationService();

            Assert.Throws<InvalidOperationException>(() => sprint.RunReleasePipeline());
        }

        [Fact]
        public void NotificationService_Getter_ReturnsAssignedInstance()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            Assert.Null(sprint.NotificationService);
            var recorder = new RecordingNotificationService();
            sprint.NotificationService = recorder;
            Assert.Same(recorder, sprint.NotificationService);
        }

        [Fact]
        public void Pipeline_Getter_ReturnsAssignedPipeline()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            var pipe = new TestPipeline();
            sprint.Pipeline = pipe;
            Assert.Same(pipe, sprint.Pipeline);
        }

        [Fact]
        public void GetProductOwner_WhenMissing_Throws()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.AddPerson(new Person("D", Role.Developer));
            Assert.Throws<InvalidOperationException>(() => sprint.GetProductOwner());
        }

        [Fact]
        public void GetProductOwner_WhenPresent_ReturnsOwner()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            var po = new Person("PO", Role.ProductOwner);
            sprint.AddPerson(po);
            Assert.Same(po, sprint.GetProductOwner());
        }

        [Fact]
        public void Created_EditStartDateAndEndDate_Allowed()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            sprint.EditStartDate(DateTime.UtcNow.AddDays(-1));
            sprint.EditEndDate(DateTime.UtcNow.AddDays(20));
        }

        [Fact]
        public void RemoveTeamMember_RemovesScrumMaster_GetScrumMasterThrows()
        {
            var sprint = SprintTestFactories.CreateReleaseSprint();
            var sm = new Person("SM", Role.ScrumMaster);
            sprint.AddTeamMember(sm);
            sprint.RemoveTeamMember(sm);
            Assert.Throws<InvalidOperationException>(() => sprint.GetScrumMaster());
        }

        [Fact]
        public void Project_AddSprint_LinksAndSkipsDuplicates()
        {
            var project = new Project();
            var sprint = SprintTestFactories.CreateReleaseSprint();
            project.AddSprint(sprint);
            project.AddSprint(sprint);
            Assert.Equal(1, PrivateStateAccessor.GetProjectSprintCount(project));
        }

        [Fact]
        public void BacklogItem_ReadyForTesting_WithNoTesters_DoesNotThrow()
        {
            var sm = new Person("SM", Role.ScrumMaster);
            var services = new List<INotificationService> { new RecordingNotificationService() };
            var item = new BacklogItem("X", "D", new List<Person>(), sm, services);
            item.StartWork();
            item.ReadyForTesting();
        }

        [Fact]
        public void Message_Timestamp_IsPopulated()
        {
            var m = new Message(new Person("P", Role.Developer), "hi");
            Assert.NotEqual(default(DateTime), m.Timestamp);
        }

        [Fact]
        public void Thread_AddMessage_OnThreadPoolThread_StillNotifiesObservers()
        {
            var thread = new DomainThread("async");
            var poster = new Person("A", Role.Developer);
            var listener = new Person("B", Role.Tester);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            thread.AddObserver(new ThreadObserver(listener, services));

#pragma warning disable xUnit1031 // Synchronous wait required to keep Console.Out redirection scoped to ConsoleCapture.Run
            string output = ConsoleCapture.Run(() =>
                Task.Run(() => thread.AddMessage(new Message(poster, "pool msg"))).GetAwaiter().GetResult());
#pragma warning restore xUnit1031

            Assert.Contains("B", output);
            Assert.Contains("pool msg", output);
        }

        private sealed class FailingAtInstallPipeline : DevelopmentPipeline
        {
            protected override void FetchSources()
            {
            }

            protected override void InstallPackages()
            {
                throw new InvalidOperationException("install fail");
            }

            protected override void Build()
            {
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
