using SOFA_bioscoop.Domain.Reporting;

namespace SOFA_bioscoop.Application.Reporting
{
    public abstract class SprintReportDecorator : ISprintReport
    {
        protected ISprintReport wrappedReport;

        public SprintReportDecorator(ISprintReport wrappedReport)
        {
            this.wrappedReport = wrappedReport;
        }

        public abstract string GenerateReport();
    }
}
