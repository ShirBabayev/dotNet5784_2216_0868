namespace Dal
{
    internal static class DataSource
    {
        internal static class Config
        {
            internal const int startTaskId = 1000;
            private static int nextTaskId = startTaskId;
            internal static int NextTaskId { get => nextTaskId++; }

            internal const int startDependencyId = 1000;
            private static int nextDependencyId = startDependencyId;
            internal static int NextDependencyId { get => nextDependencyId++; }

        }
        internal static List<DO.Engineer> Engineers { get; } = new(); //a list that contains engineer objects
        internal static List<DO.Task> Tasks { get; } = new();//a list that contains task objects
        internal static List<DO.Dependency> Dependencies { get; } = new();//a list that contains dependency objects

    }
}


