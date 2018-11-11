using System;
using System.Collections.Generic;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IPath : IComparable
    {
        IVertex Source { get; }
        IPath Next { get; }
        int Weight { get; }
        IList<IVertex> GetVertices();
        (double, int) TraverseWeight(IList<IVertex> visited, int passengers, double slowDown);
        (double, int) TraverseWeight(int passengers, double slowDown);
        IPath Reverse();

        bool Blocked(IList<VandalAgent> vandalAgents, int passengers, double time, double slowDown);
    }
}
