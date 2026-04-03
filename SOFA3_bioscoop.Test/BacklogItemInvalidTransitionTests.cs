using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA3_bioscoop.Test.Fakes;
using SOFA3_bioscoop.Test.TestSupport;
using Xunit;
using DomainThread = SOFA_bioscoop.Domain.Thread;

namespace SOFA3_bioscoop.Test
{
    public class BacklogItemInvalidTransitionTests
    {
        [Fact]
        public void Todo_StartWorkTwice_SecondCall_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void Todo_StartTesting_Throws()
        {
            var item = CreateItem();
            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }

        [Fact]
        public void Todo_FinishTesting_Throws()
        {
            var item = CreateItem();
            Assert.Throws<InvalidOperationException>(() => item.FinishTesting());
        }

        [Fact]
        public void Todo_Approve_Throws()
        {
            var item = CreateItem();
            Assert.Throws<InvalidOperationException>(() => item.Approve());
        }

        [Fact]
        public void Todo_Deny_Throws()
        {
            var item = CreateItem();
            Assert.Throws<InvalidOperationException>(() => item.Deny());
        }

        [Fact]
        public void Todo_Reject_Throws()
        {
            var item = CreateItem();
            Assert.Throws<InvalidOperationException>(() => item.Reject());
        }

        [Fact]
        public void Doing_StartWork_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void Doing_StartTesting_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }

        [Fact]
        public void Doing_FinishTesting_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.FinishTesting());
        }

        [Fact]
        public void Doing_Approve_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.Approve());
        }

        [Fact]
        public void Doing_Deny_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.Deny());
        }

        [Fact]
        public void Doing_Reject_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            Assert.Throws<InvalidOperationException>(() => item.Reject());
        }

        [Fact]
        public void ReadyForTesting_ReadyForTestingAgain_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            Assert.Throws<InvalidOperationException>(() => item.ReadyForTesting());
        }

        [Fact]
        public void ReadyForTesting_FinishTesting_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            Assert.Throws<InvalidOperationException>(() => item.FinishTesting());
        }

        [Fact]
        public void ReadyForTesting_Approve_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            Assert.Throws<InvalidOperationException>(() => item.Approve());
        }

        [Fact]
        public void ReadyForTesting_Deny_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            Assert.Throws<InvalidOperationException>(() => item.Deny());
        }

        [Fact]
        public void ReadyForTesting_Reject_Throws()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            Assert.Throws<InvalidOperationException>(() => item.Reject());
        }

        [Fact]
        public void Testing_StartWork_Throws()
        {
            var item = CreateItemThroughTesting();
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void Testing_ReadyForTesting_Throws()
        {
            var item = CreateItemThroughTesting();
            Assert.Throws<InvalidOperationException>(() => item.ReadyForTesting());
        }

        [Fact]
        public void Testing_StartTestingAgain_Throws()
        {
            var item = CreateItemThroughTesting();
            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }

        [Fact]
        public void Testing_Approve_Throws()
        {
            var item = CreateItemThroughTesting();
            Assert.Throws<InvalidOperationException>(() => item.Approve());
        }

        [Fact]
        public void Testing_Deny_Throws()
        {
            var item = CreateItemThroughTesting();
            Assert.Throws<InvalidOperationException>(() => item.Deny());
        }

        [Fact]
        public void Tested_StartWork_Throws()
        {
            var item = CreateItemThroughTested();
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void Tested_ReadyForTesting_Throws()
        {
            var item = CreateItemThroughTested();
            Assert.Throws<InvalidOperationException>(() => item.ReadyForTesting());
        }

        [Fact]
        public void Tested_StartTesting_Throws()
        {
            var item = CreateItemThroughTested();
            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }

        [Fact]
        public void Tested_FinishTesting_Throws()
        {
            var item = CreateItemThroughTested();
            Assert.Throws<InvalidOperationException>(() => item.FinishTesting());
        }

        [Fact]
        public void Tested_Reject_ThrowsWithSpecificMessage()
        {
            var item = CreateItemThroughTested();
            var ex = Assert.Throws<InvalidOperationException>(() => item.Reject());
            Assert.Contains("Deny", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Done_AllMutatingOperations_Throw()
        {
            var item = CreateItemThroughDone();
            Assert.Throws<InvalidOperationException>(() => item.StartWork());
            Assert.Throws<InvalidOperationException>(() => item.ReadyForTesting());
            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
            Assert.Throws<InvalidOperationException>(() => item.FinishTesting());
            Assert.Throws<InvalidOperationException>(() => item.Approve());
            Assert.Throws<InvalidOperationException>(() => item.Deny());
            Assert.Throws<InvalidOperationException>(() => item.Reject());
        }

        [Fact]
        public void Done_CanStill_AddThread()
        {
            var item = CreateItemThroughDone();
            var thread = new DomainThread("post-done");
            item.AddThread(thread);
            Assert.Single(item.GetThreads());
        }

        [Fact]
        public void ReadyForTesting_CanAddThread()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            var thread = new DomainThread("r4t");
            item.AddThread(thread);
            Assert.Single(item.GetThreads());
        }

        [Fact]
        public void Testing_CanAddThread()
        {
            var item = CreateItemThroughTesting();
            var thread = new DomainThread("in-test");
            item.AddThread(thread);
            Assert.Single(item.GetThreads());
        }

        [Fact]
        public void Tested_CanAddThread()
        {
            var item = CreateItemThroughTested();
            var thread = new DomainThread("tested");
            item.AddThread(thread);
            Assert.Single(item.GetThreads());
        }

        [Fact]
        public void NotifyTesters_WithMultipleTesters_SendsToEach()
        {
            var t1 = new Person("T1", Role.Tester);
            var t2 = new Person("T2", Role.Tester);
            var recorder = new RecordingNotificationService();
            var item = new BacklogItem(
                "Multi",
                "D",
                new List<Person> { t1, t2 },
                new Person("SM", Role.ScrumMaster),
                new List<INotificationService> { recorder });
            item.StartWork();
            item.ReadyForTesting();

            Assert.Equal(2, recorder.Sent.Count);
            Assert.Contains(recorder.Sent, x => x.Recipient == t1 && x.Message.Contains("Multi"));
            Assert.Contains(recorder.Sent, x => x.Recipient == t2 && x.Message.Contains("Multi"));
        }

        [Fact]
        public void NotifyTesters_WithMultipleServices_DuplicatesPerTester()
        {
            var tester = new Person("T", Role.Tester);
            var r1 = new RecordingNotificationService();
            var r2 = new RecordingNotificationService();
            var item = new BacklogItem(
                "X",
                "D",
                new List<Person> { tester },
                new Person("SM", Role.ScrumMaster),
                new List<INotificationService> { r1, r2 });
            item.StartWork();
            item.ReadyForTesting();

            Assert.Single(r1.Sent);
            Assert.Single(r2.Sent);
        }

        private static BacklogItem CreateItem()
        {
            var testers = new List<Person> { new Person("T1", Role.Tester) };
            var sm = new Person("SM", Role.ScrumMaster);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            return new BacklogItem("Story", "Desc", testers, sm, services);
        }

        private static BacklogItem CreateItemThroughTesting()
        {
            var item = CreateItem();
            item.StartWork();
            item.ReadyForTesting();
            item.StartTesting();
            return item;
        }

        private static BacklogItem CreateItemThroughTested()
        {
            var item = CreateItemThroughTesting();
            item.FinishTesting();
            return item;
        }

        private static BacklogItem CreateItemThroughDone()
        {
            var item = CreateItemThroughTested();
            item.Approve();
            Assert.Same(item.GetDoneState(), PrivateStateAccessor.GetBacklogState(item));
            return item;
        }
    }
}
