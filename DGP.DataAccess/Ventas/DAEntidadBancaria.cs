using System;
using System.Collections.Generic;
using System.Text;
using DGP.Entities.Ventas;

namespace DGP.DataAccess.Ventas
{
     public class DAEntidadBancaria
    {
         public List<BEEntidadBancaria> Listar(BEEntidadBancaria bancoFiltro)
         { //poner acceso a datos
             List<BEEntidadBancaria> result = new List<BEEntidadBancaria>();

             result.Add(new BEEntidadBancaria() {IdEntidadBancaria = 1 ,Nombre= "Banco Interbank" , Siglas= "IBK" });
             result.Add(new BEEntidadBancaria() { IdEntidadBancaria = 2, Nombre = "Banco de Credito", Siglas = "BCP" });

             result.Add(new BEEntidadBancaria() { IdEntidadBancaria = 3, Nombre = "Banco BANBIF", Siglas = "BANBIF" });

             result.Add(new BEEntidadBancaria() { IdEntidadBancaria = 4, Nombre = "Banco Continental", Siglas = "BBVA" });
             return result;
         }
    }
}
