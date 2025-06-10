namespace ModaOrientadaAObjetos.Models
{
  public class Roupa
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Marca { get; set; }
    public string Tamanho { get; set; }
    public double Preco { get; set; }

    public Roupa(int id, string nome, string marca, string tamanho, double preco)
    {
      Id = id;
      Nome = nome;
      Marca = marca;
      Tamanho = tamanho;
      Preco = preco;
    }
  }
}