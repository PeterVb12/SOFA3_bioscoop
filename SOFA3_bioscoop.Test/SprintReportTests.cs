using SOFA_bioscoop.Domain.Reporting;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    public class SprintReportTests
    {
        [Fact]
        public void SprintReport_Constructor_ShouldSetFieldsAndEmptyContent()
        {
            // Arrange and act
            var report = new SprintReport("MyCompany", "MyProject");

            // Assert
            Assert.Equal("MyCompany", report.companyName);
            Assert.Equal("MyProject", report.projectName);
            Assert.Equal("", report.content);
        }
    }
}
