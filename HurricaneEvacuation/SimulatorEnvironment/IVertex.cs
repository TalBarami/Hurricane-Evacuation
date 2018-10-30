﻿using System;
using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IVertex : IComparable
    {
        int Id { get; }
        IList<IEdge> Neighbors { get; }
        void Accept(IAgent agent);
        IList<IEdge> ValidEdges();
    }
}
