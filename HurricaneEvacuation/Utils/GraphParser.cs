using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.Utils
{
    class GraphParser
    {
        public IGraph CreateGraphFromFile(string path)
        {
            return CreateGraphFromStringList(File.ReadAllLines(path).ToList());
        }

        public IGraph CreateGraphFromString(string s)
        {
            return CreateGraphFromStringList(s.Split('\n').ToList());
        }

        public IGraph CreateGraphFromStringList(List<string> data)
        {
            var cleanData = data.Select(line => line.Split(';')[0]).ToList();

            var vertices = InitializeVertices(cleanData.FindAll(line => line.StartsWith("#V")), cleanData.FindAll(line => line.StartsWith("#E")));
            var deadline = int.Parse(cleanData.Find(line => line.StartsWith("#D")).Split(' ')[1]);

            return new Graph(deadline, vertices);
        }

        public IVertex[] InitializeVertices(List<string> verticesData, List<string> edgesData)
        {
            var sizeLine = verticesData.Find(line => !line.Contains("P") && !line.Contains("S"));
            var size = int.Parse(sizeLine.Split(' ')[1]);
            var vertices = new IVertex[size];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertexLine = verticesData.FirstOrDefault(line => int.Parse(line.Split(' ')[1]) == i);
                if (vertexLine != null)
                {
                    var parts = vertexLine.Split(' ');
                    switch (parts[2])
                    {
                        case "P":
                            vertices[i] = new EvacuationVertex(i+1, int.Parse(parts[3]));
                            break;
                        case "S":
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

            AssignEdges(vertices, edgesData);

            return vertices;
        }

        public void AssignEdges(IVertex[] vertices, List<string> data)
        {
            data.ForEach(line =>
            {
                var parts = line.Split(' ');
                var v1 = vertices.First(v => v.Id == int.Parse(parts[1]));
                var v2 = vertices.First(v => v.Id == int.Parse(parts[2]));
                var weight = int.Parse(parts[3].Substring(1));
                var e = new Edge(v1, v2, weight);
                v1.Neighbors.Add(e);
                v2.Neighbors.Add(e);
            });
        }

        /*public void ParseLine(string line, ref IVertex[] vertices, ref int deadline)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "#V":
                    var vNumber = int.Parse(parts[1]);
                    switch (parts[2])
                    {
                        case "P":
                            vertices[vNumber] = new EvacuationVertex(vNumber, int.Parse(parts[3]));
                            break;
                        case "S":
                            vertices[vNumber] = new ShelterVertex(vNumber);
                            break;
                        default:
                            vertices = new IVertex[vNumber];
                            break;
                    }
                    break;
                case "#E":
                    var v1 = vertices.First(v => v.Id == int.Parse(parts[1]));
                    var v2 = vertices.First(v => v.Id == int.Parse(parts[2]));
                    var weight = int.Parse(parts[3]);
                    var e = new Edge(v1, v2, weight);
                    v1.Neighbors.Add(e);
                    v2.Neighbors.Add(e);
                    break;
                case "#D":
                    deadline = int.Parse(parts[1]);
                    break;
                default:
                    throw new ParseException($"Unable to parse line: {line}");
            }
        }*/
    }

    public class ParseException : Exception
    {

        public ParseException()
        {
        }

        public ParseException(string message) : base(message)
        {
        }

        public ParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
