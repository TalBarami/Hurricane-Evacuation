using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Utils
{
    static class GraphUtils
    {
        public static double TraverseTime(double weight, int passengers, double slowDown)
        {
            return weight * (1 + passengers * slowDown);
        }
    }
}
