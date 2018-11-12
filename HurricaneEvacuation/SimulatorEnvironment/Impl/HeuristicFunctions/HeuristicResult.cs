using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class HeuristicResult
    {
        public Traverse Action { get; }
        public double Time { get; }
        public int Passengers { get; }
        public bool Shelter => Action.Destination is ShelterVertex;

        private double Value { get; }
        private double Deadline { get; }
        public double TravelTime { get; set; }

        public HeuristicResult(Traverse action, double value, int passengers, double time, double deadline)
        {
            Action = action;
            Value = value;
            Passengers = passengers;
            Time = time;
            Deadline = deadline;
            TravelTime = 0;
        }

        public bool GoalReached => Time >= Deadline;
        public double HValue => GoalReached ? 0 : Value;
        public double FValue => Value + Action.Cost + TravelTime;
        public double GValue => TravelTime + Action.Cost;

        public override string ToString()
        {
            return $"({Action.Destination}, cost={Action.Cost}, value={Value}, h={HValue}, g={GValue}, f={FValue})";
        }
    }
}
