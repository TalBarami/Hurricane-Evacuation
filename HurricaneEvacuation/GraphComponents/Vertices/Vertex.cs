using System.Collections.Generic;
using System.Linq;

namespace HurricaneEvacuation.GraphComponents.Vertices
{
    internal class Vertex : IVertex
    {
        public int Id { get; }
        public IGraph Graph { get; set; }
        public List<int> Neighbors { get; }
        public List<Edge> Edges => Neighbors.Select(v => new Edge(this, Graph.Vertex(v), Graph.EdgeWeight(Id, v))).ToList();

    public Vertex(int id)
        {
            Id = id;
            Neighbors = new List<int>();
        }

        public Vertex(Vertex other) : this(other.Id)
        {
            other.Neighbors.ForEach(v => Neighbors.Add(v));
        }

        public virtual IVertex Clone()
        {
            return new Vertex(this);
        }
        
        public override string ToString()
        {
            return $"V{Id}";
        }

        public int CompareTo(IVertex other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
