using System;
using System.Text;

using DBHelper;
using System.Data;
using System.Collections.Generic;
using DGP.Entities.Ventas;
using DGP.Entities;

namespace DGP.DataAccess.Ventas {

    public class DALineaVenta {

        public List<BELineaVenta> Listar(BELineaVenta pBELineaVenta) {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            List<BELineaVenta> vLista = new List<BELineaVenta>();
            IDataReader oIDataReader = null;
            BELineaVenta oBELineaVenta = null;
            try {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdVenta", pBELineaVenta.IdVenta);
                oDatabaseHelper.AddParameter("@intIdLineaVenta", (pBELineaVenta.IdLineaVenta <= 0) ? (object)DBNull.Value : pBELineaVenta.IdLineaVenta);
                oDatabaseHelper.AddParameter("@chrEsDevolucion", string.IsNullOrEmpty(pBELineaVenta.EsDevolucion) ? (object)DBNull.Value : pBELineaVenta.EsDevolucion);
                oDatabaseHelper.AddParameter("@varIdEstado", string.IsNullOrEmpty(pBELineaVenta.IdEstado) ? (object)DBNull.Value : pBELineaVenta.IdEstado);
                oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_LineaVenta", CommandType.StoredProcedure);

                while (oIDataReader.Read()) {
                    oBELineaVenta = new BELineaVenta();
                    oBELineaVenta.IdLineaVenta = int.Parse(oIDataReader["Id_Linea_Venta"].ToString());
                    oBELineaVenta.CantidadJavas = int.Parse(oIDataReader["Cantidad_Javas"].ToString());
                    oBELineaVenta.FlagJava = oIDataReader["FlagTara"].ToString();
                    oBELineaVenta.TaraEditada = decimal.Parse(oIDataReader["Tara"].ToString());
                    oBELineaVenta.PesoJava = decimal.Parse(oIDataReader["Tara"].ToString());
                    oBELineaVenta.PesoBruto = decimal.Parse(oIDataReader["Peso_Bruto"].ToString());
                    oBELineaVenta.PesoTara = decimal.Parse(oIDataReader["Peso_Tara"].ToString());
                    oBELineaVenta.PesoNeto = decimal.Parse(oIDataReader["Peso_Neto"].ToString());
                    oBELineaVenta.EsDevolucion = oIDataReader["EsDevolucion"].ToString();
                    oBELineaVenta.EsPesoTaraEditado = oIDataReader["EsPesoTaraEditado"].ToString();
                    oBELineaVenta.Observacion = oIDataReader["Observacion"].ToString();
                    oBELineaVenta.IdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());
                    oBELineaVenta.Accion = eAccion.BaseDatos;
                    vLista.Add(oBELineaVenta);
                }

                return vLista;

            } catch (Exception ex) {
                throw ex;
            } finally {
                if (!oIDataReader.IsClosed) oIDataReader.Close();
                oIDataReader.Dispose();
                oDatabaseHelper.Dispose();
            }
        }

        public List<BEVenta> Listar(BEVenta pVenta)
        {

            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            List<BEVenta> vLista = new List<BEVenta>();
            IDataReader oIDataReader = null;
            BELineaVenta oBELineaVenta = null;
            try
            {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdVenta", (pVenta.IdVenta <= 0) ? (object)DBNull.Value : pVenta.IdVenta);
                oDatabaseHelper.AddParameter("@FechaInicial", (pVenta.FechaInicio == DateTime.MinValue) ? (object)DBNull.Value : pVenta.FechaInicio);
                oDatabaseHelper.AddParameter("@FechaFinal", (pVenta.FechaFin == DateTime.MinValue) ? (object)DBNull.Value : pVenta.FechaFin);

                oDatabaseHelper.AddParameter("@IdCliente",( pVenta.IdCliente <= 0 )? (object)DBNull.Value : pVenta.IdCliente);
                oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_detalleLineaVenta", CommandType.StoredProcedure);

                while (oIDataReader.Read())
                {
                    int tmpIdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());
                    BEVenta beVenta;
                    if (vLista.Exists(x => x.IdVenta == tmpIdVenta))
                    {
                        beVenta = vLista.Find(x => x.IdVenta == tmpIdVenta);
                    }
                    else
                    {
                        beVenta = new BEVenta();
                        beVenta.IdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());
                        beVenta.ClienteEventual = oIDataReader["ClienteEventual"].ToString();
                        beVenta.MontoTotal = decimal.Parse(oIDataReader["Monto_Total"].ToString());
                        beVenta.TotalDevolucion = decimal.Parse(oIDataReader["Total_Devolucion"].ToString());
                        beVenta.TotalPesoBruto = decimal.Parse(oIDataReader["Total_Peso_Bruto"].ToString());
                        beVenta.TotalPesoTara = decimal.Parse(oIDataReader["Total_Peso_Tara"].ToString());
                        beVenta.TotalPesoNeto = decimal.Parse(oIDataReader["Total_Peso_Neto"].ToString());

                        beVenta.IdProducto = int.Parse(oIDataReader["Id_Producto"].ToString());

                        beVenta.TotalUnidades = int.Parse(oIDataReader["TotalUnidades"].ToString());
                        beVenta.Cliente = oIDataReader["CLIENTE"].ToString();
                        beVenta.Producto = oIDataReader["PRODUCTO"].ToString();
                        beVenta.BEProducto = new BEProducto()
                        {
                            IdProducto = beVenta.IdProducto,
                            TieneDetalle = bool.Parse( oIDataReader["TieneDetalle"].ToString()),
                            Nombre = oIDataReader["PRODUCTO"].ToString()
                        };
                       
                        
                        beVenta.IdVenta = int.Parse(oIDataReader["Id_Venta"].ToString());

                        vLista.Add(beVenta);
                    }

                    oBELineaVenta = new BELineaVenta();
     
                    oBELineaVenta.IdLineaVenta = int.Parse(oIDataReader["Id_Linea_Venta"].ToString());
                    oBELineaVenta.CantidadJavas = int.Parse(oIDataReader["Cantidad_Javas"].ToString());
                    //oBELineaVenta.FlagJava = oIDataReader["FlagTara"].ToString();
                    //oBELineaVenta.TaraEditada = decimal.Parse(oIDataReader["Tara"].ToString());
                    //oBELineaVenta.PesoJava = decimal.Parse(oIDataReader["Tara"].ToString());
                    oBELineaVenta.PesoBruto = decimal.Parse(oIDataReader["Peso_Bruto"].ToString());
                    oBELineaVenta.PesoTara = decimal.Parse(oIDataReader["Peso_Tara"].ToString());
                    oBELineaVenta.PesoNeto = decimal.Parse(oIDataReader["Peso_Neto"].ToString());
                    oBELineaVenta.EsDevolucion = oIDataReader["EsDevolucion"].ToString();
                    oBELineaVenta.EsPesoTaraEditado = oIDataReader["EsPesoTaraEditado"].ToString();
                    oBELineaVenta.Observacion = oIDataReader["Observacion"].ToString();
                    oBELineaVenta.IdVenta = tmpIdVenta;
                    //oBELineaVenta.Accion = eAccion.BaseDatos;
                    oBELineaVenta.IdEstado = oIDataReader["IdEstado"].ToString();
                    oBELineaVenta.IdEstado = oIDataReader["unidades"].ToString();
                    
                    beVenta.ListaLineaVenta.Add(oBELineaVenta);
                    
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
        }


        public dsLineaVenta ListarDS(BELineaVenta pBELineaVenta) {
            DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            dsLineaVenta oDsLineaVenta = new dsLineaVenta();
            IDataReader oIDataReader = null;
            try {
                oDatabaseHelper.ClearParameter();
                oDatabaseHelper.AddParameter("@IdVenta", pBELineaVenta.IdVenta);
                oDatabaseHelper.AddParameter("@intIdLineaVenta", (pBELineaVenta.IdLineaVenta <= 0) ? (object)DBNull.Value : pBELineaVenta.IdLineaVenta);
                oDatabaseHelper.AddParameter("@chrEsDevolucion", string.IsNullOrEmpty(pBELineaVenta.EsDevolucion) ? (object)DBNull.Value : pBELineaVenta.EsDevolucion);
                oDatabaseHelper.AddParameter("@varIdEstado", string.IsNullOrEmpty(pBELineaVenta.IdEstado) ? (object)DBNull.Value : pBELineaVenta.IdEstado);
                oIDataReader = oDatabaseHelper.ExecuteReader("DGP_Listar_LineaVenta", CommandType.StoredProcedure);
                while (oIDataReader.Read()) {
                    oDsLineaVenta.DTLineaVenta.AddDTLineaVentaRow(
                                        Convert.ToInt32(oIDataReader["Id_Linea_Venta"])
                                        ,Convert.ToInt32(oIDataReader["Cantidad_Javas"]).ToString()
                                        ,oIDataReader["FlagTara"].ToString()
                                        ,Convert.ToDecimal(oIDataReader["Tara"]).ToString()
                                        ,Convert.ToDecimal(oIDataReader["Tara"]).ToString()
                                        ,Convert.ToDecimal(oIDataReader["Peso_Bruto"]).ToString()
                                        ,Convert.ToDecimal(oIDataReader["Peso_Tara"]).ToString()
                                        ,Convert.ToDecimal(oIDataReader["Peso_Neto"]).ToString()
                                        ,oIDataReader["EsPesoTaraEditado"].ToString()
                                        ,oIDataReader["Observacion"].ToString()
                                        ,Convert.ToInt32(oIDataReader["Id_Venta"])
                                        ,eAccion.BaseDatos.GetHashCode()
                                        ,oIDataReader["IdEstado"].ToString()
                                        ,oIDataReader["EsDevolucion"].ToString()
                                        , (oIDataReader["Unidades"] == (object)DBNull.Value) ? 0 : int.Parse(oIDataReader["Unidades"].ToString())
                                        
                                        
                                        );
                }

                oDsLineaVenta.DTLineaVenta.AcceptChanges();
                return oDsLineaVenta;
            } catch (Exception ex) {
                throw ex;
            } finally {
                oDatabaseHelper.Dispose();
            }
        }

        public int InsertarLineaVentaDependiente(BELineaVenta pBELineaVenta, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@decPesoTara", pBELineaVenta.PesoTara);
                pDatabaseHelper.AddParameter("@decPesoBruto", pBELineaVenta.PesoBruto);
                pDatabaseHelper.AddParameter("@decPesoNeto", pBELineaVenta.PesoNeto);
                pDatabaseHelper.AddParameter("@intCantidadJavas", pBELineaVenta.CantidadJavas);
                pDatabaseHelper.AddParameter("@chrEsdevolucion", pBELineaVenta.EsDevolucion);
                pDatabaseHelper.AddParameter("@chrEsPesoTaraEditado", pBELineaVenta.EsPesoTaraEditado);
                pDatabaseHelper.AddParameter("@decTaraEditada", (pBELineaVenta.TaraEditada == decimal.MinValue) ? (object)DBNull.Value : pBELineaVenta.TaraEditada);
                pDatabaseHelper.AddParameter("@varObservacion", pBELineaVenta.Observacion);
                pDatabaseHelper.AddParameter("@varIdEstado", pBELineaVenta.IdEstado);
                pDatabaseHelper.AddParameter("@intIdVenta", pBELineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdPersonal", pBELineaVenta.BEUsuarioLogin.IdPersonal);
                pDatabaseHelper.AddParameter("@intUnidades", pBELineaVenta.Unidades);
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Insertar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int ModificarLineaVentaDependiente2(BELineaVenta pBELineaVenta, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@intIdVenta", pBELineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdLineaVenta", pBELineaVenta.IdLineaVenta);
                pDatabaseHelper.AddParameter("@decPesoTara", pBELineaVenta.PesoTara);
                pDatabaseHelper.AddParameter("@decPesoBruto", pBELineaVenta.PesoBruto);
                pDatabaseHelper.AddParameter("@decPesoNeto", pBELineaVenta.PesoNeto);
                pDatabaseHelper.AddParameter("@intCantidadJavas", pBELineaVenta.CantidadJavas);
                pDatabaseHelper.AddParameter("@chrEsdevolucion", pBELineaVenta.EsDevolucion);
                pDatabaseHelper.AddParameter("@chrEsPesoTaraEditado", pBELineaVenta.EsPesoTaraEditado);
                pDatabaseHelper.AddParameter("@decTaraEditada", (pBELineaVenta.TaraEditada == decimal.MinValue) ? (object)DBNull.Value : pBELineaVenta.TaraEditada);
                pDatabaseHelper.AddParameter("@varObservacion", pBELineaVenta.Observacion);
                pDatabaseHelper.AddParameter("@intIdPersonal", pBELineaVenta.BEUsuarioLogin.IdPersonal);
                pDatabaseHelper.AddParameter("@intUnidades", pBELineaVenta.Unidades);
                
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Modificar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int EliminarLineaVentaDependiente2(BELineaVenta pBELineaVenta, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@intIdVenta", pBELineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdLineaVenta", (pBELineaVenta.IdLineaVenta <= 0) ? (object)DBNull.Value : pBELineaVenta.IdLineaVenta);
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Eliminar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }
        

        public int InsertarLineaVentaDependiente(dsLineaVenta.DTLineaVentaRow pDRLineaVenta, int pIdPersonal, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@decPesoTara", pDRLineaVenta.PesoTara);
                pDatabaseHelper.AddParameter("@decPesoBruto", pDRLineaVenta.PesoBruto);
                pDatabaseHelper.AddParameter("@decPesoNeto", pDRLineaVenta.PesoNeto);
                pDatabaseHelper.AddParameter("@intCantidadJavas", pDRLineaVenta.CantidadJavas);
                pDatabaseHelper.AddParameter("@chrEsdevolucion", pDRLineaVenta.EsDevolucion);
                pDatabaseHelper.AddParameter("@chrEsPesoTaraEditado", pDRLineaVenta.EsPesoTaraEditado);
                pDatabaseHelper.AddParameter("@decTaraEditada", (pDRLineaVenta.FlagJava == "N") ? (object)DBNull.Value : pDRLineaVenta.TaraEditada);
                pDatabaseHelper.AddParameter("@varObservacion", pDRLineaVenta.Observacion);
                pDatabaseHelper.AddParameter("@varIdEstado", pDRLineaVenta.IdEstado);
                pDatabaseHelper.AddParameter("@intIdVenta", pDRLineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdPersonal", pIdPersonal);
                pDatabaseHelper.AddParameter("@intUnidades", pDRLineaVenta.Unidades);
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Insertar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int ModificarLineaVentaDependiente(dsLineaVenta.DTLineaVentaRow pDRLineaVenta, int pIdPersonal, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@intIdVenta", pDRLineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdLineaVenta", pDRLineaVenta.IdLineaVenta);
                pDatabaseHelper.AddParameter("@decPesoTara", pDRLineaVenta.PesoTara);
                pDatabaseHelper.AddParameter("@decPesoBruto", pDRLineaVenta.PesoBruto);
                pDatabaseHelper.AddParameter("@decPesoNeto", pDRLineaVenta.PesoNeto);
                pDatabaseHelper.AddParameter("@intCantidadJavas", pDRLineaVenta.CantidadJavas);
                pDatabaseHelper.AddParameter("@chrEsdevolucion", pDRLineaVenta.EsDevolucion);
                pDatabaseHelper.AddParameter("@chrEsPesoTaraEditado", pDRLineaVenta.EsPesoTaraEditado);
                pDatabaseHelper.AddParameter("@decTaraEditada", (pDRLineaVenta.FlagJava == "N") ? (object)DBNull.Value : pDRLineaVenta.TaraEditada);
                pDatabaseHelper.AddParameter("@varObservacion", pDRLineaVenta.Observacion);
                pDatabaseHelper.AddParameter("@intIdPersonal", pIdPersonal);
                pDatabaseHelper.AddParameter("@intUnidades", pDRLineaVenta.Unidades);
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Modificar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public int EliminarLineaVentaDependiente(dsLineaVenta.DTLineaVentaRow pDRLineaVenta, DatabaseHelper pDatabaseHelper) {
            int vResultado = 0;
            try {
                pDatabaseHelper.ClearParameter();
                pDatabaseHelper.AddParameter("@intIdVenta", pDRLineaVenta.IdVenta);
                pDatabaseHelper.AddParameter("@intIdLineaVenta", (pDRLineaVenta.IdLineaVenta <= 0) ? (object)DBNull.Value : pDRLineaVenta.IdLineaVenta);
                vResultado = pDatabaseHelper.ExecuteNonQuery("DGP_Eliminar_LineaVenta", CommandType.StoredProcedure, DBHelper.ConnectionState.KeepOpen);
                return vResultado;
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}