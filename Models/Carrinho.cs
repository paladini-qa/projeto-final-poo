using System.Collections.Generic;

namespace ModaOrientadaAObjetos.Models
{
  public class Carrinho
  {
    public List<Roupa> Itens { get; private set; }

    public Carrinho()
    {
      Itens = new List<Roupa>();
    }

    public void AdicionarItem(Roupa roupa)
    {
      Itens.Add(roupa);
    }

    public double CalcularTotal()
    {
      double total = 0;
      foreach (var roupa in Itens)
      {
        total += roupa.Preco;
      }
      return total;
    }

    public void LimparCarrinho()
    {
      Itens.Clear();
    }
  }
}