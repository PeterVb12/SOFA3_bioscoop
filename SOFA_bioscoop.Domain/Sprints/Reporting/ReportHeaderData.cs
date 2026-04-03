namespace SOFA_bioscoop.Domain
{
    public class ReportHeaderData
    {
        public string companyName;
        public string projectName;
        public string version;
        public string date;
        public string logo;

        public ReportHeaderData(string companyName, string projectName, string version, string date, string logo)
        {
            this.companyName = companyName;
            this.projectName = projectName;
            this.version = version;
            this.date = date;
            this.logo = logo;
        }
    }
}
