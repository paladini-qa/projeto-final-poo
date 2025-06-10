using System;
using System.Collections.Generic;
using ModaOrientadaAObjetos.Models;

namespace ModaOrientadaAObjetos.Views
{
  public class RoupaView
  {
    public void ExibirMenuPrincipal()
    {
      Console.WriteLine("=== MODA ORIENTADA A OBJETOS ===");
      Console.WriteLine("1. Cadastrar Roupa");
      Console.WriteLine("2. Listar Roupas");
      Console.WriteLine("3. Adicionar Roupa ao Carrinho");
      Console.WriteLine("4. Ver Carrinho");
      Console.WriteLine("5. Finalizar Compra");
      Console.WriteLine("6. Historico de Pedidos");
      Console.WriteLine("0. Sair");
      Console.Write("Escolha uma opcao: ");
    }

    public Roupa SolicitarDadosParaCadastro()
    {
      Console.WriteLine("\n=== CADASTRAR NOVA ROUPA ===");

      Console.Write("Digite o nome da roupa: ");
      string nome = Console.ReadLine();

      Console.Write("Digite a marca: ");
      string marca = Console.ReadLine();

      Console.Write("Digite o tamanho: ");
      string tamanho = Console.ReadLine();

      Console.Write("Digite o preco: R$ ");
      double preco = 0;
      while (!double.TryParse(Console.ReadLine(), out preco) || preco <= 0)
      {
        Console.Write("Preco invalido. Digite novamente: R$ ");
      }

      return new Roupa(0, nome, marca, tamanho, preco);
    }

    public void ListarTodasAsRoupas(List<Roupa> roupas)
    {
      Console.WriteLine("\n=== CATALOGO DE ROUPAS ===");

      if (roupas.Count == 0)
      {
        Console.WriteLine("Nenhuma roupa cadastrada.");
        return;
      }

      Console.WriteLine($"{"ID",-5} {"Nome",-20} {"Marca",-15} {"Tamanho",-10} {"Preco",-10}");
      Console.WriteLine(new string('-', 65));

      foreach (var roupa in roupas)
      {
        Console.WriteLine($"{roupa.Id,-5} {roupa.Nome,-20} {roupa.Marca,-15} {roupa.Tamanho,-10} R$ {roupa.Preco:F2}");
      }
    }

    public int SolicitarIdDaRoupa()
    {
      Console.Write("\nDigite o ID da roupa para adicionar ao carrinho: ");
      int id;
      while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
      {
        Console.Write("ID invalido. Digite novamente: ");
      }
      return id;
    }

    public void ExibirCarrinho(List<Roupa> itens, double total)
    {
      Console.WriteLine("\n=== MEU CARRINHO ===");

      if (itens.Count == 0)
      {
        Console.WriteLine("Carrinho vazio.");
        return;
      }

      Console.WriteLine($"{"Nome",-20} {"Marca",-15} {"Tamanho",-10} {"Preco",-10}");
      Console.WriteLine(new string('-', 60));

      foreach (var item in itens)
      {
        Console.WriteLine($"{item.Nome,-20} {item.Marca,-15} {item.Tamanho,-10} R$ {item.Preco:F2}");
      }

      Console.WriteLine(new string('-', 60));
      Console.WriteLine($"TOTAL: R$ {total:F2}");
    }

    public void ExibirTelaPagamento(double total)
    {
      Console.WriteLine("\n=== FINALIZAR COMPRA ===");
      Console.WriteLine($"Total da compra: R$ {total:F2}");
      Console.WriteLine("Processando pagamento...");
      Console.WriteLine("Pagamento realizado com sucesso!");
      Console.WriteLine("Obrigado pela preferencia!");
    }

    public void ExibirMensagem(string mensagem)
    {
      Console.WriteLine($"\n{mensagem}");
    }

    public void ExibirHistoricoPedidos(List<string> historico)
    {
      Console.WriteLine("\n=== HISTORICO DE PEDIDOS ===");

      if (historico.Count == 0)
      {
        Console.WriteLine("Nenhum pedido encontrado.");
        return;
      }

      foreach (var pedido in historico)
      {
        Console.WriteLine(pedido);
      }
    }

    public void AguardarTecla()
    {
      Console.WriteLine("\nPressione qualquer tecla para continuar...");
      Console.ReadKey();
      Console.Clear();
    }
  }
}