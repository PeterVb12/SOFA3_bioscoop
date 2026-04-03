using System.Text;

namespace SOFA_bioscoop.Domain
{
    public class HeaderDecorator : SprintReportDecorator
    {
        private ReportHeaderData headerData;

        public HeaderDecorator(ISprintReport wrappedReport, ReportHeaderData headerData)
            : base(wrappedReport)
        {
            this.headerData = headerData;
        }

        public override string GenerateReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("========== HEADER ==========");
            sb.AppendLine("Company: " + headerData.companyName);
            sb.AppendLine("Project: " + headerData.projectName);
            sb.AppendLine("Version: " + headerData.version);
            sb.AppendLine("Date: " + headerData.date);
            sb.AppendLine("Logo: " + headerData.logo);
            sb.AppendLine("============================");
            sb.AppendLine();
            sb.Append(wrappedReport.GenerateReport());
            return sb.ToString();
        }
    }
}
