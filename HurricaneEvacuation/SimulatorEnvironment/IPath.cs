using System;
using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IPath : IComparable
    {
        IVertex Source { get; }
        IPath Next { get; }
        int Weight { get; }
        IList<IVertex> GetVertices();

    }
}
