using EstoqueApp.Models;
using EstoqueApp.Services;
using EstoqueApp.Data;

EstoqueService estoque = new EstoqueService();

Database db = new Database();
db.CriarTabela();

while (true)
{
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=======================================");
    Console.WriteLine("        SISTEMA DE ESTOQUE");
    Console.WriteLine("=======================================");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine(" 1 - Adicionar produto");
    Console.WriteLine(" 2 - Listar produtos");
    Console.WriteLine(" 3 - Entrada de estoque");
    Console.WriteLine(" 4 - Saída de estoque");
    Console.WriteLine(" 5 - Buscar produto");
    Console.WriteLine(" 6 - Ver estoque baixo");
    Console.WriteLine(" 0 - Sair");
    Console.WriteLine();

    int opcao = LerInt(" Escolha uma opção: ");

    Console.Clear();

    switch (opcao)
    {
        case 1:
            Console.WriteLine("\n--- Cadastro de Produto ---\n");

            int id = LerInt("ID: ");
            string nome = LerTexto("Nome: ");
            int qtd = LerInt("Quantidade: ");
            double preco = LerDouble("Preço: ");

            Produto p = new Produto
            {
                Id = id,
                Nome = nome,
                Quantidade = qtd,
                Preco = preco
            };

            estoque.AdicionarProduto(p);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✔ Produto adicionado!");
            Console.ResetColor();
            break;

        case 2:
            estoque.ListarProdutos();
            break;

        case 3:
            Console.WriteLine("\n--- Entrada de Estoque ---\n");

            int idEntrada = LerInt("ID do produto: ");
            int qtdEntrada = LerInt("Quantidade: ");

            estoque.Entrada(idEntrada, qtdEntrada);
            break;

        case 4:
            Console.WriteLine("\n--- Saída de Estoque ---\n");

            int idSaida = LerInt("ID do produto: ");
            int qtdSaida = LerInt("Quantidade: ");

            estoque.Saida(idSaida, qtdSaida);
            break;

        case 5:
            Console.WriteLine("\n--- Buscar Produto ---\n");

            string nomeBusca = LerTexto("Digite o nome: ");
            estoque.BuscarProduto(nomeBusca);
            break;
        case 6:
            Console.WriteLine("\n--- Estoque Baixo ---\n");

            int limite = LerInt("Mostrar produtos com quantidade até: ");
            estoque.EstoqueBaixo(limite);
            break;
        case 0:
            return;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Opção inválida!");
            Console.ResetColor();
            break;
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}

//////////////////////////////////////////////////////////

int LerInt(string mensagem)
{
    int valor;
    bool valido;

    do
    {
        Console.Write(mensagem);
        string entrada = Console.ReadLine() ?? "";

        valido = int.TryParse(entrada, out valor);

        if (!valido || valor < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Digite um número inteiro válido e positivo.");
            Console.ResetColor();
        }

    } while (!valido || valor < 0);

    return valor;
}

double LerDouble(string mensagem)
{
    double valor;
    bool valido;

    do
    {
        Console.Write(mensagem);
        string entrada = Console.ReadLine() ?? "";

        valido = double.TryParse(entrada, out valor);

        if (!valido || valor < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Digite um número válido e positivo.");
            Console.ResetColor();
        }

    } while (!valido || valor < 0);

    return valor;
}

string LerTexto(string mensagem)
{
    string texto;

    do
    {
        Console.Write(mensagem);
        texto = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(texto))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Texto não pode ser vazio.");
            Console.ResetColor();
        }

    } while (string.IsNullOrWhiteSpace(texto));

    return texto;
}