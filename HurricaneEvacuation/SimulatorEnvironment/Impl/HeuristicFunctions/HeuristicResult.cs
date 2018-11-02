using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class HeuristicResult
    {
        public Traverse Action { get; }
        private double Value { get; }
        private double Time { get; }
        private double Deadline { get; }
        public int Passengers { get; }
        public bool Shelter => Action.Destination is ShelterVertex;

        public HeuristicResult(Traverse action, double value, int passengers, double time, double deadline)
        {
            Action = action;
            Value = value;
            Passengers = passengers;
            Time = time;
            Deadline = deadline;
        }

        public bool GoalReached => Time >= Deadline;
        public double HValue => GoalReached ? 0 : Value;

        public double FValue(double travelTime)
        {
            return Value + Action.Cost + travelTime;
        }

        public override string ToString()
        {
            return GetString(Value);
        }

        public string FValueToString(double travelTime)
        {
            return GetString(FValue(travelTime));
        }

        public string HValueToString()
        {
            return GetString(HValue);
        }

        private string GetString(double val)
        {
            return $"({Action.Destination},{val})";
        }
    }
}
