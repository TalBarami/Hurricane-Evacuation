using System.Collections;
using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IVertex
    {
        int Id { get; }
        IList<IEdge> Neighbors { get; }
        void Accept(IAgent agent);
    }
}
