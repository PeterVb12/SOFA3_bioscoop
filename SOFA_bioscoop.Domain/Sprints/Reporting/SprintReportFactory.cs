namespace SOFA_bioscoop.Domain
{
    public class SprintReportFactory
    {
        private ExportStrategy exportStrategy;

        public SprintReportFactory(ExportStrategy exportStrategy)
        {
            this.exportStrategy = exportStrategy;
        }

        public SprintReport generateReport(Sprint sprint)
        {
            string companyName = "SOFA Company";
            string projectName = "Unknown Project";

            if (sprint != null)
            {
                projectName = "Sprint Project";
            }

            SprintReport report = new SprintReport(companyName, projectName);
            report.generateContent();
            return report;
        }

        public void exportReport(SprintReport report)
        {
            exportStrategy.exportReport(report);
        }
    }
}
