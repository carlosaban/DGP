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
            CargarProducto();
            CargarEstadoCompra();
            BLCompra = new BLCompra();
        }

        public frmDetalleCompra(BindingSource bsCompras)
        {
            InitializeComponent();
            CargarProducto();
            CargarEstadoCompra();
            bdnCompras.BindingSource = bsCompras ;
            this.bdSourceCompras = bsCompras;
            BLCompra = new BLCompra( (BECompra)bsCompras.Current);
            //setBindings();
            
            cargarCliente();

            this.cmbClientes.Enabled = false;

        }
        
        private void cargarCliente()
        {
            
            List<BEClienteProveedor> vTemp = new BLClienteProveedor().Listar(new BEClienteProveedor (){ IdCliente = this.BLCompra.BECompra.IdProveedor});

            this.cmbClientes.DataSource = vTemp;
            this.cmbClientes.DisplayMember = "Nombre";
            this.cmbClientes.ValueMember = "IdCliente";
                        
            if (vTemp.Count > 0) this.cmbClientes.SelectedIndex = 0;
        
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
                //vLista.Insert(0, new BEParametroDetalle("0", "Todos"));
                this.cbEstado.DataSource = vLista;
                cbEstado.DisplayMember = "Texto";
                cbEstado.ValueMember = "Valor";
                
                //cbEstado.SelectedValue= "REG"
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
            setBindings();

        }

        private void setBindings()
        {
            if (this.bdSourceCompras != null)
            {

            this.lblIDCompra.DataBindings.Clear();
            this.lblIDCompra.DataBindings.Add("Text", this.bdSourceCompras, "IdCompra");

            this.dtFecha.DataBindings.Clear();
            this.dtFecha.DataBindings.Add("Value", this.bdSourceCompras, "Fecha");

            this.cmbClientes.DataBindings.Clear();
            this.cmbClientes.DataBindings.Add("SelectedValue", this.bdSourceCompras, "IdProveedor", true, DataSourceUpdateMode.OnPropertyChanged);

            this.cmbProducto.DataBindings.Clear();
            this.cmbProducto.DataBindings.Add("SelectedValue", this.bdSourceCompras, "IdProducto");

            this.nudPrecio.DataBindings.Clear();
            this.nudPrecio.DataBindings.Add("Value", this.bdSourceCompras, "Precio");

            this.txtObservacion.DataBindings.Clear();
            this.txtObservacion.DataBindings.Add("Text", this.bdSourceCompras, "Observacion");

            this.cbEstado.DataBindings.Clear();
            this.cbEstado.DataBindings.Add("SelectedValue", this.bdSourceCompras, "IdEstado");




            this.txtTotalJabas.DataBindings.Clear();
            this.txtTotalJabas.DataBindings.Add("Text", this.bdSourceCompras, "TotalJabas");

            this.txtTotalBruto.DataBindings.Clear();
            this.txtTotalBruto.DataBindings.Add("Text", this.bdSourceCompras, "TotalPesoBruto");

            this.txtTotalTara.DataBindings.Clear();
            this.txtTotalTara.DataBindings.Add("Text", this.bdSourceCompras, "TotalPesoTara");

            this.txtUnidades.DataBindings.Clear();
            this.txtUnidades.DataBindings.Add("Text", this.bdSourceCompras, "TotalUnidades");

            this.txtTotalDevolucion.DataBindings.Clear();
            this.txtTotalDevolucion.DataBindings.Add("Text", this.bdSourceCompras, "TotalDevolucion");

            this.txtTotalNeto.DataBindings.Clear();
            this.txtTotalNeto.DataBindings.Add("Text", this.bdSourceCompras, "TotalPesoNeto");

            this.txtImporte.DataBindings.Clear();
            this.txtImporte.DataBindings.Add("Text", this.bdSourceCompras, "MontoTotal");

            }

        
        
        }

        private void btnAceptarLineaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                
                bool bOk = true;
                string mensaje = string.Empty;

                if (this.cmbProducto.SelectedIndex == 0) {

                    bOk = bOk && false;
                    mensaje = "Debe Seleccionar un producto. \n";

                }
                if (this.cmbClientes.Text == string.Empty)
                {

                    bOk = bOk && false;
                    mensaje = "Debe Seleccionar un cliente. \n";

                }
                if ( bOk && BLCompra.ValidarCompra(ref mensaje)){

                    this.LoadFormCompra();                
                    bOk = BLCompra.Grabar(ref mensaje);
                    if (bOk) this.lblIDCompra.Text = BLCompra.getCompra.IdCompra.ToString();


                    this.bdSourceCompras.ResetBindings(false);

                }

                if (!bOk) MostrarMensaje(mensaje, MessageBoxIcon.Error);
            }
            catch (Exception)
            {

                MostrarMensaje("Error al cargar formulario", MessageBoxIcon.Error);
            }
        }

        //private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        //{
        //    MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        //}


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadFormCompra()
        {
            try
            {
                this.BLCompra.BECompra.Auditoria = VariablesSession.BEUsuarioSession;
                this.BLCompra.BECompra.IdCaja = VariablesSession.BEUsuarioSession.IdCaja;
                this.BLCompra.BECompra.IdCompra = string.IsNullOrEmpty(this.lblIDCompra.Text) ? 0 : int.Parse (this.lblIDCompra.Text);
                //this.BLCompra.BECompra.IdEmpresa = this.empre;
                this.BLCompra.BECompra.IdEstado = this.cbEstado.SelectedValue.ToString(); //BECompra.REGISTRADO;
                this.BLCompra.BECompra.IdPersonal = VariablesSession.BEUsuarioSession.IdPersonal;
                this.BLCompra.BECompra.IdProducto = (this.cmbProducto.SelectedValue == null) ? 0 : int.Parse(this.cmbProducto.SelectedValue.ToString());
                this.BLCompra.BECompra.IdProveedor = (this.cmbClientes.SelectedValue == null) ? 0 : int.Parse(this.cmbClientes.SelectedValue.ToString());

                this.BLCompra.BECompra.IdTipoDocumentoCompra = BECompra.TIPO_DOC_FACTURA;

                this.BLCompra.BECompra.MontoTotal = decimal.Parse(this.txtImporte.Text);
                this.BLCompra.BECompra.MontoIGV = this.BLCompra.BECompra.MontoSubTotal * VariablesSession.IGV/100;
                this.BLCompra.BECompra.MontoSubTotal = this.BLCompra.BECompra.MontoSubTotal / (1 + VariablesSession.IGV/100);
                //this.BLCompra.BECompra.NumeroDocumento = ;
                this.BLCompra.BECompra.Observacion = this.txtObservacion.Text;
                this.BLCompra.BECompra.Precio = this.nudPrecio.Value;
                this.BLCompra.BECompra.TotalDevolucion = decimal.Parse(this.txtTotalDevolucion.Text);
                this.BLCompra.BECompra.TotalJabas = int.Parse(this.txtTotalJabas.Text);
                this.BLCompra.BECompra.TotalPesoBruto = decimal.Parse(this.txtTotalBruto.Text);
                this.BLCompra.BECompra.TotalPesoNeto = decimal.Parse(this.txtTotalNeto.Text);
                this.BLCompra.BECompra.TotalPesoTara = decimal.Parse(this.txtTotalTara.Text);
                this.BLCompra.BECompra.TotalUnidades = int.Parse(this.txtUnidades.Text);
                this.BLCompra.BECompra.Fecha = this.dtFecha.Value.Date;


            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public bool validarFormulario(ref string Mensaje)
            {
                decimal valor = 0;
                bool bOk = true;
                bOk = bOk && decimal.TryParse (this.txtTotalBruto.Text, out valor);
                
                bOk = bOk && decimal.TryParse (this.txtTotalTara.Text, out valor);
                bOk = bOk && decimal.TryParse (this.txtTotalJabas.Text, out valor);
                bOk = bOk && decimal.TryParse (this.txtTotalDevolucion.Text, out valor);
                //bOk = bOk && decimal.TryParse (this.txtImporte.Text, out valor);
                //bOk = bOk && decimal.TryParse (this.txtTotalNeto.Text, out valor);
                bOk = bOk && decimal.TryParse (this.txtUnidades.Text, out valor);


                if (!bOk) Mensaje = "Error en formato de datos";
                return bOk;
            }
        public void CalcularImportes()
        {
                string mensaje= string.Empty;
                try
                {
                    decimal TotalNeto = 0;
                    decimal TotalBruto = 0;
                    decimal TotalTara = 0;
                    decimal TotalDevolucion;
                    decimal TotalJabas;
                    decimal Unidades;
                    decimal Importe;

                    if (validarFormulario(ref mensaje))
                    {
                        //TotalNeto = decimal.Parse(this.txtTotalNeto.Text);
                        TotalBruto = decimal.Parse(this.txtTotalBruto.Text);
                        TotalTara = decimal.Parse(this.txtTotalTara.Text);
                        TotalDevolucion = decimal.Parse(this.txtTotalDevolucion.Text);
                        TotalJabas = decimal.Parse(this.txtTotalJabas.Text);
                        Unidades = decimal.Parse(this.txtUnidades.Text);
                        //Importe = decimal.Parse(this.txtImporte.Text);
                        TotalNeto = TotalBruto - TotalTara - TotalDevolucion;
                        Importe = TotalNeto * this.nudPrecio.Value;


                        this.txtTotalNeto.Text = TotalNeto.ToString();
                        this.txtImporte.Text = Importe.ToString();

                        this.bdSourceCompras.EndEdit();



                    }
                    else
                    {
                        //MostrarMensaje(mensaje, MessageBoxIcon.Error);
                    
                    }
                }
                catch (Exception)
                {

                    throw;
                }   
       
        
        
        }
        private void nudPrecio_ValueChanged(object sender, EventArgs e)
        {
            CalcularImportes();
            
        }

        private void txtTotalBruto_TextChanged(object sender, EventArgs e)
        {

            CalcularImportes();
        }

        private void txtTotalTara_TextChanged(object sender, EventArgs e)
        {

            CalcularImportes();
        }

        private void txtTotalDevolucion_TextChanged(object sender, EventArgs e)
        {

            CalcularImportes();
        }

        private void cmbClientes_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bdSourceCompras != null) {

                BECompraFilter compra = (BECompraFilter)this.bdSourceCompras.Current;
                BEClienteProveedor oEntidad = new BEClienteProveedor();
                oEntidad.IdCliente = compra.IdProveedor;

                List<BEClienteProveedor> vTemp = new BLClienteProveedor().Listar(oEntidad);
                this.cmbClientes.DataSource = vTemp;
                this.cmbClientes.DisplayMember = "Nombre";
                this.cmbClientes.ValueMember = "IdCliente";
             
            }
            

            
        }

        private void bdnCompras_RefreshItems(object sender, EventArgs e)
        {
            

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                string temp = e.ToString() + " clase : " + e.GetType().Name;
                //ojo revisar esto
                int IdCompra = Convert.ToInt32((string.IsNullOrEmpty(this.lblIDCompra.Text) ? "0" : this.lblIDCompra.Text));
                BLCompra BLDP = new BLCompra();
                BECompra beCompra = new BECompra();

                beCompra.IdCompra = IdCompra;
                beCompra.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                beCompra.Observacion = "Eliminado por :" + VariablesSession.BEUsuarioSession.Nombre;

                if (MessageBox.Show("Desea Eliminar la compra?", "Eliminar Compra", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    //BLDP.Eliminar(beCompra);
                    
                    //this.bdSourceCompras.EndEdit();
                    this.bdnCompras.Refresh();
                }
                else
                {

                    //this.bdSourceCompras.CancelEdit();
                }




            }
            catch (Exception ex)
            {

                this.MostrarMensaje(ex.StackTrace, MessageBoxIcon.Error);

            }

            
        }

        private void bindingNavigatorDeleteItem_MouseDown(object sender, MouseEventArgs e)
        {
            

        }

        //private void CargarProducto()
        //{
        //    List<BEProducto> vLista = new List<BEProducto>();
        //    vLista = new BLProducto().Listar(new BEProducto());
        //    vLista.Insert(0, new BEProducto(0, "Seleccione"));
        //    this.cmbProducto.DataSource = vLista;
        //    this.cmbProducto.DisplayMember = "Nombre";
        //    this.cmbProducto.ValueMember = "IdProducto";
        //}
        
        //private void CargarEstadoVenta()
        //{
        //    BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
        //    oBEParametroDetalle.IdParametro = eParametro.Estado_Venta.GetHashCode();
        //    List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
        //    vLista.Insert(0, new BEParametroDetalle("0", "Seleccione"));
        //    cbEstadoVentaG.DataSource = vLista;
        //    cbEstadoVentaG.DisplayMember = "Texto";
        //    cbEstadoVentaG.ValueMember = "Valor";

        //}



        

    }
}