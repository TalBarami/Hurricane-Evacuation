using System;
using System.Collections.Generic;
using System.Linq;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal abstract class AbstractVertex : IVertex
    {
        public int Id { get; }
        public IList<IEdge> Neighbors { get; }

        protected AbstractVertex(int id)
        {
            Id = id;
            Neighbors = new List<IEdge>();
        }

        public abstract void Accept(IAgent agent);
        
        public IList<IEdge> ValidEdges()
        {
            return Neighbors.Where(e => !e.Blocked).ToList();
        }
        public IList<IVertex> ValidNeighbors()
        {
            return ValidEdges().Select(e => e.OtherV(this)).ToList();
        }

        public override string ToString()
        {
            return $"V{Id}";
        }

        public int CompareTo(object obj)
        {
            if (obj is IVertex vertex)
            {
                return Id.CompareTo(vertex.Id);
            }

            throw new Exception("Invalid object type");
        }
    }
}
