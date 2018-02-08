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


        public bool Insertar(ref string pmensaje, BECompra bECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;
            
            try
            {
                //compra.IdCompra = (int)DRlista["IdCompra"];
                //compra.IdTipoDocumentoCompra = (DRlista["IdTipoDocumentoCompra"] == DBNull.Value) ? string.Empty : DRlista["IdTipoDocumentoCompra"].ToString(); compra.TipoDocumentoCompra = (string)DRlista["TipoDocumentoCompra"];
                //compra.NumeroDocumento = (DRlista["NumeroDocumento"] == DBNull.Value) ? string.Empty : (string)DRlista["NumeroDocumento"];
                //compra.TotalPesoBruto = (DRlista["TotalPeso_Bruto"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Bruto"];
                //compra.TotalPesoTara = (DRlista["TotalPeso_Tara"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Tara"];
                //compra.TotalPesoNeto = (DRlista["TotalPeso_Neto"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalPeso_Neto"];
                //compra.Precio = (DRlista["Precio"] == DBNull.Value) ? 0 : (decimal)DRlista["Precio"];
                //compra.MontoSubTotal = (DRlista["MontoSubTotal"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoSubTotal"];
                //compra.MontoIGV = (DRlista["MontoIgv"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoIgv"];
                //compra.MontoTotal = (DRlista["MontoTotal"] == DBNull.Value) ? 0 : (decimal)DRlista["MontoTotal"];
                //compra.TotalDevolucion = (DRlista["TotalDevolucion"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalDevolucion"];
                //compra.TotalAmortizacion = (DRlista["TotalAmortizacion"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalAmortizacion"];
                //compra.TotalSaldo = (DRlista["TotalSaldo"] == DBNull.Value) ? 0 : (decimal)DRlista["TotalSaldo"];
                //compra.Observacion = (DRlista["Observacion"] == DBNull.Value) ? string.Empty : (string)DRlista["Observacion"];
                //compra.IdEstado = (DRlista["IdEstado"] == DBNull.Value) ? string.Empty : (string)DRlista["IdEstado"];
                //compra.IdEmpresa = (DRlista["IdEmpresa"] == DBNull.Value) ? 0 : (int)DRlista["IdEmpresa"];
                //compra.Empresa = (DRlista["Empresa"] == DBNull.Value) ? string.Empty : (string)DRlista["Empresa"];
                //compra.IdProducto = (DRlista["IdProducto"] == DBNull.Value) ? 0 : (int)DRlista["IdProducto"];
                //compra.Producto = (DRlista["Producto"] == DBNull.Value) ? string.Empty : (string)DRlista["Producto"];
                //compra.IdProveedor = (DRlista["IdCliente"] == DBNull.Value) ? 0 : (int)DRlista["IdCliente"];
                //compra.Proveedor = (DRlista["Cliente"] == DBNull.Value) ? string.Empty : (string)DRlista["Cliente"];
                //compra.IdPersonal = (DRlista["IdPersonal"] == DBNull.Value) ? 0 : (int)DRlista["IdPersonal"];
                //compra.FechaCreacion = (DateTime)DRlista["FechaCreacion"];
                //compra.TotalUnidades = (DRlista["TotalUnidades"] == DBNull.Value) ? 0 : (int)DRlista["TotalUnidades"];
      
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdCompra", (bECompra.IdCompra <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdTipoDocumentoCompra", (bECompra.IdTipoDocumentoCompra <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@NumeroDocumento", (bECompra.NumeroDocumento <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalPeso_Bruto", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalPeso_Tara", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalPeso_Neto", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@Precio", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@MontoSubTotal", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@MontoIgv", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@MontoTotal", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);

                oDatabaseHelper.AddParameter("@TotalDevolucion", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalAmortizacion", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalSaldo", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@Observacion", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdEstado", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdEmpresa", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdProducto", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdCliente", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@IdPersonal", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
                oDatabaseHelper.AddParameter("@TotalUnidades", (bECompra.IdPersonal <= 0) ? (object)DBNull.Value : pBEPersonal.IdPersonal, ParameterDirection.Output, SqlDbType.Int, 4);
               


                oDatabaseHelper.AddParameter("@usuarioModificacion", (pBEPersonal.Auditoria == null) ? (object)DBNull.Value : pBEPersonal.Auditoria.IdPersonal);

                int filasAfectadas = oDatabaseHelper.ExecuteNonQuery("Usuario_insert", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen, true);

                pBEPersonal.IdPersonal = Convert.ToInt32(oDatabaseHelper.GetParameter("@id").Value.ToString());
                return (filasAfectadas > 0);
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
            

        public bool Actualizar(BECompra bECompra, out string mensaje)
        {
            throw new NotImplementedException();
        }

        public List<BECompra> Listar(BECompraFilter pBECompra) 
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

        public List<BECompra> Listar(BECompraFilter pBECompra, DatabaseHelper pDatabaseHelper)
        {
            DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;
                
            List<BECompra> lista = new List<BECompra>();
            IDataReader DRlista = null;

            try
            {
                DBconexiones.ClearParameter();
                DBconexiones.AddParameter("@intIdCompra", pBECompra.IdCompra);
                DBconexiones.AddParameter("@varIdTipoDocumento", pBECompra.IdCompra);
                DBconexiones.AddParameter("@intIdCliente", pBECompra.IdCompra);
                DBconexiones.AddParameter("@intIdProducto", pBECompra.IdCompra);
                DBconexiones.AddParameter("@fechaIni", pBECompra.IdCompra);
                DBconexiones.AddParameter("@fechaFin", pBECompra.IdCompra);
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
                    compra.IdProveedor = (DRlista["IdCliente"] == DBNull.Value) ? 0 : (int)DRlista["IdCliente"];
                    compra.Proveedor = (DRlista["Cliente"] == DBNull.Value) ? string.Empty : (string)DRlista["Cliente"];
                    compra.IdPersonal = (DRlista["IdPersonal"] == DBNull.Value) ? 0 : (int)DRlista["IdPersonal"];
                    compra.FechaCreacion = (DateTime)DRlista["FechaCreacion"];
                    compra.TotalUnidades = (DRlista["TotalUnidades"] == DBNull.Value) ? 0 : (int)DRlista["TotalUnidades"];
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
