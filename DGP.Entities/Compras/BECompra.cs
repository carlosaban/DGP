using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Seguridad;

namespace DGP.Entities.Compras
{
    public class BECompra
    {
        public const string REGISTRADO = "REG";
        public const string CONGELADO = "CON";
        public const string ANULADO = "ANL";
        public const string CANCELADO = "CAN";
        public const int UNIDAD_JAVA = 8;

        
        public BECompra()
        { //hay q revisar si es necesario

          //  mBEUsuarioLogin = new BEPersonal();
            Proveedor = new BEClienteProveedor();
        }


        public string strFilterIds { get; set; }
        public int IdCompra { get; set; }
        public string IdTipoDocumentoCompra { get; set; }
        public string  NumeroDocumento { get; set; }
        public decimal Precio { get; set; }
        public decimal MontoSubTotal { get; set; }
        public decimal MontoIGV { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal TotalPesoBruto { get; set; }
        public decimal TotalPesoTara { get; set; }
        public decimal TotalPesoNeto { get; set; }
        public decimal TotalDevolucion { get; set; }
        public decimal TotalAmortizacion { get; set; }
        public decimal TotalSaldo { get; set; }
        public string Observacion { get; set; }
        public string IdEstado { get; set; }
        public BECaja beCaja { get; set; }
        public BEEmpresa BEEmpresa { get; set; }
        public BEProducto BEProducto { get; set; }
        public BEClienteProveedor Proveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public BEPersonal BEUsuarioLogin { get; set; }
        public int TotalUnidades { get; set; }

        public DateTime FechaCompra
        {
            get
            {
                if (beCaja != null) return beCaja.Fecha;
                else beCaja = new BECaja();
                return beCaja.Fecha;


            }
        }
        private List<BELineaCompra> mListaLineaCompra = new List<BELineaCompra>();
        public List<BELineaCompra> ListaLineaCompra
        {
            get { return mListaLineaCompra; }
            set { mListaLineaCompra = value; }
        }

        private List<BELineaCompra> mListaAmortizacion = new List<BELineaCompra>();
        public List<BELineaCompra> ListaDevolucion
        {
            get { return mListaAmortizacion; }
            set { mListaAmortizacion = value; }
        }
        private List<BEAmortizacionCompra> _BEAmortizacionCompra = new List<BEAmortizacionCompra>();
        public List<BEAmortizacionCompra> BEAmortizacionCompra
        {
            get { return _BEAmortizacionCompra; }
            set { _BEAmortizacionCompra = value; }
        }

        
        
    }
    public class BECompraFilter : BECompra
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    
    
    
    }
}
