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

        public static decimal montoAplicar;
        String accion;
        int idCliente;
        
        public frmDocumentoPago(BindingSource bs,string accion,int idcliente)
        {
            InitializeComponent();

            this.bsDocumentos.DataSource = bs.DataSource;
            this.accion = accion;
            this.idCliente = idcliente;
        }

       

        private void frmDocumentoPago_Load(object sender, EventArgs e)
        {
            if (accion.Equals("actualizar"))
            {
                txtIdDocumento.DataBindings.Add("Text", bsDocumentos, "IdDocumento");
                dtFecha.DataBindings.Add("Text", bsDocumentos, "Fecha");
                numMonto.DataBindings.Add("Text", bsDocumentos, "Monto");
                listarDetalle();
            }
        }


        private void btnMostrarDetalle_Click(object sender, EventArgs e)
        {
            
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
            Actualizar(accion);
        }


        private void Actualizar(String accion)
        {
            if (accion.Equals("actualizar")) {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();
                documento.IdDocumento = Convert.ToInt32(txtIdDocumento.Text);
                documento.IdTipoDocumento = cmbTipoDocumento.SelectedItem.ToString();
                documento.Fecha = dtFecha.Value.Date;
                documento.Monto = numMonto.Value;
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Cliente.IdCliente = idCliente;
                documento.Observacion = txtObservacion.Text;
                BLDP.ActualizarCabecera(documento);
            }

            if (accion.Equals("insertar"))
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();
                documento.IdTipoDocumento = cmbTipoDocumento.SelectedItem.ToString();
                documento.Fecha = dtFecha.Value.Date;
                documento.Monto = numMonto.Value;
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Cliente.IdCliente = idCliente;
                documento.Observacion = txtObservacion.Text;
                BLDP.InsertarCabecera(documento);
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bsDetalle_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            montoAplicar = numMontoAplica.Value;
            frmDocumentoPagoDetalle from = new frmDocumentoPagoDetalle(idCliente,montoAplicar);
            from.Show();
        }


    }
}
