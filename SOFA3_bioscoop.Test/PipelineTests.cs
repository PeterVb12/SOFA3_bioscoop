using System;
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

    }
}
