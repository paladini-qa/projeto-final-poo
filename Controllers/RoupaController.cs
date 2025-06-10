using System;
using System.Collections.Generic;
using System.Linq;
using ModaOrientadaAObjetos.Models;
using ModaOrientadaAObjetos.Views;
using ModaOrientadaAObjetos.Data;

namespace ModaOrientadaAObjetos.Controllers
{
  public class RoupaController
  {
    private readonly IRoupaRepository roupaRepository;
    private readonly ICarrinhoRepository carrinhoRepository;
    private readonly RoupaView view;

    public RoupaController()
    {
      roupaRepository = new RoupaRepository();
      carrinhoRepository = new CarrinhoRepository(roupaRepository);
      view = new RoupaView();
    }

    public void Iniciar()
    {
      int opcao;
      do
      {
        view.ExibirMenuPrincipal();
        string entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out opcao))
        {
          view.ExibirMensagem("Opcao invalida! Digite apenas numeros.");
          view.AguardarTecla();
          continue;
        }

        switch (opcao)
        {
          case 1:
            CadastrarRoupa();
            break;
          case 2:
            ListarRoupas();
            break;
          case 3:
            AdicionarRoupaAoCarrinho();
            break;
          case 4:
            VerCarrinho();
            break;
          case 5:
            FinalizarCompra();
            break;
          case 6:
            VerHistoricoPedidos();
            break;
          case 0:
            view.ExibirMensagem("Obrigado por visitar a Moda Orientada a Objetos!");
            break;
          default:
            view.ExibirMensagem("Opcao invalida! Digite um numero de 0 a 6.");
            view.AguardarTecla();
            break;
        }
      } while (opcao != 0);
    }

    public void CadastrarRoupa()
    {
      try
      {
        Roupa novaRoupa = view.SolicitarDadosParaCadastro();
        roupaRepository.Add(novaRoupa);
        view.ExibirMensagem("Roupa cadastrada com sucesso!");
      }
      catch (Exception)
      {
        view.ExibirMensagem("Erro ao cadastrar roupa. Tente novamente.");
      }
      view.AguardarTecla();
    }

    public void ListarRoupas()
    {
      var roupas = roupaRepository.GetAll();
      view.ListarTodasAsRoupas(roupas);
      view.AguardarTecla();
    }

    public void AdicionarRoupaAoCarrinho()
    {
      var roupas = roupaRepository.GetAll();
      if (roupas.Count == 0)
      {
        view.ExibirMensagem("Nenhuma roupa cadastrada. Cadastre roupas primeiro!");
        view.AguardarTecla();
        return;
      }

      view.ListarTodasAsRoupas(roupas);

      try
      {
        int id = view.SolicitarIdDaRoupa();
        Roupa roupaSelecionada = roupaRepository.GetById(id);

        if (roupaSelecionada != null)
        {
          carrinhoRepository.AdicionarItemCarrinho(id);
          view.ExibirMensagem("Roupa adicionada ao carrinho com sucesso!");
        }
        else
        {
          view.ExibirMensagem("Roupa nao encontrada. Verifique o ID digitado.");
        }
      }
      catch (Exception)
      {
        view.ExibirMensagem("Erro ao adicionar roupa ao carrinho. Tente novamente.");
      }
      view.AguardarTecla();
    }

    public void VerCarrinho()
    {
      var itens = carrinhoRepository.GetItensCarrinho();
      double total = itens.Sum(r => r.Preco);
      view.ExibirCarrinho(itens, total);
      view.AguardarTecla();
    }

    public void FinalizarCompra()
    {
      var itens = carrinhoRepository.GetItensCarrinho();
      if (itens.Count == 0)
      {
        view.ExibirMensagem("Carrinho vazio! Adicione itens antes de finalizar a compra.");
        view.AguardarTecla();
        return;
      }

      double total = itens.Sum(r => r.Preco);
      view.ExibirTelaPagamento(total);

      carrinhoRepository.SalvarPedido(itens, total);
      carrinhoRepository.LimparCarrinho();

      view.AguardarTecla();
    }

    public void VerHistoricoPedidos()
    {
      var historico = carrinhoRepository.GetHistoricoPedidos();
      view.ExibirHistoricoPedidos(historico);
      view.AguardarTecla();
    }
  }
}