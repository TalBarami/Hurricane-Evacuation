﻿using System;
using System.Collections.Generic;

namespace HurricaneEvacuation.GraphComponents.Vertices
{
    internal interface IVertex : IComparable<IVertex>
    {
        int Id { get; }
        IGraph Graph { get; set; }
        List<int> Neighbors { get; }
        List<Edge> Edges { get; }
        IVertex Clone();
    }
}
