using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class GraphParser
    {
        private const string Vertex = "#V";
        private const string Edge = "#E";
        private const string Deadline = "#D";
        private const string Pickup = "P";
        private const string Shelter = "S";
        private const char Whitespace = ' ';
        private const char Comment = ';';
        private const char Newline = '\n';

        public (IGraph, int) CreateGraphFromString(string s)
        {
            var (graph, deadline) = CreateGraphFromStringList(s.Split(Newline).ToList());
            return (graph, deadline);
        }

        public (IGraph, int) CreateGraphFromStringList(List<string> data)
        {
            var cleanData = data.Select(line => line.Split(Comment)[0]).ToList();

            var (vertices, edges) = InitializeVertices(
                cleanData.FindAll(line => line.StartsWith(Vertex)),
                cleanData.FindAll(line => line.StartsWith(Edge)));
            var deadline = int.Parse(cleanData.Find(line => line.StartsWith(Deadline)).Split(Whitespace)[1]);

            return (new Graph(vertices, edges), deadline);
        }

        public (IVertex[], IEdge[]) InitializeVertices(List<string> verticesData, List<string> edgesData)
        {
            var sizeLine = verticesData.Find(line => !line.Contains(Pickup) && !line.Contains(Shelter));
            verticesData.Remove(sizeLine);
            var size = int.Parse(sizeLine.Split(Whitespace)[1]);
            var vertices = new IVertex[size];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertexLine = verticesData.FirstOrDefault(line => int.Parse(line.Split(Whitespace)[1]) == (i+1));
                if (vertexLine != null)
                {
                    var parts = vertexLine.Split(Whitespace);
                    switch (parts[2])
                    {
                        case Pickup:
                            vertices[i] = new EvacuationVertex(i+1, int.Parse(parts[3]));
                            break;
                        case Shelter:
                            vertices[i] = new ShelterVertex(i+1);
                            break;
                        default:
                            throw new ParseException($"Unable to parse line: {vertexLine}");
                    }
                }
                else
                {
                    vertices[i] = new EmptyVertex(i+1);
                }
            }

            var edges = AssignEdges(vertices, edgesData);

            return (vertices, edges);
        }

        public IEdge[] AssignEdges(IVertex[] vertices, List<string> data)
        {
            var edges = new List<IEdge>();
            data.ForEach(line =>
            {
                var parts = line.Split(Whitespace);
                var v1 = vertices.First(v => v.Id == int.Parse(parts[1]));
                var v2 = vertices.First(v => v.Id == int.Parse(parts[2]));
                var weight = int.Parse(parts[3].Substring(1));
                var e = new Edge(v1, v2, weight);
                edges.Add(e);
                v1.Neighbors.Add(e);
                v2.Neighbors.Add(e);
            });
            return edges.ToArray();
        }
    }
}
