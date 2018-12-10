using System;
using System.Collections.Generic;
using System.Text;

namespace DGP.Entities.Compras
{
    public class BECompraFilter : BECompra
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string TipoDocumentoCompra { get; set; }
        public string Empresa { get; set; }
        public string Proveedor { get; set; }
        public string Producto { get; set; }
    }
}
