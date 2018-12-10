using System;
using System.Collections.Generic;
using System.Text;

using DBHelper;
using DGP.Entities;
using DGP.Entities.Compras;
using DGP.DataAccess.Compra;

namespace DGP.BusinessLogic.Compra
{
    public class BLCompra
    {
        public BECompra BECompra;

        public BLCompra()
        {
            this.BECompra = new BECompra();
        }

        public BLCompra(int  IdCompra)
        {
            List<BECompraFilter> result = this.Listar(new BECompraFilter() { IdCompra = IdCompra });
            if (result.Count == 0)
            {
                this.BECompra = new BECompra();
            }
            else if (result.Count == 1)
            {
                this.BECompra = result[0];
            }
            else throw new Exception("Existe mas de una registro de compra. Validar  BD");
            
        }

        public BLCompra(BECompra beCompra)
        {
            this.BECompra = beCompra;
        }
        
        public bool ValidarCompra(ref string mensaje) {

            mensaje = string.Empty;
            
           

            return true;
        
        }
        public bool ValidarLineaCompra(ref string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }
         public bool ValidarLineaDevolucion(ref string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }

         public bool addLineaCompra( BELineaCompra beLineaCompra ,  ref string mensaje)
         {
             if ( ValidarLineaCompra(ref mensaje) ) this.BECompra.ListaLineaCompra.Add(beLineaCompra);

             return (mensaje == string.Empty);
         }

         public bool addLineaDevolucion(BELineaCompra beLineaCompra, ref string mensaje)
         {
             if (ValidarLineaDevolucion(ref mensaje)) this.BECompra.ListaDevolucion.Add(beLineaCompra);
             return (mensaje == string.Empty);
         }

        
        public bool Grabar ( ref string mensaje) {
            bool bOK = false;
            if (!this.ValidarCompra(ref mensaje)) return false;

            if (BECompra.IdCompra == 0) bOK = new DACompra().Insertar( ref mensaje , this.BECompra);
            else bOK = new DACompra().Actualizar( ref mensaje,  this.BECompra);


            return (mensaje == string.Empty);
        
        }
        public  bool Insertar(  ref string mensaje)
        {
            try
            {
                return new DACompra().Insertar(ref mensaje , this.BECompra );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Actualizar(ref string mensaje)
        {
            try
            {
                return new DACompra().Actualizar(ref mensaje , this.BECompra );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Eliminar(BECompra beCompra)
        {
            string mensaje = string.Empty;
            try
            {
                return new DACompra().Eliminar(ref mensaje, beCompra);
            }
            catch (Exception ex)
            {
                throw new Exception(mensaje);
            }
        }

        public List<BECompraFilter> Listar(BECompraFilter pBECompra)
        {
            try
            {
                List<BECompraFilter> resultado = new DACompra().Listar(pBECompra);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BECompra getCompra
        {
            get {


                return this.BECompra;
            }
        
        
        }
    }
}
