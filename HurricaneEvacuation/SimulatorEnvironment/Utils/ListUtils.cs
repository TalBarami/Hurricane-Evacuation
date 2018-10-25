using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Utils
{
    internal static class ListUtils
    {
        public static string ListToString<T>(this IList<T> lst)
        {
            return string.Join(" ; ", lst);
        }
    }
}
