using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    public class GraphParser
    {
        private const string Vertex = "#V";
        private const string Edge = "#E";
        private const string Pickup = "P";
        private const string Shelter = "S";
        private const char Whitespace = ' ';
        private const char Comment = ';';
        private const char Newline = '\n';

        public IGraph CreateGraphFromString(string s)
        {
            return CreateGraphFromStringList(s.Replace("\r", "").Split(Newline).ToList());
        }

        public IGraph CreateGraphFromStringList(List<string> data)
        {
            var cleanData = data.Select(line => line.Split(Comment)[0]).ToList();

            return InitializeGraphComponents(
                cleanData.FindAll(line => line.StartsWith(Vertex)),
                cleanData.FindAll(line => line.StartsWith(Edge)));
        }

        private Graph InitializeGraphComponents(List<string> verticesData, List<string> edgesData)
        {
            var sizeLine = verticesData.Find(line => !line.Contains(Pickup) && !line.Contains(Shelter));
            verticesData.Remove(sizeLine);
            var size = int.Parse(sizeLine.Split(Whitespace)[1]);
            var vertices = new IVertex[size];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertexLine = verticesData.FirstOrDefault(line => int.Parse(line.Split(Whitespace)[1]) == i);
                if (vertexLine != null)
                {
                    var parts = vertexLine.Split(Whitespace);
                    switch (parts[2].ToUpper())
                    {
                        case Pickup:
                            vertices[i] = new EvacuationVertex(i, int.Parse(parts[3]));
                            break;
                        case Shelter:
                            vertices[i] = new ShelterVertex(i);
                            break;
                        default:
                            throw new ParseException($"Unable to parse line: {vertexLine}");
                    }
                }
                else
                {
                    vertices[i] = new Vertex(i);
                }
            }

            var graph = new Graph(vertices.ToList());
            AssignEdges(graph, edgesData);

            return graph;
        }

        private void AssignEdges(Graph graph, List<string> data)
        {
            data.ForEach(line =>
            {
                var parts = line.Split(Whitespace);
                var weight = int.Parse(parts[3].Substring(1));
                graph.AddEdge(int.Parse(parts[1]), int.Parse(parts[2]), weight);
            });
        }

        internal class ParseException : Exception
        {
            public ParseException(string message) : base(message)
            {
            }
        }
    }
}
