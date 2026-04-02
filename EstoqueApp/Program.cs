using EstoqueApp.Models;
using EstoqueApp.Services;
using EstoqueApp.Data;

EstoqueService estoque = new EstoqueService();

Database db = new Database();
db.CriarTabela();

while (true)
{
    Console.Clear();

    Console.WriteLine("===== SISTEMA DE ESTOQUE =====");
    Console.WriteLine("1 - Adicionar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Entrada de estoque");
    Console.WriteLine("4 - Saída de estoque");
    Console.WriteLine("0 - Sair");

    int opcao = LerInt("\nEscolha: ");

    Console.Clear();

    switch (opcao)
    {
        case 1:
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
            Console.WriteLine("\nProduto adicionado!");
            break;

        case 2:
            estoque.ListarProdutos();
            break;

        case 3:
            int idEntrada = LerInt("ID do produto: ");
            int qtdEntrada = LerInt("Quantidade: ");

            estoque.Entrada(idEntrada, qtdEntrada);
            break;

        case 4:
            int idSaida = LerInt("ID do produto: ");
            int qtdSaida = LerInt("Quantidade: ");

            estoque.Saida(idSaida, qtdSaida);
            break;

        case 0:
            return;

        default:
            Console.WriteLine("Opção inválida!");
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
        string entrada = Console.ReadLine();

        valido = int.TryParse(entrada, out valor);

        if (!valido || valor < 0)
        {
            Console.WriteLine("Digite um número inteiro válido e positivo.");
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
        string entrada = Console.ReadLine();

        valido = double.TryParse(entrada, out valor);

        if (!valido || valor < 0)
        {
            Console.WriteLine("Digite um número válido e positivo.");
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
        texto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(texto))
        {
            Console.WriteLine("Texto não pode ser vazio.");
        }

    } while (string.IsNullOrWhiteSpace(texto));

    return texto;
}