using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    abstract class AbstractVertex : IVertex
    {
        public int Id { get; private set; }
        public IList<IEdge> Neighbors { get; }

        protected AbstractVertex(int id)
        {
            Id = id;
            Neighbors = new List<IEdge>();
        }

        public abstract void Accept(IAgent agent);
    }
}
