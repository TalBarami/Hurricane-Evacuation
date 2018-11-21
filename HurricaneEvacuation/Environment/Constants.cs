namespace HurricaneEvacuation.Environment
{
    internal static class Constants
    {
        public static double Deadline { get; private set; }
        public static double SlowDown { get; private set; }
        public static double WeightConstant { get; private set; }
        public static int Cutoff { get; private set; }

        public static void Initialize(int deadline, double slowDown, int weightConstant, int cutoff)
        {
            Deadline = deadline;
            SlowDown = slowDown;
            WeightConstant = weightConstant;
            Cutoff = cutoff;
        }
    }
}
