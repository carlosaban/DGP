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
                oDatabaseHelper.AddParameter("@IdCliente", codCliProv);
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
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumento.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumento.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumento.Personal.IdPersonal);

                object vResultado = oDatabaseHelper.ExecuteScalar("InsertarDocumento", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
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


        public bool ActualizarCabeceraDocumento(BEDocumento beDocumento)
        {
            return this.InsertarCabeceraDocumento(beDocumento, null);

        }

        public bool ActualizarCabeceraDocumento(BEDocumento beDocumento, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumento.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumento.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumento.Monto);
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumento.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumento.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumento.Personal.IdPersonal);

                object vResultado = oDatabaseHelper.ExecuteScalar("InsertarDocumento", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
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

        public bool EliminarCabeceraDocumento(BEDocumento beDocumento)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdDocumento", beDocumento.IdDocumento);
                oDatabaseHelper.AddParameter("@Usuario", beDocumento.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@observacion", beDocumento.Observacion);

                object vResultado = oDatabaseHelper.ExecuteScalar("EliminarDocumento", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                beDocumento.IdDocumento = int.Parse(vResultado.ToString());
                return (vResultado != null);
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
