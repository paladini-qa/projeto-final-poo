using System.Collections.Generic;
using ModaOrientadaAObjetos.Models;

namespace ModaOrientadaAObjetos.Data
{
  public interface IRoupaRepository
  {
    List<Roupa> GetAll();
    Roupa GetById(int id);
    void Add(Roupa roupa);
    void Update(Roupa roupa);
    void Delete(int id);
    int GetNextId();
  }
}