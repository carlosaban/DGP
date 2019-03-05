using System;
using System.Collections.Generic;
using System.Text;
using DGP.Entities.Seguridad; 

namespace DGP.Entities.Compras
{
    public class BEDocumentoCompra
    {
        public const string TIPO_AMORTIZACION_AMR = "AMR";
        public const string TIPO_AMORTIZACION_NOTACREDITO = "NC";
        public string IdTipoDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string IdFormaPago { get; set; }
        public decimal Monto { get; set; }
        public string idEstado { get; set;}
 
        public BEPersonal BEUsuarioLogin { get; set; }
       // public List<BEAmortizacionVenta> delleAmortizacion { get; set; }
        public int IdDocumentoCompra { get; set; }

       // public int IdCliente { get; set; }
        public BEClienteProveedor Cliente { get; set; }
        public BEPersonal Personal { get; set; }
        public BEDocumentoCompra()
        {
          
            this.Cliente = new BEClienteProveedor();
            this.Personal = new BEPersonal();
           // this.Estado = DOCUMENTO_ESTADO_REGISTRADO;
        
        }

        public string IdBanco { get; set; }
        public string NumeroReciboPago { get; set; }
        public string ClienteNombre { get { return (Cliente != null) ? Cliente.Nombre : string.Empty; } }
        public string NumeroOperacion { get; set; }

        public int IdPersonal { get { return (this.Personal == null)?0: this.Personal.IdPersonal; } }

        public int IdCliente { get { return (this.Cliente == null) ? 0 : this.Cliente.IdCliente; } }
    }
}
