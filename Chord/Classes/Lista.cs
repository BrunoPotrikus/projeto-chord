namespace Chord.Classes
{
    public class Lista
    {
        public No? Inicio { get; set; }
        public No? Final { get; set; }
        public int Tamanho { get; set; }
        public void InserirNo(No no)
        {
            if (Tamanho == 0)
            {
                Inicio = no;
                Final = no;
                Inicio.Proximo = Final;
                Final.Anterior = Inicio;
            }
            else
            {
                InserirNoFinal(no);
            }

            Inicio.Anterior = Final;
            Final.Proximo = Inicio;

            Tamanho++;
        }
        private void InserirNoFinal(No no)
        {
            No noAux = new(Final.Id);
            noAux = Final;
            Final.Anterior.Proximo = noAux;
            noAux.Anterior = Final.Anterior;
            noAux.Proximo = Final;
            Final = no;
            Final.Anterior = noAux;
        }
    }
}
