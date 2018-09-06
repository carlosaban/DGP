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
    public partial class frmMantenimientoComprasV2: Form
    {
        BLCompra blCompra = new BLCompra();

        public frmMantenimientoComprasV2()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void frmMantenimientoCompras_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscarCompras_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            BECompraFilter oBECompraFiltros = ObtenerCompraBusqueda();

            this.bdCompras = new System.Windows.Forms.BindingSource(this.components);

            
            this.bdCompras.DataSource =  blCompra.Listar(oBECompraFiltros);
            this.dgrvCompras.DataSource = null;
            this.dgrvCompras.DataSource = this.bdCompras;


        }

        /**/
        private void InicializarFormulario()
        {
            LimpiarFiltrosBusqueda();
            CargarTipoDocumento();
            CargarProducto();
            CargarPrivilegios();
        }

        private void LimpiarFiltrosBusqueda()
        {
            txtCodigoCompra.Text = string.Empty;
            DGP_Util.LiberarComboBox(cbTipoDocumento);
            DGP_Util.LiberarComboBox(cbProducto);
            DGP_Util.SetDateTimeNow(dtpFechaInicial);
            DGP_Util.SetDateTimeNow(dtpFechaFinal);
            DGP_Util.LiberarGridView(dgrvCompras);
        }

        private void CargarTipoDocumento()
        {
            List<BEParametroDetalle> vLista = new List<BEParametroDetalle>();
            BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
            oBEParametroDetalle.IdParametro = eParametro.TipoDocumento_Venta.GetHashCode();
            vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
            vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
            cbTipoDocumento.DataSource = vLista;
            cbTipoDocumento.DisplayMember = "Texto";
            cbTipoDocumento.ValueMember = "Valor";
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
        private void CargarProducto()
        {
            List<BEProducto> vLista = new List<BEProducto>();
            vLista = new BLProducto().Listar(new BEProducto());
            vLista.Insert(0, new BEProducto(0, "Todos"));
            cbProducto.DataSource = vLista;
            cbProducto.DisplayMember = "Nombre";
            cbProducto.ValueMember = "IdProducto";
        }

        private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        {
            MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        }

        private BECompraFilter ObtenerCompraBusqueda()
        {
            BECompraFilter oBECompra = new BECompraFilter();

            //oBECompra.strFilterIds = txtCodigoVenta.Text;// string.IsNullOrEmpty(txtCodigoVenta.Text) ? 0 : int.Parse(txtCodigoVenta.Text);
            //oBECompra.IdTipoDocumentoVenta = (cbTipoDocumento.SelectedIndex == 0) ? string.Empty : cbTipoDocumento.SelectedValue.ToString();
            oBECompra.IdProveedor = (this.cmbClientes.Text == string.Empty) ? -1 : Convert.ToInt32(cmbClientes.SelectedValue);
            oBECompra.IdProducto = (cbProducto.SelectedIndex == 0) ? 0 : Convert.ToInt32(cbProducto.SelectedValue);
            oBECompra.FechaInicio = dtpFechaInicial.Value.Date;
            oBECompra.FechaFin = dtpFechaFinal.Value.Date;
            return oBECompra;
        }
        /**/



        private void CargarPrivilegios()
        {
            //this.gbCambiarPrecios.Visible = VariablesSession.Privilegios.Exists(t => t.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Act_Precio_Venta_Masivo);
        }

        private void dgrvCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //int IdCompra = (int)this.dgrvCompras["IdCompra", e.RowIndex].Value;
                frmDetalleCompra form = new frmDetalleCompra(this.bdCompras) ;
                form.ShowDialog(this);


               
            }
            catch (Exception ex)
            {
                this.MostrarMensaje(ex.StackTrace, MessageBoxIcon.Error);

            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                frmDetalleCompra form = new frmDetalleCompra();
                form.ShowDialog(this);

            }
            catch (Exception ex)
            {
                this.MostrarMensaje(ex.StackTrace, MessageBoxIcon.Error);

            }


        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //ojo revisar esto
                BLCompra BLDP = new BLCompra();
                List<BECompra> lista = new List<BECompra>();

                foreach (DataGridViewRow dgvRow in this.dgrvCompras.Rows)
                {
                    if (Convert.ToBoolean(dgvRow.Cells["Seleccionado"].Value).Equals(true))
                    {
                        BECompra beCompra = new BECompra();
                        beCompra.IdCompra = Convert.ToInt32(dgvRow.Cells["IdCompra"].Value.ToString());
                        beCompra.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                        beCompra.Observacion = "Eliminado por :" + VariablesSession.BEUsuarioSession.Nombre;
                        BLDP.Eliminar(beCompra);
                    }
                }
                CargarGrilla();

            }
            catch (Exception ex)
            {
                
                this.MostrarMensaje(ex.StackTrace, MessageBoxIcon.Error);

            }



        }

        private void dgrvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgrvCompras.EndEdit();

        }

        
       
    }
}
