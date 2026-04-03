using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.Pipelines;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class PipelineTests
    {
        [Fact]
        public void DevelopmentPipeline_HasPublic_ReleasePipeline_Method()
        {
            var method = typeof(DevelopmentPipeline).GetMethod(
                "ReleasePipeline",
                Type.EmptyTypes);

            Assert.NotNull(method);
            Assert.False(method.IsAbstract);
            Assert.Equal(typeof(void), method.ReturnType);
        }

        [Fact]
        public void DeploymentPipeline_ReleasePipeline_RunsSteps_InOrder()
        {
            string output = ConsoleCapture.Run(() => new DeploymentPipeline().ReleasePipeline());

            int fetch = output.IndexOf("Fetching source code", StringComparison.Ordinal);
            int install = output.IndexOf("Installing packages", StringComparison.Ordinal);
            int build = output.IndexOf("Building the project", StringComparison.Ordinal);
            int test = output.IndexOf("Running tests", StringComparison.Ordinal);
            int analyse = output.IndexOf("SonarQube", StringComparison.Ordinal);
            int deploy = output.IndexOf("Deploying to production", StringComparison.Ordinal);

            Assert.True(fetch >= 0 && install >= 0 && build >= 0 && test >= 0 && analyse >= 0 && deploy >= 0);
            Assert.True(fetch < install && install < build && build < test && test < analyse && analyse < deploy);
        }

        [Fact]
        public void DeploymentPipeline_ShouldExecute_DeployStep()
        {
            string output = ConsoleCapture.Run(() => new DeploymentPipeline().ReleasePipeline());
            Assert.Contains("Deploying to production", output);
        }

        [Fact]
        public void Pipeline_TestPipeline_ShouldSkipDeploy_ConsoleLine()
        {
            string output = ConsoleCapture.Run(() => new TestPipeline().ReleasePipeline());

            Assert.Contains("Fetching source code", output);
            Assert.Contains("Running SonarQube analysis", output);
            Assert.DoesNotContain("Deploying to production", output);
        }

        [Fact]
        public void Pipeline_ShouldNotifyScrumMaster_WhenFailureOccurs()
        {
            const string scrumMasterName = "Pat";
            var sprint = new Sprint(
                "Sprint1",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
            sprint.AddPerson(new Person(scrumMasterName, Role.ScrumMaster));
            sprint.AddPerson(new Person("Dev Dan", Role.Developer));
            sprint.NotificationService = new ConsoleNotificationService();
            sprint.Pipeline = new FailingPipeline();

            string output = ConsoleCapture.Run(() => sprint.RunReleasePipeline());

            Assert.Contains("Pipeline failed", output);
            Assert.Contains(scrumMasterName, output);
        }

        [Fact]
        public void GetScrumMaster_ShouldReturnCorrectPerson()
        {
            var sprint = new Sprint(
                "S",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(7),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
            sprint.AddPerson(new Person("Bob", Role.Developer));
            sprint.AddPerson(new Person("Sam SM", Role.ScrumMaster));
            sprint.AddPerson(new Person("Tina", Role.Tester));

            Person sm = sprint.GetScrumMaster();

            Assert.Equal(Role.ScrumMaster, sm.Role);
            Assert.Equal("Sam SM", sm.Name);
        }

        [Fact]
        public void GetScrumMaster_WhenNoScrumMaster_ShouldThrow()
        {
            var sprint = new Sprint(
                "S",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(7),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
            sprint.AddPerson(new Person("OnlyDev", Role.Developer));

            Assert.Throws<InvalidOperationException>(() => sprint.GetScrumMaster());
        }

        [Fact]
        public void Notification_ShouldContainCorrectMessage()
        {
            const string scrumMasterName = "Jordan";
            var sprint = new Sprint(
                "Sprint2",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
            sprint.AddPerson(new Person(scrumMasterName, Role.ScrumMaster));
            sprint.NotificationService = new ConsoleNotificationService();
            sprint.Pipeline = new FailingPipeline();

            string output = ConsoleCapture.Run(() => sprint.RunReleasePipeline());

            Assert.Contains("Notification", output);
            Assert.Contains(scrumMasterName, output);
            Assert.Contains("Pipeline failed", output);
        }
    }

    internal class FailingPipeline : DevelopmentPipeline
    {
        protected override void FetchSources()
        {
            throw new Exception("simulated pipeline failure");
        }

        protected override void InstallPackages()
        {
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
