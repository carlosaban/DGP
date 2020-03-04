using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities;
using DGP.DataAccess.Compra;
using DBHelper;
using DGP.Entities.Compras;
using DGP.BusinessLogic.Ventas;
using DGP.Entities.Ventas;

namespace DGP.BusinessLogic.Compra
{
    public class BLDocumentoPagoCompra
    {
        #region "Métodos de BLLdocumentoPago"

        public List<BEDocumentoCompra> Listar(int codClientProv,DateTime? FechaInicio, DateTime? FechaFinal)
        {
            try
            {
                return new DADocumentoPagoCompra().ListarDocumento(codClientProv, FechaInicio, FechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EliminarCabecera(BEDocumentoCompra beDocumentoCompra)
        {
            try
            {
                return new DADocumentoPagoCompra().EliminarCabeceraDocumento(beDocumentoCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCabecera(BEDocumentoCompra beDocumentoCompra)
        {
            try
            {
                return new DADocumentoPagoCompra().ActualizarCabeceraDocumento(beDocumentoCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public bool InsertarCabecera(BEDocumentoCompra beDocumentoCompra){

            return InsertarCabecera(beDocumentoCompra , null);
        }
        public bool InsertarCabecera(BEDocumentoCompra beDocumentoCompra, DatabaseHelper pDatabaseHelper)
        {

            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;
            bool bOk = true; 
            try
            {
                bOk = bOk && new DADocumentoPagoCompra().InsertarCabeceraDocumento(beDocumentoCompra, oDatabaseHelper);
                bOk = bOk && (new BLVenta().CrearDocumentoDebito(beDocumentoCompra.Monto ,  beDocumentoCompra.BEUsuarioLogin, beDocumentoCompra.Cliente.IdCliente, beDocumentoCompra.Fecha, beDocumentoCompra.IdPersonal, BEVenta.ID_NOTA_DEBITO_AMR_COMPRA) != null);




                return bOk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            
            }
        }

    
        #endregion

    }
}
