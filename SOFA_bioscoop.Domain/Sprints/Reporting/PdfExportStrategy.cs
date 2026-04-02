using System;

namespace SOFA_bioscoop.Domain
{
    public class PdfExportStrategy : ExportStrategy
    {
        public void exportReport(SprintReport report)
        {
            Console.WriteLine("=== Exporting Sprint Report as PDF ===");
            Console.WriteLine(report.content);
            Console.WriteLine("=== End PDF Export ===");
        }
    }
}
