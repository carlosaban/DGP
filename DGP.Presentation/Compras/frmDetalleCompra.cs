using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities.Compras;
using DGP.Entities;
using DGP.BusinessLogic.Compra;
using DGP.BusinessLogic;

namespace DGP.Presentation.Compras {

    public partial class frmDetalleCompra : Form {
        private int vg_intUnidades = 8;
        private int vg_intIdCompra = 0;
        private int vg_intIdProducto = 0;
        private int vg_intIdCliente = 0;
        private decimal vg_decPrecioCompra = decimal.Zero;
        private decimal vg_decTara = decimal.Zero;
        private string vg_strCantidad = string.Empty;
        private string vg_strPesoJava = string.Empty;
        private string vg_strPesoBruto = string.Empty;
        private string vg_strPesoTara = string.Empty;
        private BECompra vg_BECompra = null;
        private List<VistaAmortizacionCompra> vg_ListaAmortizacionCompra = null;
        DataGridViewCellStyle oCellStyleObservaciones = null;
        DataGridViewCellStyle oCeldaPagoCuenta = null;

        //private dsLineaCompra vg_dsLineaVenta = new dsLineaVenta();
        //private dsLineaVenta vg_dsLineaVentaEliminados = new dsLineaVenta();

        //private dsLineaVenta vg_dsDevolucionVenta = new dsLineaVenta();
        //private dsLineaVenta vg_dsDevolucionVentaEliminados = new dsLineaVenta();

        BLCompra compra = new BLCompra();

        private decimal PrecioInicial = 0;

        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        public frmDetalleCompra(BindingSource bs)
        {
            InitializeComponent();
            CargarProducto();
            CargarEstadoCompra();
            CargarEmpresa();
            this.bsCompraBusq.DataSource = bs.DataSource;
            //  InicializarVenta();
            //InicializarLineaVenta();
        }

        #region "Eventos de frmDetalleCompra"

        private void cmbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbCliente_KeyUp(object sender, KeyEventArgs e)
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
                string actual = cmbCliente.Text;
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
                    cmbCliente.Text = string.Empty;
                    cmbCliente.DataSource = vTemp;
                    cmbCliente.DisplayMember = "Nombre";
                    cmbCliente.ValueMember = "IdCliente";
                    cmbCliente.DroppedDown = true;
                    cmbCliente.Refresh();
                    cmbCliente.Text = actual;
                    if (!string.IsNullOrEmpty(actual))
                    {
                        cmbCliente.Select(actual.Length, 0);
                    }
                    else
                    {
                        cmbCliente.SelectedIndex = -1;
                    }
                }
                else
                {
                    cmbCliente.DroppedDown = false;
                    cmbCliente.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void dgvLineaCompra_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Validar que sea solo para algunas celdas
            try
            {
                if (e.ColumnIndex != ePosicionLineaDS.BotonEliminar.GetHashCode())
                {
                    Point oPoint = dgvLineaCompra.CurrentCellAddress;
                    if (oPoint.X == e.ColumnIndex && oPoint.Y == e.RowIndex && e.Button == MouseButtons.Left && dgvLineaCompra.EditMode != DataGridViewEditMode.EditProgrammatically)
                    {
                        if (!dgvLineaCompra.IsCurrentCellInEditMode)
                        {
                            dgvLineaCompra.BeginEdit(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error); ;
            }

        }

        /*private void dgvLineaCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ePosicionLineaDS.BotonEliminar.GetHashCode() && e.RowIndex >= 0 && !string.IsNullOrEmpty(dgvLineaCompra[ePosicionLineaDS.BotonEliminar.GetHashCode(), e.RowIndex].FormattedValue.ToString()))
            {
                if (vg_dsLineaVenta.DTLineaVenta.Rows.Count > 0)
                {
                    if (vg_dsLineaVenta.DTLineaVenta[e.RowIndex].EsLineaVentaBD())
                    {
                        // Agregar la fila eliminada
                        object[] obj = vg_dsLineaVenta.DTLineaVenta[e.RowIndex].ItemArray;
                        dsLineaVenta.DTLineaVentaRow oLineaVenta = vg_dsLineaVentaEliminados.DTLineaVenta.NewDTLineaVentaRow();
                        oLineaVenta.ItemArray = obj;
                        vg_dsLineaVentaEliminados.DTLineaVenta.AddDTLineaVentaRow(oLineaVenta);
                        vg_dsLineaVentaEliminados.DTLineaVenta.AcceptChanges();
                    }
                    // Eliminar la fila de la tabla
                    vg_dsLineaVenta.DTLineaVenta.RemoveDTLineaVentaRow(vg_dsLineaVenta.DTLineaVenta[e.RowIndex]);
                    //ActualizarLineaVenta();
                    dgrvLineaVentaDS.Refresh();
                }
            }
        }*/

        private void dgvLineaCompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            switch ((ePosicionLineaDS)e.ColumnIndex)
            {
                case ePosicionLineaDS.CantidadJavas:
                    // Validar si existe valor
                    object oCantidad = dgvLineaCompra[ePosicionLineaDS.CantidadJavas.GetHashCode(), e.RowIndex].Value;
                    if (oCantidad == null || string.IsNullOrEmpty(oCantidad.ToString()))
                    {
                        vg_strCantidad = string.Empty;
                    }
                    else
                    {
                        vg_strCantidad = oCantidad.ToString();
                    }
                    break;
                case ePosicionLineaDS.PesoJava:
                    // Validar si existe valor
                    object oPesoJava1 = dgvLineaCompra[ePosicionLineaDS.PesoJava.GetHashCode(), e.RowIndex].Value;
                    if (oPesoJava1 == null || string.IsNullOrEmpty(oPesoJava1.ToString()))
                    {
                        vg_strPesoJava = string.Empty;
                    }
                    else
                    {
                        vg_strPesoJava = oPesoJava1.ToString();
                    }
                    break;
                case ePosicionLineaDS.PesoBruto:
                    // Validar si existe valor
                    object oPesoBruto = dgvLineaCompra[ePosicionLineaDS.PesoBruto.GetHashCode(), e.RowIndex].Value;
                    if (oPesoBruto == null || string.IsNullOrEmpty(oPesoBruto.ToString()))
                    {
                        vg_strPesoBruto = string.Empty;
                    }
                    else
                    {
                        vg_strPesoBruto = oPesoBruto.ToString();
                    }
                    break;
                case ePosicionLineaDS.PesoTara:
                    // Validar si existe valor
                    object oPesoTara = dgvLineaCompra[ePosicionLineaDS.PesoTara.GetHashCode(), e.RowIndex].Value;
                    if (oPesoTara == null || string.IsNullOrEmpty(oPesoTara.ToString()))
                    {
                        vg_strPesoTara = string.Empty;
                    }
                    else
                    {
                        // Calcular Peso tara
                        object oCantJavas = dgvLineaCompra[ePosicionLineaDS.CantidadJavas.GetHashCode(), e.RowIndex].Value;
                        object oPesoJava = dgvLineaCompra[ePosicionLineaDS.PesoJava.GetHashCode(), e.RowIndex].Value;
                        if (!string.IsNullOrEmpty(oCantJavas.ToString()) && !string.IsNullOrEmpty(oPesoJava.ToString()))
                        {
                            vg_strPesoTara = (int.Parse(oCantJavas.ToString()) * decimal.Parse(oPesoJava.ToString())).ToString();
                        }
                        else
                        {
                            vg_strPesoTara = oPesoTara.ToString();
                        }
                    }
                    break;
            }
        }

        /*private void dgvLineaCompra_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strMensaje = string.Empty;
                if (!dgvLineaCompra.Rows[e.RowIndex].IsNewRow)
                {
                    // Validar si es una fila que esta en el DS
                    int intLastIndex = vg_dsLineaVenta.DTLineaVenta.Rows.Count - 1;
                    if (e.RowIndex <= intLastIndex)
                    {
                        if (ValidarCamposLineaVentaDS(ref strMensaje, e.RowIndex, e.ColumnIndex))
                        {
                            vg_dsLineaVenta.DTLineaVenta.AcceptChanges();
                        }
                        else
                        {
                            MostrarMensaje(strMensaje, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        if (!ValidarCamposLineaVentaGridView(ref strMensaje, e.RowIndex, e.ColumnIndex))
                        {
                            MostrarMensaje(strMensaje, MessageBoxIcon.Exclamation);
                        }
                    }
                    dgvLineaCompra.RefreshEdit();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
        }*/


        private void cmbCliente_Leave(object sender, EventArgs e)
        {
            if (this.cmbCliente.SelectedIndex >= 0)
            {
                BEClienteProveedor oBEClienteProveedor = (BEClienteProveedor)this.cmbCliente.SelectedItem;
            }
        }

        #endregion

        #region "Métodos de frmDetalleCompra.aspx"

        private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        {
            MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        }

        private void CargarProducto()
        {
            List<BEProducto> vLista = new List<BEProducto>();
            vLista = new BLProducto().Listar(new BEProducto());
            vLista.Insert(0, new BEProducto(0, "Todos"));
            cmbProducto.DataSource = vLista;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = (cmbProducto.Items.Count > 0) ? 1 : 0;
        }

        private void CargarEstadoCompra()
        {
            BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
            oBEParametroDetalle.IdParametro = eParametro.Estado_Venta.GetHashCode();
            List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
            vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
            cbEstado.DataSource = vLista;
            cbEstado.DisplayMember = "Texto";
            cbEstado.ValueMember = "Valor";
            cbEstado.SelectedIndex = (cbEstado.Items.Count > 0) ? 1 : 0;
        }

        private void CargarEmpresa()
        {
            List<BEEmpresa> vLista = new List<BEEmpresa>();
            vLista = new BLEmpresa().Listar(new BEEmpresa());
            vLista.Insert(0, new BEEmpresa(0, "Seleccione"));
            cmbEmpresa.DataSource = vLista;
            cmbEmpresa.DisplayMember = "RazonSocial";
            cmbEmpresa.ValueMember = "IdEmpresa";
            cmbEmpresa.SelectedIndex = (cmbEmpresa.Items.Count > 0) ? 1 : 0;
        }

        #endregion

        #region "Métodos de Validacìón de frmDetalleCompra.aspx"


        private enum ePosicionLineaDS
        {
            IdLinea = 0,
            FlagJava = 1,
            TaraEditada = 2,
            FlagPesoTara = 3,
            CantidadJavas = 4,
            PesoJava = 5,
            PesoBruto = 6,
            PesoTara = 7,
            PesoNeto = 8,
            Unidades = 9,
            Observacion = 10,
            BotonEliminar = 11
        }

        #endregion


        private void TxtTABToENTER_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}