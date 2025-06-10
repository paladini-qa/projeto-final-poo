using System.Collections.Generic;
using ModaOrientadaAObjetos.Models;

namespace ModaOrientadaAObjetos.Data
{
  public interface ICarrinhoRepository
  {
    List<Roupa> GetItensCarrinho();
    void AdicionarItemCarrinho(int roupaId);
    void LimparCarrinho();
    void SalvarPedido(List<Roupa> itens, double total);
    List<string> GetHistoricoPedidos();
  }
}