using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using ModaOrientadaAObjetos.Models;

namespace ModaOrientadaAObjetos.Data
{
  public class CarrinhoRepository : ICarrinhoRepository
  {
    private readonly string connectionString;
    private readonly IRoupaRepository roupaRepository;

    public CarrinhoRepository(IRoupaRepository roupaRepository)
    {
      connectionString = "Data Source=loja.db";
      this.roupaRepository = roupaRepository;
      InitializeDatabase();
    }

    private void InitializeDatabase()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                CREATE TABLE IF NOT EXISTS CarrinhoItens (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoupaId INTEGER NOT NULL,
                    FOREIGN KEY (RoupaId) REFERENCES Roupas(Id)
                );

                CREATE TABLE IF NOT EXISTS Pedidos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    DataPedido TEXT NOT NULL,
                    Total REAL NOT NULL
                );

                CREATE TABLE IF NOT EXISTS PedidoItens (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PedidoId INTEGER NOT NULL,
                    RoupaId INTEGER NOT NULL,
                    NomeRoupa TEXT NOT NULL,
                    MarcaRoupa TEXT NOT NULL,
                    TamanhoRoupa TEXT NOT NULL,
                    PrecoRoupa REAL NOT NULL,
                    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id)
                )";
      command.ExecuteNonQuery();
    }

    public List<Roupa> GetItensCarrinho()
    {
      var itens = new List<Roupa>();

      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                SELECT r.Id, r.Nome, r.Marca, r.Tamanho, r.Preco 
                FROM CarrinhoItens c 
                INNER JOIN Roupas r ON c.RoupaId = r.Id 
                ORDER BY c.Id";

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var roupa = new Roupa(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetDouble(4)
        );
        itens.Add(roupa);
      }

      return itens;
    }

    public void AdicionarItemCarrinho(int roupaId)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "INSERT INTO CarrinhoItens (RoupaId) VALUES (@roupaId)";
      command.Parameters.AddWithValue("@roupaId", roupaId);

      command.ExecuteNonQuery();
    }

    public void LimparCarrinho()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "DELETE FROM CarrinhoItens";
      command.ExecuteNonQuery();
    }

    public void SalvarPedido(List<Roupa> itens, double total)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      using var transaction = connection.BeginTransaction();

      try
      {
        var commandPedido = connection.CreateCommand();
        commandPedido.CommandText = @"
                    INSERT INTO Pedidos (DataPedido, Total) 
                    VALUES (@data, @total)";
        commandPedido.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        commandPedido.Parameters.AddWithValue("@total", total);
        commandPedido.ExecuteNonQuery();

        commandPedido.CommandText = "SELECT last_insert_rowid()";
        var pedidoId = Convert.ToInt32(commandPedido.ExecuteScalar());

        foreach (var item in itens)
        {
          var commandItem = connection.CreateCommand();
          commandItem.CommandText = @"
                        INSERT INTO PedidoItens (PedidoId, RoupaId, NomeRoupa, MarcaRoupa, TamanhoRoupa, PrecoRoupa) 
                        VALUES (@pedidoId, @roupaId, @nome, @marca, @tamanho, @preco)";

          commandItem.Parameters.AddWithValue("@pedidoId", pedidoId);
          commandItem.Parameters.AddWithValue("@roupaId", item.Id);
          commandItem.Parameters.AddWithValue("@nome", item.Nome);
          commandItem.Parameters.AddWithValue("@marca", item.Marca);
          commandItem.Parameters.AddWithValue("@tamanho", item.Tamanho);
          commandItem.Parameters.AddWithValue("@preco", item.Preco);

          commandItem.ExecuteNonQuery();
        }

        transaction.Commit();
      }
      catch
      {
        transaction.Rollback();
        throw;
      }
    }

    public List<string> GetHistoricoPedidos()
    {
      var historico = new List<string>();

      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                SELECT p.Id, p.DataPedido, p.Total,
                       GROUP_CONCAT(pi.NomeRoupa || ' (' || pi.MarcaRoupa || ')') as Itens
                FROM Pedidos p 
                LEFT JOIN PedidoItens pi ON p.Id = pi.PedidoId 
                GROUP BY p.Id, p.DataPedido, p.Total 
                ORDER BY p.DataPedido DESC";

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        var pedidoInfo = $"Pedido #{reader.GetInt32(0)} - {reader.GetString(1)} - R$ {reader.GetDouble(2):F2}";
        if (!reader.IsDBNull(3))
        {
          pedidoInfo += $" - Itens: {reader.GetString(3)}";
        }
        historico.Add(pedidoInfo);
      }

      return historico;
    }
  }
}