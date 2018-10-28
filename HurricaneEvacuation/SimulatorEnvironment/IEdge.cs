using System;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IEdge : IComparable
    {
        int Weight { get; }
        bool Blocked { get; set; }
        bool Contains(IVertex v1);
        bool Contains(IVertex v1, IVertex v2);
        IVertex OtherV(IVertex v);
    }
}
