using System;

namespace SOFA_bioscoop.Domain
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

        public void generateContent()
        {
            content = "Sprint Report\n";
            content += "Company: " + companyName + "\n";
            content += "Project: " + projectName + "\n";
            content += "Generated at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
