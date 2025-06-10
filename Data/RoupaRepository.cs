using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using ModaOrientadaAObjetos.Models;

namespace ModaOrientadaAObjetos.Data
{
  public class RoupaRepository : IRoupaRepository
  {
    private readonly string connectionString;

    public RoupaRepository()
    {
      connectionString = "Data Source=loja.db";
      InitializeDatabase();
    }

    private void InitializeDatabase()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Roupas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Marca TEXT NOT NULL,
                    Tamanho TEXT NOT NULL,
                    Preco REAL NOT NULL
                )";
      command.ExecuteNonQuery();
    }

    public List<Roupa> GetAll()
    {
      var roupas = new List<Roupa>();

      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "SELECT Id, Nome, Marca, Tamanho, Preco FROM Roupas ORDER BY Id";

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
        roupas.Add(roupa);
      }

      return roupas;
    }

    public Roupa GetById(int id)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "SELECT Id, Nome, Marca, Tamanho, Preco FROM Roupas WHERE Id = @id";
      command.Parameters.AddWithValue("@id", id);

      using var reader = command.ExecuteReader();
      if (reader.Read())
      {
        return new Roupa(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetDouble(4)
        );
      }

      return null;
    }

    public void Add(Roupa roupa)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                INSERT INTO Roupas (Nome, Marca, Tamanho, Preco) 
                VALUES (@nome, @marca, @tamanho, @preco)";

      command.Parameters.AddWithValue("@nome", roupa.Nome);
      command.Parameters.AddWithValue("@marca", roupa.Marca);
      command.Parameters.AddWithValue("@tamanho", roupa.Tamanho);
      command.Parameters.AddWithValue("@preco", roupa.Preco);

      command.ExecuteNonQuery();

      command.CommandText = "SELECT last_insert_rowid()";
      roupa.Id = Convert.ToInt32(command.ExecuteScalar());
    }

    public void Update(Roupa roupa)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
                UPDATE Roupas 
                SET Nome = @nome, Marca = @marca, Tamanho = @tamanho, Preco = @preco 
                WHERE Id = @id";

      command.Parameters.AddWithValue("@id", roupa.Id);
      command.Parameters.AddWithValue("@nome", roupa.Nome);
      command.Parameters.AddWithValue("@marca", roupa.Marca);
      command.Parameters.AddWithValue("@tamanho", roupa.Tamanho);
      command.Parameters.AddWithValue("@preco", roupa.Preco);

      command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "DELETE FROM Roupas WHERE Id = @id";
      command.Parameters.AddWithValue("@id", id);

      command.ExecuteNonQuery();
    }

    public int GetNextId()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "SELECT COALESCE(MAX(Id), 0) + 1 FROM Roupas";

      var result = command.ExecuteScalar();
      return Convert.ToInt32(result);
    }
  }
}