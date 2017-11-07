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

namespace DGP.Presentation.Ventas {

    public partial class frmMantenimientoVentas : Form {
        private System.Windows.Forms.DataGridView dgrvVentas;
            
        public frmMantenimientoVentas() {
            this.dgrvVentas = new System.Windows.Forms.DataGridView();
                    
            InitializeComponent();
            InicializarFormulario();
        }

        #region "Eventos de frmMantenimientoVentas"
        
            private void frmMantenimientoVentas_Load(object sender, EventArgs e) {
                try {
                    this.dgrvVentas.AutoGenerateColumns = false;
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void txtCodigoVenta_KeyPress(object sender, KeyPressEventArgs e) {
                //if (Char.IsNumber(e.KeyChar)) {
                //    e.Handled = false;
                //} else {
                //    e.Handled = true;
                //}
            }

            private void btnBuscarVentas_Click(object sender, EventArgs e) {
                try {
                    BEVenta oBEVenta = ObtenerVentaBusqueda();
                    this.bdVentas.DataSource= new BLVenta().ListarVentaMantenimiento(oBEVenta);
                    this.dgrvVentas.DataSource = this.bdVentas;
                    
                    //dgrvVentas.Refresh();
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void dgrvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
                try {
                    DGP.Entities.Ventas.VistaVenta objIDVenta = (DGP.Entities.Ventas.VistaVenta) this.dgrvVentas.Rows[e.RowIndex].DataBoundItem;
                    frmDetalleVenta frmMantVenta = new frmDetalleVenta(objIDVenta.IdVenta);
                    frmMantVenta.ShowDialog(); 
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

        #endregion

        #region "M�todos de frmMantenimientoVentas"

            private void InicializarFormulario() {
                LimpiarFiltrosBusqueda();
                CargarTipoDocumento();
                CargarProducto();
            }

            private void LimpiarFiltrosBusqueda() {
                txtCodigoVenta.Text = string.Empty;
                DGP_Util.LiberarComboBox(cbTipoDocumento);
                DGP_Util.LiberarComboBox(cbProducto);
                DGP_Util.SetDateTimeNow(dtpFechaInicial);
                DGP_Util.SetDateTimeNow(dtpFechaFinal);
                DGP_Util.LiberarGridView(dgrvVentas);
            }

            private void CargarTipoDocumento() {
                List<BEParametroDetalle> vLista = new List<BEParametroDetalle>();
                BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
                oBEParametroDetalle.IdParametro = eParametro.TipoDocumento_Venta.GetHashCode();
                vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
                vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
                cbTipoDocumento.DataSource = vLista;
                cbTipoDocumento.DisplayMember = "Texto";
                cbTipoDocumento.ValueMember = "Valor";
            }

            //private void CargarCliente() {
            //    List<BEClienteProveedor> vListaCliente = new List<BEClienteProveedor>();
            //    vListaCliente = new BLClienteProveedor().Listar(new BEClienteProveedor());
            //    vListaCliente.Insert(0, new BEClienteProveedor(0, "CE : Cliente Eventual"));
            //    vListaCliente.Insert(0, new BEClienteProveedor(-1, "Todos"));
            //    cbCliente.DataSource = vListaCliente;
            //    cbCliente.DisplayMember = "Nombre";
            //    cbCliente.ValueMember = "IdCliente";
            //}

            private void CargarProducto() {
                List<BEProducto> vLista = new List<BEProducto>();
                vLista = new BLProducto().Listar(new BEProducto());
                vLista.Insert(0, new BEProducto(0, "Todos"));
                cbProducto.DataSource = vLista;
                cbProducto.DisplayMember = "Nombre";
                cbProducto.ValueMember = "IdProducto";
            }

            

            private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon) {
                MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
            }

            private BEVenta ObtenerVentaBusqueda() {
                BEVenta oBEVenta = new BEVenta();

                oBEVenta.strFilterIds = txtCodigoVenta.Text;// string.IsNullOrEmpty(txtCodigoVenta.Text) ? 0 : int.Parse(txtCodigoVenta.Text);

                oBEVenta.IdTipoDocumentoVenta = (cbTipoDocumento.SelectedIndex == 0) ? string.Empty : cbTipoDocumento.SelectedValue.ToString();
                oBEVenta.IdCliente = (this.cmbClientes.Text == string.Empty) ? -1 : Convert.ToInt32(cmbClientes.SelectedValue);
                oBEVenta.IdProducto = (cbProducto.SelectedIndex == 0) ? 0 : Convert.ToInt32(cbProducto.SelectedValue);
                oBEVenta.FechaInicio = dtpFechaInicial.Value.Date;
                oBEVenta.FechaFin = dtpFechaFinal.Value.Date;
                return oBEVenta;
            }

        #endregion

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
                    
                    //MostrarMensaje(oBEClienteProveedor.IdCliente.ToString() + oBEClienteProveedor.Nombre, MessageBoxIcon.Information);

                }
            }


    }
}