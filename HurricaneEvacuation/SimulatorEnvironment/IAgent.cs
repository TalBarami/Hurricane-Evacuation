using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IAgent
    {
        IVertex Position { get; set; }
        IVertex Goal { get; set; }
        int TicksLeft { get; set; }
        int Passengers { get; set; }
        IAction PerformStep(IGraph world);
        void Visit(EvacuationVertex v);
        void Visit(ShelterVertex v);
        void Visit(EmptyVertex v);
    }
}
