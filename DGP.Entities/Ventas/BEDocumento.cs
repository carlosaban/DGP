using System;
using System.Collections.Generic;
using System.Text;
using DGP.Entities.Seguridad; 

namespace DGP.Entities.Ventas
{
    public class BEDocumento
    {
        public const string TIPO_AMORTIZACION_AMR = "AMR";
        public const string TIPO_AMORTIZACION_VUELTO = "VLT";

        public const string TIPO_AMORTIZACION_REDONDEO = "RDO";


        public const string DOCUMENTO_ESTADO_REGISTRADO = "REG";
        public const string DOCUMENTO_ESTADO_ANULADO = "ANL";
        public string IdTipoDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { 
            get {
                decimal localMonto = 0;
                this.delleAmortizacion.ForEach(t => localMonto += t.Monto);
                return localMonto;
            } 
        }
        public string Estado { get; set;} 
        public bool EsEliminado { get; set; }
        public BEPersonal BEUsuarioLogin { get; set; }
        public List<BEAmortizacionVenta> delleAmortizacion { get; set; }
        public int IdDocumento { get; set; }

        public int IdCliente { get; set; }
        public int IdPersonal { get; set; }
        public BEDocumento()
        {
          
            delleAmortizacion = new List<BEAmortizacionVenta>();
            this.Estado = DOCUMENTO_ESTADO_REGISTRADO;
        
        }


    }
}
