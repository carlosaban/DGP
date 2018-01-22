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
        //public List<BECompra> ListarCompraCliente(BECompra pBECompra)
        //{
        //    DatabaseHelper oDatabaseHelper = new DatabaseHelper();
        //    List<BECompra> vLista = new List<BECompra>();
        //    IDataReader oIDataReader = null;
        //    BECompra oBECompra = null;
        //    try
        //    {
        //        //oDatabaseHelper.ClearParameter();
        //        //oDatabaseHelper.AddParameter("@intIdCliente", pBECompra.IdCliente);
        //        //oDatabaseHelper.AddParameter("@intIdProducto", pBECompra.IdProducto);
        //        //oDatabaseHelper.AddParameter("@intIdCaja", pBECompra.IdCaja);
        //        //oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_CompraCliente", CommandType.StoredProcedure);
        //        //while (oIDataReader.Read())
        //        //{
        //        //    oBECompra = new BECompra();
        //        //    oBECompra.IdCompra = oIDataReader.GetInt32(0);
        //        //    oBECompra.NombreCompra = oIDataReader.GetString(1);
        //        //    vLista.Add(oBECompra);
        //        //}

        //        return vLista;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!oIDataReader.IsClosed) oIDataReader.Close();
        //        oIDataReader.Dispose();
        //        oDatabaseHelper.Dispose();
        //    }
        //}

        //public List<BECompra> ListarCompra(int pIdCompra, int pIdCaja, DatabaseHelper pDatabaseHelper)
        //{
        //    DatabaseHelper oDatabaseHelper = (pDatabaseHelper == null) ? new DatabaseHelper() : pDatabaseHelper;
        //    List<BECompra> vLista = new List<BECompra>();
        //    BECompra oBECompra = null;
        //    IDataReader oIDataReader = null;
        //    try
        //    {
        //        oDatabaseHelper.ClearParameter();
        //        oDatabaseHelper.AddParameter("@intIdVenta", (pIdCompra <= 0) ? (object)DBNull.Value : pIdCompra);
        //        oDatabaseHelper.AddParameter("@intIdCaja", (pIdCaja <= 0) ? (object)DBNull.Value : pIdCaja);
        //        oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_Compra", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);

        //        while (oIDataReader.Read())
        //        {
        //            oBECompra = new BECompra();
        //            oBECompra.IdCompra = int.Parse(oIDataReader["IdCompra"].ToString());
        //            oBECompra.IdTipoDocumentoCompra = (oIDataReader["IdTipoDocumentoCompra"] == (object)DBNull.Value) ? string.Empty : oIDataReader["IdTipoDocumentoCompra"].ToString();
        //            oBECompra.TipoDocumentoCompra = (oIDataReader["TipoDocumentoCompra"] == (object)DBNull.Value) ? string.Empty : oIDataReader["TipoDocumentoCompra"].ToString();
        //            oBECompra.NumeroDocumento = (oIDataReader["NumeroDocumento"] == (object)DBNull.Value) ? string.Empty : oIDataReader["NumeroDocumento"].ToString();
        //            oBECompra.TotalPesoBruto = decimal.Parse(oIDataReader["TotalPeso_Bruto"].ToString());
        //            oBECompra.TotalPesoTara = decimal.Parse(oIDataReader["TotalPeso_Tara"].ToString());
        //            oBECompra.TotalPesoNeto = decimal.Parse(oIDataReader["TotalPeso_Neto"].ToString());
        //            oBECompra.Precio = decimal.Parse(oIDataReader["Precio"].ToString());
        //            oBECompra.MontoSubTotal = decimal.Parse(oIDataReader["MontoSubTotal"].ToString());
        //            oBECompra.MontoIGV = decimal.Parse(oIDataReader["MontoIgv"].ToString());
        //            oBECompra.MontoTotal = decimal.Parse(oIDataReader["MontoTotal"].ToString());
        //            oBECompra.TotalDevolucion = (oIDataReader["TotalDevolucion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["TotalDevolucion"].ToString());
        //            oBECompra.TotalAmortizacion = (oIDataReader["TotalAmortizacion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["TotalAmortizacion"].ToString());
        //            oBECompra.TotalSaldo = decimal.Parse(oIDataReader["TotalSaldo"].ToString());
        //            oBECompra.Observacion = oIDataReader["Observacion"].ToString();
        //            oBECompra.IdEstado = oIDataReader["IdEstado"].ToString();
        //            oBECompra.IdCaja = int.Parse(oIDataReader["IdCaja"].ToString());
        //            oBECompra.IdEmpresa = int.Parse(oIDataReader["IdEmpresa"].ToString());
        //            oBECompra.Empresa = oIDataReader["Empresa"].ToString();
        //            oBECompra.IdProducto = int.Parse(oIDataReader["IdProducto"].ToString());
        //            oBECompra.Producto = oIDataReader["Producto"].ToString();
        //            oBECompra.IdCliente = (oIDataReader["IdCliente"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["IdCliente"].ToString());
        //            oBECompra.Cliente = oIDataReader["Cliente"].ToString();
        //            oBECompra.FechaCreacion = Convert.ToDateTime(oIDataReader["FechaCreacion"]).ToShortDateString();
        //            oBECompra.TotalUnidades = (oIDataReader["TotalUnidades"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["TotalUnidades"].ToString());

        //            vLista.Add(oBECompra);
        //        }
        //        return vLista;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!oIDataReader.IsClosed) oIDataReader.Close();
        //        oIDataReader.Dispose();
        //        if (pDatabaseHelper == null) oDatabaseHelper.Dispose();
        //    }
        //}

        //public List<BECompra> ListarCompraMantenimiento(BECompra pBECompra)
        //{
        //    DatabaseHelper oDatabaseHelper = new DatabaseHelper();
        //    List<VistaCompra> vLista = new List<VistaCompra>();
        //    VistaCompra oVistaVenta = null;
        //    IDataReader oIDataReader = null;
        //    try
        //    {
        //        oDatabaseHelper.ClearParameter();
        //        oDatabaseHelper.AddParameter("@intIdVenta", (pBECompra.IdCompra <= 0) ? (object)DBNull.Value : pBECompra.IdVenta);
        //        oDatabaseHelper.AddParameter("@varIdTipoDocumentoVenta", string.IsNullOrEmpty(pBECompra.IdTipoDocumentoVenta) ? (object)DBNull.Value : pBECompra.IdTipoDocumentoVenta);
        //        oDatabaseHelper.AddParameter("@varNumeroDocumento", string.IsNullOrEmpty(pBEVenta.NumeroDocumento) ? (object)DBNull.Value : pBECompra.NumeroDocumento);
        //        oDatabaseHelper.AddParameter("@intIdCliente", (pBECompra.IdCliente <= 0) ? (object)DBNull.Value : pBECompra.IdCliente);
        //        oDatabaseHelper.AddParameter("@intIdProducto", (pBECompra.IdProducto <= 0) ? (object)DBNull.Value : pBECompra.IdProducto);
        //        oDatabaseHelper.AddParameter("@intIdEmpresa", (pBECompra.IdEmpresa <= 0) ? (object)DBNull.Value : pBECompra.IdEmpresa);
        //        oDatabaseHelper.AddParameter("@datFechaInicial", pBECompra.FechaInicio);
        //        oDatabaseHelper.AddParameter("@datFechaFinal", pBECompra.FechaFin);
        //        oDatabaseHelper.AddParameter("@varFilterIdVentas", string.IsNullOrEmpty(pBECompra.strFilterIds) ? (object)DBNull.Value : pBECompra.strFilterIds);
        //        oDatabaseHelper.AddParameter("@TienePrecioVariable", (!pBECompra.TienePrecioVariable) ? (object)DBNull.Value : pBECompra.TienePrecioVariable);


        //        oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_Venta_Cabecera", CommandType.StoredProcedure);

        //        while (oIDataReader.Read())
        //        {
        //            oVistaVenta = new VistaVenta();
        //            oVistaVenta.IdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());
        //            oVistaVenta.IdCliente = (oIDataReader["Id_Cliente"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["Id_Cliente"].ToString());
        //            oVistaVenta.Cliente = oIDataReader["Cliente"].ToString();
        //            oVistaVenta.IdProducto = int.Parse(oIDataReader["Id_Producto"].ToString());
        //            oVistaVenta.Producto = oIDataReader["Producto"].ToString();
        //            oVistaVenta.IdTipoDocumento = (oIDataReader["IdTipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["IdTipoDocumentoVenta"].ToString();
        //            oVistaVenta.TipoDocumento = (oIDataReader["TipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["TipoDocumentoVenta"].ToString();
        //            oVistaVenta.Fecha = Convert.ToDateTime(oIDataReader["FechaCreacion"]);
        //            oVistaVenta.CantidadJavas = Convert.ToInt32(oIDataReader["CantidadJavas"]);
        //            oVistaVenta.TotalPesoBruto = decimal.Parse(oIDataReader["Total_Peso_Bruto"].ToString());
        //            oVistaVenta.TotalPesoTara = decimal.Parse(oIDataReader["Total_Peso_Tara"].ToString());
        //            oVistaVenta.TotalPesoNeto = decimal.Parse(oIDataReader["Total_Peso_Neto"].ToString());
        //            oVistaVenta.Precio = decimal.Parse(oIDataReader["Precio"].ToString());
        //            oVistaVenta.MontoSubTotal = decimal.Parse(oIDataReader["Monto_SubTotal"].ToString());
        //            oVistaVenta.MontoIGV = decimal.Parse(oIDataReader["Monto_Igv"].ToString());
        //            oVistaVenta.MontoTotal = decimal.Parse(oIDataReader["Monto_Total"].ToString());
        //            oVistaVenta.TotalDevolucion = (oIDataReader["Total_Devolucion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Devolucion"].ToString());
        //            oVistaVenta.TotalAmortizacion = (oIDataReader["Total_Amortizacion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Amortizacion"].ToString());
        //            oVistaVenta.TotalSaldo = decimal.Parse(oIDataReader["Total_Saldo"].ToString());
        //            oVistaVenta.Estado = (oIDataReader["Estado"] == (object)DBNull.Value) ? string.Empty : oIDataReader["Estado"].ToString();
        //            oVistaVenta.TotalUnidades = (oIDataReader["TotalUnidades"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["TotalUnidades"].ToString());
        //            oVistaVenta.Margen = (oIDataReader["Margen"] == (object)DBNull.Value) ? (decimal)0.3 : decimal.Parse(oIDataReader["Margen"].ToString());

        //            oVistaVenta.TienePrecioVariable = (oIDataReader["TienePrecioVariable"] == (object)DBNull.Value) ? false : bool.Parse(oIDataReader["TienePrecioVariable"].ToString());

        //            vLista.Add(oVistaVenta);
        //        }
        //        return vLista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!oIDataReader.IsClosed) oIDataReader.Close();
        //        oIDataReader.Dispose();
        //        oDatabaseHelper.Dispose();
        //    }
        //}

        //public List<BEProducto> ListarProductoCliente(int pIdCliente)
        //{
        //    DatabaseHelper oDatabaseHelper = new DatabaseHelper();
        //    List<BEProducto> vLista = new List<BEProducto>();
        //    BEProducto oBEProducto = null;
        //    IDataReader oIDataReader = null;
        //    try
        //    {
        //        oDatabaseHelper.ClearParameter();
        //        oDatabaseHelper.AddParameter("@intIdCliente", pIdCliente);
        //        oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_ProductoClientCompra", CommandType.StoredProcedure);

        //        while (oIDataReader.Read())
        //        {
        //            oBEProducto = new BEProducto();
        //            oBEProducto.IdProducto = int.Parse(oIDataReader["Id_Producto"].ToString());
        //            oBEProducto.Nombre = oIDataReader["Producto"].ToString();
        //            vLista.Add(oBEProducto);
        //        }
        //        return vLista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (!oIDataReader.IsClosed) oIDataReader.Close();
        //        oIDataReader.Dispose();
        //        oDatabaseHelper.Dispose();
        //    }
        //}



        public bool Insertar(BECompra bECompra, out string mensaje)
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
=======

>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4
        /*public List<VistaCompra> ListarCompraMantenimiento(BECompra pBECompra)
        {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            List<VistaVenta> vLista = new List<VistaVenta>();
            VistaVenta oVistaVenta = null;
            IDataReader oIDataReader = null;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@intIdVenta", (pBEVenta.IdVenta <= 0) ? (object)DBNull.Value : pBEVenta.IdVenta);
                oDatabaseHelper.AddParameter("@varIdTipoDocumentoVenta", string.IsNullOrEmpty(pBEVenta.IdTipoDocumentoVenta) ? (object)DBNull.Value : pBEVenta.IdTipoDocumentoVenta);
                oDatabaseHelper.AddParameter("@varNumeroDocumento", string.IsNullOrEmpty(pBEVenta.NumeroDocumento) ? (object)DBNull.Value : pBEVenta.NumeroDocumento);
                oDatabaseHelper.AddParameter("@intIdCliente", (pBEVenta.IdCliente <= 0) ? (object)DBNull.Value : pBEVenta.IdCliente);
                oDatabaseHelper.AddParameter("@intIdProducto", (pBEVenta.IdProducto <= 0) ? (object)DBNull.Value : pBEVenta.IdProducto);
                oDatabaseHelper.AddParameter("@intIdEmpresa", (pBEVenta.IdEmpresa <= 0) ? (object)DBNull.Value : pBEVenta.IdEmpresa);
                oDatabaseHelper.AddParameter("@datFechaInicial", pBEVenta.FechaInicio);
                oDatabaseHelper.AddParameter("@datFechaFinal", pBEVenta.FechaFin);
                oDatabaseHelper.AddParameter("@varFilterIdVentas", string.IsNullOrEmpty(pBEVenta.strFilterIds) ? (object)DBNull.Value : pBEVenta.strFilterIds);
                oDatabaseHelper.AddParameter("@TienePrecioVariable", (!pBEVenta.TienePrecioVariable) ? (object)DBNull.Value : pBEVenta.TienePrecioVariable);


                oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_Venta_Cabecera", CommandType.StoredProcedure);

                while (oIDataReader.Read())
                {
                    oVistaVenta = new VistaVenta();
                    oVistaVenta.IdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());
                    oVistaVenta.IdCliente = (oIDataReader["Id_Cliente"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["Id_Cliente"].ToString());
                    oVistaVenta.Cliente = oIDataReader["Cliente"].ToString();
                    oVistaVenta.IdProducto = int.Parse(oIDataReader["Id_Producto"].ToString());
                    oVistaVenta.Producto = oIDataReader["Producto"].ToString();
                    oVistaVenta.IdTipoDocumento = (oIDataReader["IdTipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["IdTipoDocumentoVenta"].ToString();
                    oVistaVenta.TipoDocumento = (oIDataReader["TipoDocumentoVenta"] == (object)DBNull.Value) ? string.Empty : oIDataReader["TipoDocumentoVenta"].ToString();
                    oVistaVenta.Fecha = Convert.ToDateTime(oIDataReader["FechaCreacion"]);
                    oVistaVenta.CantidadJavas = Convert.ToInt32(oIDataReader["CantidadJavas"]);
                    oVistaVenta.TotalPesoBruto = decimal.Parse(oIDataReader["Total_Peso_Bruto"].ToString());
                    oVistaVenta.TotalPesoTara = decimal.Parse(oIDataReader["Total_Peso_Tara"].ToString());
                    oVistaVenta.TotalPesoNeto = decimal.Parse(oIDataReader["Total_Peso_Neto"].ToString());
                    oVistaVenta.Precio = decimal.Parse(oIDataReader["Precio"].ToString());
                    oVistaVenta.MontoSubTotal = decimal.Parse(oIDataReader["Monto_SubTotal"].ToString());
                    oVistaVenta.MontoIGV = decimal.Parse(oIDataReader["Monto_Igv"].ToString());
                    oVistaVenta.MontoTotal = decimal.Parse(oIDataReader["Monto_Total"].ToString());
                    oVistaVenta.TotalDevolucion = (oIDataReader["Total_Devolucion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Devolucion"].ToString());
                    oVistaVenta.TotalAmortizacion = (oIDataReader["Total_Amortizacion"] == (object)DBNull.Value) ? decimal.Zero : decimal.Parse(oIDataReader["Total_Amortizacion"].ToString());
                    oVistaVenta.TotalSaldo = decimal.Parse(oIDataReader["Total_Saldo"].ToString());
                    oVistaVenta.Estado = (oIDataReader["Estado"] == (object)DBNull.Value) ? string.Empty : oIDataReader["Estado"].ToString();
                    oVistaVenta.TotalUnidades = (oIDataReader["TotalUnidades"] == (object)DBNull.Value) ? 0 : Convert.ToInt32(oIDataReader["TotalUnidades"].ToString());
                    oVistaVenta.Margen = (oIDataReader["Margen"] == (object)DBNull.Value) ? (decimal)0.3 : decimal.Parse(oIDataReader["Margen"].ToString());

                    oVistaVenta.TienePrecioVariable = (oIDataReader["TienePrecioVariable"] == (object)DBNull.Value) ? false : bool.Parse(oIDataReader["TienePrecioVariable"].ToString());

                    vLista.Add(oVistaVenta);
                }
                return vLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!oIDataReader.IsClosed) oIDataReader.Close();
                oIDataReader.Dispose();
                oDatabaseHelper.Dispose();
            }
        }*/
<<<<<<< HEAD
=======

>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4
        public bool Actualizar(BECompra bECompra, out string mensaje)
        {
            throw new NotImplementedException();
        }
<<<<<<< HEAD
=======

>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4

        public List<BECompra> Listar(BECompraFilter pBECompra)
        {
            throw new NotImplementedException();
        }

    }
}
