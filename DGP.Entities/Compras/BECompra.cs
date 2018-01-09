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

        private int mIdCompra;
        private string mNombreCompra;
        private string mIdTipoDocumentoCompra;
        private string mTipoDocumentoCompra;
        private string mNumeroDocumento;
        private decimal mTotalPesoBruto;
        private decimal mTotalPesoTara;
        private decimal mTotalPesoNeto;
        private decimal mPrecio;
        private decimal mMontoSubTotal;
        private decimal mMontoIGV;
        private decimal mMontoTotal;
        private eCompraEsSobrante mEsSobrante;
        private string mTieneDevolucion;
        private decimal mTotalDevolucion;
        private decimal mTotalAmortizacion;
        private decimal mTotalSaldo;
        private string mObservacion;
        private string mClienteEventual;
        private string mIdEstado;
        private int mIdCaja;
        private int mIdEmpresa;
        private string mEmpresa;
        private int mIdProducto;
        private string mProducto;
        private int mIdCliente;
        private string mCliente;
        private BEPersonal mBEUsuarioLogin;
        private string mFechaCreacion;

        public BECompra()
        {
            mIdCompra = 0;
            mNombreCompra = string.Empty;
            mIdTipoDocumentoCompra = string.Empty;
            mTipoDocumentoCompra = string.Empty;
            mNumeroDocumento = string.Empty;
            mTotalPesoBruto = 0;
            mTotalPesoTara = 0;
            mTotalPesoNeto = 0;
            mPrecio = 0;
            mMontoSubTotal = 0;
            mMontoIGV = 0;
            mMontoTotal = 0;
            mEsSobrante = eCompraEsSobrante.No;
            mTieneDevolucion = "N";
            mTotalDevolucion = 0;
            mTotalAmortizacion = 0;
            mTotalSaldo = 0;
            mObservacion = string.Empty;
            mClienteEventual = string.Empty;
            mIdEstado = string.Empty;
            mIdCaja = 0;
            mIdEmpresa = 0;
            mEmpresa = string.Empty;
            mIdProducto = 0;
            mProducto = string.Empty;
            mIdCliente = 0;
            mFechaCreacion = string.Empty;
            mBEUsuarioLogin = new BEPersonal();
        }

        public BECompra(int pIdCompra, string pNombreCompra)
        {
            mIdCompra = pIdCompra;
            mNombreCompra = pNombreCompra;
        }

        public string strFilterIds { get; set; }

        public int IdCompra
        {
            get { return mIdCompra; }
            set { mIdCompra = value; }
        }

        public string NombreCompra
        {
            get { return mNombreCompra; }
            set { mNombreCompra = value; }
        }

        public string IdTipoDocumentoCompra
        {
            get { return mIdTipoDocumentoCompra; }
            set { mIdTipoDocumentoCompra = value; }
        }

        public string TipoDocumentoCompra
        {
            get { return mTipoDocumentoCompra; }
            set { mTipoDocumentoCompra = value; }
        }

        public string NumeroDocumento
        {
            get { return mNumeroDocumento; }
            set { mNumeroDocumento = value; }
        }

        public decimal Precio
        {
            get { return mPrecio; }
            set { mPrecio = value; }
        }

        public decimal MontoSubTotal
        {
            get { return mMontoSubTotal; }
            set { mMontoSubTotal = value; }
        }

        public decimal MontoIGV
        {
            get { return mMontoIGV; }
            set { mMontoIGV = value; }
        }

        public decimal MontoTotal
        {
            get { return mMontoTotal; }
            set { mMontoTotal = value; }
        }

        public eCompraEsSobrante EsSobrante
        {
            get { return mEsSobrante; }
            set { mEsSobrante = value; }
        }

        public int IdEsSobrante
        {
            get { return mEsSobrante.GetHashCode(); }
        }

        public decimal TotalPesoBruto
        {
            get { return mTotalPesoBruto; }
            set { mTotalPesoBruto = value; }
        }

        public decimal TotalPesoTara
        {
            get { return mTotalPesoTara; }
            set { mTotalPesoTara = value; }
        }

        public decimal TotalPesoNeto
        {
            get { return mTotalPesoNeto; }
            set { mTotalPesoNeto = value; }
        }

        public string TieneDevolucion
        {
            get { return mTieneDevolucion; }
            set { mTieneDevolucion = value; }
        }

        public decimal TotalDevolucion
        {
            get { return mTotalDevolucion; }
            set { mTotalDevolucion = value; }
        }

        public decimal TotalAmortizacion
        {
            get { return mTotalAmortizacion; }
            set { mTotalAmortizacion = value; }
        }

        public decimal TotalSaldo
        {
            get { return mTotalSaldo; }
            set { mTotalSaldo = value; }
        }

        public string Observacion
        {
            get { return mObservacion; }
            set { mObservacion = value; }
        }

        public string ClienteEventual
        {
            get { return mClienteEventual; }
            set { mClienteEventual = value; }
        }

        public string IdEstado
        {
            get { return mIdEstado; }
            set { mIdEstado = value; }
        }

        public int IdCaja
        {
            get { return mIdCaja; }
            set { mIdCaja = value; }
        }

        public int IdEmpresa
        {
            get { return mIdEmpresa; }
            set { mIdEmpresa = value; }
        }

        public string Empresa
        {
            get { return mEmpresa; }
            set { mEmpresa = value; }
        }

        public int IdProducto
        {
            get { return mIdProducto; }
            set { mIdProducto = value; }
        }

        public string Producto
        {
            get { return mProducto; }
            set { mProducto = value; }
        }

        public int IdCliente
        {
            get { return mIdCliente; }
            set { mIdCliente = value; }
        }

        public string Cliente
        {
            get { return mCliente; }
            set { mCliente = value; }
        }

        private List<BELineaCompra> mListaLineaCompra = new List<BELineaCompra>();
        public List<BELineaCompra> ListaLineaCompra
        {
            get { return mListaLineaCompra; }
            set { mListaLineaCompra = value; }
        }

        private List<BELineaCompra> mListaAmortizacion = new List<BELineaCompra>();
        public List<BELineaCompra> ListaAmortizacion
        {
            get { return mListaAmortizacion; }
            set { mListaAmortizacion = value; }
        }

        public string FechaCreacion
        {
            get { return mFechaCreacion; }
            set { mFechaCreacion = value; }
        }

        public BEPersonal BEUsuarioLogin
        {
            get { return mBEUsuarioLogin; }
            set { mBEUsuarioLogin = value; }
        }

        private DateTime mFechaInicio = DateTime.Now;
        public DateTime FechaInicio
        {
            get { return mFechaInicio; }
            set { mFechaInicio = value; }
        }

        private DateTime mFechaFin = DateTime.Now;
        public DateTime FechaFin
        {
            get { return mFechaFin; }
            set { mFechaFin = value; }
        }

        private BEAmortizacionCompra _BEAmortizacionCompra;
        public BEAmortizacionCompra BEAmortizacionCompra
        {
            get { return _BEAmortizacionCompra; }
            set { _BEAmortizacionCompra = value; }
        }

        private int _TotalUnidades;
        public int TotalUnidades
        {

            get { return _TotalUnidades; }
            set { _TotalUnidades = value; }
        }

        public bool TienePrecioVariable { get; set; }
    }
}
