using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class State : IState
    {
        public ISettings Settings { get; }
        public IList<IVertex> Visited { get; }
        public int Passengers { get; }
        public double Time { get; }
        public Traverse Action { get;  }

        public State(ISettings settings, IList<IVertex> visited, Traverse action, int passengers, double time)
        {
            Settings = settings;
            Visited = visited;
            Action = action;
            Passengers = passengers;
            Time = time;
        }

        public State(IState state)
        {
            Settings = state.Settings;
            Passengers = state.Passengers;
            Time = state.Time;
            Action = state.Action;
        }

        public State(IState state, int passengers, double time) : this(state)
        {
            Passengers = passengers;
            Time = time;
        }
    }
}
