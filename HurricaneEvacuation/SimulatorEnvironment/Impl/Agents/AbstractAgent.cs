using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal abstract class AbstractAgent : IAgent
    {
        public string Id { get; protected set; }
        public IVertex Position { get; protected set; }
        public int Passengers { get; protected set; }
        public int PeopleSaved { get; protected set; }
        public int ActionsPerformed { get; protected set; }

        protected AbstractAgent(int id, IVertex position)
        {
            Id = $"{GetType().Name}{id}";
            Position = position;
            Position.Accept(this);
        }

        public IAction PerformStep()
        {
            Console.WriteLine($"{Id} is playing from vertex {Position}.");
            var action = PlayNext();
            return action;
        }

        protected abstract IAction PlayNext();

        public abstract void Visit(EvacuationVertex v);

        public abstract void Visit(ShelterVertex v);

        public void Visit(EmptyVertex v)
        {
        }

        public void MoveTo(IVertex destination)
        {
            ActionsPerformed++;
            Position = destination;
            Position.Accept(this);
        }
    }
}
