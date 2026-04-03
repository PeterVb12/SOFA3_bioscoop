using System;
using System.Collections.Generic;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.Pipelines;

namespace SOFA3_bioscoop.Test.TestSupport
{
    internal static class SprintTestFactories
    {
        public static Sprint CreateReleaseSprint(
            DevelopmentPipeline? pipeline = null,
            INotificationService? notifications = null)
        {
            return new Sprint(
                "Sprint",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReleaseStrategy(),
                new Project(),
                pipeline,
                notifications);
        }

        public static Sprint CreateReviewSprint(
            DevelopmentPipeline? pipeline = null,
            INotificationService? notifications = null)
        {
            return new Sprint(
                "ReviewSprint",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(14),
                new List<BacklogItem>(),
                new ReviewStrategy(),
                new Project(),
                pipeline,
                notifications);
        }

        public static BacklogItem MinimalBacklogItem()
        {
            var testers = new List<Person> { new Person("T", Role.Tester) };
            var sm = new Person("SM", Role.ScrumMaster);
            var services = new List<INotificationService> { new ConsoleNotificationService() };
            return new BacklogItem("BI", "Desc", testers, sm, services);
        }
    }
}
