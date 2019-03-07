using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities;
using DGP.Entities.Compras;
using DGP.BusinessLogic;
using DGP.BusinessLogic.Compra;

namespace DGP.Presentation.Compras
{
    public partial class frmMantenimientoDocumentoPago : Form
    {
        public frmMantenimientoDocumentoPago()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listarDocumentos();
        }

        private void listarDocumentos()
        {
           BLDocumentoPagoCompra BLDP = new BLDocumentoPagoCompra();
           int codigo = Convert.ToInt32(cmbClientes.SelectedValue);
           List<BEDocumentoCompra> lista = BLDP.Listar(codigo, dtFechaInicial.Value.Date, dtFechaFinal.Value.Date);
           this.bsDocumentosPago.DataSource = lista;
           this.dgvDocumentoPago.DataSource = this.bsDocumentosPago;
        }

        private void CmbClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbClientes_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // Do nothing for certain keys, such as navigation keys.
                if (
                    (e.KeyCode == Keys.Escape) ||

                    (e.KeyCode == Keys.Left) ||
                    (e.KeyCode == Keys.Right) ||
                    (e.KeyCode == Keys.Up) ||
                    (e.KeyCode == Keys.Down) ||
                    (e.KeyCode == Keys.PageUp) ||
                    (e.KeyCode == Keys.PageDown) ||
                    (e.KeyCode == Keys.Home) ||
                    (e.KeyCode == Keys.End) ||

                    (e.KeyCode == Keys.Enter) ||

                    (e.KeyCode == Keys.Multiply) ||
                    (e.KeyCode == Keys.Divide) ||
                    (e.KeyCode == Keys.Subtract) ||
                    (e.KeyCode == Keys.Add) ||
                    (e.KeyCode == Keys.NumLock)
                    )
                {
                    e.Handled = true;
                    return;
                }
                string actual = cmbClientes.Text;
                //
                int intIdZona = 0;
                BEClienteProveedor oEntidad = new BEClienteProveedor();

                oEntidad.Nombre = actual;
                oEntidad.IdZona = intIdZona;
                oEntidad.IdCliente = 0;
                List<BEClienteProveedor> vTemp = new BLClienteProveedor().Listar(oEntidad);
                vTemp.Insert(0, new BEClienteProveedor(0, ""));
                if (vTemp != null && vTemp.Count > 0)
                {
                    cmbClientes.Text = string.Empty;
                    cmbClientes.DataSource = vTemp;
                    cmbClientes.DisplayMember = "Nombre";
                    cmbClientes.ValueMember = "IdCliente";
                    cmbClientes.DroppedDown = true;
                    cmbClientes.Refresh();
                    cmbClientes.Text = actual;
                    if (!string.IsNullOrEmpty(actual))
                    {
                        cmbClientes.Select(actual.Length, 0);
                    }
                    else
                    {
                        cmbClientes.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbClientes.DroppedDown = false;
                    cmbClientes.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
        }
        private void cmbClientes_Leave(object sender, EventArgs e)
        {
            if (this.cmbClientes.SelectedIndex >= 0)
            {
                BEClienteProveedor oBEClienteProveedor = (BEClienteProveedor)this.cmbClientes.SelectedItem;


            }
        }
        private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        {
            MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDocumentoPago_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.bsDocumentosPago.EndEdit();

            //BEClienteProveedor cliente = new BEClienteProveedor();
            //cliente.IdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
            //cliente.Nombre = cmbClientes.Text;
            //int cell = dgvDocumentoPago.CurrentRow.Index;
            frmMantenimientoDocumentoPagoDetalle from = new frmMantenimientoDocumentoPagoDetalle(this.bsDocumentosPago);
            from.ShowDialog(this);

        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {//ojo revisar esto
            BLDocumentoPagoCompra BLDP = new BLDocumentoPagoCompra();
            List<BEDocumentoCompra> lista = new List<BEDocumentoCompra>();
            
            foreach (DataGridViewRow dgvRow in dgvDocumentoPago.Rows)
            {
                if(Convert.ToBoolean(dgvRow.Cells["Seleccionado"].Value).Equals(true)){
                    BEDocumentoCompra beDocumento = new BEDocumentoCompra();
                    beDocumento.IdDocumentoCompra = Convert.ToInt32(dgvRow.Cells["IdDocumentoCompra"].Value.ToString());
                    beDocumento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                    beDocumento.Observacion = "";
                    BLDP.EliminarCabecera(beDocumento);
                }
            }
            int codigo = Convert.ToInt32(cmbClientes.SelectedValue);
            this.bsDocumentosPago.DataSource = BLDP.Listar(codigo, dtFechaInicial.Value.Date, dtFechaFinal.Value.Date);
            this.dgvDocumentoPago.DataSource = this.bsDocumentosPago;
            
        }

        private void frmMantenimientoDocumentoPago_Load(object sender, EventArgs e)
        {

        }

        private void tsbAgregar_Click(object sender, EventArgs e)
        {
            //BindingSource bs = this.bsDocumentosPago;
            //BEClienteProveedor cliente = new BEClienteProveedor();
            //cliente.IdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
            //cliente.Nombre = cmbClientes.SelectedText;
            //BindingSource bs = new BindingSource();
            this.bsDocumentosPago.DataSource = new List<BEDocumentoCompra>();
            frmMantenimientoDocumentoPagoDetalle from = new frmMantenimientoDocumentoPagoDetalle(this.bsDocumentosPago);
            from.ShowDialog(this);
        }

        private void dgvDocumentoPago_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDocumentoPago_Leave(object sender, EventArgs e)
        {

        }

        private void dgvDocumentoPago_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvDocumentoPago.EndEdit();

        }

    }
}
