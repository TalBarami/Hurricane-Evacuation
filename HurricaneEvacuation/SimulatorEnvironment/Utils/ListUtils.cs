using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Utils
{
    internal static class ListUtils
    {
        public static string ListToString<T>(this IList<T> lst)
        {
            return string.Join(" ; ", lst);
        }

        public static HeuristicResult SelectMinimal(this IList<HeuristicResult> list)
        {
            return list.Aggregate(list.First(), (min, next) => min.FValue > next.FValue ? next : min);
        }
    }
}
