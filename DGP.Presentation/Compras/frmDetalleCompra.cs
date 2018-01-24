using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities.Ventas;
using DGP.Entities;
using DGP.BusinessLogic.Ventas;
using DGP.BusinessLogic;

namespace DGP.Presentation.Ventas {

    public partial class frmDetalleCompra : Form {
        private int vg_intUnidades = 8;
        private int vg_intIdVenta = 0;
        private int vg_intIdProducto = 0;
        private int vg_intIdCliente = 0;
        private decimal vg_decPrecioVenta = decimal.Zero;
        private decimal vg_decTara = decimal.Zero;
        private string vg_strCantidad = string.Empty;
        private string vg_strPesoJava = string.Empty;
        private string vg_strPesoBruto = string.Empty;
        private string vg_strPesoTara = string.Empty;
        private BEVenta vg_BEVenta = null;
        private List<VistaAmortizacion> vg_ListaAmortizacionVenta = null;
        DataGridViewCellStyle oCellStyleObservaciones = null;
        DataGridViewCellStyle oCeldaPagoCuenta = null;

        private dsLineaVenta vg_dsLineaVenta = new dsLineaVenta();
        private dsLineaVenta vg_dsLineaVentaEliminados = new dsLineaVenta();

        private dsLineaVenta vg_dsDevolucionVenta = new dsLineaVenta();
        private dsLineaVenta vg_dsDevolucionVentaEliminados = new dsLineaVenta();

        private decimal PrecioInicial = 0;

        public frmDetalleCompra() {
            InitializeComponent();
        }

        public frmDetalleCompra(int pIdVenta)
        {
            InitializeComponent();
            vg_intIdVenta = pIdVenta;
          //  InicializarVenta();
            //InicializarLineaVenta();
        }

        #region "Eventos de frmDetalleCompra"

     
    

        

  

        #endregion

        #region "Métodos de frmDetalleVenta.aspx"

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

   


  



  




        #endregion

        #region "Métodos de Validacìón de frmDetalleVenta.aspx"

   
 
  
      
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

    }
}