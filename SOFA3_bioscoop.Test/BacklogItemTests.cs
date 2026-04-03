using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.BacklogItems;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;
using DomainThread = SOFA_bioscoop.Domain.Thread;

namespace SOFA3_bioscoop.Test
{
    public class BacklogItemTests
    {
        [Fact]
        public void BacklogItem_ShouldStartInTodoState_WhenCreated()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();

            // Act
            IBacklogitemState? current = PrivateStateAccessor.GetBacklogState(item);

            // Assert
            Assert.Same(item.GetTodoState(), current);
        }

        [Fact]
        public void BacklogItem_ShouldMoveFromTodoToDoing_WhenDeveloperStartsWork()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();

            // Act
            item.StartWork();

            // Assert
            Assert.Same(item.GetDoingState(), PrivateStateAccessor.GetBacklogState(item));
        }

        [Fact]
        public void BacklogItem_ShouldThrow_WhenReadyForTestingCalledFromTodo()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => item.ReadyForTesting());
        }

        [Fact]
        public void BacklogItem_ShouldMoveToReadyForTestingAndNotifyTesters_WhenMarkedReadyFromDoing()
        {
            // Arrange
            Person tester = new Person("Alex", Role.Tester);
            BacklogItem item = CreateBacklogItem(new List<Person> { tester });
            item.StartWork();

            // Act
            string output = ConsoleCapture.Run(() => item.ReadyForTesting());

            // Assert
            Assert.Same(item.GetReadyForTestingState(), PrivateStateAccessor.GetBacklogState(item));
            Assert.Contains("Alex", output);
            Assert.Contains("klaar voor testing", output, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void BacklogItem_ShouldThrow_WhenStartWorkFromReadyForTesting()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();
            item.StartWork();
            item.ReadyForTesting();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void BacklogItem_ShouldMoveThroughTestingToTested_WhenTesterStartsAndFinishes()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();

            // Act
            item.FinishTesting();

            // Assert
            Assert.Same(item.GetTestedState(), PrivateStateAccessor.GetBacklogState(item));
        }

        [Fact]
        public void BacklogItem_ShouldMoveToDone_WhenApprovedFromTested()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();
            item.FinishTesting();

            // Act
            item.Approve();

            // Assert
            Assert.Same(item.GetDoneState(), PrivateStateAccessor.GetBacklogState(item));
        }

        [Fact]
        public void BacklogItem_ShouldMoveToReadyForTesting_WhenDeniedFromTested()
        {
            // Arrange
            Person tester = new Person("Alex", Role.Tester);
            BacklogItem item = CreateBacklogItem(new List<Person> { tester });
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();
            item.FinishTesting();

            // Act
            string output = ConsoleCapture.Run(() => item.Deny());

            // Assert
            Assert.Same(item.GetReadyForTestingState(), PrivateStateAccessor.GetBacklogState(item));
            Assert.Contains("Alex", output);
        }

        [Fact]
        public void BacklogItem_ShouldMoveToTodoAndNotifyScrumMaster_WhenRejectedFromTesting()
        {
            // Arrange
            Person scrumMaster = new Person("Sam", Role.ScrumMaster);
            BacklogItem item = CreateBacklogItem(scrumMaster: scrumMaster);
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();

            // Act
            string output = ConsoleCapture.Run(() => item.Reject());

            // Assert
            Assert.Same(item.GetTodoState(), PrivateStateAccessor.GetBacklogState(item));
            Assert.Contains("Sam", output);
            Assert.Contains("afgekeurd", output, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void BacklogItem_ShouldNotReturnToDoing_WhenRejectedFromTesting()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();

            // Act
            item.Reject();

            // Assert
            Assert.NotSame(item.GetDoingState(), PrivateStateAccessor.GetBacklogState(item));
            Assert.Same(item.GetTodoState(), PrivateStateAccessor.GetBacklogState(item));
        }

        [Fact]
        public void BacklogItem_ShouldAddThread_WhenInTodoState()
        {
            // Arrange
            BacklogItem item = CreateBacklogItem();
            var thread = new DomainThread("Discussie login");

            // Act
            item.AddThread(thread);

            // Assert
            Assert.Single(item.GetThreads());
            Assert.Same(thread, item.GetThreads()[0]);
        }

        private static BacklogItem CreateBacklogItem(
            List<Person>? testers = null,
            Person? scrumMaster = null)
        {
            testers ??= new List<Person> { new Person("T1", Role.Tester) };
            scrumMaster ??= new Person("SM", Role.ScrumMaster);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            return new BacklogItem("User story", "Beschrijving", testers, scrumMaster, services);
        }
    }
}
