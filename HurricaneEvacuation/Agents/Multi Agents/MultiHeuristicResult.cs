using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Actions;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class MultiHeuristicResult : HeuristicResult
    {
        public double Alpha { get; set; }
        public double Beta { get; set; }

        public MultiHeuristicResult(IAction action, double value) : base(action, value)
        {
            Alpha = int.MinValue;
            Beta = int.MaxValue;
        }

        public override string ToString()
        {
            return $"({Value},{Alpha},{Beta})->{GetType().Name}, {Action}";
        }
    }
}
