namespace HurricaneEvacuation.GraphComponents.Vertices
{
    public class ShelterVertex : Vertex
    {
        public ShelterVertex(int id) : base(id)
        {
        }

        public ShelterVertex(ShelterVertex sv) : base(sv)
        {
        }

        public override IVertex Clone()
        {
            return new ShelterVertex(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}S";
        }
    }
}
