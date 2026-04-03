using SOFA_bioscoop.Application.Reporting;
using SOFA_bioscoop.Domain.Reporting;
using SOFA3_bioscoop.Test.Fakes;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class ISprintReportContractTests
    {
        [Fact]
        public void BasicSprintReportWrapper_ShouldImplement_ISprintReport()
        {
            var dto = new SprintReport("A", "B");
            var wrapper = new BasicSprintReportWrapper(dto, sprint: null);

            Assert.IsAssignableFrom<ISprintReport>(wrapper);
        }

        [Fact]
        public void HeaderDecorator_ShouldImplement_ISprintReport()
        {
            var inner = new FakeSprintReport("x");
            var header = new ReportHeaderData("c", "p", "1", "d", "l");
            var dec = new HeaderDecorator(inner, header);

            Assert.IsAssignableFrom<ISprintReport>(dec);
        }

        [Fact]
        public void FooterDecorator_ShouldImplement_ISprintReport()
        {
            var inner = new FakeSprintReport("x");
            var footer = new ReportFooterData("u", "n");
            var dec = new FooterDecorator(inner, footer);

            Assert.IsAssignableFrom<ISprintReport>(dec);
        }
    }
}
