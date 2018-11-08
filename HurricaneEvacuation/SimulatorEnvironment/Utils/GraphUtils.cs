namespace HurricaneEvacuation.SimulatorEnvironment.Utils
{
    static class GraphUtils
    {
        public static double TraverseTime(int weight, int passengers, double slowDown)
        {
            return weight * (1 + passengers * slowDown);
        }
    }
}
