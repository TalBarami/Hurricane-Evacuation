using HurricaneEvacuation.SimulatorEnvironment.Tests;

namespace HurricaneEvacuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tests = new ManualTests();

            tests.TestAStarVandal();
        }
    }
}
