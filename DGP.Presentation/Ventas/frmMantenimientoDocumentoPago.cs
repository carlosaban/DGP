﻿using System;
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
           BLDocumentoPago BLDP = new BLDocumentoPago();
           int codigo = Convert.ToInt32(cmbClientes.SelectedValue);
           this.bsDocumentosPagoVenta.DataSource = BLDP.Listar(codigo, dtFechaInicial.Value.Date, dtFechaFinal.Value.Date);
           this.dgvDocumentoPago.DataSource = this.bsDocumentosPagoVenta;
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
            BEClienteProveedor cliente = new BEClienteProveedor();
            cliente.IdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
            cliente.Nombre = cmbClientes.Text;
            int cell = dgvDocumentoPago.CurrentRow.Index;
            frmDocumentoPago from = new frmDocumentoPago(this.bsDocumentosPagoVenta, "actualizar", cliente,cell);
            from.Show();

        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {//ojo revisar esto
<<<<<<< HEAD
            BLDocumentoPago BLDP = new BLDocumentoPago();
            List<BEDocumento> lista = new List<BEDocumento>();
            
            foreach (DataGridViewRow dgvRow in dgvDocumentoPago.Rows)
            {
                if(Convert.ToBoolean(dgvRow.Cells["Seleccionado"].Value).Equals(true)){
                    BEDocumento beDocumento = new BEDocumento();
                    beDocumento.IdDocumento = Convert.ToInt32(dgvRow.Cells["idDocumentoDataGridViewTextBoxColumn"].Value.ToString());
<<<<<<< HEAD
=======
=======
            try
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();


                for (int i = 0; i < this.dgvDocumentoPago.RowCount; i++)
                {
                    bool Selecionado = (dgvDocumentoPago["Seleccionado", i].Value == null) ? false : (bool) dgvDocumentoPago["Seleccionado", i].Value;
                    if (!Selecionado) continue;

                    BEDocumento beDocumento = new BEDocumento();
                    beDocumento.IdDocumento = Convert.ToInt32(dgvDocumentoPago["IdDocumento" , i].Value.ToString());
>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4
>>>>>>> cb12352013250c1275d17ffec9809e72fe04bab7
                    beDocumento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                    beDocumento.Observacion = "";
                    BLDP.EliminarCabecera(beDocumento);
                }
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> cb12352013250c1275d17ffec9809e72fe04bab7
            }
            int codigo = Convert.ToInt32(cmbClientes.SelectedValue);
            this.bsDocumentosPagoVenta.DataSource = BLDP.Listar(codigo, dtFechaInicial.Value.Date, dtFechaFinal.Value.Date);
            this.dgvDocumentoPago.DataSource = this.bsDocumentosPagoVenta;
=======
                


                CargarDocumentos();

            }
            catch (Exception ex)
            {
                
                MostrarMensaje( ex.Message ,MessageBoxIcon.Error);
            }
>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4
            
        }

        private void frmMantenimientoDocumentoPago_Load(object sender, EventArgs e)
        {


        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

>>>>>>> b6ee30bea750bd095abc945472cf2a773f2210c4
>>>>>>> cb12352013250c1275d17ffec9809e72fe04bab7
        private void tsbAgregar_Click(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            BEClienteProveedor cliente = new BEClienteProveedor();
            cliente.IdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
            cliente.Nombre = cmbClientes.SelectedText;
            frmDocumentoPago from = new frmDocumentoPago(bs, "insertar", cliente);
            from.Show();
        }
        private void CargarDocumentos()
        {
            int codigo = Convert.ToInt32(cmbClientes.SelectedValue);
            this.bsDocumentosPagoVenta.DataSource = (new BLDocumentoPago()).Listar(codigo, dtFechaInicial.Value.Date, dtFechaFinal.Value.Date);
            this.dgvDocumentoPago.DataSource = this.bsDocumentosPagoVenta;
            
 
        
        
        }

        private void dgvDocumentoPago_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDocumentoPago_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dgvDocumentoPago.Columns["Seleccionado"].Index)
            {
                dgvDocumentoPago.EndEdit();
            }
        }

    }
}
