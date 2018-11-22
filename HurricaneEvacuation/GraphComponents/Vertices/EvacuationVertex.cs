namespace HurricaneEvacuation.GraphComponents.Vertices
{
    internal class EvacuationVertex : Vertex
    {
        public int PeopleCount { get; set; }

        public EvacuationVertex(int id, int peopleCount) : base(id)
        {
            PeopleCount = peopleCount;
        }

        public EvacuationVertex(EvacuationVertex ev) : base(ev)
        {
            PeopleCount = ev.PeopleCount;
        }

        public override IVertex Clone()
        {
            return PeopleCount > 0 ? new EvacuationVertex(this) : base.Clone();
        }

        public override string ToString()
        {
            return $"{base.ToString()}P{PeopleCount}";
        }
    }
}
