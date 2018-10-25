using System.Collections.Generic;
using System.IO;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class GraphParser
    {
        private const string VERTEX = "#V";
        private const string EDGE = "#E";
        private const string DEADLINE = "#D";
        private const string PICKUP = "P";
        private const string SHELTER = "S";
        private const char WHITESPACE = ' ';
        private const char COMMENT = ';';
        private const char NEWLINE = '\n';

        public (IGraph, int) CreateGraphFromString(string s)
        {
            var (graph, deadline) = CreateGraphFromStringList(s.Split(NEWLINE).ToList());
            return (graph, deadline);
        }

        public (IGraph, int) CreateGraphFromStringList(List<string> data)
        {
            var cleanData = data.Select(line => line.Split(COMMENT)[0]).ToList();

            var (vertices, edges) = InitializeVertices(
                cleanData.FindAll(line => line.StartsWith(VERTEX)),
                cleanData.FindAll(line => line.StartsWith(EDGE)));
            var deadline = int.Parse(cleanData.Find(line => line.StartsWith(DEADLINE)).Split(WHITESPACE)[1]);

            return (new Graph(vertices, edges), deadline);
        }

        public (IVertex[], IEdge[]) InitializeVertices(List<string> verticesData, List<string> edgesData)
        {
            var sizeLine = verticesData.Find(line => !line.Contains(PICKUP) && !line.Contains(SHELTER));
            verticesData.Remove(sizeLine);
            var size = int.Parse(sizeLine.Split(WHITESPACE)[1]);
            var vertices = new IVertex[size];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertexLine = verticesData.FirstOrDefault(line => int.Parse(line.Split(WHITESPACE)[1]) == (i+1));
                if (vertexLine != null)
                {
                    var parts = vertexLine.Split(WHITESPACE);
                    switch (parts[2])
                    {
                        case PICKUP:
                            vertices[i] = new EvacuationVertex(i+1, int.Parse(parts[3]));
                            break;
                        case SHELTER:
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
                var parts = line.Split(WHITESPACE);
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
