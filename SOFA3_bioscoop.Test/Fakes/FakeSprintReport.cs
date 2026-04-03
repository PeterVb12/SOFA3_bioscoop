using SOFA_bioscoop.Domain.Reporting;

namespace SOFA3_bioscoop.Test.Fakes
{
    public class FakeSprintReport : ISprintReport
    {
        private string body;

        public FakeSprintReport(string body)
        {
            this.body = body;
        }

        public string GenerateReport()
        {
            return body;
        }
    }
}
