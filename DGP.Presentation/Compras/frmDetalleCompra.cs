using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities;
using DGP.BusinessLogic;
using DGP.BusinessLogic.Compra;
using DGP.Entities.Compras;

namespace DGP.Presentation.Compras {

    public partial class frmDetalleCompra : Form {

        private BLCompra BLCompra;
        private BindingSource bdSourceCompras;
        public frmDetalleCompra() {
            InitializeComponent();
            BLCompra = new BLCompra(0);
        }

        public frmDetalleCompra(BindingSource bsCompras)
        {
            InitializeComponent();

            bdnCompras.BindingSource = bsCompras;
            BLCompra = new BLCompra( (BECompra)bsCompras.Current);
         
        }

        #region "Eventos de frmDetalleCompra"

     
    

        

  

        #endregion

        #region "Métodos de carga de combobox"

            private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon) {
                MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
            }

            private void CargarEstadoCompra() {
                BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
                oBEParametroDetalle.IdParametro = eParametro.Estado_Venta.GetHashCode();
                List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
                vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
                this.cbEstado.DataSource = vLista;
                cbEstado.DisplayMember = "Texto";
                cbEstado.ValueMember = "Valor";

            }

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
            private void CargarProducto()
            {
                List<BEProducto> vLista = new List<BEProducto>();
                vLista = new BLProducto().Listar(new BEProducto());
                vLista.Insert(0, new BEProducto(0, "Todos"));
                cmbProducto.DataSource = vLista;
                cmbProducto.DisplayMember = "Nombre";
                cmbProducto.ValueMember = "IdProducto";
            }
            private void CargarEmpresa()
            {
                List<BEEmpresa> vLista = new BLEmpresa().Listar(new BEEmpresa());
                
                vLista.Insert(0, new BEEmpresa(0, "Seleccione"));
                cmbEmpresa.DataSource = vLista;
                cmbEmpresa.DisplayMember = "RazonSocial";
                cmbEmpresa.ValueMember = "IdEmpresa";
                cmbEmpresa.SelectedIndex = (cmbEmpresa.Items.Count > 0) ? 1 : 0;

            }
            private void CargarEstadoVenta()
            {
                BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
                oBEParametroDetalle.IdParametro = eParametro.Estado_Venta.GetHashCode();
                List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
                vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
                cmbEmpresa.DataSource = vLista;
                cmbEmpresa.DisplayMember = "Texto";
                cmbEmpresa.ValueMember = "Valor";

            }

        #endregion

        #region "Métodos de Validacion"

   
 
  
      
        #endregion

  
   

        //private void TxtTABToENTER_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        SendKeys.Send("{TAB}");
        //    }   
        //}

      

        
        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptarLineaVenta_Click(object sender, EventArgs e)
        {

        }

       

        

    }
}