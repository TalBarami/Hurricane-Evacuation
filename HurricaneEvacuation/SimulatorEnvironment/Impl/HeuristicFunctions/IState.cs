using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    interface IState
    {
        ISettings Settings { get; }
        Traverse Action { get; }
        int Passengers { get; }
        double Time { get; }
    }
}
