using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal abstract class AbstractAgent : IAgent
    {
        public ISettings Settings { get; protected set; }
        public string Id { get; protected set; }
        public IVertex Position { get; protected set; }
        public int Passengers { get; protected set; }
        public int PeopleSaved { get; protected set; }
        public int ActionsPerformed { get; protected set; }

        protected AbstractAgent(int id, ISettings settings, IVertex position)
        {
            Id = $"{GetType().Name}{id}";
            Settings = settings;
            Position = position;
            Position.Accept(this);
        }

        public double PerformStep(double time)
        {
            Console.WriteLine($"{Id} is playing from vertex {Position}.");
            var action = PlayNext(time);
            var newTime = time + action.Cost;
            if (newTime > Settings.Deadline)
            {
                Console.WriteLine($"{Id} decided to {action}, but there was not enough time for {Id} to finish his action.");
                newTime = Settings.Deadline;
            }
            else
            {
                action.Approve();
            }

            return newTime;
        }

        protected abstract IAction PlayNext(double time);

        protected IAction NoOperation()
        {
            return new NoOperation(this);
        }

        protected IAction Traverse(IEdge edge)
        {
            return new Traverse(this, edge, Settings.SlowDown);
        }

        protected IAction Block(IEdge edge)
        {
            return new Block(this, edge);
        }

        public virtual void Visit(EvacuationVertex v) { }

        public virtual void Visit(ShelterVertex v) { }

        public virtual void Visit(EmptyVertex v) { }

        public void MoveTo(IVertex destination)
        {
            ActionsPerformed++;
            Position = destination;
            Position.Accept(this);
        }
    }
}
