using System;

namespace SOFA_bioscoop.Domain.Pipelines
{
    public abstract class DevelopmentPipeline
    {
        public void ReleasePipeline()
        {
            FetchSources();
            InstallPackages();
            Build();
            Test();
            Analyse();
            Deploy();
        }

        protected abstract void FetchSources();
        protected abstract void InstallPackages();
        protected abstract void Build();
        protected abstract void Test();
        protected abstract void Analyse();
        protected abstract void Deploy();
    }
}
