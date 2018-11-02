using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;
using HurricaneEvacuation.SimulatorEnvironment.Tests;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tests = new ManualTests();

            tests.Test2();
        }
    }
}
