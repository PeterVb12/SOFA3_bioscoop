using System.Collections.Generic;
using System;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;
using DomainThread = SOFA_bioscoop.Domain.Thread;

namespace SOFA3_bioscoop.Test
{
    public class ThreadTests
    {
        [Fact]
        public void Thread_ShouldStoreTitle_WhenCreated()
        {
            // Arrange & Act
            var thread = new DomainThread("Vraag over API");

            // Assert
            Assert.Equal("Vraag over API", thread.Title);
        }

        [Fact]
        public void Thread_ShouldContainMessage_WhenMessagePosted()
        {
            // Arrange
            var thread = new DomainThread("Discussie");
            var sender = new Person("Dev", Role.Developer);
            var message = new Message(sender, "Hier is mijn update");

            // Act
            thread.AddMessage(message);

            // Assert
            Assert.Single(thread.GetMessages());
            Assert.Equal("Hier is mijn update", thread.GetMessages()[0].Content);
            Assert.Same(sender, thread.GetMessages()[0].Sender);
        }

        [Fact]
        public void Thread_ShouldNotifyObserver_WhenAnotherUserPostsMessage()
        {
            // Arrange
            var thread = new DomainThread("Team chat");
            var poster = new Person("Kim", Role.Developer);
            var listener = new Person("Lee", Role.Tester);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            var observer = new ThreadObserver(listener, services);
            thread.AddObserver(observer);

            // Act
            string output = ConsoleCapture.Run(() =>
                thread.AddMessage(new Message(poster, "Kunnen jullie reviewen?")));

            // Assert
            Assert.Contains("Lee", output);
            Assert.Contains("Kunnen jullie reviewen?", output);
        }

        [Fact]
        public void Thread_ShouldNotNotifyObserver_WhenSenderIsSamePersonAsObserver()
        {
            // Arrange
            var thread = new DomainThread("Solo log");
            var person = new Person("Morgan", Role.Developer);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            thread.AddObserver(new ThreadObserver(person, services));

            // Act
            string output = ConsoleCapture.Run(() =>
                thread.AddMessage(new Message(person, "Eigen notitie")));

            // Assert
            Assert.DoesNotContain("Notification sent to Morgan", output);
        }

        [Fact]
        public void Thread_ShouldStopNotifying_WhenObserverRemoved()
        {
            // Arrange
            var thread = new DomainThread("Review");
            var poster = new Person("A", Role.Developer);
            var listener = new Person("Zoe", Role.Tester);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            var observer = new ThreadObserver(listener, services);
            thread.AddObserver(observer);
            thread.RemoveObserver(observer);

            // Act
            string output = ConsoleCapture.Run(() =>
                thread.AddMessage(new Message(poster, "Ping")));

            // Assert
            Assert.DoesNotContain("Notification sent to Zoe", output);
        }

        [Fact]
        public void BacklogItem_ShouldAttachThreadToItem_WhenAddThreadCalledInDoingState()
        {
            // Arrange
            var item = CreateBacklogItem();
            item.StartWork();
            var thread = new DomainThread("Technisch detail");

            // Act
            item.AddThread(thread);

            // Assert
            Assert.Single(item.GetThreads());
        }

        private static BacklogItem CreateBacklogItem()
        {
            var testers = new List<Person> { new Person("T1", Role.Tester) };
            var sm = new Person("SM", Role.ScrumMaster);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            return new BacklogItem("Story", "Desc", testers, sm, services);
        }
    }
}
