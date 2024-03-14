namespace Chord.Classes
{
    public class No
    {
        public int Id { get; set; }
        public No? Anterior { get; set; }
        public No? Proximo { get; set; }
        public List<object> Informacoes { get; set; } = new();
        public List<No> NosResponsavel { get; set; } = new();
        public bool Ativo { get; set; } = false;

        public No(int id)
        {
            Id = id;
        }
    }
}
