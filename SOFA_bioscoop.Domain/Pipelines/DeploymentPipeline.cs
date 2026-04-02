using System;

namespace SOFA_bioscoop.Domain
{
    public class DeploymentPipeline : DevelopmentPipeline
    {
        protected override void FetchSources()
        {
            Console.WriteLine("Fetching source code...");
        }

        protected override void InstallPackages()
        {
            Console.WriteLine("Installing packages...");
        }

        protected override void Build()
        {
            Console.WriteLine("Building the project...");
        }

        protected override void Test()
        {
            Console.WriteLine("Running tests...");
        }

        protected override void Analyse()
        {
            Console.WriteLine("Running SonarQube analysis...");
        }

        protected override void Deploy()
        {
            Console.WriteLine("Deploying to production...");
        }
    }
}
