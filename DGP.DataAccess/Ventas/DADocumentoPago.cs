using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Ventas;
using System.Data;
using DBHelper;

namespace DGP.DataAccess.Ventas
{
    public class DADocumentoPago
    {
        #region "Métodos de Documentos"

        public List<BEDocumento> ListarDocumento(int codCliProv)
        {
            return this.ListarDocumento(codCliProv, null, null);

        }

        public List<BEDocumento> ListarDocumento(int codCliProv, DateTime? FechaInicio, DateTime? FechaFinal)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            List<BEDocumento> vLista = new List<BEDocumento>();
            IDataReader oIDataReader = null;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@FechaInicio", FechaInicio);
                oDatabaseHelper.AddParameter("@FechaFinal", FechaFinal);
                oDatabaseHelper.AddParameter("@IdCliente", (codCliProv == 0) ? DBNull.Value : (object)codCliProv);
                oIDataReader = oDatabaseHelper.ExecuteReader("ListarDocumento", CommandType.StoredProcedure);
                while (oIDataReader.Read())
                {
                    vLista.Add(new BEDocumento()
                    {
                        IdDocumento = (int)oIDataReader["IdDocumento"],
                        Fecha = Convert.ToDateTime(oIDataReader["Fecha"]),
                        IdTipoDocumento = oIDataReader["IdTipoDocumento"].ToString(),
                        Monto = decimal.Parse(oIDataReader["Monto"].ToString()),
                        idEstado = oIDataReader["idEstado"].ToString(),
                        Cliente = new DGP.Entities.BEClienteProveedor {
                            IdCliente = (int)oIDataReader["IdCliente"],
                            Nombre = oIDataReader["ClienteNombre"].ToString()
                        
                        }
                    });

                }
                return (vLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public List<BEAmortizacionVenta> ListarDetalle(int idDocumento)
        {
            DatabaseHelper oDBH = new DatabaseHelper();
            List<BEAmortizacionVenta> vLista = new List<BEAmortizacionVenta>();
            BEAmortizacionVenta oBEAmortizacionVenta = null;
            IDataReader oIDataReader = null;
            try
            {
                oDBH.ClearParameter();
                oDBH.AddParameter("@IdDocumento", (idDocumento <= 0) ? (object)DBNull.Value : idDocumento);
                oIDataReader = oDBH.ExecuteReader("ListarAmortVenta", CommandType.StoredProcedure);

                while (oIDataReader.Read())
                {
                    oBEAmortizacionVenta = new BEAmortizacionVenta();
                    oBEAmortizacionVenta.IdAmortizacionVenta = Convert.ToInt32(oIDataReader["Id_Amort_Venta"]);
                    oBEAmortizacionVenta.Monto = (oIDataReader["Monto"] == (object)DBNull.Value) ? decimal.Zero : Convert.ToDecimal(oIDataReader["Monto"]);
                    oBEAmortizacionVenta.NroDocumento = oIDataReader["NumeroDocumento"].ToString();
                    oBEAmortizacionVenta.IdFormaPago = oIDataReader["IdFormaPago"].ToString();
                    oBEAmortizacionVenta.FechaPago = Convert.ToDateTime(oIDataReader["FechaPago"]);
                    oBEAmortizacionVenta.IdTipoAmortizacion = oIDataReader["IdTipoAmortizacion"].ToString();
                    oBEAmortizacionVenta.Observacion = oIDataReader["Observacion"].ToString();
                    oBEAmortizacionVenta.IdEstado = oIDataReader["IdEstado"].ToString();
                    oBEAmortizacionVenta.IdVenta = (oIDataReader["Id_Venta"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["Id_Venta"]);
                    oBEAmortizacionVenta.IdCliente = (oIDataReader["Id_Cliente"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["Id_Cliente"]);
                    oBEAmortizacionVenta.IdDocumento = (oIDataReader["IdDocumento"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["IdDocumento"]);
                    oBEAmortizacionVenta.SaldoMaximoVenta = (oIDataReader["SaldoMaximoVenta"] == (object)DBNull.Value) ? 0 : Convert.ToDecimal(oIDataReader["SaldoMaximoVenta"]);
                    
                    


                    vLista.Add(oBEAmortizacionVenta);

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

        public List<BEVenta> ListarVentasXCliente(int idCliente , int idDocumento )
        {
            return this.ListarVentasXCliente(idCliente,idDocumento, null);

        }

        public List<BEVenta> ListarVentasXCliente(int idCliente, int idDocumento, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            List<BEVenta> vLista = new List<BEVenta>();
            IDataReader oIDataReader = null;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCliente", idCliente);

                oDatabaseHelper.AddParameter("@IdDocumento", idDocumento);
                oIDataReader = oDatabaseHelper.ExecuteReader("ListarVentaXCliente", CommandType.StoredProcedure);
                while (oIDataReader.Read())
                {
                    vLista.Add(new BEVenta()
                    {
                        IdVenta = Convert.ToInt32(oIDataReader["Id_Venta"])

                        ,IdCliente = (oIDataReader["Id_Cliente"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["Id_Cliente"].ToString())
                        ,IdProducto = int.Parse(oIDataReader["Id_Producto"].ToString())
                        ,Producto = oIDataReader["Producto"].ToString()
                        ,IdTipoDocumentoVenta = (oIDataReader["IdTipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["IdTipoDocumentoVenta"].ToString()
                        ,TipoDocumentoVenta = (oIDataReader["TipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["TipoDocumentoVenta"].ToString()
                        ,FechaCreacion = Convert.ToDateTime(oIDataReader["FechaCreacion"]).ToShortDateString()
                        
                        ,TotalPesoBruto = decimal.Parse(oIDataReader["Total_Peso_Bruto"].ToString())
                        ,TotalPesoTara = decimal.Parse(oIDataReader["Total_Peso_Tara"].ToString())
                        ,TotalPesoNeto = decimal.Parse(oIDataReader["Total_Peso_Neto"].ToString())
                        ,Precio = decimal.Parse(oIDataReader["Precio"].ToString())
                        ,MontoSubTotal = decimal.Parse(oIDataReader["Monto_SubTotal"].ToString())
                        ,MontoIGV = decimal.Parse(oIDataReader["Monto_Igv"].ToString())
                        ,MontoTotal = decimal.Parse(oIDataReader["Monto_Total"].ToString())
                        ,TotalDevolucion = (oIDataReader["Total_Devolucion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Devolucion"].ToString())
                        ,TotalAmortizacion = (oIDataReader["Total_Amortizacion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Amortizacion"].ToString())
                        ,TotalSaldo = decimal.Parse(oIDataReader["Total_Saldo"].ToString())
                        ,IdEstado = (oIDataReader["Estado"] == (object)DBNull.Value) ? string.Empty : oIDataReader["Estado"].ToString()
                
                    });

                }
                return (vLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool InsertarCabeceraDocumento(BEDocumento beDocumento)
        {
            return this.InsertarCabeceraDocumento(beDocumento, null);

        }

        public bool InsertarCabeceraDocumento(BEDocumento beDocumento, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumento.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumento.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumento.Monto);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumento.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumento.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumento.Personal.IdPersonal);
                oDatabaseHelper.AddParameter("@IdTipoPago", beDocumento.IdTipoPago);
                oDatabaseHelper.AddParameter("@observacion", beDocumento.Observacion);
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);

                object vResultado = oDatabaseHelper.ExecuteScalar("InsertarDocumento", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
                
                return true;
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


        public bool InsertarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta)
        {
            return this.InsertarAmortizacionVenta(beAmortizacionVenta, null);

        }

        public bool InsertarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@decMonto", beAmortizacionVenta.Monto);
                oDatabaseHelper.AddParameter("@varNumeroDocumento", beAmortizacionVenta.NroDocumento);
                oDatabaseHelper.AddParameter("@varObservacion", beAmortizacionVenta.Observacion);
                oDatabaseHelper.AddParameter("@intIdVenta", beAmortizacionVenta.IdVenta);
                oDatabaseHelper.AddParameter("@intIdCliente", beAmortizacionVenta.IdCliente);
                oDatabaseHelper.AddParameter("@intIdPersonal", beAmortizacionVenta.IdPersonal);
                oDatabaseHelper.AddParameter("@intIdUsuarioCreacion", beAmortizacionVenta.IdPersonal);
                oDatabaseHelper.AddParameter("@intIdCaja", beAmortizacionVenta.Caja.IdCaja);
                oDatabaseHelper.AddParameter("@intIdDocumento", beAmortizacionVenta.IdDocumento);
                oDatabaseHelper.AddParameter("@varIdEstado", beAmortizacionVenta.IdEstado);

                oDatabaseHelper.ExecuteScalar("InsertarAmortizacionVenta", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                return true;
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
        public bool ActualizarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta)
        {
            return this.ActualizarAmortizacionVenta(beAmortizacionVenta, null);

        }


        public bool ActualizarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@decMonto", beAmortizacionVenta.Monto);
                oDatabaseHelper.AddParameter("@varNumeroDocumento", beAmortizacionVenta.NroDocumento);
                oDatabaseHelper.AddParameter("@varObservacion", beAmortizacionVenta.Observacion);
                oDatabaseHelper.AddParameter("@intIdVenta", beAmortizacionVenta.IdVenta);
                oDatabaseHelper.AddParameter("@intIdPersonal", beAmortizacionVenta.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@intIdUsuarioModificacion", beAmortizacionVenta.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@intIdAmortVenta", beAmortizacionVenta.IdAmortizacionVenta);
                oDatabaseHelper.AddParameter("@varIdEstado", beAmortizacionVenta.IdEstado);

                oDatabaseHelper.ExecuteScalar("ActualizarAmortizacionVenta", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                return true;
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


        public bool ActualizarCabeceraDocumento(BEDocumento beDocumento)
        {
            return this.ActualizarCabeceraDocumento(beDocumento, null);

        }

        public bool ActualizarCabeceraDocumento(BEDocumento beDocumento, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdDocumento", beDocumento.IdDocumento);
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumento.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumento.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumento.Monto);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumento.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumento.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumento.Personal.IdPersonal);
                oDatabaseHelper.AddParameter("@IdTipoPago", beDocumento.IdTipoPago);
                oDatabaseHelper.AddParameter("@observacion", beDocumento.Observacion);

                oDatabaseHelper.ExecuteScalar("ActualizarDocumento", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
                
                return true;
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

        public bool EliminarCabeceraDocumento(BEDocumento beDocumento)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdDocumento", beDocumento.IdDocumento);
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@observacion", beDocumento.Observacion);

                oDatabaseHelper.ExecuteScalar("EliminarDocumento", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                
                return true;
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

        public bool EliminarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta)
        {
            return this.EliminarAmortizacionVenta(beAmortizacionVenta, null);

        }

        public bool EliminarAmortizacionVenta(BEAmortizacionVenta beAmortizacionVenta, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@idAmortizacionVenta", beAmortizacionVenta.IdAmortizacionVenta);

                oDatabaseHelper.ExecuteScalar("EliminarAmortizacionVenta", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                return true;
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

        #endregion
    }
}
