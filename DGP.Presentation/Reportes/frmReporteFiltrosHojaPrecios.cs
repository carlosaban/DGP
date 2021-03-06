using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DGP.Entities.Ventas;
using DGP.BusinessLogic.Ventas;
using DGP.BusinessLogic;
using DGP.Util;
using DGP.Entities.Reportes;
using DGP.Entities.DataSet;


namespace DGP.Presentation.Reportes
{
    public partial class frmReporteFiltrosHojaPrecios : Form
    {
        public frmReporteFiltrosHojaPrecios()
        {
            InitializeComponent();
            cargarDatos();
        }

        private void lblFechaInicial_Click(object sender, EventArgs e)
        {

        }
        public bool cargarDatos()
        {
            
             // bEProductoBindingSource.DataSource = ;
            //bEProductoBindingSource.DataMember = "IdProducto";
            this.chkListProductos.Items.AddRange( (new BLProducto ()).Listar(new DGP.Entities.BEProducto() ).ToArray() );
            ((ListBox)this.chkListProductos).ValueMember = "Nombre";

            //Carga zonas
            this.chkListZonas.Items.AddRange((new BLZona()).Listar(new DGP.Entities.BEZona()).ToArray());
            ((ListBox)this.chkListZonas).ValueMember = "Descripcion";

            this.dtpFechaInicial.Value = VariablesSession.BECaja.Fecha;
            this.dtpFechaFinal.Value = VariablesSession.BECaja.Fecha;


            return true;

        
        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string strListZonas = Cadenas.ListToString(this.chkListProductos.CheckedItems, "");
            string strListProductos = Cadenas.ListToString(this.chkListZonas.CheckedItems, "");
            BEFiltroTablero filtro = new BEFiltroTablero();
            DSRptClientes oDSRpt = new DSRptClientes();
            
            filtro.strListZonas = strListZonas;
            filtro.strListProductos = strListProductos;

            filtro.dtFechaInicio = this.dtpFechaInicial.Value.Date;
            filtro.dtFechaFinal = this.dtpFechaFinal.Value.Date;

            oDSRpt = new BLVenta().ReporteHojaPrecios(filtro);

            CRptHojaPrecios oCRptReporte = new CRptHojaPrecios();
            oCRptReporte.Refresh();
            oCRptReporte.SetDataSource(oDSRpt);
            //oCRptReporte.SetParameterValue("fecha" ,DateTime.Now.Date );
            frmReporteViewer ofrmReporteViewer = new frmReporteViewer(oCRptReporte);
            
            ofrmReporteViewer.MdiParent = this.ParentForm;
            ofrmReporteViewer.Visible = true;
            ofrmReporteViewer.Show();

            


        }
    }
}