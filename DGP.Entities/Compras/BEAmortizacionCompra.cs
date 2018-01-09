using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Seguridad;

namespace DGP.Entities.Compras
{
    public class BEAmortizacionCompra
    {
        public const string ESTADO_REGISTRADO = "REG";
        public const string ESTADO_ELIMINADO = "ELM";
        public const string FORMAPAGO_EFECTIVO = "EFE";
        public const string FORMAPAGO_NOEFECTIVO = "NEF";

        public const string TIPOAMORTIZACION_AMORTIZACION = "AMR";
        public const string TIPOAMORTIZACION_ADELANTO = "ADL";
        public const string TIPOAMORTIZACION_VUELTO = "VLT";
        public const string TIPOAMORTIZACION_REDONDEO = "RDN";

        private int mIdAmortizacionCompra;
        private decimal mMonto;
        private string mNroDocumento;
        private string mIdFormaPago;
        private DateTime mFechaPago;
        private string mIdTipoAmortizacion;
        private string mObservacion;
        private string mIdEstado;
        private int mIdCompra;
        private int mIdCliente;
        private int mIdPersonal;
        private BEPersonal mBEUsuarioLogin;

        public BEAmortizacionCompra()
        {
            mIdAmortizacionCompra = 0;
            mMonto = 0;
            mNroDocumento = string.Empty;
            mIdFormaPago = string.Empty;
            mFechaPago = DateTime.MinValue.Date;
            mIdTipoAmortizacion = string.Empty;
            mObservacion = string.Empty;
            mIdEstado = string.Empty;
            mIdCompra = 0;
            mIdCliente = 0;
            mIdPersonal = 0;
            mBEUsuarioLogin = new BEPersonal();
        }

        public int IdAmortizacionCompra
        {
            get { return mIdAmortizacionCompra;}
            set { mIdAmortizacionCompra = value; }
        }

        public decimal Monto
        {
            get { return mMonto; }
            set { mMonto = value; }
        }

        public string NroDocumento
        {
            get { return mNroDocumento; }
            set { mNroDocumento = value; }
        }

        public string IdFormaPago
        {
            get { return mIdFormaPago; }
            set { mIdFormaPago = value; }
        }

        public DateTime FechaPago
        {
            get { return mFechaPago; }
            set { mFechaPago = value; }
        }

        public string IdTipoAmortizacion
        {
            get { return mIdTipoAmortizacion; }
            set { mIdTipoAmortizacion = value; }
        }

        public string Observacion
        {
            get { return mObservacion; }
            set { mObservacion = value; }
        }

        public string IdEstado
        {
            get { return mIdEstado; }
            set { mIdEstado = value; }
        }

        public int IdCompra
        {
            get { return mIdCompra; }
            set { mIdCompra = value; }
        }

        public int IdCliente
        {
            get { return mIdCliente; }
            set { mIdCliente = value; }
        }

        public int IdPersonal
        {
            get { return mIdPersonal; }
            set { mIdPersonal = value; }
        }

        public BEPersonal BEUsuarioLogin
        {
            get { return mBEUsuarioLogin; }
            set { mBEUsuarioLogin = value; }
        }

        private BECaja mCaja;
        public BECaja Caja
        {
            get { return mCaja; }
            set { mCaja = value; }
        }

        private bool _CancelarCompra = false;
        public bool CancelarCompra
        {
            get { return _CancelarCompra; }
            set { _CancelarCompra = value; }
        }
    }
}
