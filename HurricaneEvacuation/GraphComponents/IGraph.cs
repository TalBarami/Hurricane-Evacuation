using System.Collections.Generic;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    internal interface IGraph
    {
        List<IVertex> Vertices { get;  }
        List<Edge> Edges { get; }
        IGraph Clone();


        IVertex Vertex(int id);

        void AddEdge(int v1, int v2, int weight);
        List<Path> Dfs(int src, int dst);
        List<Path> Dfs(IVertex src, IVertex dst);
        List<Path> Dijkstra(int source);
        List<Path> Dijkstra(IVertex source);

        int EdgeWeight(int v1, int v2);
        int EdgeWeight(int v1, IVertex v2);
        int EdgeWeight(IVertex v1, int v2);
        int EdgeWeight(IVertex v1, IVertex v2);

        void RemoveEdge(int v1, int v2);
        void RemoveEdge(int v1, IVertex v2);
        void RemoveEdge(IVertex v1, int v2);
        void RemoveEdge(IVertex v1, IVertex v2);
    }
}
