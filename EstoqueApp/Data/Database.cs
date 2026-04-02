using Microsoft.Data.Sqlite;

namespace EstoqueApp.Data
{
    class Database
    {
        private string connectionString = "Data Source=estoque.db";

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }

        public void CriarTabela()
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Produtos (
                    Id INTEGER PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Quantidade INTEGER NOT NULL,
                    Preco REAL NOT NULL
                );
            ";

            command.ExecuteNonQuery();
        }
    }
}