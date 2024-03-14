using Chord.Classes;

namespace Chord
{
    public class Program()
    {
        private static void AtivarNo(List<No> lista, No no)
        {
            if (!no.Ativo)
            {
                var noResponsavel = lista.Where(x => x.NosResponsavel.Contains(no)).FirstOrDefault();

                if (noResponsavel != null)
                {
                    no.NosResponsavel.AddRange(noResponsavel.NosResponsavel.Where(x => x.Id <= no.Id));
                    noResponsavel.NosResponsavel = noResponsavel.NosResponsavel.Where(x => x.Id > no.Id).ToList();
                }

                no.Ativo = true;

                Console.WriteLine($"Nó {no.Id} ativado.");
            }
            else
            {
                Console.WriteLine($"O nó {no.Id} já está ativo.");
            }
        }
        private static void DesativarNo(List<No> lista, No no)
        {
            if (no.Ativo)
            {
                var noResponsavel = lista.Where(x => x.Id > no.Id && x.Ativo).FirstOrDefault();
                if (noResponsavel != null)
                {
                    noResponsavel.NosResponsavel.AddRange(no.NosResponsavel);
                }
                no.NosResponsavel.Clear();
                no.Ativo = false;

                Console.WriteLine($"Nó {no.Id} desativado.");
            }
            else
            {
               Console.WriteLine($"O nó {no.Id} já está inativo.");
            }
        }
        private static int ObterIdentificador(string informacao)
        {
            int somaChar = 0;
            int identificador = 0;

            while (identificador < 1 || identificador > 10)
            {
                for (var i = 0; i < informacao.Length; i++)
                {
                    somaChar += (int)informacao[i];
                }

                identificador = somaChar % 11;
                informacao = somaChar.ToString();
            }

            if (identificador > 0 && identificador <= 10)
            {
                return identificador;
            }

            return identificador;
        }
        private static void InserirInformacao(List<No> lista, string informacao)
        {
            var identificador = ObterIdentificador(informacao);

            foreach (var no in lista)
            {
                if (no.Id == identificador)
                {
                    if (no.Ativo)
                    {
                        no.Informacoes.Add(informacao);
                        Console.WriteLine($"Informação inserida no nó {no.Id}");
                        return;
                    }
                    else
                    {
                        identificador++;
                    }
                }
            }

            Console.WriteLine("Não foi possível inserir a informação.");
        }
        private static void BuscarInformacao(List<No> lista, string informacao)
        {
            var identificador = ObterIdentificador(informacao);

           foreach (var no in lista)
            {
                if (no.Ativo)
                {
                    if (identificador == no.Id)
                    {
                        var informacaoBuscada = no.Informacoes.Where(x => x.ToString() == informacao).FirstOrDefault();
                        if (informacaoBuscada != null)
                        {
                            Console.WriteLine($"A informação {informacaoBuscada} foi encontrada no nó {no.Id}");
                            return;
                        }
                    }
                    else if (no.NosResponsavel.Where(x => x.Id == identificador).FirstOrDefault() != null)
                    {
                        var noBuscado = no.NosResponsavel.Where(n => n.Id == identificador).FirstOrDefault();
                        var objeto = noBuscado
                            .Informacoes
                                .Where(info => info.ToString() == informacao)
                                .FirstOrDefault();
                        Console.WriteLine($"A informação {objeto} foi encontrada no nó {noBuscado.Id}");
                        return;
                    }
                    else
                    {
                        identificador++;
                    }
                }
            }

            Console.WriteLine("Informação não encontrada.");
            return;
        }

        private static void OpcaoAdicionarInformacao(List<No> lista)
        {
            Console.Write("Adicione uma informação: ");
            var informacao = Console.ReadLine();
            InserirInformacao(lista, informacao ?? "");
        }

        private static void OpcaoBuscarInformacao(List<No> lista)
        {
            Console.Write("Insira a informação desejada: ");
            var informacao = Console.ReadLine();
            BuscarInformacao(lista, informacao ?? "");
        }
        private static void OpcaoAtivarNo(List<No> lista)
        {
            Console.Write("Qual nó deseja ativar?: ");
            var no = Console.ReadLine();

            if (!string.IsNullOrEmpty(no) && int.Parse(no) < lista.Count && int.Parse(no) > 0)
            {
                var noAtivar = lista.Where(x => x.Id == int.Parse(no)).FirstOrDefault();
                AtivarNo(lista, noAtivar);
            }
        }
        private static void OpcaoDesativarNo(List<No> lista)
        {
            Console.Write("Qual nó deseja desativar?: ");
            var no = Console.ReadLine();

            if (!string.IsNullOrEmpty(no) && int.Parse(no) < lista.Count && int.Parse(no) > 0)
            {
                var noDesativar = lista.Where(x => x.Id == int.Parse(no)).FirstOrDefault();
                DesativarNo(lista, noDesativar);
            }
        }

        private static void Menu(List<No> lista)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine();
            Console.WriteLine("1 - Inserir Informação.");
            Console.WriteLine("2 - Buscar Informação.");
            Console.WriteLine("3 - Ativar Nó.");
            Console.WriteLine("4 - Desativar Nó.");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    OpcaoAdicionarInformacao(lista);
                    break;

                case "2":
                    OpcaoBuscarInformacao(lista);
                    break;

                case"3":
                    OpcaoAtivarNo(lista);
                    break;

                case "4":
                    OpcaoDesativarNo(lista);
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        public static void Main(string[] args)
        {
            List<No> lista = new();

            for (var i = 0; i < 10; i++)
            {
                lista.Add(new No(i));
            }

            while (true)
            {
                Menu(lista);
            }
        }
    }
}
