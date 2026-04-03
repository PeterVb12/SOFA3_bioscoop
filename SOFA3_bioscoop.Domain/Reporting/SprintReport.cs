namespace SOFA_bioscoop.Domain.Reporting
{
    public class SprintReport
    {
        public string companyName;
        public string projectName;
        public string content;

        public SprintReport(string companyName, string projectName)
        {
            this.companyName = companyName;
            this.projectName = projectName;
            this.content = "";
        }
    }
}
