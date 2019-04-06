using System;
using System.Collections.Generic;
using System.Text;

using DBHelper;
using DGP.Entities;
using DGP.Entities.Compras;
using DGP.DataAccess.Compra;

namespace DGP.BusinessLogic.Compra
{
    public class BLCompra
    {
        public BECompra BECompra;

        public BLCompra()
        {
            this.BECompra = new BECompra();
        }

        public BLCompra(int  IdCompra)
        {
            List<BECompraFilter> result = this.Listar(new BECompraFilter() { IdCompra = IdCompra });
            if (result.Count == 0)
            {
                this.BECompra = new BECompra();
            }
            else if (result.Count == 1)
            {
                this.BECompra = result[0];
            }
            else throw new Exception("Existe mas de una registro de compra. Validar  BD");
            
        }

        public BLCompra(BECompra beCompra)
        {
            this.BECompra = beCompra;
        }
        
        public bool ValidarCompra(ref string mensaje) {

            mensaje = string.Empty;
            
           

            return true;
        
        }
        public bool ValidarLineaCompra(ref string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }
         public bool ValidarLineaDevolucion(ref string mensaje) {

            mensaje = string.Empty;
            return true;
        
        }

         public bool addLineaCompra( BELineaCompra beLineaCompra ,  ref string mensaje)
         {
             if ( ValidarLineaCompra(ref mensaje) ) this.BECompra.ListaLineaCompra.Add(beLineaCompra);

             return (mensaje == string.Empty);
         }

         public bool addLineaDevolucion(BELineaCompra beLineaCompra, ref string mensaje)
         {
             if (ValidarLineaDevolucion(ref mensaje)) this.BECompra.ListaDevolucion.Add(beLineaCompra);
             return (mensaje == string.Empty);
         }

        
        public bool Grabar ( ref string mensaje) {
            bool bOK = false;
            if (!this.ValidarCompra(ref mensaje)) return false;

            if (BECompra.IdCompra == 0) bOK = this.Insertar( ref mensaje );
            else bOK = this.Actualizar( ref mensaje);


            return (mensaje == string.Empty);
        
        }
        public  bool Insertar(  ref string mensaje)
        {
            DBHelper.DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            bool bOk = false;    
            try
            {
                oDatabaseHelper.BeginTransaction();


                DGP.BusinessLogic.Ventas.BLDocumentoPago BLDocumentoPago = new DGP.BusinessLogic.Ventas.BLDocumentoPago();

                DGP.Entities.Ventas.BEDocumento BEDocumentoNotaCreditoCompra = new DGP.Entities.Ventas.BEDocumento()
                {

                    IdTipoDocumento = DGP.Entities.Ventas.BEDocumento.TIPO_DOC_NOTACREDITO,
                    Cliente = new BEClienteProveedor() { IdCliente = this.BECompra.IdProveedor },
                    IdFormaPago = DGP.Entities.Ventas.BEDocumento.TIPO_AMR_NCCOMPRA,
                    Monto = this.BECompra.MontoTotal,
                    BEUsuarioLogin = BECompra.Auditoria,
                    Fecha = this.BECompra.Fecha,
                    Observacion = "",
                    Personal = new DGP.Entities.Seguridad.BEPersonal() { IdPersonal = this.BECompra.IdPersonal }

                };
                bOk = BLDocumentoPago.InsertarCabecera(BEDocumentoNotaCreditoCompra , oDatabaseHelper );

                this.BECompra.IdNotaCreditoCompra = BEDocumentoNotaCreditoCompra.IdDocumento;

                bOk = bOk && new DACompra().Insertar(ref mensaje, this.BECompra, oDatabaseHelper);


                DGP.BusinessLogic.Ventas.BLAmortizacionVenta BEAmortizacionVenta = new DGP.BusinessLogic.Ventas.BLAmortizacionVenta();
                
                bOk = bOk && BEAmortizacionVenta.ReaplicarAmortizacion(new DGP.Entities.Ventas.BEVenta()
                {
                    IdCliente = this.BECompra.IdProveedor,
                    BEUsuarioLogin = this.BECompra.BEUsuarioLogin

                }, oDatabaseHelper);
                
                if (bOk)
                {
                    oDatabaseHelper.CommitTransaction();
                }
                else
                {
                    //oDatabaseHelper.RollbackTransaction();
                   throw new Exception("Error al registrar Amortización");

                }

                return bOk;
            }
            catch (Exception ex)
            {
                oDatabaseHelper.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oDatabaseHelper.Dispose();
            }

        }

        public bool  Actualizar(ref string mensaje)
        {
            DBHelper.DatabaseHelper oDatabaseHelper = new DatabaseHelper();
            bool bOk = false;    
            
            try
            {
               // oDatabaseHelper.BeginTransaction();

                DGP.DataAccess.Compra.DACompra DACompra = new DACompra();

                bOk = DACompra.Actualizar(ref mensaje, this.BECompra, oDatabaseHelper);


                if (bOk && this.BECompra.MontoTotal != this.BECompra.MontoTotalBD)
                {
                    DGP.BusinessLogic.Ventas.BLDocumentoPago BLDocumentoPago = new DGP.BusinessLogic.Ventas.BLDocumentoPago();
                    List<DGP.Entities.Ventas.BEDocumento> ListDocVenta = BLDocumentoPago.Listar(new DGP.Entities.Ventas.BEDocumento()
                    {
                        IdDocumento = this.BECompra.IdNotaCreditoCompra

                    }, oDatabaseHelper);
                    if (ListDocVenta.Count != 1) throw new Exception("Error al listar Documento ");
                    DGP.Entities.Ventas.BEDocumento BEDocumento  = ListDocVenta[0];

                    BEDocumento.Monto = this.BECompra.MontoTotal;
                    BEDocumento.BEUsuarioLogin = BECompra.Auditoria;
                   
                    bOk = bOk && BLDocumentoPago.ActualizarCabecera(BEDocumento, oDatabaseHelper);
                    bOk = bOk && (new BusinessLogic.Ventas.BLAmortizacionVenta()).Eliminar(new DGP.Entities.Ventas.BEAmortizacionVenta()
                    {
                        BEUsuarioLogin = BECompra.Auditoria,
                        IdDocumento = this.BECompra.IdNotaCreditoCompra

                    }, oDatabaseHelper);
                    DGP.BusinessLogic.Ventas.BLAmortizacionVenta BlAmortizacionVenta = new DGP.BusinessLogic.Ventas.BLAmortizacionVenta();

                    bOk = bOk && BlAmortizacionVenta.Eliminar(new DGP.Entities.Ventas.BEAmortizacionVenta()
                    {
                        IdDocumento = this.BECompra.IdNotaCreditoCompra,
                    
                    BEUsuarioLogin = BECompra.Auditoria, 
                    Observacion =    "Elimando desde compra"


                    }, oDatabaseHelper);
                    bOk = bOk && BlAmortizacionVenta.ReaplicarAmortizacion(new DGP.Entities.Ventas.BEVenta()
                    {
                        IdCliente = this.BECompra.IdProveedor,
                        BEUsuarioLogin = this.BECompra.BEUsuarioLogin

                    }, oDatabaseHelper);
               
                
                
                
                }
                if (bOk)
                {

                    //if (oDatabaseHelper.Command.Transaction!= null)  oDatabaseHelper.CommitTransaction();
                }
                else
                {
                    //oDatabaseHelper.RollbackTransaction();
                    throw new Exception("Error al registrar Amortización");

                }

                return bOk;
            }
            catch (Exception ex)
            {
                oDatabaseHelper.RollbackTransaction();
                throw ex;
            }
            finally
            {
                oDatabaseHelper.Dispose();
            }
        }
        public bool Eliminar(BECompra beCompra)
        {
            string mensaje = string.Empty;
            try
            {
                return new DACompra().Eliminar(ref mensaje, beCompra);
            }
            catch (Exception ex)
            {
                throw new Exception(mensaje);
            }
        }

        public List<BECompraFilter> Listar(BECompraFilter pBECompra)
        {
            try
            {
                List<BECompraFilter> resultado = new DACompra().Listar(pBECompra);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BECompra getCompra
        {
            get {


                return this.BECompra;
            }
        
        
        }
    }
}
