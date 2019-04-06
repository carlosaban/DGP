using System;
using System.Collections.Generic;
using System.Text;

using DGP.Entities;
using DGP.Entities.Compras;
using DBHelper;
using System.Data;

namespace DGP.DataAccess.Compra
{
    public class DACompra
    {
        public int MyProperty { get; set; }
     
        DatabaseHelper DBconexiones = new DatabaseHelper();

        public bool Insertar(ref string pmensaje, BECompra bECompra)
        {

            bool bOk = this.Insertar(ref pmensaje, bECompra, null);

            return bOk;
        
        }
        public bool Insertar(ref string pmensaje, BECompra bECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;
            
            try
            {
                if (pDatabaseHelper == null) oDatabaseHelper.BeginTransaction();

      
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCompra", (bECompra.IdCompra <= 0) ? (object)DBNull.Value : bECompra.IdCompra, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@idTipoDocumento", (string.IsNullOrEmpty(bECompra.IdTipoDocumentoCompra)) ? (object)DBNull.Value : bECompra.IdTipoDocumentoCompra);
                oDatabaseHelper.AddParameter("@idClienteProveedor", bECompra.IdProveedor);
                oDatabaseHelper.AddParameter("@Total_Jabas", bECompra.TotalJabas);

                oDatabaseHelper.AddParameter("@Total_Peso_Bruto", bECompra.TotalPesoBruto);
                oDatabaseHelper.AddParameter("@Total_Peso_Tara", bECompra.TotalPesoTara);
                oDatabaseHelper.AddParameter("@Total_Peso_Neto", bECompra.TotalPesoNeto);
                oDatabaseHelper.AddParameter("@Precio", bECompra.Precio);
                oDatabaseHelper.AddParameter("@Monto_SubTotal", bECompra.MontoSubTotal);
                oDatabaseHelper.AddParameter("@Monto_Igv", bECompra.MontoIGV);
                oDatabaseHelper.AddParameter("@Monto_Total", bECompra.MontoTotal);
                
                oDatabaseHelper.AddParameter("@Total_Devolucion", bECompra.TotalDevolucion);
                oDatabaseHelper.AddParameter("@Total_Amortizacion", bECompra.TotalAmortizacion);
                oDatabaseHelper.AddParameter("@Total_Saldo", bECompra.TotalAmortizacion);
                oDatabaseHelper.AddParameter("@IdCaja", bECompra.Auditoria.IdCaja );
                
                //oDatabaseHelper.AddParameter("@EsSobrante", bECompra);
                //oDatabaseHelper.AddParameter("@IdEstado", bECompra.TotalAmortizacion); ;
                
                oDatabaseHelper.AddParameter("@Observacion", bECompra.Observacion);
                oDatabaseHelper.AddParameter("@IdProducto",bECompra.IdProducto );
                oDatabaseHelper.AddParameter("@TotalUnidades",bECompra.TotalUnidades );
                oDatabaseHelper.AddParameter("@Usuario", (bECompra.Auditoria == null) ? (object)DBNull.Value : bECompra.Auditoria.IdPersonal);
                oDatabaseHelper.AddParameter("@Fecha", bECompra.Fecha);
                oDatabaseHelper.AddParameter("@IdNotaCreditoCompra", (bECompra.IdNotaCreditoCompra <= 0) ? (object)DBNull.Value : bECompra.IdNotaCreditoCompra);
                
                int filasAfectadas = oDatabaseHelper.ExecuteNonQuery("DGP_Insertar_Compra", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen, true);

                bECompra.IdCompra = Convert.ToInt32(oDatabaseHelper.GetParameter("@IdCompra").Value.ToString());

                if (pDatabaseHelper == null && bECompra.IdCompra > 0) oDatabaseHelper.CommitTransaction();
                return (bECompra.IdCompra > 0);
            }
            catch (Exception ex) {
                    if (pDatabaseHelper == null)  oDatabaseHelper.RollbackTransaction();
                    throw ex;
                } finally {
                    if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
                }


        }


        public bool Actualizar(ref string pmensaje, BECompra bECompra)
        {
            return this.Actualizar(ref pmensaje, bECompra, null);
        }
        public bool Actualizar(ref string pmensaje, BECompra bECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                if (pDatabaseHelper == null) oDatabaseHelper.BeginTransaction();


                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCompra",  bECompra.IdCompra);
                oDatabaseHelper.AddParameter("@idTipoDocumento", (string.IsNullOrEmpty(bECompra.IdTipoDocumentoCompra)) ? (object)DBNull.Value : bECompra.IdTipoDocumentoCompra);
                oDatabaseHelper.AddParameter("@idClienteProveedor", bECompra.IdProveedor);
                oDatabaseHelper.AddParameter("@Total_Jabas", bECompra.TotalJabas);

                oDatabaseHelper.AddParameter("@Total_Peso_Bruto", bECompra.TotalPesoBruto);
                oDatabaseHelper.AddParameter("@Total_Peso_Tara", bECompra.TotalPesoTara);
                oDatabaseHelper.AddParameter("@Total_Peso_Neto", bECompra.TotalPesoNeto);

                oDatabaseHelper.AddParameter("@Total_Devolucion", bECompra.TotalDevolucion);
                oDatabaseHelper.AddParameter("@Total_Amortizacion", bECompra.TotalAmortizacion);
                oDatabaseHelper.AddParameter("@Total_Saldo", bECompra.TotalAmortizacion);

                oDatabaseHelper.AddParameter("@Precio", bECompra.Precio);
                oDatabaseHelper.AddParameter("@Monto_SubTotal", bECompra.MontoSubTotal);
                oDatabaseHelper.AddParameter("@Monto_Igv", bECompra.MontoIGV);
                oDatabaseHelper.AddParameter("@Monto_Total", bECompra.MontoTotal);
                //oDatabaseHelper.AddParameter("@EsSobrante", bECompra.EsSobrante);
                oDatabaseHelper.AddParameter("@IdEstado", bECompra.IdEstado); ;

                oDatabaseHelper.AddParameter("@Observacion", bECompra.Observacion);
                oDatabaseHelper.AddParameter("@IdProducto", bECompra.IdProducto);
                oDatabaseHelper.AddParameter("@TotalUnidades", bECompra.TotalUnidades);
                oDatabaseHelper.AddParameter("@Usuario", (bECompra.Auditoria == null) ? (object)DBNull.Value : bECompra.Auditoria.IdPersonal);
                oDatabaseHelper.AddParameter("@IdNotaCreditoCompra", (bECompra.IdNotaCreditoCompra<=0) ? (object)DBNull.Value : bECompra.IdNotaCreditoCompra);
               
                int filasAfectadas = oDatabaseHelper.ExecuteNonQuery("DGP_Actualizar_Compra", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen, true);

                if (pDatabaseHelper == null && filasAfectadas > 0) oDatabaseHelper.CommitTransaction();

                return (filasAfectadas > 0);
            }
            catch (Exception ex)
            {
                if (pDatabaseHelper == null) oDatabaseHelper.RollbackTransaction();
                throw ex;
            }
            finally
            {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }


        }


        public bool Eliminar(ref string pmensaje, BECompra bECompra)
        {
            return this.Eliminar(ref pmensaje, bECompra, null);
        }
        public bool Eliminar(ref string pmensaje, BECompra bECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            try
            {
                if (pDatabaseHelper == null) oDatabaseHelper.BeginTransaction();


                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCompra", bECompra.IdCompra);
                oDatabaseHelper.AddParameter("@Usuario", (bECompra.Auditoria == null) ? (object)DBNull.Value : bECompra.Auditoria.IdPersonal);

                int filasAfectadas = oDatabaseHelper.ExecuteNonQuery("DGP_Eliminar_Compra", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen, true);

                if (pDatabaseHelper == null && filasAfectadas > 0) oDatabaseHelper.CommitTransaction();

                return (filasAfectadas > 0);
            }
            catch (Exception ex)
            {
                if (pDatabaseHelper == null) oDatabaseHelper.RollbackTransaction();
                throw ex;
            }
            finally
            {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }


        }


        public List<BECompraFilter> Listar(BECompraFilter pBECompra) 
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            try
            {
                return Listar(pBECompra, oDatabaseHelper);

            }
            catch (Exception ex)
            {
                 oDatabaseHelper.Dispose();
                throw ex;
            }
            finally
            {
                
               oDatabaseHelper.Dispose();
            }
        
        }

        public List<BECompraFilter> Listar(BECompraFilter pBECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;

            List<BECompraFilter> lista = new List<BECompraFilter>();
            IDataReader DRlista = null;

            try
            {
                DBconexiones.ClearParameter();
                DBconexiones.AddParameter("@IdCompra",  (pBECompra.IdCompra == 0) ? DBNull.Value : (object)pBECompra.IdCompra);
                DBconexiones.AddParameter("@IdProveedor", (pBECompra.IdProveedor <= 0) ? DBNull.Value : (object)pBECompra.IdProveedor);
                DBconexiones.AddParameter("@IdProducto", (pBECompra.IdProducto == 0) ? DBNull.Value : (object)pBECompra.IdProducto);
                DBconexiones.AddParameter("@fechaIni", (pBECompra.FechaInicio == null) ? DBNull.Value : (object)pBECompra.FechaInicio);
                DBconexiones.AddParameter("@fechaFin", (pBECompra.FechaFin == null) ? DBNull.Value : (object)pBECompra.FechaFin);
                DRlista = DBconexiones.ExecuteReader("DGP_Listar_Compra", CommandType.StoredProcedure);

                while (DRlista.Read())
                {
                    BECompraFilter compra = new BECompraFilter();
                    compra.IdCompra = (int)DRlista["IdCompra"];
                    compra.IdTipoDocumentoCompra =(DRlista["IdTipoDocumentoCompra"] == DBNull.Value)?string.Empty:DRlista["IdTipoDocumentoCompra"].ToString();                    compra.TipoDocumentoCompra = (string)DRlista["TipoDocumentoCompra"];
                    compra.NumeroDocumento = (DRlista["NumeroDocumento"] == DBNull.Value) ? string.Empty : (string)DRlista["NumeroDocumento"];
                    compra.TotalPesoBruto = (DRlista["TotalPeso_Bruto"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Bruto"];
                    compra.TotalPesoTara = (DRlista["TotalPeso_Tara"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Tara"];
                    compra.TotalPesoNeto = (DRlista["TotalPeso_Neto"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Neto"];
                    compra.Precio = (DRlista["Precio"] == DBNull.Value) ? 0 : (decimal)DRlista["Precio"];
                    compra.MontoSubTotal = (DRlista["MontoSubTotal"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoSubTotal"];
                    compra.MontoIGV = (DRlista["MontoIgv"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoIgv"];
                    compra.MontoTotal = (DRlista["MontoTotal"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoTotal"];
                    compra.TotalDevolucion = (DRlista["TotalDevolucion"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalDevolucion"];
                    compra.TotalAmortizacion = (DRlista["TotalAmortizacion"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalAmortizacion"];
                    compra.TotalSaldo = (DRlista["TotalSaldo"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalSaldo"];
                    compra.Observacion = (DRlista["Observacion"] == DBNull.Value) ? string.Empty : (string)DRlista["Observacion"];
                    compra.IdEstado = (DRlista["IdEstado"] == DBNull.Value) ? string.Empty : (string)DRlista["IdEstado"];
                    compra.IdEmpresa = (DRlista["IdEmpresa"] == DBNull.Value) ? 0 : (int)DRlista["IdEmpresa"];
                    compra.Empresa = (DRlista["Empresa"] == DBNull.Value) ? string.Empty : (string)DRlista["Empresa"];
                    compra.IdProducto = (DRlista["IdProducto"] == DBNull.Value) ? 0 : (int)DRlista["IdProducto"];
                    compra.Producto = (DRlista["Producto"] == DBNull.Value) ? string.Empty : (string)DRlista["Producto"];
                    compra.IdProveedor = (DRlista["IdProveedor"] == DBNull.Value) ? 0 : (int)DRlista["IdProveedor"];
                    compra.Proveedor = (DRlista["Cliente"] == DBNull.Value) ? string.Empty : (string)DRlista["Cliente"];
                    compra.IdPersonal = (DRlista["IdPersonal"] == DBNull.Value) ? 0 : (int)DRlista["IdPersonal"];
                    compra.FechaCreacion = (DateTime)DRlista["FechaCreacion"];
                    compra.Fecha = (DateTime)DRlista["Fecha"];
                    compra.TotalUnidades = (DRlista["TotalUnidades"] == DBNull.Value) ? 0 : (int)DRlista["TotalUnidades"];
                    compra.TotalJabas = (DRlista["TotalJabas"] == DBNull.Value) ? 0 : (int)DRlista["TotalJabas"];
                    compra.IdNotaCreditoCompra = (DRlista["IdNotaCreditoCompra"] == DBNull.Value) ? 0 : (int)DRlista["IdNotaCreditoCompra"];
                    compra.MontoTotalBD = (DRlista["MontoTotal"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoTotal"];
                    
                    lista.Add(compra);
                }

                return lista;
            }
            catch (Exception ex)
            {
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
                throw ex;
            }
            finally
            {
                if (!DRlista.IsClosed) DRlista.Close();
                DRlista.Dispose();
                if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
            }
            
        }
    }
}
