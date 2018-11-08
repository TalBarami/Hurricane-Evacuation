using System;
using System.Collections.Generic;
using System.Text;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class Path : IPath
    {
        public IVertex Source { get; }
        public IPath Next { get; }
        public int Weight { get; }

        public Path(IVertex source, IPath next, int weight)
        {
            Source = source;
            Next = next;
            Weight = weight;
        }

        public IList<IVertex> GetVertices()
        {
            var vertices = new List<IVertex> {Source};
            var iterator = Next;

            while (iterator != null)
            {
                vertices.Add(iterator.Source);
                iterator = iterator.Next;
            }

            return vertices;
        }

        public (double, int) TraverseWeight(IList<IVertex> visited, int passengers, double slowDown)
        {
            var currentPassengers = passengers;
            GetPassengers(Source, visited, ref currentPassengers);
            var weight = 0.0;
            var currentWeight = Weight;
            var iterator = Next;
            while (iterator != null)
            {
                var edgeWeight = currentWeight - iterator.Weight;
                weight += GraphUtils.TraverseTime(edgeWeight, currentPassengers, slowDown);

                GetPassengers(iterator.Source, visited, ref currentPassengers);

                currentWeight = iterator.Weight;
                iterator = iterator.Next;
            }
            return (weight, currentPassengers);
        }

        public (double, int) TraverseWeight(int passengers, double slowDown)
        {
            return TraverseWeight(new List<IVertex>(), passengers, slowDown);
        }
        public IPath Reverse()
        {
            var currentNode = new Path(Source, null, 0);
            var currentWeight = Weight;
            var iterator = Next;

            while (iterator != null)
            {
                currentNode = new Path(iterator.Source, currentNode, currentWeight - iterator.Weight);
                iterator = iterator.Next;
            }

            return currentNode;
        }

        private void GetPassengers(IVertex v, IList<IVertex> visited, ref int currentPassengers)
        {
            if (!visited.Contains(v) && v is EvacuationVertex ev)
            {
                currentPassengers += ev.PeopleCount;
            } else if (v is ShelterVertex)
            {
                currentPassengers = 0;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Weight: {Weight}, Path: {Source}");

            var iterator = Next;
            while (iterator != null)
            {
                sb.Append("->").Append(iterator.Source);
                iterator = iterator.Next;
            }

            return sb.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj != null && obj is IPath path)
            {
                return Weight.CompareTo(path.Weight);
            }
            throw new NotSupportedException();
        }
    }
}
