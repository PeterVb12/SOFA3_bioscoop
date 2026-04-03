using System.Reflection;
using SOFA_bioscoop.Application.Reporting;
using SOFA_bioscoop.Domain.Reporting;
using SOFA3_bioscoop.Test.Fakes;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class DecoratorTests
    {
        [Fact]
        public void HeaderDecorator_IsAssignableFrom_ISprintReport()
        {
            var dec = new HeaderDecorator(new FakeSprintReport("b"), new ReportHeaderData("a", "b", "c", "d", "e"));
            Assert.IsAssignableFrom<ISprintReport>(dec);
        }

        [Fact]
        public void FooterDecorator_IsAssignableFrom_ISprintReport()
        {
            var dec = new FooterDecorator(new FakeSprintReport("b"), new ReportFooterData("g", "n"));
            Assert.IsAssignableFrom<ISprintReport>(dec);
        }

        [Fact]
        public void HeaderDecorator_IsAssignableFrom_SprintReportDecorator()
        {
            var dec = new HeaderDecorator(new FakeSprintReport("b"), new ReportHeaderData("a", "b", "c", "d", "e"));
            Assert.IsAssignableFrom<SprintReportDecorator>(dec);
        }

        [Fact]
        public void HeaderDecorator_ShouldHoldWrapped_ISprintReport()
        {
            var inner = new FakeSprintReport("BODY");
            var dec = new HeaderDecorator(inner, new ReportHeaderData("c", "p", "v", "d", "l"));
            var field = typeof(SprintReportDecorator).GetField(
                "wrappedReport",
                BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.NotNull(field);
            Assert.Same(inner, field.GetValue(dec));
        }

        [Fact]
        public void HeaderDecorator_ShouldPrependHeader_BeforeBaseReport()
        {
            var inner = new FakeSprintReport("BODY");
            var dec = new HeaderDecorator(inner, new ReportHeaderData("Acme", "P", "1", "d", "lg"));
            string result = dec.GenerateReport();

            Assert.StartsWith("========== HEADER ==========", result);
            Assert.Contains("BODY", result);
            Assert.True(result.IndexOf("BODY") > result.IndexOf("HEADER"));
        }

        [Fact]
        public void FooterDecorator_ShouldAppendFooter_AfterBaseReport()
        {
            var inner = new FakeSprintReport("BODY");
            var dec = new FooterDecorator(inner, new ReportFooterData("gen", "notes"));
            string result = dec.GenerateReport();

            Assert.Contains("BODY", result);
            Assert.Contains("========== FOOTER ==========", result);
            Assert.True(result.IndexOf("FOOTER") > result.IndexOf("BODY"));
        }

        [Fact]
        public void Decorators_ShouldChain_HeaderThenFooter()
        {
            ISprintReport core = new FakeSprintReport("MIDDLE");
            core = new FooterDecorator(core, new ReportFooterData("u", "n"));
            core = new HeaderDecorator(core, new ReportHeaderData("c", "p", "v", "d", "l"));
            string result = core.GenerateReport();

            int h = result.IndexOf("========== HEADER");
            int m = result.IndexOf("MIDDLE");
            int f = result.IndexOf("========== FOOTER");
            Assert.True(h >= 0 && m > h && f > m);
        }

        [Fact]
        public void SprintReportDecorator_IsAbstract()
        {
            Assert.True(typeof(SprintReportDecorator).IsAbstract);
        }

        [Fact]
        public void BasicSprintReportWrapper_GenerateReport_ContainsBodySections()
        {
            var dto = new SprintReport("CoName", "PrName");
            var wrapper = new BasicSprintReportWrapper(dto, sprint: null);
            string text = wrapper.GenerateReport();

            Assert.Contains("--- Sprint report body ---", text);
            Assert.Contains("No sprint", text);
            Assert.Contains("Portfolio: CoName / PrName", text);
            Assert.Contains("Team composition:", text);
        }
    }
}
