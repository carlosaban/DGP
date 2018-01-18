using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities;
using DGP.Entities.Ventas;
using DGP.BusinessLogic;
using DGP.BusinessLogic.Ventas;

namespace DGP.Presentation.Ventas
{
    public partial class frmDocumentoPagoDetalle : Form
    {
        int idCliente;
        decimal montoAplicar;

        public frmDocumentoPagoDetalle(int idCliente, decimal montoAplicar)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            this.montoAplicar = montoAplicar;
        }

        private void frmDocumentoPagoDetalle_Load(object sender, EventArgs e)
        {
            BLDocumentoPago BLDP = new BLDocumentoPago();
            cmbIdVenta.DataSource = BLDP.ListarVentaXCliente(idCliente);

            cmbIdVenta.ValueMember = "MontoTotal";
            cmbIdVenta.DisplayMember = "IdVenta";
            numMontoAplicar.Value = montoAplicar;
        }

        private void cmbIdVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                
            
        }

        private void cmbIdVenta_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdVenta_MouseClick(object sender, MouseEventArgs e)
        {
            numMontoVenta.Value = Convert.ToInt32(cmbIdVenta.SelectedValue);
        }

        private void numMonto_ValueChanged(object sender, EventArgs e)
        {
            if (numMonto.Value > numMontoAplicar.Value)
            {
                MessageBox.Show("DGP", "El monto debe ser menor o igual que el monto a aplicar");
            }
        }
    }
}
