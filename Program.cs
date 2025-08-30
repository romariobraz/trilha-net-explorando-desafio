using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

//Instâncias
List<Pessoa> hospedes = new List<Pessoa>();
Suite suite = new Suite();


//Menu Principal
inicio:
Console.WriteLine("Seja muito bem vindo ao nosso sistema de hotéis!\n" +
    "Escolha a opção desejada:\n" +
    "1 - Fazer uma reserva.\n" +
    "2 - Cadadastrar uma Suíte\n" +
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
            Console.WriteLine("Digite a quantidade de hóspedes: ");
            int numeroHospede = Convert.ToInt32(Console.ReadLine());

            // Valida se número de hóspedes não excede a capacidade da suíte
            if (numeroHospede > suite.Capacidade)
            {
                throw new InvalidOperationException("Número de hóspedes excede a capacidade da suíte!");
            }

            Console.WriteLine("Digite a quantidade de dias: ");
            int numeroDias = Convert.ToInt32(Console.ReadLine());

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

        goto inicio;

    case 2:
        Console.Clear();

        Console.WriteLine("Qual o tipo da suite?");
        suite.TipoSuite = Console.ReadLine();

        Console.WriteLine("Qual a capacidade?");
        suite.Capacidade = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Qual o valor da diária desta suíte?");
        suite.ValorDiaria = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Suíte Cadastrada com sucesso!\nPressione qualquer tecla para voltar ao Menu.");

        Console.ReadKey();

        Console.Clear();

        goto inicio;

    case 0:
        Console.WriteLine("Encerrando o sistema...");
        break;

    default:
        Console.WriteLine("Opção inválida, digite uma opção válida.");
        goto inicio;
}