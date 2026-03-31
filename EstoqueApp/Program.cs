using EstoqueApp.Models;
using EstoqueApp.Services;

EstoqueService estoque = new EstoqueService();

while (true)
{
    Console.Clear();

    Console.WriteLine("===== SISTEMA DE ESTOQUE =====");
    Console.WriteLine("1 - Adicionar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Entrada de estoque");
    Console.WriteLine("4 - Saída de estoque");
    Console.WriteLine("0 - Sair");

    Console.Write("\nEscolha: ");
    int opcao = int.Parse(Console.ReadLine());

    Console.Clear();

    switch (opcao)
    {
        case 1:
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Quantidade: ");
            int qtd = int.Parse(Console.ReadLine());

            Console.Write("Preço: ");
            double preco = double.Parse(Console.ReadLine());

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
            Console.Write("ID do produto: ");
            int idEntrada = int.Parse(Console.ReadLine());

            Console.Write("Quantidade: ");
            int qtdEntrada = int.Parse(Console.ReadLine());

            estoque.Entrada(idEntrada, qtdEntrada);
            break;

        case 4:
            Console.Write("ID do produto: ");
            int idSaida = int.Parse(Console.ReadLine());

            Console.Write("Quantidade: ");
            int qtdSaida = int.Parse(Console.ReadLine());

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