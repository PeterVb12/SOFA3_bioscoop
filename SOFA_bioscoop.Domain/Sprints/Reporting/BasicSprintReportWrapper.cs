using System.Text;

namespace SOFA_bioscoop.Domain
{
    public class BasicSprintReportWrapper : ISprintReport
    {
        private SprintReport report;
        private Sprint sprint;

        public BasicSprintReportWrapper(SprintReport report, Sprint sprint)
        {
            this.report = report;
            this.sprint = sprint;
        }

        public string GenerateReport()
        {
            StringBuilder sb = new StringBuilder();
            string sprintLabel = sprint != null ? "Current sprint" : "No sprint";

            sb.AppendLine("--- Sprint report body ---");
            sb.AppendLine("Sprint: " + sprintLabel);
            sb.AppendLine("Portfolio: " + report.companyName + " / " + report.projectName);
            sb.AppendLine();
            sb.AppendLine("Team composition:");
            sb.AppendLine("example");
            sb.AppendLine();
            sb.AppendLine("Burndown chart:");
            sb.AppendLine("example");
            sb.AppendLine();
            sb.AppendLine("Effort points per developer");
            sb.AppendLine("example");

            return sb.ToString();
        }
    }
}
