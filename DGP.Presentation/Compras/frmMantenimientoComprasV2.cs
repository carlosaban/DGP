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
            try
            {
                
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
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
            this.bdCompras.DataSource = blCompra.Listar(oBECompraFiltros);
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

        //private void CargarCliente() {
        //    List<BEClienteProveedor> vListaCliente = new List<BEClienteProveedor>();
        //    vListaCliente = new BLClienteProveedor().Listar(new BEClienteProveedor());
        //    vListaCliente.Insert(0, new BEClienteProveedor(0, "CE : Cliente Eventual"));
        //    vListaCliente.Insert(0, new BEClienteProveedor(-1, "Todos"));
        //    cbCliente.DataSource = vListaCliente;
        //    cbCliente.DisplayMember = "Nombre";
        //    cbCliente.ValueMember = "IdCliente";
        //}

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

            oBECompra.IdCompra = int.Parse(txtCodigoCompra.Text);// string.IsNullOrEmpty(txtCodigoVenta.Text) ? 0 : int.Parse(txtCodigoVenta.Text);
            oBECompra.IdTipoDocumentoCompra = cbTipoDocumento.SelectedIndex.ToString();
            oBECompra.IdProveedor = cmbClientes.SelectedIndex;
            oBECompra.IdProducto = cbProducto.SelectedIndex;
            oBECompra.FechaInicio = dtpFechaInicial.Value.Date;
            oBECompra.FechaFin = dtpFechaFinal.Value.Date;

            return oBECompra;
        }
        /**/

        private void cmbClientes_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CargarPrivilegios()
        {
            //this.gbCambiarPrecios.Visible = VariablesSession.Privilegios.Exists(t => t.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Act_Precio_Venta_Masivo);
        }

        private void dgrvCompras_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmDetalleCompra detalle = new frmDetalleCompra(bdCompras);
            detalle.ShowDialog();
        }
    }
}
