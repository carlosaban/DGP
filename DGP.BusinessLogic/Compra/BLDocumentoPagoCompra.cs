using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities;
using DGP.DataAccess.Compra;
using DBHelper;
using DGP.Entities.Compras;

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

        public bool InsertarCabecera(BEDocumentoCompra beDocumentoCompra)
        {
            try
            {
                return new DADocumentoPagoCompra().InsertarCabeceraDocumento(beDocumentoCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        #endregion

    }
}
