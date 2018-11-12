﻿using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IHeuristicFunction
    {
        int SearchExpansions { get; }
        HeuristicResult Apply(IState state);
    }
}
