using System.Reflection;
using SOFA_bioscoop.Domain;
using SOFA_bioscoop.Domain.BacklogItems;

namespace SOFA3_bioscoop.Test.TestSupport
{
    internal static class PrivateStateAccessor
    {
        private static readonly FieldInfo SprintStateField =
            typeof(Sprint).GetField("state", BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("Sprint.state field not found");

        private static readonly FieldInfo BacklogStateField =
            typeof(BacklogItem).GetField("state", BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("BacklogItem.state field not found");

        public static ISprintState GetSprintState(Sprint sprint)
        {
            return (ISprintState)SprintStateField.GetValue(sprint)!;
        }

        public static IBacklogitemState? GetBacklogState(BacklogItem item)
        {
            return (IBacklogitemState?)BacklogStateField.GetValue(item);
        }
    }
}
