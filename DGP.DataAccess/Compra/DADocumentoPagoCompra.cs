using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities.Compras;
using System.Data;
using DBHelper;

namespace DGP.DataAccess.Compra
{
    public class DADocumentoPagoCompra
    {
        #region "Métodos de Documentos"

        public List<BEDocumentoCompra> ListarDocumento(int codCliProv)
        {
            return this.ListarDocumento(codCliProv, null, null);

        }

        public List<BEDocumentoCompra> ListarDocumento(int codCliProv, DateTime? FechaInicio, DateTime? FechaFinal)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            List<BEDocumentoCompra> vLista = new List<BEDocumentoCompra>();
            IDataReader oIDataReader = null;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@FechaInicio", FechaInicio);
                oDatabaseHelper.AddParameter("@FechaFinal", FechaFinal);
                oDatabaseHelper.AddParameter("@IdCliente", (codCliProv == 0) ? DBNull.Value : (object)codCliProv);
                oIDataReader = oDatabaseHelper.ExecuteReader("ListarDocumentoCompra", CommandType.StoredProcedure);
                while (oIDataReader.Read())
                {
                    vLista.Add(new BEDocumentoCompra()
                    {
                        IdDocumentoCompra = (int)oIDataReader["IdDocumentoCompra"],
                        Fecha = Convert.ToDateTime(oIDataReader["Fecha"]),
                        IdTipoDocumento = oIDataReader["IdTipoDocumento"].ToString(),
                        Monto = decimal.Parse(oIDataReader["Monto"].ToString()),
                        idEstado = oIDataReader["idEstado"].ToString(),
                        Cliente = new DGP.Entities.BEClienteProveedor {
                            IdCliente = (int)oIDataReader["IdCliente"],
                            Nombre = oIDataReader["ClienteNombre"].ToString()
                        
                        },
                        Personal = new DGP.Entities.Seguridad.BEPersonal{
                            IdPersonal = (int)oIDataReader["IdPersonal"]
                           
                        
                        },
                        IdFormaPago = oIDataReader["IdFormaPago"].ToString()
                        ,
                        IdBanco = oIDataReader["IdBanco"].ToString()
                        ,
                        NumeroOperacion = oIDataReader["NumeroOperacion"].ToString()
                        ,
                        NumeroReciboPago = oIDataReader["NumeroRecibo"].ToString()
                        ,
                        Observacion = oIDataReader["Observacion"].ToString()


                    });

                }
                return (vLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertarCabeceraDocumento(BEDocumentoCompra beDocumentoCompra)
        {
            return this.InsertarCabeceraDocumento(beDocumentoCompra, null);

        }

        public bool InsertarCabeceraDocumento(BEDocumentoCompra beDocumentoCompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumentoCompra.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumentoCompra.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumentoCompra.Monto);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumentoCompra.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumentoCompra.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumentoCompra.Personal.IdPersonal);
                oDatabaseHelper.AddParameter("@Usuario", beDocumentoCompra.BEUsuarioLogin.IdPersonal);

                oDatabaseHelper.AddParameter("@IdFormaPago", beDocumentoCompra.IdFormaPago);
                oDatabaseHelper.AddParameter("@observacion", beDocumentoCompra.Observacion);

                oDatabaseHelper.AddParameter("@NumeroRecibo", (beDocumentoCompra.NumeroReciboPago == string.Empty) ? DBNull.Value : (object)beDocumentoCompra.NumeroReciboPago);
                oDatabaseHelper.AddParameter("@IdBanco", (beDocumentoCompra.IdBanco == string.Empty) ? DBNull.Value : (object)beDocumentoCompra.IdBanco);
                oDatabaseHelper.AddParameter("@NumeroOperacion", (beDocumentoCompra.NumeroOperacion == string.Empty) ? DBNull.Value : (object)beDocumentoCompra.NumeroOperacion);



                object vResultado = oDatabaseHelper.ExecuteScalar("InsertarDocumentoCompra", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);

                beDocumentoCompra.IdDocumentoCompra = (vResultado == null)?0 : int.Parse (vResultado.ToString());
                
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


    

        public bool ActualizarCabeceraDocumento(BEDocumentoCompra beDocumentoCompra)
        {
            return this.ActualizarCabeceraDocumento(beDocumentoCompra, null);

        }

        public bool ActualizarCabeceraDocumento(BEDocumentoCompra beDocumentoCompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdDocumentoCompra", beDocumentoCompra.IdDocumentoCompra);
                oDatabaseHelper.AddParameter("@IdTipoDocumento", beDocumentoCompra.IdTipoDocumento);
                oDatabaseHelper.AddParameter("@Fecha", beDocumentoCompra.Fecha.Date);
                oDatabaseHelper.AddParameter("@Monto", beDocumentoCompra.Monto);
                oDatabaseHelper.AddParameter("@IdCaja", beDocumentoCompra.BEUsuarioLogin.IdCaja);
                oDatabaseHelper.AddParameter("@IdCliente", beDocumentoCompra.Cliente.IdCliente);
                oDatabaseHelper.AddParameter("@IdPersonal", beDocumentoCompra.Personal.IdPersonal);
                oDatabaseHelper.AddParameter("@IdFormaPago", beDocumentoCompra.IdFormaPago);

                oDatabaseHelper.AddParameter("@IdBanco", beDocumentoCompra.IdBanco);
                oDatabaseHelper.AddParameter("@NumeroRecibo", beDocumentoCompra.NumeroReciboPago);
                oDatabaseHelper.AddParameter("@NumeroOperacion", beDocumentoCompra.NumeroOperacion);



                oDatabaseHelper.AddParameter("@observacion", beDocumentoCompra.Observacion);

                oDatabaseHelper.AddParameter("@idPersonalAuditoria", beDocumentoCompra.BEUsuarioLogin.IdPersonal);

                oDatabaseHelper.ExecuteScalar("ActualizarDocumentoCompra", CommandType.StoredProcedure, (pDatabaseHelper == null) ? DBHelper.ConnectionState.CloseOnExit : DBHelper.ConnectionState.KeepOpen);
                
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

        public bool EliminarCabeceraDocumento(BEDocumentoCompra beDocumentoCompra)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();

            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdDocumentoCompra", beDocumentoCompra.IdDocumentoCompra);
                oDatabaseHelper.AddParameter("@Usuario", beDocumentoCompra.BEUsuarioLogin.IdPersonal);
                oDatabaseHelper.AddParameter("@observacion", beDocumentoCompra.Observacion);

                oDatabaseHelper.ExecuteScalar("EliminarDocumentoCompra", CommandType.StoredProcedure, DBHelper.ConnectionState.CloseOnExit);
                
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

      
        #endregion
    }
}
