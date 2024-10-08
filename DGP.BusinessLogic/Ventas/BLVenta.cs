using System;
using System.Collections.Generic;
using System.Text;

using DBHelper;
using DGP.Entities.DataSet;
using DGP.Entities.Ventas;
using DGP.Entities;
using DGP.DataAccess.Ventas;
using DGP.Entities.Reportes;
using DGP.Entities.Seguridad;

namespace DGP.BusinessLogic.Ventas {

    public class BLVenta {
        public const string ERROR_ID_VENTA = "Error en los Ids de Venta";
        #region "M�todos de BLVenta"

            public List<BEVenta> ListarVentaCliente(BEVenta pBEVenta) {
            try {
            return new DAVenta().ListarVentaCliente(pBEVenta);
            } catch (Exception ex) {
            throw ex;
            }
            }

            public BEVenta ObtenerVenta(int pIdVenta) {
            try {
            return new DAVenta().ObtenerVenta(pIdVenta);
            } catch (Exception ex) {
            throw ex;
            }
            }
            public List<BEVenta> ListarVenta(int pIdVenta, int pIdCaja)
            {
            try
            {
            return new DAVenta().ListarVenta(pIdVenta, pIdCaja, null);
            }
            catch (Exception ex)
            {
            throw ex;
            }
            }
            public List<BEVenta> ListarVenta(int pIdVenta, int pIdCaja, DBHelper.DatabaseHelper pDatabaseHelper)
            {
            try {
            return new DAVenta().ListarVenta(pIdVenta, pIdCaja, pDatabaseHelper);
            } catch (Exception ex) {
            throw ex;
            }
            }

            public List<BEVenta> ListarVenta(int pIdVenta, int pIdCaja, int pIdZona, int pIdProducto, int pIdCliente) {
            try {
                return new DAVenta().ListarVenta(pIdVenta, pIdCaja, pIdZona, pIdProducto, pIdCliente);
            } catch (Exception ex) {
            throw ex;
            }
            }

            public List<BEProducto> ListarProductoCliente(int pIdCliente) {
            try {
                return new DAVenta().ListarProductoCliente(pIdCliente);
            } catch (Exception ex) {
            throw ex;
            }
            }

            public int RegistrarVentaInicialDependiente(BEVenta pBEVenta , bool bAlContado , decimal dMontoAmortizacion ) {
            DatabaseHelper dbh = new DatabaseHelper();
            try {
            int intResultado = 0;    
            dbh.BeginTransaction();
            // Insertar Venta Inicial
            intResultado += new DAVenta().InsertarVentaInicial(ref pBEVenta, dbh);
            // Insertar Lineas de Venta
            int intContador = 0;
            pBEVenta.ListaLineaVenta.ForEach(delegate(BELineaVenta oEntidad) {
                oEntidad.IdVenta = pBEVenta.IdVenta;
                oEntidad.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                intContador += new DALineaVenta().InsertarLineaVentaDependiente(oEntidad, dbh);
            });
            intResultado += (intContador == pBEVenta.ListaLineaVenta.Count) ? 1 : 0;
            // Insertar Venta Final
            intResultado += new DAVenta().InsertarVentaFinal(pBEVenta, dbh);
            //
            //verifica si tiene amortizacion, si es asi 

            if (bAlContado || dMontoAmortizacion>0)
            {

                BEVenta tmppBEVenta = (new BLVenta().ListarVenta(pBEVenta.IdVenta, pBEVenta.IdCaja, dbh))[0];
                BEAmortizacionVenta BEAmortizacion = new BEAmortizacionVenta();
                BEAmortizacion.FechaPago = tmppBEVenta.FechaInicio;
                BEAmortizacion.IdCliente = tmppBEVenta.IdCliente;
                BEAmortizacion.IdEstado = BEAmortizacionVenta.ESTADO_REGISTRADO;
                BEAmortizacion.IdFormaPago = BEAmortizacionVenta.FORMAPAGO_EFECTIVO;
                BEAmortizacion.IdPersonal = pBEVenta.BEUsuarioLogin.IdPersonal;
                BEAmortizacion.IdTipoAmortizacion = BEAmortizacionVenta.TIPOAMORTIZACION_AMORTIZACION;
                BEAmortizacion.IdVenta = pBEVenta.IdVenta;
                BEAmortizacion.Monto = (bAlContado) ? tmppBEVenta.TotalSaldo : dMontoAmortizacion;
                BEAmortizacion.Observacion = "";
                BEAmortizacion.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                List<BEAmortizacionVenta> lista = new List<BEAmortizacionVenta>();
                lista.Add(BEAmortizacion);

                BEDocumento documento = new BEDocumento();
                documento.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                documento.Fecha = tmppBEVenta.FechaInicio;
                documento.IdTipoDocumento = BEDocumento.TIPO_AMORTIZACION_AMR;
                documento.IdFormaPago = "EFE";
                documento.delleAmortizacion = lista;
                documento.Cliente.IdCliente = tmppBEVenta.IdCliente;
                documento.Personal.IdPersonal = pBEVenta.BEUsuarioLogin.IdPersonal;
                documento.Observacion = "cancelado en despacho";
                documento.NumeroReciboPago = string.Empty;
                documento.NumeroOperacion = string.Empty;
                documento.IdBanco = string.Empty;

                new BLAmortizacionVenta().Insertar(documento, dbh);
                //Actualizar la venta
                new DAVenta().InsertarVentaFinal(pBEVenta, dbh);


            } 

             

            if (intResultado == 3) {
                dbh.CommitTransaction();
            } else {
                dbh.RollbackTransaction();
            }
            return intResultado;
            } catch (Exception ex) {
            dbh.RollbackTransaction();
            throw ex;
            } finally {
            dbh.Dispose();
            }
            }

            public int ModificarVentaInicialDependiente(BEVenta pBEVenta, bool bAlContado, decimal dMontoAmortizacion)
            {
            DatabaseHelper dbh = new DatabaseHelper();
            try {
            int intResultado = 0;
            dbh.BeginTransaction();
            // Insertar Lineas de Venta
            int intContador = 0;
            pBEVenta.ListaLineaVenta.ForEach(delegate(BELineaVenta oEntidad) {
                oEntidad.IdVenta = pBEVenta.IdVenta;
                oEntidad.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                intContador += new DALineaVenta().InsertarLineaVentaDependiente(oEntidad, dbh);
            });
            intResultado += (intContador == pBEVenta.ListaLineaVenta.Count) ? 1 : 0;
            // Insertar Venta Final
            intResultado += new DAVenta().InsertarVentaFinal(pBEVenta, dbh);
            //
            //verifica si tiene amortizacion, si es asi 

            if (bAlContado || dMontoAmortizacion > 0)
            {

                BEVenta tmppBEVenta = (new BLVenta().ListarVenta(pBEVenta.IdVenta, pBEVenta.IdCaja, dbh))[0];

                BEAmortizacionVenta BEAmortizacion = new BEAmortizacionVenta();

                BEAmortizacion.FechaPago = tmppBEVenta.FechaInicio;
                BEAmortizacion.IdCliente = tmppBEVenta.IdCliente;
                BEAmortizacion.IdEstado = BEAmortizacionVenta.ESTADO_REGISTRADO;
                BEAmortizacion.IdFormaPago = BEAmortizacionVenta.FORMAPAGO_EFECTIVO;
                BEAmortizacion.IdPersonal = pBEVenta.BEUsuarioLogin.IdPersonal;
                BEAmortizacion.IdTipoAmortizacion = BEAmortizacionVenta.TIPOAMORTIZACION_AMORTIZACION;
                BEAmortizacion.IdVenta = pBEVenta.IdVenta;
                BEAmortizacion.Monto = (bAlContado) ? tmppBEVenta.TotalSaldo : dMontoAmortizacion;
                BEAmortizacion.Observacion = "";
                BEAmortizacion.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                List<BEAmortizacionVenta> lista = new List<BEAmortizacionVenta>();
                lista.Add(BEAmortizacion);

                BEDocumento documento = new BEDocumento();
                documento.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                documento.Fecha = tmppBEVenta.FechaInicio;
                documento.IdTipoDocumento = BEDocumento.TIPO_AMORTIZACION_AMR;
                documento.delleAmortizacion = lista;


                new BLAmortizacionVenta().Insertar(documento, dbh);
                //Actualizar la venta
                new DAVenta().InsertarVentaFinal(pBEVenta, dbh);


            } 
            if (intResultado == 2) {
                dbh.CommitTransaction();
                intResultado++;
            } else {
                dbh.RollbackTransaction();
            }
            return intResultado;
            } catch (Exception ex) {
            dbh.RollbackTransaction();
            throw ex;
            } finally {
            dbh.Dispose();
            }
            }

            public int RegistrarVentaDevolucionDependiente(BEVenta pBEVenta) { 
            DatabaseHelper dbh = new DatabaseHelper();
            try {
            int intResultado = 0;
            dbh.BeginTransaction();
            // Insertar Lineas de Venta
            int intContador = 0;
            pBEVenta.ListaAmortizacion.ForEach(delegate(BELineaVenta oEntidad) {
                oEntidad.IdVenta = pBEVenta.IdVenta;
                oEntidad.BEUsuarioLogin = pBEVenta.BEUsuarioLogin;
                intContador += new DALineaVenta().InsertarLineaVentaDependiente(oEntidad, dbh);
            });
            intResultado += (intContador == pBEVenta.ListaAmortizacion.Count) ? 1 : 0;
            // Insertar Venta Final
            intResultado += new DAVenta().InsertarVentaFinal(pBEVenta, dbh);
            //
            if (intResultado == 2) {
                dbh.CommitTransaction();
            } else {
                dbh.RollbackTransaction();
            }
            return intResultado;
            } catch (Exception ex) {
            dbh.RollbackTransaction();
            throw ex;
            } finally {
            dbh.Dispose();
            }            
            }

            public List<VistaVenta> ListarVentaMantenimiento(BEVenta pBEVenta) {
            try {
            string strMensaje = string.Empty;
            if (!ValidarFiltrosVenta(ref strMensaje, pBEVenta)) throw new Exception(strMensaje);



            return new DAVenta().ListarVentaMantenimiento(pBEVenta);
            } catch (Exception ex) {
            throw ex;
            }
            }

            private bool ValidarFiltrosVenta(ref string strMensaje, BEVenta pBEVenta)
            {
            bool bOk = true;

            string[] strIdVentas = pBEVenta.strFilterIds.Trim().Split(',');
            if (strIdVentas.Length > 0 && ! string.IsNullOrEmpty(strIdVentas[0]) )
            {
            foreach (var item in strIdVentas)
            {
                int tempIdVenta = 0;
                bOk = int.TryParse(item, out tempIdVenta);
                if (!bOk)
                {
                    strMensaje = ERROR_ID_VENTA;
                    return bOk;

                }
            }
            }



            return bOk;
            }


            public bool ValidarVentaSobranteDia(int pIdCaja) {
            try {
            return new DAVenta().ValidarVentaSobranteDia(pIdCaja);
            } catch (Exception ex) {
            throw ex;
            }
            }

            public int ActualizarEstado(int pIdVenta, string pEstado, string pObservacion) {
            try {
            return new DAVenta().ActualizarEstado(pIdVenta, pEstado, pObservacion);
            } catch (Exception ex) {  
            throw ex;
            }
            }

            public DSHojaCobranza ReporteCobranza(VistaVenta pVistaVenta) {
            try {
            return new DAVenta().ReporteCobranza(pVistaVenta);
            } catch (Exception ex) {
            throw ex;
            }
            }
            public DSRptTablero ReporteCobranzaCobrador(BEFiltroTablero pVistaVenta)
            {
            try
            {
            return new DAVenta().ReporteCobranzaCobrador(pVistaVenta);
            }
            catch (Exception ex)
            {
            throw ex;
            }
            }

            public DSRptTablero ReporteTableroVenta(DGP.Entities.Reportes.BEFiltroTablero pVistaVenta)
            {
            try
            {
                return new DAVenta().ReporteTablero(pVistaVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }
            public DSRptTablero ReporteHojaTablero(DGP.Entities.Reportes.BEFiltroTablero pVistaVenta)
            {
            try
            {
            return new DAVenta().ReporteHojaTablero(pVistaVenta);
            }
            catch (Exception ex)
            {
            throw ex;
            }
            }
            public DSRptClientes ReporteHojaPrecios(DGP.Entities.Reportes.BEFiltroTablero pVistaVenta)
            {
            try
            {
            return new DAVenta().ReporteHojaPrecios(pVistaVenta);
            }
            catch (Exception ex)
            {
            throw ex;
            }
            }
            public DSRptClientes ReporteEstadoCuentaCliente(DateTime? fechaInicio , string clientes, bool CanInfoCompra)
            {
            try
            {
            return new DAVenta().ReporteEstadoCuentaClientes(fechaInicio, clientes, CanInfoCompra);
            }
            catch (Exception ex)
            {
            throw ex;
            }
            }
            public void ActualizarPrecioMasivo(int IdVenta, decimal NuevoPrecio)
            {
            DBHelper.DatabaseHelper dbh = new DatabaseHelper();
            try
            {
            DAVenta daVenta = new DAVenta();
            daVenta.InsertarVentaFinalSinReaplicar(new BEVenta() { IdVenta = IdVenta, Precio = NuevoPrecio }, dbh);
            }
            catch(Exception ex){
            throw ex;            
            }
            finally
            {
            dbh.Dispose();
            }

            }
            public BEVenta CrearNotaDebitoXRedondeo(BEPersonal BEUsuarioLogin, int IdCliente, DateTime fecha, int IdPersonal, int TipoNotaDebito)
            {
                try
                {
                    //crear nota de debito
                    decimal vuelto = (new BLAmortizacionVenta()).ObtenerAmortizacionSinAplicar(IdCliente);
                    BLVenta blVenta = new BLVenta();
                    BECaja CajaNotaDebito = new BLCaja().ObtenerCaja(new BECaja()
                    {
                        Fecha = fecha
                        ,
                        IdPersonal = IdPersonal

                    });
                    BEVenta beVenta = new BEVenta()
                    {
                        IdCliente = IdCliente
                        ,
                        IdProducto = TipoNotaDebito  //BEVenta.ID_NOTA_DEBITO
                        ,
                        IdCaja = CajaNotaDebito.IdCaja
                        ,
                        Precio = 1
                        ,
                        IdEmpresa = 1
                        ,
                        BEUsuarioLogin = BEUsuarioLogin
                        ,
                        ListaLineaVenta = new List<BELineaVenta>()
                        ,
                        Observacion = "Nota de Debito"

                    };

                    beVenta.ListaLineaVenta.Add(new BELineaVenta()
                    {
                        EsDevolucion = "N"

                                                                                            ,
                        EsPesoTaraEditado = "N"
                                                                                            ,
                        IdEstado = BEVenta.REGISTRADO
                                                                                            ,
                        Accion = eAccion.Agregar
                                                                                            ,
                        PesoBruto = vuelto
                                                                                            ,
                        PesoNeto = vuelto
                    });


                    int IdNotaDebito = blVenta.RegistrarVentaInicialDependiente(beVenta, false, 0);

                    List<BEVenta> listasResult =  this.ListarVenta(IdNotaDebito, CajaNotaDebito.IdCaja);

                    if (listasResult.Count == 0) throw new Exception("No se genero nota de debito");

                    if (listasResult.Count > 1) throw new Exception("Se creo mas de una nota de debito");

                    return listasResult[0];



                    //crear nota de debito

                }
                catch (Exception Ex)
                {
                    
                    throw;
                }
            
            
            
            }
            public BEVenta CrearDocumentoDebito(decimal monto , BEPersonal BEUsuarioLogin, int IdCliente, DateTime fecha, int IdPersonal, int TipoNotaDebito)
            {
                try
                {
                    //crear nota de debito
                    //decimal vuelto = monto;
                    BLVenta blVenta = new BLVenta();
                    BECaja CajaNotaDebito = new BLCaja().ObtenerCaja(new BECaja()
                    {
                        Fecha = fecha
                        ,
                        IdPersonal = IdPersonal

                    });
                    BEVenta beVenta = new BEVenta()
                    {
                        IdCliente = IdCliente
                        ,
                        IdProducto = TipoNotaDebito  //BEVenta.ID_NOTA_DEBITO
                        ,
                        IdCaja = CajaNotaDebito.IdCaja
                        ,
                        Precio = 1
                        ,
                        IdEmpresa = 1
                        ,
                        BEUsuarioLogin = BEUsuarioLogin
                        ,
                        ListaLineaVenta = new List<BELineaVenta>()
                        ,
                        Observacion = "Nota de Debito"

                    };

                    beVenta.ListaLineaVenta.Add(new BELineaVenta()
                    {
                        EsDevolucion = "N"

                                                                                            ,
                        EsPesoTaraEditado = "N"
                                                                                            ,
                        IdEstado = BEVenta.REGISTRADO
                                                                                            ,
                        Accion = eAccion.Agregar
                                                                                            ,
                        PesoBruto = monto
                                                                                            ,
                        PesoNeto = monto
                    });


                    int IdNotaDebito = blVenta.RegistrarVentaInicialDependiente(beVenta, false, 0);

                    List<BEVenta> listasResult = this.ListarVenta(beVenta.IdVenta, CajaNotaDebito.IdCaja);

                    if (listasResult.Count == 0) throw new Exception("No se genero nota de debito");

                    if (listasResult.Count > 1) throw new Exception("Se creo mas de una nota de debito");

                    return listasResult[0];



                    //crear nota de debito

                }
                catch (Exception Ex)
                {

                    throw;
                }



            }
        #endregion
        public DSReporteCuentasPorCobrar ReporteSaldos()
        {
            try
            {
                DAVenta daVenta = new DAVenta();
                return daVenta.ReporteSaldos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public DSReporteCuentasPorCobrar ReporteHojaCobranzaV2()
        {
            try
            {

                DAVenta daVenta = new DAVenta();

                return daVenta.ReporteHojaCobranzaV2();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DSRptClientes ReporteListaProveedor(DateTime dtfechaInicial, DateTime dtFechaFinal)
        {
            return new DAVenta().ReporteListaPreciosProveedor(dtfechaInicial, dtFechaFinal);
        }
        public DSRptClientes ReporteConsolidado(DateTime dtfechaInicial, DateTime dtFechaFinal)
        {
            return new DAVenta().ReporteConsolidado(dtfechaInicial, dtFechaFinal);

        }
    }
}