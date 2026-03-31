using EstoqueApp.Models;

namespace EstoqueApp.Services
{
    class EstoqueService
    {
        private List<Produto> produtos = new List<Produto>();

        // 📌 Adicionar produto
        public void AdicionarProduto(Produto produto)
        {
            var existente = produtos.Find(p => p.Id == produto.Id);

            if (existente != null)
            {
                Console.WriteLine("Já existe um produto com esse ID!");
                return;
            }

            produtos.Add(produto);
        }

        // 📌 Listar produtos
        public void ListarProdutos()
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.WriteLine("===== LISTA DE PRODUTOS =====");

            foreach (var p in produtos)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Qtd: {p.Quantidade} | Preço: R$ {p.Preco}");
            }
        }

        // 📥 Entrada de estoque
        public void Entrada(int id, int quantidade)
        {
            var produto = produtos.Find(p => p.Id == id);

            if (produto != null)
            {
                produto.Quantidade += quantidade;
                Console.WriteLine("Entrada realizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Produto não encontrado!");
            }
        }

        // 📤 Saída de estoque
        public void Saida(int id, int quantidade)
        {
            var produto = produtos.Find(p => p.Id == id);

            if (produto != null)
            {
                if (produto.Quantidade >= quantidade)
                {
                    produto.Quantidade -= quantidade;
                    Console.WriteLine("Saída realizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Estoque insuficiente!");
                }
            }
            else
            {
                Console.WriteLine("Produto não encontrado!");
            }
        }
    }
}