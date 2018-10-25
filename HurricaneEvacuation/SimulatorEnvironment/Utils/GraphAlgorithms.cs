using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Utils
{
    static class GraphAlgorithms
    {
        public static IList<IPath> Dijkstra(IGraph g, IVertex s)
        {
            var paths = new Dictionary<IVertex, IPath>();
            var flags = new Dictionary<IVertex, bool>();

            var vertices = new List<IVertex>(g.Vertices);
            foreach (var v in vertices)
            {
                paths[v] = new Path(v, null, v == s ? 0 : int.MaxValue);
                flags[v] = false;
            }

            var unreachable = new EmptyVertex(-1);
            while (true)
            {
                IPath least = new Path(unreachable, null, int.MaxValue);
                foreach (var v in vertices)
                {
                    if (!flags[v] && paths[v].Weight < least.Weight)
                    {
                        least = paths[v];
                    }
                }
                if (least.Source == unreachable)
                {
                    break;
                }
                var src = least.Source;
                flags[src] = true;

                foreach (var e in src.ValidEdges())
                {
                    var dst = e.OtherV(src);
                    if (e.Weight <= 0 || flags[dst]) continue;

                    var oldPath = paths[dst];
                    var newWeight = e.Weight + least.Weight;
                    if (newWeight < oldPath.Weight)
                    {
                        paths[oldPath.Source] = new Path(oldPath.Source, least, newWeight);
                    }
                }
            }

            var result = new List<IPath>(paths.Values);
            result.Sort((p1, p2) => p1.Source.CompareTo(p2.Source));
            return result;
        }
    }
}
