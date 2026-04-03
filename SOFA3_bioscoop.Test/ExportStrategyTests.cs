using System;
using SOFA_bioscoop.Infrastructure.Reporting;
using SOFA3_bioscoop.Test.Fakes;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class ExportStrategyTests
    {
        [Fact]
        public void PdfExportStrategy_Implements_ExportStrategy()
        {
            Assert.True(typeof(ExportStrategy).IsAssignableFrom(typeof(PdfExportStrategy)));
        }

        [Fact]
        public void PngExportStrategy_Implements_ExportStrategy()
        {
            Assert.True(typeof(ExportStrategy).IsAssignableFrom(typeof(PngExportStrategy)));
        }

        [Fact]
        public void RecordingExportStrategy_Implements_ExportStrategy()
        {
            Assert.True(typeof(ExportStrategy).IsAssignableFrom(typeof(RecordingExportStrategy)));
        }

        [Fact]
        public void PdfExportStrategy_Output_ContainsPdfMarker()
        {
            string captured = ConsoleCapture.Run(() =>
            {
                var pdf = new PdfExportStrategy();
                pdf.exportReport("hello");
            });

            Assert.Contains("PDF", captured);
            Assert.Contains("hello", captured);
        }

        [Fact]
        public void PngExportStrategy_Output_ContainsPngMarker()
        {
            string captured = ConsoleCapture.Run(() =>
            {
                var png = new PngExportStrategy();
                png.exportReport("hello");
            });

            Assert.Contains("PNG", captured);
            Assert.Contains("hello", captured);
        }

        [Fact]
        public void Strategy_PdfAndPng_ProduceDifferentConsoleOutput()
        {
            string pdfOut = ConsoleCapture.Run(() => new PdfExportStrategy().exportReport("x"));
            string pngOut = ConsoleCapture.Run(() => new PngExportStrategy().exportReport("x"));

            Assert.NotEqual(pdfOut, pngOut);
        }
    }
}
