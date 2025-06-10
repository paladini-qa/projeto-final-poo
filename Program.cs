using ModaOrientadaAObjetos.Controllers;

namespace ModaOrientadaAObjetos
{
  class Program
  {
    static void Main(string[] args)
    {
      RoupaController controller = new RoupaController();
      controller.Iniciar();
    }
  }
}