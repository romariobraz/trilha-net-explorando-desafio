using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

//Instâncias
List<Pessoa> hospedes = new List<Pessoa>();
Suite suite = null;
var reservas = new List<Reserva>();


//Menu Principal
inicio:
Console.WriteLine("Seja muito bem vindo ao nosso sistema de hotéis!\n" +
    "Escolha a opção desejada:\n" +
    "1 - Fazer uma reserva.\n" +
    "2 - Cadadastrar uma Suíte\n" +
    "3 - Exibir Reservas existenstes.\n" +
    "0 - Encerrar o sistema"
    );

int menu = Convert.ToInt32(Console.ReadLine());

switch (menu)
{
    case 1:
        try
        {
            // Verifica se há suíte cadastrada
            if (suite == null || suite.Capacidade == 0)
            {
                throw new InvalidOperationException("Não há suítes cadastradas! Efetue o cadastro (opção 2).");
            }

            // Captando os dados necessários
            NumeroHospede:
            Console.WriteLine("Digite a quantidade de hóspedes: ");
            int numeroHospede = Convert.ToInt32(Console.ReadLine());

            // Valida se número de hóspedes não excede a capacidade da suíte
            if (numeroHospede > suite.Capacidade)
            {
                Console.WriteLine("Número de hóspedes excede a capacidade da suíte!");
                goto NumeroHospede;
            }

            // Valida se número de hóspedes não é 0 ou menor
            if (numeroHospede <= 0)
            {
                Console.WriteLine("Número de hóspedes não pode ser menor que 0");
                goto NumeroHospede;
            }


        NumeroDias:
            Console.WriteLine("Digite a quantidade de dias: ");
            int numeroDias = Convert.ToInt32(Console.ReadLine());
            if (numeroDias <= 0)
            {
                Console.WriteLine("Por favor, digite um número válido maior que 0.");
                goto NumeroDias;

            }

                hospedes.Clear();

            // Laço para adicionar os hóspedes
            for (int i = 0; i < numeroHospede; i++)
            {
                Console.WriteLine($"Digite o nome do hóspede {i + 1}: ");
                string nome = Console.ReadLine();

                Console.WriteLine($"Digite o sobrenome do hóspede {i + 1}: ");
                string sobrenome = Console.ReadLine();

                Pessoa pessoa = new Pessoa(nome, sobrenome);
                hospedes.Add(pessoa);
            }

            // Cria a reserva
            Reserva res = new Reserva(diasReservados: numeroDias);
            res.CadastrarSuite(suite);

            // Tenta cadastrar hóspedes (pode lançar exceção se houver problema)
            res.CadastrarHospedes(hospedes);

            //Lança a reserva
            reservas.Add(res);

            Console.Clear();
            Console.WriteLine("Sejam muito bem-vindos ao nosso hotel!\n");

            foreach (var h in hospedes)
            {
                Console.WriteLine($"Sr(a) {h.NomeCompleto}");
            }

            Console.WriteLine($"\nValor da hospedagem: {res.CalcularValorDiaria():c}");
            Console.WriteLine($"Uma excelente estadia em nossa suíte {suite.TipoSuite}");

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
        catch (InvalidOperationException ex)
        {
            // Mostra mensagem de erro amigável
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
        catch (System.FormatException ex)
        {
            Console.WriteLine("Verifique o  tipo de dado inserido e tente novamente");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        goto inicio;

    case 2:
        Console.Clear();

        suite = new Suite();

        Console.WriteLine("Qual o tipo da suite?");
        suite.TipoSuite = Console.ReadLine();

        SuiteCapacidade:
        Console.WriteLine("Qual a capacidade?");
        suite.Capacidade = Convert.ToInt32(Console.ReadLine());
        if (suite.Capacidade <= 0)
        {
            Console.WriteLine("Por favor, digite um número válido maior que 0.");
            goto SuiteCapacidade;

        }

        Diaria:
        Console.WriteLine("Qual o valor da diária desta suíte?");
        suite.ValorDiaria = Convert.ToDecimal(Console.ReadLine());
        if (suite.ValorDiaria <= 0)
        {
            Console.WriteLine("Por favor, digite um número válido maior que 0.");
            goto Diaria;

        }


        Console.WriteLine("Suíte Cadastrada com sucesso!\nPressione qualquer tecla para voltar ao Menu.");

        Console.ReadKey();

        Console.Clear();

        goto inicio;

    case 3:
        if (reservas.Count == 0)
        {
            Console.WriteLine("\nNão há reservas cadastradas.");
        }
        else
        {
            Console.WriteLine("\nReservas cadastadas:");
            int index = 1;
            foreach (var r in reservas)
            {
                Console.WriteLine($"\nReserva {index++}:");
                Console.WriteLine($"Suíte: {r.Suite.TipoSuite}");
                Console.WriteLine($"Dias reservados: {r.DiasReservados}");
                Console.WriteLine($"Hóspedes:");
                foreach (var h in r.Hospedes)
                {
                    Console.WriteLine($"- {h.NomeCompleto}");
                }
                Console.WriteLine($"Valor total: {r.CalcularValorDiaria():C}");
            }
        }
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        goto inicio;


    case 0:
        Console.WriteLine("Encerrando o sistema...");
        break;

    default:
        Console.WriteLine("Opção inválida, digite uma opção válida.");
        goto inicio;
}