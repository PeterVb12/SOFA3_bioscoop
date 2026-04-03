using SOFA_bioscoop.Infrastructure.Reporting;

namespace SOFA3_bioscoop.Test.Fakes
{
    public class RecordingExportStrategy : ExportStrategy
    {
        public string LastContent { get; private set; } = "";

        public void exportReport(string content)
        {
            LastContent = content;
        }
    }
}
