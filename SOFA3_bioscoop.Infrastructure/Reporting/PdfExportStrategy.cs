using System;

namespace SOFA_bioscoop.Infrastructure.Reporting
{
    public class PdfExportStrategy : ExportStrategy
    {
        public void exportReport(string content)
        {
            Console.WriteLine("=== Exporting Sprint Report as PDF ===");
            Console.WriteLine(content);
            Console.WriteLine("=== End PDF Export ===");
        }
    }
}
