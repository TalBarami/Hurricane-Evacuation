using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IEdge
    {
        bool Blocked { get; set; }
        bool ConnectedTo(IVertex v);
    }
}
