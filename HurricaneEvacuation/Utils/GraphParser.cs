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
        private const string VERTEX = "#V";
        private const string EDGE = "#E";
        private const string DEADLINE = "#D";
        private const char PICKUP = 'P';
        private const char SHELTER = 'S';
        private const char WHITESPACE = ' ';
        private const char COMMENT = ';';
        private const char NEWLINE = '\n';


        public (IGraph, string) CreateGraphFromFile(string path)
        {
            return CreateGraphFromString(File.ReadAllText(path));
        }

        public (IGraph, string) CreateGraphFromString(string s)
        {
            return (CreateGraphFromStringList(s.Split(NEWLINE).ToList()), s);
        }

        public IGraph CreateGraphFromStringList(List<string> data)
        {
            var cleanData = data.Select(line => line.Split(COMMENT)[0]).ToList();

            var vertices = InitializeVertices(
                cleanData.FindAll(line => line.StartsWith(VERTEX)),
                cleanData.FindAll(line => line.StartsWith(EDGE)));
            var deadline = int.Parse(cleanData.Find(line => line.StartsWith(DEADLINE)).Split(WHITESPACE)[1]);

            return new Graph(deadline, vertices);
        }

        public IVertex[] InitializeVertices(List<string> verticesData, List<string> edgesData)
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
                var parts = line.Split(WHITESPACE);
                var v1 = vertices.First(v => v.Id == int.Parse(parts[1]));
                var v2 = vertices.First(v => v.Id == int.Parse(parts[2]));
                var weight = int.Parse(parts[3].Substring(1));
                var e = new Edge(v1, v2, weight);
                v1.Neighbors.Add(e);
                v2.Neighbors.Add(e);
            });
        }
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
