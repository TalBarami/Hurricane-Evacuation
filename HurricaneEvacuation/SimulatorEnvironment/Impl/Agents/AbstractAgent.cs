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
        public int Passengers { get; set; }
        public int PeopleSaved { get; set; }
        public int ActionsPerformed { get; set; }

        protected AbstractAgent(IVertex position)
        {
            Position = position;
            Position.Accept(this);
        }

        public IAction PerformStep()
        {
            Console.WriteLine($"{Id} is playing from vertex {Position}.");
            var action = PlayNext();
            Console.WriteLine($"{Id} decided to {action}.");
            ActionsPerformed++;
            Position = action.Destination;
            Position.Accept(this);

            return action;
            /*if (TicksLeft == 0)
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
            }*/
        }

        public abstract IAction PlayNext();

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
