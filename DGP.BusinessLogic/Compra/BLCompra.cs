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
        public List<BECompra> ListarCompraCliente(BECompra pBECompra)
        {
            try
            {
                return new DACompra().ListarCompraCliente(pBECompra);
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

        public List<BECompra> ListarCompra(int pIdCompra, int pIdCaja, DatabaseHelper pDatabaseHelper)
        {
            try
            {
                return new DACompra().ListarCompra(pIdCompra, pIdCaja, pDatabaseHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
