using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Actions;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class MultiValuesHeuristicResult
    {
        public IAction Action { get; }
        public double[] Values { get; set; }

        public MultiValuesHeuristicResult(IAction action, double[] values)
        {
            Action = action;
            Values = values;
        }

        public override string ToString()
        {
            return $"({string.Join(",", Values)})->{GetType().Name}, {Action}";
        }
    }
}
