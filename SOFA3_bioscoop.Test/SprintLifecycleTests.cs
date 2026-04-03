using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;

namespace SOFA3_bioscoop.Test
{
    // TC-05 (FR-03, AC-03)
    public class SprintLifecycleTests
    {
        [Fact]
        public void Sprint_ShouldBecomeInProgress_WhenStartedFromCreated()
        {
            // arrange
            Sprint sprint = CreateReleaseSprint();

            // assert
            Assert.Same(sprint.GetCreatedState(), PrivateStateAccessor.GetSprintState(sprint));

            // act
            sprint.StartSprint();

            // assert 
            Assert.Same(sprint.GetInProgressState(), PrivateStateAccessor.GetSprintState(sprint));
        }

        [Fact]
        public void Sprint_ShouldThrowException_WhenNameModifiedAfterStart()
        {
            // arrange
            Sprint sprint = CreateReleaseSprint();
            sprint.StartSprint();

            // act
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => sprint.EditName("RenamedSprint"));

            // assert
            Assert.Contains("in progress", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Sprint_ShouldThrowException_WhenStartDateModifiedAfterStart()
        {
            // arrange
            Sprint sprint = CreateReleaseSprint();
            sprint.StartSprint();
            DateTime newStart = DateTime.UtcNow.AddDays(1);

            // act
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => sprint.EditStartDate(newStart));

            // assert
            Assert.Contains("in progress", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Sprint_ShouldThrowException_WhenEndDateModifiedAfterStart()
        {
            // arrange
            Sprint sprint = CreateReleaseSprint();
            sprint.StartSprint();
            DateTime newEnd = DateTime.UtcNow.AddDays(30);

            // act
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => sprint.EditEndDate(newEnd));

            // assert
            Assert.Contains("in progress", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        private static Sprint CreateReleaseSprint()
        {
            return new Sprint(
                "TC05-Sprint",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project());
        }
    }
}
