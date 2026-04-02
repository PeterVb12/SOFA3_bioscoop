using System;

namespace SOFA_bioscoop.Domain
{
    public class PngExportStrategy : ExportStrategy
    {
        public void exportReport(SprintReport report)
        {
            Console.WriteLine("=== Exporting Sprint Report as PNG ===");
            Console.WriteLine(report.content);
            Console.WriteLine("=== End PNG Export ===");
        }
    }
}
