using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    class VandalAgent : AbstractAgent
    {
        private int InitialDelay { get; set; }
        public VandalAgent(IVertex position, int initialDelay) : base(position)
        {
            InitialDelay = initialDelay;
        }

        public override IAction PerformStep(IGraph world)
        {
            if (InitialDelay > 0)
            {
                InitialDelay--;
                return new NoOperation();
            }

            return new Traverse();
        }
    }
}
