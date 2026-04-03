namespace SOFA_bioscoop.Domain.Reporting
{
    public class ReportFooterData
    {
        public string generatedBy;
        public string notes;

        public ReportFooterData(string generatedBy, string notes)
        {
            this.generatedBy = generatedBy;
            this.notes = notes;
        }
    }
}
