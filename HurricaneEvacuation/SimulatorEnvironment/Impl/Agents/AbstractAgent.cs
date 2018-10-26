using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal abstract class AbstractAgent : IAgent
    {
        public string Id { get; protected set; }
        public IVertex Position { get; set; }
        public IVertex Goal { get; set; }
        public int TicksLeft { get; set; }
        public int Passengers { get; set; }
        public int PeopleSaved { get; set; }
        public int ActionsPerformed { get; set; }

        protected AbstractAgent(IVertex position)
        {
            Position = Goal = position;
        }

        public void PerformStep(IGraph world)
        {
            if (TicksLeft == 0)
            {
                Reach();
                var action = PlayNext(world);
                ActionsPerformed++;
                Goal = action.Destination;
                TicksLeft = (int) Math.Round(action.Cost()) - 1;
                Console.WriteLine($"{Id} decided to {action}.");
            }
            else
            {
                Console.WriteLine($"{Id} is on his way from {Position} to {Goal}. {TicksLeft} ticks left.");
                TicksLeft--;
            }
        }

        private void Reach()
        {
            Console.WriteLine($"{Id} has reached {Goal}.");
            Position = Goal;
            Position.Accept(this);
        }

        public abstract IAction PlayNext(IGraph world);

        public void Visit(EvacuationVertex v)
        {
            if (v.PeopleCount <= 0) return;
            Passengers += v.PeopleCount;
            Console.WriteLine($"{Id} picked {v.PeopleCount} passengers and now carries {Passengers} passengers.");
            v.PeopleCount = 0;
        }

        public void Visit(ShelterVertex v)
        {
            if (Passengers <= 0) return;
            PeopleSaved += Passengers;
            Console.WriteLine($"{Id} dropped {Passengers} passengers in a shelter, saved total of {PeopleSaved} passengers");
            Passengers = 0;
        }

        public void Visit(EmptyVertex v)
        {
        }
    }
}
