using System;
using System.Collections.Generic;
using System.Text;
using DGP.Entities.Ventas;
using DGP.DataAccess.Ventas;

namespace DGP.BusinessLogic.Ventas
{
    public class BLEntidadBancaria
    {
        public BEEntidadBancaria Banco { get; set; }

        public BLEntidadBancaria()
        {
            this.Banco = new BEEntidadBancaria();
        
        }
        public List<BEEntidadBancaria> Listar(BEEntidadBancaria bancoFiltro)
        {
            return (new DAEntidadBancaria()).Listar(bancoFiltro )  ;

        
        
        }
        

    }
}
