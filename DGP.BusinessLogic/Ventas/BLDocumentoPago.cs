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
        /*
        public dsLineaVenta ListarDS(BELineaVenta pBELineaVenta)
        {
            try
            {
                return new DALineaVenta().ListarDS(pBELineaVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RegistrarLineaVentaMantenimientoDependiente(BEVenta pBEVenta, dsLineaVenta.DTLineaVentaDataTable pDTLineasVentas, dsLineaVenta.DTLineaVentaDataTable pDTEliminados, bool HayCambioPrecio)
        {
            DatabaseHelper dbh = new DatabaseHelper();
            int intResultado = 0;
            try
            {
                dbh.BeginTransaction();
                int modificados = new DAVenta().InsertarVentaFinal(pBEVenta, dbh);
                // Eliminar los registros
                int intEliminados = 0;
                foreach (dsLineaVenta.DTLineaVentaRow vRow in pDTEliminados.Rows)
                {
                    intEliminados += new DALineaVenta().EliminarLineaVentaDependiente(vRow, dbh);
                }
                intResultado += (intEliminados == pDTEliminados.Rows.Count) ? 1 : 0;
                // Registrar las Lineas de Venta
                int intContadorLV = 0;
                int intContadorAll = pDTLineasVentas.Rows.Count;
                foreach (dsLineaVenta.DTLineaVentaRow vRow in pDTLineasVentas.Rows)
                {
                    if (vRow.IdAccion == eAccion.Agregar.GetHashCode())
                    {
                        intContadorLV += new DALineaVenta().InsertarLineaVentaDependiente(vRow, pBEVenta.BEUsuarioLogin.IdPersonal, dbh);
                    }
                    else if (vRow.IdAccion == eAccion.Modificar.GetHashCode())
                    {
                        intContadorLV += new DALineaVenta().ModificarLineaVentaDependiente(vRow, pBEVenta.BEUsuarioLogin.IdPersonal, dbh);
                    }
                    else
                    {
                        intContadorAll--;
                    }
                }
                intResultado += (intContadorLV == intContadorAll) ? 1 : 0;

                //if (HayCambioPrecio) new DAAmortizacionVenta().AnularAmortizacionVenta(pBEVenta); //.ReaplicarAmortizacion(pBEVenta);

                // Insertar Venta Final
                intResultado += new DAVenta().InsertarVentaFinal(pBEVenta, dbh);
                //
                if (intResultado == 3)
                {
                    dbh.CommitTransaction();
                }
                else
                {
                    dbh.RollbackTransaction();
                }
                return intResultado;
            }
            catch (Exception ex)
            {
                dbh.RollbackTransaction();
                throw ex;
            }
            finally
            {
                dbh.Dispose();
            }
        }*/

        #endregion

    }
}
