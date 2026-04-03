using System;

namespace SOFA_bioscoop.Domain
{
    public class PngExportStrategy : ExportStrategy
    {
        public void exportReport(string content)
        {
            Console.WriteLine("=== Exporting Sprint Report as PNG ===");
            Console.WriteLine(content);
            Console.WriteLine("=== End PNG Export ===");
        }
    }
}
