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
    public partial class frmRptConsolidado : Form
    {
        public frmRptConsolidado()
        {
            InitializeComponent();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //lbClientes.Items.to

                bool canInfoCompra = VariablesSession.Privilegios.Find(x => x.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Visualizar_Precios_compra)!= null;
                DSRptClientes oDSRptClientes = null; //new BLVenta().ReporteEstadoCuentaCliente(dtpFechaInicial.Value.Date, getClientesList(this.lbClientes), canInfoCompra);
                
                DGP.Entities.Reportes.CRptEstadoCuentaCliente oCRptEstadoCuentaCliente = new DGP.Entities.Reportes.CRptEstadoCuentaCliente();
                oCRptEstadoCuentaCliente.Refresh();
                oCRptEstadoCuentaCliente.SetDataSource(oDSRptClientes);
                this.CRptEstadoCuentaCliente.ReportSource = oCRptEstadoCuentaCliente;
                this.CRptEstadoCuentaCliente.Refresh();

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

         private void frmRptConsolidado_Load(object sender, EventArgs e)
         {

         }

    }
}
