using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities;
using DGP.Entities.Ventas;
using DGP.BusinessLogic.Ventas;
using DGP.BusinessLogic;
using DGP.Util;
using DGP.Entities.Reportes;
using DGP.Entities.DataSet;

namespace DGP.Presentation.Reportes
{
    public partial class frmRptPreciosProveedor : Form
    {
        public frmRptPreciosProveedor()
        {
            InitializeComponent();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //lbClientes.Items.to

               // bool canInfoCompra = VariablesSession.Privilegios.Find(x => x.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Visualizar_Precios_compra)!= null;
                DSRptClientes oDSRptClientes = new BLVenta().ReporteListaProveedor(dtpFechaInicial.Value.Date, dtpFechaFinal.Value.Date);

                DGP.Entities.Reportes.CRptHojaPreciosProveedor oCRptHojaPreciosProveedor = new DGP.Entities.Reportes.CRptHojaPreciosProveedor();
                oCRptHojaPreciosProveedor.Refresh();
                oCRptHojaPreciosProveedor.SetDataSource(oDSRptClientes);
                this.CRpt.ReportSource = oCRptHojaPreciosProveedor;
                this.CRpt.Refresh();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private string getClientesList(ListBox plbClientes) {
           
            string resultado = string.Empty;
            foreach (BEClienteProveedor item in plbClientes.Items)
            {
                resultado += "," + item.IdCliente;
            }
            
            return (resultado.Length> 0)? resultado.Substring(1): resultado;
        } 
        
        private void CmbClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

         private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon) {
                MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
            }

         
    
         private void frmReporteEstadoCuentaCliente_Load(object sender, EventArgs e)
         {
             this.dtpFechaInicial.Value = new DateTime (2017,1,1);
         }

         private void frmRptPreciosProveedor_Load(object sender, EventArgs e)
         {

         }

    }
}
