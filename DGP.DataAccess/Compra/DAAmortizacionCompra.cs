using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Compras;
using System.Data;
using DBHelper;

namespace DGP.DataAccess.Compra
{
    public class DAAmortizacionCompra
    {
        #region "Métodos de DAAmortizacionCompra"

        public int InsertarAdelantoCompra(BEAmortizacionCompra pBEAmortizacionCompra)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            int vResultado = 0;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@decMonto", pBEAmortizacionCompra.Monto);
                oDatabaseHelper.AddParameter("@varIdFormaPago", pBEAmortizacionCompra.IdFormaPago);
                oDatabaseHelper.AddParameter("@varIdTipoAmortizacion", pBEAmortizacionCompra.IdTipoAmortizacion);
                oDatabaseHelper.AddParameter("@varObservacion", pBEAmortizacionCompra.Observacion);
                oDatabaseHelper.AddParameter("@varIdEstado", pBEAmortizacionCompra.IdEstado);
                oDatabaseHelper.AddParameter("@intIdCliente", pBEAmortizacionCompra.IdCliente);
                oDatabaseHelper.AddParameter("@intIdPersonal", pBEAmortizacionCompra.BEUsuarioLogin.IdPersonal);
                vResultado = oDatabaseHelper.ExecuteNonQuery("DGP_Insertar_AdelantoVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                return vResultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDatabaseHelper.Dispose();
            }
        }

        public List<VistaAmortizacionCompra> Listar(VistaAmortizacionCompra pVistaAmortizacion)
        {
            DatabaseHelper oDBH = new DatabaseHelper();
            List<VistaAmortizacionCompra> vLista = new List<VistaAmortizacionCompra>();
            VistaAmortizacionCompra oVistaAmortizacion = null;
            IDataReader oIDataReader = null;
            try
            {
                oDBH.ClearParameter();
                oDBH.AddParameter("@@intIdCompra", (pVistaAmortizacion.IdCompra <= 0) ? (object)DBNull.Value : pVistaAmortizacion.IdCompra);
                oDBH.AddParameter("@intIdCliente", (pVistaAmortizacion.IdCliente <= 0) ? (object)DBNull.Value : pVistaAmortizacion.IdCliente);
                oDBH.AddParameter("@intIdProducto", (pVistaAmortizacion.IdProducto == 0) ? (object)DBNull.Value : pVistaAmortizacion.IdProducto);
                oDBH.AddParameter("@intIncluCanceldos", (pVistaAmortizacion.IncluyeCancelados) ? (object)1 : (object)0);
                oIDataReader = oDBH.ExecuteReader("DGP_Listar_Amortizacion_Compra", CommandType.StoredProcedure);

                while (oIDataReader.Read())
                {
                    oVistaAmortizacion = new VistaAmortizacionCompra();
                    oVistaAmortizacion.IdAmortizacion = Convert.ToInt32(oIDataReader["intIdAmortizacion"]);
                    oVistaAmortizacion.IdCompra = Convert.ToInt32(oIDataReader["intIdCompra"]);
                    oVistaAmortizacion.TipoDocumento = oIDataReader["varTipoDocumento"].ToString();
                    oVistaAmortizacion.Producto = oIDataReader["varProducto"].ToString();
                    oVistaAmortizacion.Fecha = (oIDataReader["datFecha"] == (object)DBNull.Value) ? string.Empty : Convert.ToDateTime(oIDataReader["datFecha"]).Date.ToShortDateString();
                    oVistaAmortizacion.CantidadJavas = (oIDataReader["intCantidadJavas"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["intCantidadJavas"]);
                    oVistaAmortizacion.PesoNeto = (oIDataReader["decPesoNeto"] == (object)DBNull.Value) ? decimal.Zero : Convert.ToDecimal(oIDataReader["decPesoNeto"]);
                    oVistaAmortizacion.Importe = Convert.ToDecimal(oIDataReader["decImporte"]);
                    oVistaAmortizacion.Saldo = (oIDataReader["decSaldo"] == (object)DBNull.Value) ? decimal.Zero : Convert.ToDecimal(oIDataReader["decSaldo"]);
                    oVistaAmortizacion.Indicador = Convert.ToInt32(oIDataReader["intIndicador"]);

                    oVistaAmortizacion.IdPersonal = (oIDataReader["intIdPersonal"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["intIdPersonal"].ToString()); ;
                    oVistaAmortizacion.Personal = (oIDataReader["varPesonal"] == (object)DBNull.Value) ? string.Empty : oIDataReader["varPesonal"].ToString();
                    oVistaAmortizacion.IdEstado = (oIDataReader["varEstado"] == (object)DBNull.Value) ? string.Empty : oIDataReader["varEstado"].ToString();

                    oVistaAmortizacion.IncluyeCancelados = false;

                    vLista.Add(oVistaAmortizacion);

                }
                return vLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDBH.Dispose();
            }
        }

        public bool InsertarDependiente(BEDocumentoPagoCompra pBEDocumento, BEAmortizacionCompra pBEAmortizacionCompra, DatabaseHelper pDatabaseHelper)
        {
            int vResultado = 0;
            try
            {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@decMonto", pBEAmortizacionCompra.Monto);
                pDatabaseHelper.AddParameter("@varNumeroDocumento", pBEAmortizacionCompra.NroDocumento);
                pDatabaseHelper.AddParameter("@varIdFormaPago", pBEAmortizacionCompra.IdFormaPago);
                pDatabaseHelper.AddParameter("@varIdTipoAmortizacion", pBEAmortizacionCompra.IdTipoAmortizacion);
                pDatabaseHelper.AddParameter("@varObservacion", pBEAmortizacionCompra.Observacion);
                pDatabaseHelper.AddParameter("@varIdEstado", pBEAmortizacionCompra.IdEstado);
                pDatabaseHelper.AddParameter("@@intIdCompra", pBEAmortizacionCompra.IdCompra);
                pDatabaseHelper.AddParameter("@intIdCliente", (pBEAmortizacionCompra.IdCliente <= 0) ? (object)DBNull.Value : pBEAmortizacionCompra.IdCliente);
                pDatabaseHelper.AddParameter("@intIdPersonal", pBEAmortizacionCompra.IdPersonal);
                pDatabaseHelper.AddParameter("@intIdUsuarioCreacion", pBEAmortizacionCompra.BEUsuarioLogin.IdPersonal);
                pDatabaseHelper.AddParameter("@intIdCaja", pBEAmortizacionCompra.BEUsuarioLogin.IdCaja);
                pDatabaseHelper.AddParameter("@intIdDocumento", pBEDocumento.IdDocumento);

                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Insertar_AmortizacionCompra", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return (vResultado > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarAdelantoCompra(BEAmortizacionCompra pBEAmortizacionCompra)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            int vResultado = 0;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@intIdAmortizacion", pBEAmortizacionCompra.IdAmortizacionCompra);
                oDatabaseHelper.AddParameter("@intIdUsuario", pBEAmortizacionCompra.BEUsuarioLogin.IdPersonal);
                vResultado = oDatabaseHelper.ExecuteNonQuery("DGP_Eliminar_AmortizacionCompra", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                return vResultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDatabaseHelper.Dispose();
            }
        }

        #endregion
        #region "Métodos de Documentos"
        public bool InsertarCabeceraDocumento(BEDocumentoPagoCompra beDocumento)
        {
            return this.InsertarCabeceraDocumento(beDocumento, null);

        }

        public bool InsertarCabeceraDocumento(BEDocumentoPagoCompra beDocumento, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumento.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumento.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumento.Monto);
                //oDatabaseHelper.AddParameter("@Estado", beDocumento.Estado);
                //oDatabaseHelper.AddParameter("@EsEliminado", beDocumento.EsEliminado);
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumento.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumento.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumento.Personal.IdPersonal);

                object vResultado = oDatabaseHelper.ExecuteScalar("DGP_Insertar_DocumentoPagoCompra", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
                beDocumento.IdDocumento = int.Parse(vResultado.ToString());
                return (vResultado != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }

        }

        public bool ReaplicarAmortizacion(BECompra beCompra)
        {
            return this.ReaplicarAmortizacion(beCompra, null);
        }

        public bool ReaplicarAmortizacion(BECompra beCompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCliente", beCompra.IdCliente);
                oDatabaseHelper.AddParameter("@idUsuario", beCompra.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@IdCaja", beCompra.BEUsuarioLogin.IdCaja);

                int vResultado = oDatabaseHelper.ExecuteNonQuery("DGP_ReAplicar_AmortizacionesCompra", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                return (vResultado > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }
        }

        public bool AnularAmortizacionCompra(BECompra beCompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCompra", beCompra.IdCompra);
                oDatabaseHelper.AddParameter("@idUsuario", beCompra.BEUsuarioLogin.IdPersonal);

                int vResultado = oDatabaseHelper.ExecuteNonQuery("DGP_AnularAmortizacionesCompra", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                return (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }
        }

        public decimal ObtenerAmortizacionSinAplicar(int IdCliente)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            object vResultado;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCliente", IdCliente);
                vResultado = oDatabaseHelper.ExecuteScalar("DGP_ObtenerAmortizacionCompraSinAplicar", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                decimal result = 0;
                decimal.TryParse(vResultado.ToString(), out result);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDatabaseHelper.Dispose();
            }
        }
        #endregion
    }
}
