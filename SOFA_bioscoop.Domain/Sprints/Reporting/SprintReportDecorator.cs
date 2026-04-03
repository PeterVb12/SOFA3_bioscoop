namespace SOFA_bioscoop.Domain
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
