﻿using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Ventas;
using DGP.Entities;
using DGP.DataAccess.Ventas;
using DBHelper;

namespace DGP.BusinessLogic.Ventas
{
    public class BLDocumentoPago
    {
        #region "Métodos de BLLdocumentoPago"

        public List<BEDocumento> Listar(int codClientProv,DateTime? FechaInicio, DateTime? FechaFinal)
        {
            try
            {
                return new DADocumentoPago().ListarDocumento(codClientProv, FechaInicio, FechaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BEAmortizacionVenta> ListarDetalle(int idDocumento)
        {
            try
            {
                return new DADocumentoPago().ListarDetalle(idDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool EliminarCabecera(BEDocumento beDocumento)
        {
            try
            {
                return new DADocumentoPago().EliminarCabeceraDocumento(beDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCabecera(BEDocumento beDocumento)
        {
            try
            {
                return new DADocumentoPago().ActualizarCabeceraDocumento(beDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCabecera(BEDocumento beDocumento)
        {
            try
            {
                return new DADocumentoPago().InsertarCabeceraDocumento(beDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarAmortizacionVenta(BEAmortizacionVenta amort)
        {
            try
            {
                return new DADocumentoPago().InsertarAmortizacionVenta(amort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarAmortizacionVenta(BEAmortizacionVenta amort)
        {
            try
            {
                return new DADocumentoPago().ActualizarAmortizacionVenta(amort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarAmortizacionVenta(BEAmortizacionVenta amort)
        {
            try
            {
                return new DADocumentoPago().EliminarAmortizacionVenta(amort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BEVenta> ListarVentaXCliente(int idCliente)
        {
            try
            {
                return new DADocumentoPago().ListarVentasXCliente(idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
