using System.Collections.Generic;
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

        private static readonly FieldInfo SprintBacklogField =
            typeof(Sprint).GetField("sprintBacklog", BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("Sprint.sprintBacklog field not found");

        private static readonly FieldInfo ProjectSprintsField =
            typeof(Project).GetField("sprints", BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("Project.sprints field not found");

        private static readonly FieldInfo BacklogStateField =
            typeof(BacklogItem).GetField("state", BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException("BacklogItem.state field not found");

        public static ISprintState GetSprintState(Sprint sprint)
        {
            return (ISprintState)SprintStateField.GetValue(sprint)!;
        }

        public static int GetSprintBacklogCount(Sprint sprint)
        {
            var list = (List<BacklogItem>)SprintBacklogField.GetValue(sprint)!;
            return list.Count;
        }

        public static int GetProjectSprintCount(Project project)
        {
            var list = (List<Sprint>)ProjectSprintsField.GetValue(project)!;
            return list.Count;
        }

        public static IBacklogitemState? GetBacklogState(BacklogItem item)
        {
            return (IBacklogitemState?)BacklogStateField.GetValue(item);
        }
    }
}
