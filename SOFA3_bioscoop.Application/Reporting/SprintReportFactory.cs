using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.Reporting;
using SOFA_bioscoop.Infrastructure.Reporting;

namespace SOFA_bioscoop.Application.Reporting
{
    public class SprintReportFactory
    {
        private ExportStrategy exportStrategy;

        public SprintReportFactory(ExportStrategy exportStrategy)
        {
            this.exportStrategy = exportStrategy;
        }

        private SprintReport createSprintReportDto(Sprint sprint)
        {
            string companyName = "SOFA Company";
            string projectName = "Unknown Project";

            if (sprint != null)
            {
                projectName = "Sprint Project";
            }

            return new SprintReport(companyName, projectName);
        }

        public SprintReport generateReport(Sprint sprint)
        {
            SprintReport report = createSprintReportDto(sprint);
            report.content = new BasicSprintReportWrapper(report, sprint).GenerateReport();
            return report;
        }

        public SprintReport generateDecoratedReport(Sprint sprint, ReportHeaderData header, ReportFooterData footer)
        {
            SprintReport report = createSprintReportDto(sprint);
            report.companyName = header.companyName;
            report.projectName = header.projectName;

            ISprintReport decorated = new BasicSprintReportWrapper(report, sprint);
            decorated = new HeaderDecorator(decorated, header);
            decorated = new FooterDecorator(decorated, footer);
            report.content = decorated.GenerateReport();
            return report;
        }

        public void exportReport(SprintReport report)
        {
            string text = report.content != null ? report.content : "";
            exportStrategy.exportReport(text);
        }
    }
}
