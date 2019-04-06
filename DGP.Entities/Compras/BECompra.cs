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

        public const string TIPO_DOC_FACTURA = "FAC";
        public const string TIPO_DOC_BOLETA= "BOL";

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
        public int IdCaja { get; set; }
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdPersonal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public BEPersonal Auditoria { get; set; }
        public int TotalUnidades { get; set; }

        public BEPersonal BEUsuarioLogin { get; set; }
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
        public bool EsSobrante { get; set; }
        public int TotalJabas { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoTotalBD { get; set; }

        public BECompra()
        {
            this.MontoSubTotal = 0;
            this.MontoTotal = 0;
        
        
        }
        public int IdNotaCreditoCompra { get; set; }
    }

    
}
