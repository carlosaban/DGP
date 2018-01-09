using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Seguridad;

namespace DGP.Entities.Compras
{
    public class BELineaCompra
    {
        private int mIdLineaCompra;
        private decimal? mPesoJava;
        private decimal mPesoBruto;
        private decimal mPesoTara;
        private decimal mPesoNeto;
        private int mCantidadJavas;
        private string mEsDevolucion;
        private string mEsPesoTaraEditado;
        private decimal mTaraEditada;
        private string mObservacion;
        private string mIdEstado;
        private int mIdCompra;
        private eAccion mAccion = eAccion.Agregar;
        private BEPersonal mBEUsuarioLogin;

        public BELineaCompra()
        {
            mIdLineaCompra = 0;
            mPesoJava = 0;
            mPesoBruto = 0;
            mPesoTara = 0;
            mPesoNeto = 0;
            mCantidadJavas = 0;
            mEsDevolucion = "N";
            mEsPesoTaraEditado = "N";
            mTaraEditada = 0;
            mObservacion = string.Empty;
            mIdEstado = string.Empty;
            mIdCompra = 0;
            mBEUsuarioLogin = new BEPersonal();
        }

        public int IdLineaCompra
        {
            get { return mIdLineaCompra; }
            set { mIdLineaCompra = value; }
        }

        public decimal PesoJava
        {
            get { return mPesoJava.Value; }
            set { mPesoJava = value; }
        }

        public decimal PesoBruto
        {
            get { return mPesoBruto; }
            set { mPesoBruto = value; }
        }

        public decimal PesoTara
        {
            get { return mPesoTara; }
            set { mPesoTara = value; }
        }

        public decimal PesoNeto
        {
            get { return mPesoNeto; }
            set { mPesoNeto = value; }
        }

        public int CantidadJavas
        {
            get { return mCantidadJavas; }
            set { mCantidadJavas = value; }
        }

        public string EsDevolucion
        {
            get { return mEsDevolucion; }
            set { mEsDevolucion = value; }
        }

        public string EsPesoTaraEditado
        {
            get { return mEsPesoTaraEditado; }
            set { mEsPesoTaraEditado = value; }
        }

        public decimal TaraEditada
        {
            get { return mTaraEditada; }
            set { mTaraEditada = value; }
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

        public eAccion Accion
        {
            get { return mAccion; }
            set { mAccion = value; }
        }

        public BEPersonal BEUsuarioLogin
        {
            get { return mBEUsuarioLogin; }
            set { mBEUsuarioLogin = value; }
        }



        private string mFlagJava = "N";

        public string FlagJava
        {
            get { return mFlagJava; }
            set { mFlagJava = value; }
        }

        private int mSecuencial = 0;

        public int Secuencial
        {
            get { return mSecuencial; }
            set { mSecuencial = value; }
        }

        private int mUnidades;

        public int Unidades
        {
            get { return mUnidades; }
            set { mUnidades = value; }
        }
    }
}
