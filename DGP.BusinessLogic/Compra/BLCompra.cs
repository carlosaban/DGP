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

        public BLCompra(BECompra beCompra)
        {

            this.BECompra = beCompra;
        
        }
        
        public bool ValidarCompra(out string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }
        public bool ValidarLineaCompra(out string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }
         public bool ValidarLineaDevolucion(out string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }

         public bool addLineaCompra( BELineaCompra beLineaCompra ,  out string mensaje)
         {
             if ( ValidarLineaCompra(out mensaje) ) this.BECompra.ListaLineaCompra.Add(beLineaCompra);

             return (mensaje == string.Empty);
         
         
         }
         public bool addLineaDevolucion(BELineaCompra beLineaCompra, out string mensaje)
         {
             if (ValidarLineaDevolucion(out mensaje)) this.BECompra.ListaDevolucion.Add(beLineaCompra);

             return (mensaje == string.Empty);
         }

        
        public bool Grabar ( out string mensaje) {
            bool bOK = false;
            if (!this.ValidarCompra(out mensaje)) return false;

            if (BECompra.IdCompra == 0) bOK = new DACompra().Insertar(this.BECompra, out mensaje);
            else bOK = new DACompra().Actualizar(this.BECompra, out mensaje);


            return (mensaje == string.Empty);
        
        }
        public  bool Insertar(  out string mensaje)
        {
            try
            {
                return new DACompra().Insertar(this.BECompra, out mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Actualizar(out string mensaje)
        {
            try
            {
                return new DACompra().Actualizar(this.BECompra, out mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static  List<BECompra> Listar(BECompraFilter pBECompra)
        {
            try
            {
                return new DACompra().Listar(pBECompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public BECompra ObtenerCompra(int pIdCompra)
        {
            try
            {
                return new DACompra().ObtenerCompra(pIdCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        //public List<BECompra> ListarCompra(int pIdCompra, int pIdCaja, DatabaseHelper pDatabaseHelper)
        //{
        //    try
        //    {
        //        return new DACompra().ListarCompra(pIdCompra, pIdCaja, pDatabaseHelper);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /*public List<BECompra> ListarVenta(int pIdVenta, int pIdCaja, int pIdZona, int pIdProducto, int pIdCliente)
        {
            try
            {
                //return new DACompra().ListarCompra(pIdVenta, pIdCaja, pIdZona, pIdProducto, pIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
    }
}
