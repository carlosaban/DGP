using System;
using System.Collections.Generic;
using System.Text;
using DGP.Entities.Util;
using System.Configuration;
using System.IO;
using DGP.Entities;
using DGP.BusinessLogic.Ventas;
using DGP.Entities.Ventas;

namespace DGP.BusinessLogic.Reporte
{
    public class ReporteWebGuia: IReportWeb 
    {
        private string rutaReporte;
        private BEClienteProveedor beClienteProveedor;
        private List<BEVenta> listaVentas;

        private DateTime fecha;
        public ReporteWebGuia(BEClienteProveedor beClienteProveedor , DateTime fecha ,int pIdVenta) {
            this.rutaReporte = ConfigurationSettings.AppSettings["RutaReportes"] + "\\ReporteGuia.html";
            refrescar(beClienteProveedor, fecha, pIdVenta);
        }
        private void refrescar(BEClienteProveedor beClienteProveedor , DateTime fecha, int pIdVenta )
        {
            this.beClienteProveedor = beClienteProveedor;
            this.fecha = fecha;

            BLLineaVenta bl = new BLLineaVenta();
            listaVentas = bl.Listar(new BEVenta() { IdCliente = this.beClienteProveedor.IdCliente
                                                    , FechaInicio = fecha
                                                    , FechaFin = fecha
                                                    , IdVenta = pIdVenta
                                                    });
          
        
        }
        private string detalleventas() {

            StringBuilder sb = new StringBuilder();
           
            
     
   
            foreach (var venta in listaVentas)
            {
                if (!venta.BEProducto.TieneDetalle) continue;
                sb.AppendFormat(@"
                <table id = 'DetalleCabecera'><!--inicio venta cabecera-->
                <tr>
                    <td style='text-align:left;'>Detalle: {0} - Venta: {1} </td>
                    <td>Devolucion</td>
                </tr>
                <tr><td valign='top' style=''>
                ", venta.Producto,venta.IdVenta);
                sb.Append(@"                
                <table id = 'DetalleProducto'><!--inicio venta detalle--> 
	                <tr>
	                
	                <th>P.B1</th>
	                <th>P.B2</th>              
	                <th>P.B3</th>
	                <th>Jab.</th>
	                <th>Tara</th>
	                
	                </tr>
                ");


                bool primerafila = true;
                foreach (var linea in this.transformarLineaVenta(venta, venta.ListaLineaVenta.FindAll(x => x.EsDevolucion == "N"),3) )
                {
                    string cant1 = (linea.PesoBruto.Count >= 1) ? linea.CantJavasPesoBruto[0].ToString() : string.Empty;
                    string cant2 = (linea.PesoBruto.Count >= 2) ? linea.CantJavasPesoBruto[1].ToString() : string.Empty;
                    string cant3 = (linea.PesoBruto.Count >= 3) ? linea.CantJavasPesoBruto[2].ToString() : string.Empty;

                    string pb1 = (linea.PesoBruto.Count >= 1) ? linea.PesoBruto[0].ToString() : string.Empty;
                    string pb2 = (linea.PesoBruto.Count >= 2) ? linea.PesoBruto[1].ToString() : string.Empty;
                    string pb3 = (linea.PesoBruto.Count >= 3) ? linea.PesoBruto[2].ToString() : string.Empty;

                    string pt1 = (linea.PesoTara.Count >= 1) ? linea.PesoTara[0].ToString() : string.Empty;
                    string pt2 = (linea.PesoTara.Count >= 2) ? linea.PesoTara[1].ToString() : string.Empty;
                    string pt3 = (linea.PesoTara.Count >= 3) ? linea.PesoTara[2].ToString() : string.Empty;

                    sb.Append("<tr>");

                    
                    sb.AppendFormat("<td>{0}{2}{1}</td>", cant1, pb1, (pb1 != string.Empty) ? ")" : string.Empty);

                    sb.AppendFormat("<td>{0}{2}{1}</td>", cant2, pb2, (pb2 != string.Empty) ? ")" : string.Empty);
                    sb.AppendFormat("<td>{0}{2}{1}</td>", cant3, pb3, (pb3 != string.Empty) ? ")" : string.Empty);
                    ///
                    if (primerafila) {
                        int totalJabas = 0;
                        venta.ListaLineaVenta.ForEach(x => totalJabas += ( x.EsDevolucion =="N" )? x.CantidadJavas:0);
                        sb.AppendFormat("<td>&nbsp;{0})</td>", totalJabas);

                        sb.AppendFormat("<td>{0}</td>", venta.TotalPesoTara);
                       // sb.AppendFormat("<td>{0}{2}{1}</td>", cant3, pt3, (pt3 != string.Empty) ? ")" : string.Empty);
                    
                    
                    }
                    sb.Append("</tr>");

                    primerafila = false;
                }
                sb.AppendLine(@"</table><!--fin venta detalle--></td><td valign='top'>");
                sb.Append(@"                
                <table id = 'DetalleDevolucion'><!--inicio devolucion venta --> 
	                <tr>
	                <th>P.B1</th>
	              <!--  <th>P.B2</th>-->
	                <th>P.T1</th>
	              <!--  <th>P.T2</th>-->
	                </tr>
                ");
                //sb.AppendFormat(@"<tr><td 'colspan=7' colspan>Devolucion {0} </td></tr>" , venta.Producto);
                //sb.AppendLine(@"<tr><td 'colspan=7' colspan><hr/></td></tr>");
                foreach (var linea in this.transformarLineaVenta(venta, venta.ListaLineaVenta.FindAll(x => x.EsDevolucion == "S"),1))
                {
                    string cant1 = (linea.PesoBruto.Count >= 1) ? linea.CantJavasPesoBruto[0].ToString() : string.Empty;
                   // string cant2 = (linea.PesoBruto.Count >= 2) ? linea.CantJavasPesoBruto[1].ToString() : string.Empty;
                   // string cant3 = (linea.PesoBruto.Count >= 3) ? linea.CantJavasPesoBruto[2].ToString() : string.Empty;

                    string pb1 = (linea.PesoBruto.Count >= 1) ? linea.PesoBruto[0].ToString() : string.Empty;
                    //string pb2 = (linea.PesoBruto.Count >= 2) ? linea.PesoBruto[1].ToString() : string.Empty;
                    //string pb3 = (linea.PesoBruto.Count >= 3) ? linea.PesoBruto[2].ToString() : string.Empty;

                    string pt1 = (linea.PesoTara.Count >= 1) ? linea.PesoTara[0].ToString() : string.Empty;
                    //string pt2 = (linea.PesoTara.Count >= 2) ? linea.PesoTara[1].ToString() : string.Empty;
                    //string pt3 = (linea.PesoTara.Count >= 3) ? linea.PesoTara[2].ToString() : string.Empty;

                    sb.Append("<tr>");

                   // sb.AppendFormat("<td>{0}</td>", (primerafila) ? linea.Producto : string.Empty);

                    sb.AppendFormat("<td>{0}{2}{1}</td>", cant1, pb1, (pb1 != string.Empty)?")": string.Empty);

                   // sb.AppendFormat("<td>{0}{2}{1}</td>", cant2, pb2, (pb2 != string.Empty) ? ")" : string.Empty);
                   // sb.AppendFormat("<td>{0}{2}{1}</td>", cant3, pb3, (pb3 != string.Empty) ? ")" : string.Empty);

                    sb.AppendFormat("<td>{0}{2}{1}</td>", cant1, pt1, (pt1 != string.Empty) ? ")" : string.Empty);

                   //sb.AppendFormat("<td>{0}{2}{1}</td>", cant2, pt2, (pt2 != string.Empty) ? ")" : string.Empty);
                   // sb.AppendFormat("<td>{0}{2}{1}</td>", cant3, pt3, (pt3 != string.Empty) ? ")" : string.Empty);
                    sb.Append("</tr>");
                }

                sb.Append(@"</table><!--fin devoluciones--></td></tr></table><!--fin venta -->");
                


            }

            sb.Append(@"<hr />                
                <table width = '100%'> 
	                <tr>
	                    <th>Producto</th>
	                    <th>P.Bruto</th>
	                    <th>P.Tara</th>
                        <th>Dev</th>

                        <th>P.Neto</th>
                        <th>Precio</th>
                        <th>Importe</th>
	                </tr>
                    
                ");
            
            decimal total = 0;
            foreach (var venta in this.listaVentas)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td>",venta.Producto);
                sb.AppendFormat("<td>{0}</td>", venta.TotalPesoBruto);
                sb.AppendFormat("<td>{0}</td>", venta.TotalPesoTara);
                sb.AppendFormat("<td>{0}</td>", venta.TotalDevolucion);
                sb.AppendFormat("<td>{0}</td>", venta.TotalPesoNeto);
                sb.AppendFormat("<td>&nbsp;</td>", venta.Precio);
                sb.AppendFormat("<td>{0}</td>", venta.MontoTotal);
                total += venta.MontoTotal;

                sb.AppendLine("</tr>");
            }
            sb.AppendFormat("<tr><td colspan = '7' align = 'right'>Imp. Total : {0}</td></tr></table>", total);
            /*venta.MontoTotal;
                venta.TotalPesoBruto;
                venta.TotalPesoTara;
                venta.TotalPesoNeto;
                venta.TotalDevolucion;
                venta.TotalUnidades*/
            return sb.ToString();
        
        }
        #region IReportWeb Members

        string IReportWeb.generateHTML()
        {
            StringBuilder Sb = new StringBuilder();

            using (StreamReader Reader = new StreamReader(rutaReporte) )
            {
                Sb.Append(Reader.ReadToEnd());
                {
                    Sb.Replace("{CLIENTE}", this.beClienteProveedor.Nombre);

                    Sb.Replace("{FECHA}", this.fecha.ToShortDateString() );
                    string detalleVenta = this.detalleventas();
                    Sb.Replace("{@@DETALLEVENTA}", detalleVenta);
                    Sb.Replace("{@@DETALLEVENTACOPIA}", detalleVenta);

                }
            }
           
            return Sb.ToString();
        }

        #endregion

        public List< vistaLineaVenta> transformarLineaVenta(BEVenta ventas, List<BELineaVenta> lineaVenta, int columnas)

        {
            List<vistaLineaVenta> resultado = new List<vistaLineaVenta>();

            int TotalLineas = lineaVenta.Count / columnas;
            for (int i = 0; i <= TotalLineas; i++)
			{
                int inicio = i * columnas;
                int range = (inicio + columnas > lineaVenta.Count) ? lineaVenta.Count % columnas : columnas;
                vistaLineaVenta vistaLineaVenta = new vistaLineaVenta();
                    
                foreach (var item in lineaVenta.GetRange(inicio, range))
                {
                    
                    vistaLineaVenta.PesoBruto.Add(item.PesoBruto);
                    vistaLineaVenta.PesoTara.Add(item.PesoTara);
                    vistaLineaVenta.CantJavasPesoBruto.Add(item.CantidadJavas);
                    vistaLineaVenta.Producto = ventas.Producto;
                    vistaLineaVenta.EsDevolucion = (item.EsDevolucion == "S");
                    
                }
                resultado.Add(vistaLineaVenta);
                    
			 
			}
            return resultado;
        
        }
        
            
    }
    public class vistaLineaVenta {

        public bool EsDevolucion { get; set; }
        public string Producto { get; set; }
        public List<decimal> PesoBruto = new List<decimal>();
        public List<int> CantJavasPesoBruto = new List<int>();
        public List<decimal> PesoTara = new List<decimal>();
    }
}
