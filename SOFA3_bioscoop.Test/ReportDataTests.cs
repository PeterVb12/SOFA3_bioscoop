using SOFA_bioscoop.Domain.Reporting;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class ReportDataTests
    {
        [Fact]
        public void ReportHeaderData_Constructor_ShouldAssignAllFields()
        {
            var data = new ReportHeaderData("Co", "Pr", "v2", "2026-04-01", "logo.gif");

            Assert.Equal("Co", data.companyName);
            Assert.Equal("Pr", data.projectName);
            Assert.Equal("v2", data.version);
            Assert.Equal("2026-04-01", data.date);
            Assert.Equal("logo.gif", data.logo);
        }

        [Fact]
        public void ReportFooterData_Constructor_ShouldAssignAllFields()
        {
            var data = new ReportFooterData("CI bot", "Approved");

            Assert.Equal("CI bot", data.generatedBy);
            Assert.Equal("Approved", data.notes);
        }
    }
}
