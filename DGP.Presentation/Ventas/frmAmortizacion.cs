using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.BusinessLogic.Seguridad;
using DGP.BusinessLogic.Ventas;
using DGP.Entities.Seguridad;
using DGP.Entities.Ventas;
using DGP.BusinessLogic;
using DGP.Entities;

namespace DGP.Presentation.Ventas {

    public partial class frmAmortizacion : Form {

        DataGridViewCellStyle oCeldaPagoCuenta = null;
        frmTableroElectronico _frmTableroElectronico = null;
        public frmAmortizacion() {
            InitializeComponent();
            InicializarFormulario();
        }
        public frmAmortizacion(int IdCliente, frmTableroElectronico frmTableroElectronico)
        {
            InitializeComponent();
            InicializarFormulario();
            this._frmTableroElectronico = frmTableroElectronico;
            
        }
        public void SetCliente(int IdCliente , int IdProducto)
        {
            this.cmbClientes.SelectedValue = IdCliente;
            this.CargarAmortizaciones(IdCliente, IdProducto);
          
        
        
        }
        public void mensajeSalida()
        {
            if (_frmTableroElectronico != null)
            {
                _frmTableroElectronico.refrescar();
            
            
            }
        
        }

        #region "Eventos de frmAmortizacion.aspx"
        
            private void frmAmortizacion_Load(object sender, EventArgs e) {
                try {
                    dgrvAmortizacion.AutoGenerateColumns = false;
                    lblSimboloMoneda.Text = VariablesSession.ISOCulture.NumberFormat.CurrencySymbol;
                    this.CargarTipoDocumentoPago();

                    BEParametroDetalle parametro = (BEParametroDetalle)this.cmbTipoPago.SelectedItem;
                    this.CargarFormaPago( parametro );
                    this.CargarEntidadBancaria();
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e) {
                try {
                    //int intIdCliente = 0;
                    
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void cbProducto_SelectedIndexChanged(object sender, EventArgs e) {
                try {
                    int intIdCliente = 0;
                    int intIdProducto = 0;
                    int.TryParse(cmbClientes.SelectedValue.ToString(), out intIdCliente);
                    if (cbProducto.SelectedIndex > 0) {
                        intIdProducto = Convert.ToInt32(cbProducto.SelectedValue);
                        CargarAmortizaciones(intIdCliente, intIdProducto);
                    } else {
                        CargarAmortizaciones(intIdCliente, 0);
                    }
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void AplicarMonto() {
                decimal montoTotal = this.nudMontoDocumento.Value;
                decimal montoAcarreo = 0;

                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows)
                {

                    if (montoAcarreo >= montoTotal) break;

                    object oIndicador = vRow.Cells["Indicador"].Value;
                    string sEstado = vRow.Cells["IdEstado"].Value.ToString();

                    if (oIndicador.ToString() == "1" && sEstado == BEVenta.REGISTRADO)
                    {

                        decimal montoSaldo = 0;
                        bool okParse = decimal.TryParse(vRow.Cells["Saldo"].Value.ToString(), out montoSaldo);
                        if (!okParse || montoSaldo < 0) continue; //excluir a los negativos
                        decimal delta = montoTotal - montoAcarreo;

                        decimal montoAmortizar = (delta >= montoSaldo) ? montoSaldo : montoTotal - montoAcarreo;
                        vRow.Cells["Pago"].Value = montoAmortizar.ToString();
                        montoAcarreo = montoAcarreo + montoAmortizar;


                    }
                }
                //primero los registrados, luego los congelados,  falta el bucle para los congelados

                

                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows)
                {

                    if (montoAcarreo >= montoTotal) break;

                    object oIndicador = vRow.Cells["Indicador"].Value;
                    string sEstado = vRow.Cells["IdEstado"].Value.ToString();

                    if (oIndicador.ToString() == "1" && sEstado == BEVenta.CONGELADO)
                    {

                        decimal montoSaldo = 0;
                        bool okParse = decimal.TryParse(vRow.Cells["Saldo"].Value.ToString(), out montoSaldo);
                        if (!okParse || montoSaldo < 0) continue; //excluir a los negativos
                        decimal delta = montoTotal - montoAcarreo;

                        decimal montoAmortizar = (delta >= montoSaldo) ? montoSaldo : montoTotal - montoAcarreo;
                        vRow.Cells["Pago"].Value = montoAmortizar.ToString();
                        montoAcarreo = montoAcarreo + montoAmortizar;


                    }
                }

            
            }

            private void btnAplicarMonto_Click(object sender, EventArgs e) {
                try {

                    AplicarMonto();



                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

            private void dgrvAmortizacion_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
                try
                {
                    string strMensaje = string.Empty;
                    if (!ValidarCampos(ref strMensaje, e.RowIndex, e.ColumnIndex))
                    {
                        MostrarMensaje(strMensaje, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        this.nudMontoAplicadoDocumento.Value = SumaAmortizaciones();


                    }

                }
                catch (Exception ex)
                {
                    
                     MostrarMensaje(ex.Message, MessageBoxIcon.Error);;
                }
                
            }
            private decimal SumaAmortizaciones()
            {
                decimal montoResult= 0;
                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows) {
                    bool bok;
                    decimal tempValue;
                    if ( vRow.Cells["Pago"].Value == null) continue;
                    bok = decimal.TryParse(vRow.Cells["Pago"].Value.ToString() , out tempValue);
                    montoResult += (bok)?tempValue: 0;
                }

                return montoResult;
            } 


            private void btnGrabar_Click(object sender, EventArgs e) {
                string strMensaje = string.Empty;
                bool bCancelarVenta = false;
                this.btnGrabar.Enabled = false;
                
                try
                {
                    if (ValidarFormularioAmortizacion(ref strMensaje))
                    {

                        if (this.SumaAmortizaciones() == 0) this.AplicarMonto();
                        int intIdUsuario = 0;
                        bool boIndicador = true;
                        int.TryParse(cbUsuario.SelectedValue.ToString(), out intIdUsuario);
                        if (intIdUsuario > 0 && intIdUsuario != VariablesSession.BEUsuarioSession.IdPersonal)
                        {
                            boIndicador = (MessageBox.Show("La amortización se va a registrar con otro usuario, desea continuar?", "DGP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                        }
                        if (boIndicador)
                        {
                            // Obtener las Amortizaciones

                            List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones(boIndicador);
                            int intResultado = 0;

                            BEDocumento documento = new BEDocumento();
                            documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                            documento.Fecha = this.dtpFechaPago.Value.Date;
                            documento.IdTipoDocumento = cmbTipoPago.SelectedValue.ToString();
                            documento.Cliente.IdCliente = int.Parse(this.cmbClientes.SelectedValue.ToString());
                            documento.Personal.IdPersonal = int.Parse(cbUsuario.SelectedValue.ToString());
                            documento.delleAmortizacion = vLista;
                            documento.Monto = this.nudMontoDocumento.Value;
                            documento.IdFormaPago = this.cmbFormaPago.SelectedValue.ToString();
                            documento.IdBanco = (this.cmbBanco.Enabled) ? this.cmbBanco.SelectedValue.ToString() : string.Empty;
                            documento.Observacion = this.txtObservacion.Text;
                            documento.NumeroOperacion = (this.txtNumeroOperacion.Enabled) ? this.txtNumeroOperacion.Text : string.Empty;
                            documento.NumeroReciboPago = this.txtNumeroRecibo.Text;

                            //documento.IdTipoDocumento


                            bool bOk = new BLAmortizacionVenta().Insertar(documento);
                            if (bOk)
                            {
                                MostrarMensaje("Se registró la amortización correctamente", MessageBoxIcon.Information);
                                int intIdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
                                int intIdProducto = Convert.ToInt32(cbProducto.SelectedValue);
                                LimpiarFormulario();
                                CargarAmortizaciones(intIdCliente, intIdProducto);
                            }
                            else
                            {
                                MostrarMensaje("No se pudo registrar la venta, intentelo de nuevo", MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    else
                    {
                        MostrarMensaje(strMensaje, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
                finally {

                    this.btnGrabar.Enabled = true;
                
                
                }
            }

            private void btnCancelar_Click(object sender, EventArgs e) {
                try {
                    ResetearFormulario();
                    InicializarFormulario();
                } catch (Exception ex) {
                    MostrarMensaje(ex.Message, MessageBoxIcon.Error);
                }
            }

        #endregion

        #region "Métodos de frmAmortizacion"

            private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon) {
                MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
            }

            private void InicializarFormulario() {
                DGP_Util.LiberarComboBox(cmbClientes);
                DGP_Util.EnabledComboBox(cmbClientes, true);
                DGP_Util.LiberarComboBox(cbProducto);
                DGP_Util.EnabledComboBox(cbProducto, false);
                //DGP_Util.EnableControl(nudPrecioAmortizacion, false);
                //DGP_Util.EnableControl(btnAplicarMonto, false);
               // DGP_Util.EnableControl(btnGrabar, false);
               // DGP_Util.EnableControl(btnCancelar, false);
                LimpiarFormulario();
                //CargarCliente();
                // Definir al Usuario
                CargarUsuarios();
                dtpFechaPago.Value = DateTime.Now.Date;
                cbUsuario.SelectedValue = VariablesSession.BEUsuarioSession.IdPersonal;
                this.AplicarPrivilegios();
            }

            //private void CargarCliente() {
            //    List<BEClienteProveedor> vListaCliente = new List<BEClienteProveedor>();
            //    BEClienteProveedor oTemp = new BEClienteProveedor();
            //    oTemp.IdZona = 0;
            //    vListaCliente = new BLClienteProveedor().Listar(oTemp);
            //    //vListaCliente.Insert(0, new BEClienteProveedor(0, "CE : Cliente Eventual"));
            //    vListaCliente.Insert(0, new BEClienteProveedor(0, "--------Todos--------"));
            //    cbCliente.DataSource = vListaCliente;
            //    cbCliente.DisplayMember = "Nombre";
            //    cbCliente.ValueMember = "IdCliente";
            //}

            private void ResetearFormulario() {
                if (cbProducto.DataSource != null) {
                    DGP_Util.LiberarComboBox(cbProducto);                    
                }
                DGP_Util.EnabledComboBox(cbProducto, false);
                //DGP_Util.EnableControl(nudPrecioAmortizacion, false);
                //DGP_Util.EnableControl(btnAplicarMonto, false);
               // DGP_Util.EnableControl(btnGrabar, false);
               // DGP_Util.EnableControl(btnCancelar, false);
                DGP_Util.LiberarGridView(dgrvAmortizacion);
                LimpiarFormulario();
            }

            private void LimpiarFormulario() {
                nudMontoDocumento.Value = 0;
                nudVuelto.Value = 0;
                this.nudMontoAplicadoDocumento.Value = 0;
                this.txtObservacion.Text = string.Empty;
                this.txtNumeroRecibo.Text = "";
                this.txtNumeroOperacion.Text = string.Empty;

                this.cmbBanco.SelectedIndex = (this.cmbBanco.Items.Count > 0) ? 0 : this.cmbBanco.SelectedIndex;
                this.cmbTipoPago.SelectedIndex = (this.cmbTipoPago.Items.Count > 0) ? 0 : this.cmbTipoPago.SelectedIndex; ;
                this.cmbFormaPago.SelectedIndex = (this.cmbFormaPago.Items.Count > 0) ? 0 : this.cmbFormaPago.SelectedIndex; ;

                this.cmbBanco.Enabled = false;
                this.txtNumeroOperacion.Enabled = false;

            }

            private void CargarProductoCliente(int pIdCliente) {
                List<BEProducto> vLista = new List<BEProducto>();
                vLista = new BLVenta().ListarProductoCliente(pIdCliente);
                vLista.Insert(0, new BEProducto(0, "Todos"));
                cbProducto.DataSource = vLista;
                cbProducto.DisplayMember = "Nombre";
                cbProducto.ValueMember = "IdProducto";
            }

            private void CargarUsuarios() { 
                List<BEPersonal> vLista = new BLPersonal().ListarPersonal(new BEPersonal());
                cbUsuario.DataSource = vLista;
                cbUsuario.DisplayMember = "Login";
                cbUsuario.ValueMember = "IdPersonal";
            }

            private void CargarAmortizaciones(int pIdCliente, int pIdProducto) {
                VistaAmortizacion oEntidad = new VistaAmortizacion();
                oEntidad.IdCliente = pIdCliente;
                oEntidad.IdProducto = pIdProducto;
                List<VistaAmortizacion> vLista = new List<VistaAmortizacion>();
                vLista = new BLAmortizacionVenta().Listar(oEntidad);
                dgrvAmortizacion.DataSource = vLista;
                // Validar Controles
                if (vLista.Count > 0)
                {
                    //DGP_Util.EnableControl(btnGrabar, true);
                    //DGP_Util.EnableControl(btnCancelar, true);
                }
                else
                {
                    //DGP_Util.EnableControl(btnGrabar, false);
                    //DGP_Util.EnableControl(btnCancelar, false);
                }
                // Habilitar solamente las Ventas
                oCeldaPagoCuenta = new DataGridViewCellStyle();
                oCeldaPagoCuenta.SelectionBackColor = Color.Silver;
                oCeldaPagoCuenta.BackColor = Color.Silver;
                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows) {
                    // Obtener Indicador
                    object oIndicador = vRow.Cells["Indicador"].Value;
                    if (oIndicador.ToString() == "0") {
                        vRow.Cells["Cantidad"].Value = "";
                        vRow.Cells["PesoNeto"].Value = "";
                        vRow.Cells["Saldo"].Value = "";
                        vRow.Cells["Pago"].Value = "";
                        vRow.Cells["Pago"].ReadOnly = true;
                        vRow.Cells["Pago"].Style = oCeldaPagoCuenta;
                    }

                }
            }

            private bool ValidarFormularioAmortizacion(ref string pMensaje) {
                bool boResultado = true;
                int intCantidad = 0;
                bool bExisteCancelar = false;
                bool ExisteNegativos = false;
                bool ExisteAmortizacionMayorSaldo = false;
                bool ExisteOtroCliente = false;
                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows) { 
                    // Obtener el indicador
                    object oIndicador = vRow.Cells["Indicador"].Value;
                    if (oIndicador.ToString() == "1") {
                        // Obtener el Pago a Cuenta
                        decimal decSaldoCuenta = decimal.Zero;
                        object oPagoCuenta = vRow.Cells["Pago"].Value;
                        
                        if (oPagoCuenta != null && !string.IsNullOrEmpty(oPagoCuenta.ToString())) {
                            intCantidad++;
                            decimal.TryParse(vRow.Cells["Saldo"].Value.ToString(), out decSaldoCuenta);
                            ExisteNegativos = ExisteNegativos || (decSaldoCuenta < 0);
                            decimal tempPagoAcuenta = 0;
                            decimal.TryParse(oPagoCuenta.ToString(), out tempPagoAcuenta);
                            ExisteAmortizacionMayorSaldo = ExisteAmortizacionMayorSaldo || (tempPagoAcuenta > decSaldoCuenta);
                        }
                        //obtener el checkbox cancelado
                        bExisteCancelar = bExisteCancelar || ((bool)vRow.Cells["CancelarVenta"].Value);

                        ExisteOtroCliente = ExisteOtroCliente || (int.Parse(vRow.Cells["IdCliente"].Value.ToString()) != int.Parse(this.cmbClientes.SelectedValue.ToString()));
                        
                    }
                }
                //if (intCantidad <= 0) {
                //    pMensaje = "Debe ingresar como mínimo un pago";
                //    boResultado = false;
                //}
                if (this.nudMontoDocumento.Value < nudMontoAplicadoDocumento.Value)
                {
                    pMensaje = "El Monto del documento debe ser mayor o igual al monto Aplicado";
                    boResultado = false;
                }
                if (this.nudMontoDocumento.Value <= 0 )
                {
                    pMensaje = "Debe Ingresar un monto de Documento de Pago";
                    boResultado = false;
                }
                if (ExisteNegativos)
                {
                    pMensaje = "No debe Aplicar Amortizaciones a valores negativos";
                    boResultado = false;
                }
                if (ExisteAmortizacionMayorSaldo)
                {
                    pMensaje = "Monto a Aplicar no debe ser mayor al saldo";
                    boResultado = false;
                }

                if (int.Parse(this.cmbClientes.SelectedValue.ToString()) <= 0)
                {
                    pMensaje = "Debe Seleccionar un cliente";
                    boResultado = false;
                }
                //if (int.Parse(this.cmbClientes.SelectedValue.ToString()) > 0 && int.Parse(this.cmbClientes.SelectedValue.ToString()) != this.dgrvAmortizacion.Rows[0].Cells["IdCliente"])
                //{
                //    pMensaje = "Debe Seleccionar un cliente";
                //    boResultado = false;
                //}
               
                return boResultado;
            }

            private bool ValidarCampos(ref string pMensaje, int pRowIndex, int pColumnIndex) {
                bool boResultado = true;
                try {
                    switch ((ePosicionCol)pColumnIndex) {
                        case ePosicionCol.Pago:
                            // Validar campo vacio y valor
                            if (!ValidarPagoCuenta(ref pMensaje, pRowIndex)) {
                                boResultado = false;
                                dgrvAmortizacion[pColumnIndex, pRowIndex].Value = "";
                                dgrvAmortizacion.CancelEdit();
                            }
                            break;
                    }
                } catch (Exception ex) {
                    pMensaje = ex.Message;
                    boResultado = false;
                }
                return boResultado;
            }

            private bool ValidarPagoCuenta(ref string pMensaje, int pRowIndex) {
                bool boIndicador = true;
                object oPagoCuenta = dgrvAmortizacion["Pago", pRowIndex].Value;
                if (oPagoCuenta == null || string.IsNullOrEmpty(oPagoCuenta.ToString())) {
                    boIndicador = true;
                } else {
                    // Validar los decimales
                    //if (DGP_Util.ValidarDigitosDecimales(oPagoCuenta.ToString())) {
                    // Validar que sea de tipo decimal
                    decimal decPagoCuenta = decimal.Zero;
                    decimal.TryParse(oPagoCuenta.ToString(), out decPagoCuenta);
                    if (decPagoCuenta <= decimal.Zero) {
                        boIndicador = false;
                        pMensaje = "Ingresar pago cuenta válido";
                    }
                    //} else {
                    //    boIndicador = false;
                    //    pMensaje = "Ingresar pago cuenta con dos(2) decimales";
                    //}
                }
                return boIndicador;
            }

            private List<BEAmortizacionVenta> ObtenerAmortizaciones(bool pIndicadorUsuario ) {
                List<BEAmortizacionVenta> vLista = new List<BEAmortizacionVenta>();
                BEAmortizacionVenta oBEAmortizacionVenta = null;
                //pCancelarVenta = false;
                //bool tempCancelar = false;

                foreach (DataGridViewRow vRow in dgrvAmortizacion.Rows) {
                    // Obtener el indicador
                    object oIndicador = vRow.Cells["Indicador"].Value;
                    if (oIndicador.ToString() == "1") {
                        // Obtener el Pago a Cuenta
                        decimal decPagoCuenta = decimal.Zero;
                        object oPagoCuenta = vRow.Cells["Pago"].Value;
                        if (oPagoCuenta != null && !string.IsNullOrEmpty(oPagoCuenta.ToString())) {
                            decPagoCuenta = Convert.ToDecimal(oPagoCuenta);
                            // Obtener los demas Valores de la fila
                            int intIdVenta = 0;
                            int intIdProducto = 0;
                            int intIdCliente = 0;
                            intIdVenta = Convert.ToInt32(vRow.Cells["IdVenta"].Value);
                            intIdProducto = Convert.ToInt32(vRow.Cells["IdProducto"].Value);
                            intIdCliente = Convert.ToInt32(this.cmbClientes.SelectedValue);
                            oBEAmortizacionVenta = new BEAmortizacionVenta();
                            oBEAmortizacionVenta.Monto = decPagoCuenta;
                            oBEAmortizacionVenta.NroDocumento = string.Empty;
                            oBEAmortizacionVenta.IdFormaPago = BEAmortizacionVenta.FORMAPAGO_EFECTIVO;
                            oBEAmortizacionVenta.IdTipoAmortizacion = BEAmortizacionVenta.TIPOAMORTIZACION_AMORTIZACION;
                            oBEAmortizacionVenta.Observacion = string.Empty;
                            oBEAmortizacionVenta.IdEstado = BEAmortizacionVenta.ESTADO_REGISTRADO;
                            oBEAmortizacionVenta.IdVenta = intIdVenta;
                            oBEAmortizacionVenta.IdCliente = intIdCliente;
                            oBEAmortizacionVenta.FechaPago = dtpFechaPago.Value.Date;
                            oBEAmortizacionVenta.IdPersonal = (pIndicadorUsuario) ? int.Parse(cbUsuario.SelectedValue.ToString()) : VariablesSession.BEUsuarioSession.IdPersonal;
                            oBEAmortizacionVenta.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                            //tempCancelar = Convert.ToBoolean( vRow.Cells[ePosicionCol.Cancelar.GetHashCode()].Value );
                            oBEAmortizacionVenta.CancelarVenta = Convert.ToBoolean(vRow.Cells["CancelarVenta"].Value);
                            vLista.Add(oBEAmortizacionVenta);
                        }
                    }
                }
                return vLista;
            }

        #endregion

        private enum ePosicionCol { 
            Indicador = 0
            ,IdVenta = 1
            ,IdProducto = 2
            ,Cantidad = 7
            ,PesoNeto = 8
            ,Importe = 9
            ,Saldo = 10
            ,Pago = 11
            ,Cancelar = 12
            ,IdEstado = 14
        }

        private void dgrvAmortizacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex>-1 && e.ColumnIndex>-1)
                {

                    if ( this.dgrvAmortizacion.Columns[e.ColumnIndex].Name == "dgvBtnEliminar"  )
                    {
                        VistaAmortizacion vistaAmortizacion = (VistaAmortizacion)this.dgrvAmortizacion.Rows[e.RowIndex].DataBoundItem;

                        if (vistaAmortizacion.IdAmortizacion > 0 && MessageBox.Show("Usted Eliminara todo el documento de Pago asociado a esta amortizacion. Desea continuar?", "Eliminar Amortizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)//esto para saltar las ventas
                        {
                            BEAmortizacionVenta bean = new BEAmortizacionVenta();
                            bean.IdAmortizacionVenta = vistaAmortizacion.IdAmortizacion;
                            bean.BEUsuarioLogin = VariablesSession.BEUsuarioSession;

                            //elimina todo el detalle de amortizacion y libera el documento
                            new BLAmortizacionVenta().EliminarAdelantoVenta(bean);

                            int intIdCliente = Convert.ToInt32(this.cmbClientes.SelectedValue);
                            int intIdProducto = Convert.ToInt32(cbProducto.SelectedValue);
                            CargarAmortizaciones(intIdCliente, intIdProducto);

                        }
                    
                    
                    }

                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
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
            try 
	        {
                int intIdCliente = 0;
                if (this.cmbClientes.SelectedIndex >= 0)
                {
                    BEClienteProveedor oBEClienteProveedor = (BEClienteProveedor)this.cmbClientes.SelectedItem;

                    int.TryParse(cmbClientes.SelectedValue.ToString(), out intIdCliente);
                    CargarProductoCliente(intIdCliente);
                    DGP_Util.EnabledComboBox(cbProducto, true);
                    CargarAmortizaciones(Convert.ToInt32(cmbClientes.SelectedValue), 0);
                    DGP_Util.EnableControl(nudMontoDocumento, true);
                    CargarAmortizacionesSinAplicar(intIdCliente);

                    //if (cmbClientes.SelectedIndex > 0)
                    //{
                    //    int.TryParse(cmbClientes.SelectedValue.ToString(), out intIdCliente);
                    //    CargarProductoCliente(intIdCliente);
                    //    DGP_Util.EnabledComboBox(cbProducto, true);
                    //    CargarAmortizaciones(Convert.ToInt32(cmbClientes.SelectedValue), 0);
                    //    DGP_Util.EnableControl(nudMontoDocumento, true);
                    //    CargarAmortizacionesSinAplicar(intIdCliente);
                    //}
                    //else
                    //{
                    //    ResetearFormulario();
                    //}

                    //MostrarMensaje(oBEClienteProveedor.IdCliente.ToString() + oBEClienteProveedor.Nombre, MessageBoxIcon.Information);

                }
                else
                {

                    ResetearFormulario();
                }
        		
	        }
	        catch (Exception)
	        {
        		
		        throw;
	        }
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void AplicarPrivilegios()
        {
            this.cbUsuario.Enabled = VariablesSession.Privilegios.Exists(t => t.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Amortizacion_Cambio_Cobrador);
            this.dtpFechaPago.Enabled = VariablesSession.Privilegios.Exists(t => t.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Amortizacion_Cambio_Fecha_de_Pago);
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CargarAmortizacionesSinAplicar(int IdCliente){

            this.nudVuelto.Value = new BLAmortizacionVenta().ObtenerAmortizacionSinAplicar(IdCliente); 
        
        
        
        }

        private void btnAplicarVuelto_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbClientes.SelectedValue== null) return;

               // VistaAmortizacion vistaAmortizacion = (VistaAmortizacion)this.dgrvAmortizacion.Rows[0].DataBoundItem;
                int IdCliente = int.Parse ( this.cmbClientes.SelectedValue.ToString());
                bool bOk = new BLAmortizacionVenta().ReaplicarAmortizacion(new BEVenta() { BEUsuarioLogin = VariablesSession.BEUsuarioSession, IdCliente = IdCliente });
                if (bOk)
                {
                    int intIdCliente = Convert.ToInt32(cmbClientes.SelectedValue);
                    int intIdProducto = Convert.ToInt32(cbProducto.SelectedValue);
                    CargarAmortizaciones(intIdCliente, intIdProducto);
                }

            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Error Controlado: " + ex.Message , MessageBoxIcon.Error);
            }
        }

        private void dgrvAmortizacion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == this.dgrvAmortizacion.Columns["ImgPagoInfo"].Index) && e.Value != null)
            {
                DataGridViewCell cell = this.dgrvAmortizacion.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //if (VariablesSession.Privilegios.Find(x => x.IdPrivilegio == DGP.Entities.Seguridad.BEPrivilegio.Visualizar_Precios_compra) != null)
                //{
                cell.ToolTipText = this.dgrvAmortizacion.Rows[e.RowIndex].Cells["DocumentoPagoInfo"].Value.ToString();

                //}
            }
        }

        //private void dtpFechaPago_ValueChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void label11_Click(object sender, EventArgs e)
        //{

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pValor"></param>
        private void CargarTipoDocumentoPago()
        {
            List<BEParametroDetalle> vLista = new List<BEParametroDetalle>();
            
            BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
            oBEParametroDetalle.IdParametro = eParametro.TipoAmortizacion.GetHashCode();
            vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
            cmbTipoPago.DataSource = vLista;
            cmbTipoPago.DisplayMember = "Texto";
            cmbTipoPago.ValueMember = "Valor";

        }

        private void CargarFormaPago(BEParametroDetalle beTipoAmortizacion)
        {
            List<BEParametroDetalle> vLista = new List<BEParametroDetalle>();

            BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
            oBEParametroDetalle.IdParametro = eParametro.FormaPago.GetHashCode();
            oBEParametroDetalle.ParametroDetallePadre = beTipoAmortizacion.IdItem;

            vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
            vLista.Remove(vLista.Find(x => x.Valor == "NCC"));
            this.cmbFormaPago.DataSource = vLista;
            cmbFormaPago.DisplayMember = "Texto";
            cmbFormaPago.ValueMember = "Valor";

        }

        private void CargarEntidadBancaria()
        {
            List<BEParametroDetalle> vLista = new List<BEParametroDetalle>();

            BEParametroDetalle oBEParametroDetalle = new BEParametroDetalle();
            oBEParametroDetalle.IdParametro = eParametro.EntidadBancaria.GetHashCode();
            
            vLista = new BLParametroDetalle().Listar(oBEParametroDetalle);
            this.cmbBanco.DataSource = vLista;
            cmbBanco.DisplayMember = "Texto";
            cmbBanco.ValueMember = "Valor";

        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BEParametroDetalle TipoPago = (BEParametroDetalle)this.cmbTipoPago.SelectedItem;
                //BEParametroDetalle pBEParametroDetalle = new BEParametroDetalle()
                //{
                //    IdItem = 0,
                //    ParametroDetallePadre = TipoPago.IdItem

                //};
                this.CargarFormaPago(TipoPago);

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BEParametroDetalle formaPago = (BEParametroDetalle)this.cmbFormaPago.SelectedItem;

                this.cmbBanco.SelectedIndex = (this.cmbBanco.Items.Count > 0) ? 0 : this.cmbBanco.SelectedIndex;
                this.txtNumeroOperacion.Text = string.Empty;

                if (formaPago.Valor == "CHQ" || formaPago.Valor == "DEP" || formaPago.Valor == "DET")
                {
                    this.cmbBanco.Enabled = true;
                    this.txtNumeroOperacion.Enabled = true;


                }
                else {

                    this.cmbBanco.Enabled = false;
                    this.txtNumeroOperacion.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void nudVuelto_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            BEClienteProveedor cliente = (BEClienteProveedor)this.cmbClientes.SelectedItem;
            if (cliente != null)
            {
                Reportes.frmReporteEstadoCuentaCliente rpt = new DGP.Presentation.Reportes.frmReporteEstadoCuentaCliente(cliente, -4);
                rpt.MdiParent = this.ParentForm;
                rpt.Show();           
            
            }
            
        }



        

    }
}