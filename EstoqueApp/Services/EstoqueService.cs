using Microsoft.Data.Sqlite;
using EstoqueApp.Models;
using EstoqueApp.Data;

namespace EstoqueApp.Services
{
    class EstoqueService
    {
        private Database db = new Database();

        // 
        public void AdicionarProduto(Produto produto)
        {
            using var connection = db.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Produtos (Id, Nome, Quantidade, Preco)
                VALUES ($id, $nome, $quantidade, $preco);
            ";

            command.Parameters.AddWithValue("$id", produto.Id);
            command.Parameters.AddWithValue("$nome", produto.Nome);
            command.Parameters.AddWithValue("$quantidade", produto.Quantidade);
            command.Parameters.AddWithValue("$preco", produto.Preco);

            try
            {
                command.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Produto salvo no banco!");
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Erro: já existe um produto com esse ID!");
                Console.ResetColor();
            }
        }

        // 
        public void ListarProdutos()
        {
            using var connection = db.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Produtos";

            using var reader = command.ExecuteReader();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== LISTA DE PRODUTOS =====\n");
            Console.ResetColor();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32(0)} | " +
                    $"Nome: {reader.GetString(1)} | " +
                    $"Qtd: {reader.GetInt32(2)} | " +
                    $"Preço: R$ {reader.GetDouble(3):F2}"
                );
            }
        }

        // 
        public void Entrada(int id, int quantidade)
        {
            using var connection = db.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Produtos
                SET Quantidade = Quantidade + $qtd
                WHERE Id = $id;
            ";

            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$qtd", quantidade);

            int linhas = command.ExecuteNonQuery();

            if (linhas > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Entrada realizada!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Produto não encontrado!");
            }

            Console.ResetColor();
        }

        // 
        public void Saida(int id, int quantidade)
        {
            using var connection = db.GetConnection();
            connection.Open();

            var check = connection.CreateCommand();
            check.CommandText = "SELECT Quantidade FROM Produtos WHERE Id = $id";
            check.Parameters.AddWithValue("$id", id);

            var result = check.ExecuteScalar();

            if (result == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Produto não encontrado!");
                Console.ResetColor();
                return;
            }

            int quantidadeAtual = Convert.ToInt32(result);

            if (quantidadeAtual < quantidade)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠ Estoque insuficiente!");
                Console.ResetColor();
                return;
            }

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Produtos
                SET Quantidade = Quantidade - $qtd
                WHERE Id = $id;
            ";

            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$qtd", quantidade);

            command.ExecuteNonQuery();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✔ Saída realizada!");
            Console.ResetColor();
        }

        // 🔍 BUSCAR PRODUTO
        public void BuscarProduto(string nome)
        {
            using var connection = db.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Produtos
                WHERE Nome LIKE $nome;
            ";

            command.Parameters.AddWithValue("$nome", "%" + nome + "%");

            using var reader = command.ExecuteReader();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== RESULTADOS DA BUSCA =====\n");
            Console.ResetColor();

            bool encontrou = false;

            while (reader.Read())
            {
                encontrou = true;

                Console.WriteLine(
                    $"ID: {reader.GetInt32(0)} | " +
                    $"Nome: {reader.GetString(1)} | " +
                    $"Qtd: {reader.GetInt32(2)} | " +
                    $"Preço: R$ {reader.GetDouble(3):F2}"
                );
            }

            if (!encontrou)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Nenhum produto encontrado.");
                Console.ResetColor();
            }
        }

        // 📊 NOVO — ESTOQUE BAIXO
        public void EstoqueBaixo(int limite)
        {
            using var connection = db.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Produtos
                WHERE Quantidade <= $limite;
            ";

            command.Parameters.AddWithValue("$limite", limite);

            using var reader = command.ExecuteReader();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n⚠ PRODUTOS COM ESTOQUE BAIXO:\n");
            Console.ResetColor();

            bool encontrou = false;

            while (reader.Read())
            {
                encontrou = true;

                Console.WriteLine(
                    $"{reader.GetString(1)} - Qtd: {reader.GetInt32(2)}"
                );
            }

            if (!encontrou)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Nenhum produto com estoque baixo.");
                Console.ResetColor();
            }
        }
    }
}