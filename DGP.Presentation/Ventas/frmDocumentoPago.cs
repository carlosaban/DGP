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
    public partial class frmDocumentoPago : Form
    {
        public frmDocumentoPago(BindingSource bs)
        {
            InitializeComponent();

            this.bsDocumentos.DataSource = bs.DataSource;
        }

        private void frmDocumentoPago_Load(object sender, EventArgs e)
        {
            txtIdDocumento.DataBindings.Add("Text", bsDocumentos, "IdDocumento");
            txtTipoDoc.DataBindings.Add("Text", bsDocumentos, "IdTipoDocumento");
            dtFecha.DataBindings.Add("Text", bsDocumentos, "Fecha");
            txtEstado.DataBindings.Add("Text", bsDocumentos, "estado");
            numericUpDown1.DataBindings.Add("Text", bsDocumentos, "Monto");
        }

        private void btnMostrarDetalle_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void listarDetalle()
        {
            BLDocumentoPago BLDP = new BLDocumentoPago();
            int idDocumento = Convert.ToInt32(txtIdDocumento.Text);
            this.bsDetalle.DataSource = BLDP.ListarDetalle(idDocumento);
            this.dgvDetalle.DataSource = this.bsDetalle;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bsDetalle_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }


    }
}
