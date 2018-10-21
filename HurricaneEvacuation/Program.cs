using HurricaneEvacuation.SimulatorEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Utils;

namespace HurricaneEvacuation
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "#V 4    ; number of vertices n in graph (from 1 to n)\n" +
                       "#E 1 2 W1                 ; Edge from vertex 1 to vertex 2, weight 1\n" +
                       "#E 3 4 W1                 ; Edge from vertex 3 to vertex 4, weight 1\n" +
                       "#E 2 3 W1                 ; Edge from vertex 2 to vertex 3, weight 1\n" +
                       "#E 1 3 W4                 ; Edge from vertex 1 to vertex 3, weight 4\n" +
                       "#E 2 4 W5                 ; Edge from vertex 2 to vertex 4, weight 5\n" +
                       "#V 2 P 1                  ; Vertex 2 initially contains 1 person to be rescued\n" +
                       "#V 1 S                    ; Vertex 1 contains a hurricane shelter (a \"goal vertex\" - there may be more than one)\n" +
                       "#V 4 P 2                  ; Vertex 4 initially contains 2 persons to be rescued\n" +
                       "#D 10                     ; Deadline is at time 10";

            var parser = new GraphParser();
            IGraph g = parser.CreateGraphFromString(s);
            Console.Read();
        }
    }
}
