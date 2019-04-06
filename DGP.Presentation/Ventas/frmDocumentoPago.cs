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
using DGP.Entities.Seguridad;
using DGP.BusinessLogic.Seguridad;
using DGP.Entities.Compras;

namespace DGP.Presentation.Ventas
{
    public partial class frmDocumentoPago : Form
    {

        public static decimal montoAplicar;
        String accion;
        BEClienteProveedor Cliente;
        
        public frmDocumentoPago(BindingSource bs,string accion,BEClienteProveedor cliente,int cell)
        {
            InitializeComponent();

            CargarTipoDocumento();
            CargarUsuarios();
            
            this.CargarFormaPago((BEParametroDetalle)this.cmbTipoDocumento.SelectedItem);
            
            CargarEntidadBancaria();

            cargarCliente(cliente);


            this.bindingNavigator1.BindingSource = bs;
            this.bsDocumentos = bs;
            this.accion = accion;
            this.Cliente = cliente;
            this.cmbClientes.Enabled = false;
        }
        public frmDocumentoPago(BindingSource bs, string accion, BEClienteProveedor cliente)
        {
            InitializeComponent();
            
            CargarTipoDocumento();
            CargarUsuarios();

            //BEParametroDetalle parametro = (BEParametroDetalle)this.cmbTipoPago.SelectedItem;
            CargarFormaPago((BEParametroDetalle)this.cmbTipoDocumento.SelectedItem);
            CargarEntidadBancaria();

            cargarCliente(cliente);

            this.bindingNavigator1.BindingSource = bs;
            this.bsDocumentos = bs;
            
            this.accion = accion;
            this.Cliente = cliente;
        }

        private void cargarCliente(BEClienteProveedor cliente)
        {

            List<BEClienteProveedor> vTemp = new BLClienteProveedor().Listar(new BEClienteProveedor() { IdCliente = cliente.IdCliente });

            this.cmbClientes.DataSource = vTemp;
            this.cmbClientes.DisplayMember = "Nombre";
            this.cmbClientes.ValueMember = "IdCliente";

            if (vTemp.Count > 0) this.cmbClientes.SelectedIndex = 0;

        }
       

        private void frmDocumentoPago_Load(object sender, EventArgs e)
        {
            

               if (accion.Equals("actualizar"))
               {
                txtIdDocumento.DataBindings.Add("Text", bsDocumentos, "IdDocumento");
                dtFecha.DataBindings.Add("Text", bsDocumentos, "Fecha");
                numMonto.DataBindings.Add("Text", bsDocumentos, "Monto");

                this.cmbClientes.DataBindings.Clear();
                this.cmbClientes.DataBindings.Add("SelectedValue", this.bsDocumentos, "IdCliente", true,DataSourceUpdateMode.OnPropertyChanged);

                this.cmbTipoDocumento.DataBindings.Clear();
                this.cmbTipoDocumento.DataBindings.Add("SelectedValue", this.bsDocumentos, "IdTipoDocumento", true, DataSourceUpdateMode.OnPropertyChanged);

                this.cmbPersonal.DataBindings.Clear();
                this.cmbPersonal.DataBindings.Add("SelectedValue", this.bsDocumentos, "IdPersonal", true, DataSourceUpdateMode.OnPropertyChanged);


                this.cmbEntidadBancaria.DataBindings.Clear();
                this.cmbEntidadBancaria.DataBindings.Add("SelectedValue", this.bsDocumentos, "IdBanco", true, DataSourceUpdateMode.OnPropertyChanged);

                this.cmbFormaPago.DataBindings.Clear();
                this.cmbFormaPago.DataBindings.Add("SelectedValue", this.bsDocumentos, "IdFormaPago", true, DataSourceUpdateMode.OnPropertyChanged);

                txtCodigoOperacion.DataBindings.Add("Text", bsDocumentos, "NumeroOperacion");
                txtCodigoReferencia.DataBindings.Add("Text", bsDocumentos, "NumeroReciboPago");
                txtObservacion.DataBindings.Add("Text", bsDocumentos, "Observacion");
                

                listarDetalle();

            }
        }


        private void btnMostrarDetalle_Click(object sender, EventArgs e)
        {
            
        }

        private void listarDetalle()
        {
            BLDocumentoPago BLDP = new BLDocumentoPago();
            int idDocumento = Convert.ToInt32(txtIdDocumento.Text);
            this.bsDetalle.DataSource = BLDP.ListarDetalle(idDocumento);
            this.dgvDetalle.DataSource = this.bsDetalle;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                Actualizar(accion);


            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error:" + ex.Message , MessageBoxIcon.Error);
            }        }


        private void Actualizar(String accion)
        {
            //if (accion.Equals("actualizar"))

            if (string.IsNullOrEmpty(this.txtIdDocumento.Text))
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();

                documento.IdTipoDocumento = cmbTipoDocumento.SelectedValue.ToString();
                documento.Fecha = dtFecha.Value.Date;
                documento.Monto = numMonto.Value;
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Cliente.IdCliente = Cliente.IdCliente;
                documento.Observacion = txtObservacion.Text;
                documento.IdBanco = this.cmbEntidadBancaria.SelectedValue.ToString();
                documento.NumeroOperacion = this.txtCodigoReferencia.Text;
                documento.NumeroReciboPago = this.txtCodigoOperacion.Text;
                documento.Personal.IdPersonal = (this.cmbPersonal.SelectedValue != null)? int.Parse(this.cmbPersonal.SelectedValue.ToString()):0;
                documento.IdFormaPago = this.cmbFormaPago.SelectedValue.ToString();


                this.bsDetalle.EndEdit();

                BLDP.InsertarCabecera(documento);
                this.txtIdDocumento.Text = (documento.IdDocumento == 0) ? string.Empty : documento.IdDocumento.ToString();





            }
            else
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();
                documento.IdDocumento = Convert.ToInt32(txtIdDocumento.Text);
                documento.IdTipoDocumento = cmbTipoDocumento.SelectedValue.ToString();
                documento.Fecha = dtFecha.Value.Date;
                documento.Monto = numMonto.Value;
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Cliente.IdCliente = Cliente.IdCliente;
                documento.Observacion = txtObservacion.Text;
                documento.IdBanco = this.cmbEntidadBancaria.SelectedValue.ToString();
                documento.NumeroOperacion = this.txtCodigoReferencia.Text;
                documento.NumeroReciboPago = this.txtCodigoOperacion.Text;

                documento.Personal.IdPersonal = (this.cmbPersonal.SelectedValue != null) ? int.Parse(this.cmbPersonal.SelectedValue.ToString()) : 0;
                
                documento.IdFormaPago = this.cmbFormaPago.SelectedValue.ToString();
                this.bsDetalle.EndEdit();
                BLDP.ActualizarCabecera(documento);
            
            
            }
            this.bsDocumentos.EndEdit();
//            if (accion.Equals("insertar"))
            
        }




        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bsDetalle_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            listarDetalle();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (!txtIdDocumento.Text.Equals(""))
            {
                List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones();

                decimal sumMonto = 0;

                foreach (BEAmortizacionVenta amort in vLista)
                {
                    sumMonto += amort.Monto;                
                }
                int idDocumento = Convert.ToInt32(txtIdDocumento.Text);
                frmDocumentoPagoDetalle from = new frmDocumentoPagoDetalle(Cliente, numMonto.Value, sumMonto, idDocumento, (int)this.cmbPersonal.SelectedValue);
                from.Show();
            }
            else
            {
                MostrarMensaje("Debe grabar un documento antes de agregar amortizaciones", MessageBoxIcon.Information);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDetalle();
        }
        private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        {
            MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        }
        private void ActualizarDetalle()
        {
            try
            {
                if (!txtIdDocumento.Text.Equals(""))
                {

                    List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones();

                    decimal sumMonto = 0;

                    foreach (BEAmortizacionVenta amort in vLista)
                    {
                        sumMonto += amort.Monto;


                    }
                    if (sumMonto <= numMonto.Value)
                    {
                        foreach (BEAmortizacionVenta amort in vLista)
                        {
                            bool bOk = new BLDocumentoPago().ActualizarAmortizacionVenta(amort);


                        }

                        MostrarMensaje("Se actualizo la amortización correctamente", MessageBoxIcon.Information);
                    }
                    else
                    {

                        MostrarMensaje("El monto de las amostizaciones exceden el monto del documento", MessageBoxIcon.Information);
                    }
                }
                else {
                    MostrarMensaje("Debe grabar un documento antes de agregar amortizaciones", MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
        }

        private List<BEAmortizacionVenta> ObtenerAmortizaciones()
        {
            List<BEAmortizacionVenta> vLista = new List<BEAmortizacionVenta>();
            BEAmortizacionVenta oBEAmortizacionVenta = null;

            foreach (DataGridViewRow vRow in dgvDetalle.Rows)
            {
                oBEAmortizacionVenta = new BEAmortizacionVenta();
                oBEAmortizacionVenta.Monto = Convert.ToDecimal(vRow.Cells["Monto"].Value.ToString());
                oBEAmortizacionVenta.NroDocumento = (vRow.Cells["NumeroDocumento"].Value == null) ? "" : vRow.Cells["NumeroDocumento"].Value.ToString();
                oBEAmortizacionVenta.Observacion = vRow.Cells["Observacion"].Value.ToString();
                oBEAmortizacionVenta.IdEstado = vRow.Cells["IdEstado"].Value.ToString();
                oBEAmortizacionVenta.IdVenta = Convert.ToInt32(vRow.Cells["IdVenta"].Value.ToString());
                oBEAmortizacionVenta.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                oBEAmortizacionVenta.IdAmortizacionVenta = Convert.ToInt32(vRow.Cells["IdAmortizacionVenta"].Value.ToString());
                vLista.Add(oBEAmortizacionVenta);


            }
            return vLista;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BLDocumentoPago BLDP = new BLDocumentoPago();

            foreach (DataGridViewRow dgvRow in dgvDetalle.Rows)
            {
                if (Convert.ToBoolean(dgvRow.Cells["Seleccionado"].Value).Equals(true))
                {
                    BEAmortizacionVenta beAmortiza = new BEAmortizacionVenta();
                    beAmortiza.IdAmortizacionVenta = Convert.ToInt32(dgvRow.Cells["IdAmortizacionVenta"].Value.ToString());
                    BLDP.EliminarAmortizacionVenta(beAmortiza);
                }
            }
            MostrarMensaje("Se elimino la amortización correctamente", MessageBoxIcon.Information);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void CargarUsuarios()
        {
            List<BEPersonal> vLista = new BLPersonal().ListarPersonal(new BEPersonal());
            this.cmbPersonal.DataSource = vLista;
            cmbPersonal.DisplayMember = "Nombre";
            cmbPersonal.ValueMember = "IdPersonal";
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
            List<BEEntidadBancaria> vLista = new BLEntidadBancaria().Listar(new BEEntidadBancaria());
            vLista.Insert(0, new BEEntidadBancaria() { IdEntidadBancaria = 0, Nombre = "Seleccione", Siglas = "" });

            this.cmbEntidadBancaria.DataSource = vLista;
            cmbEntidadBancaria.DisplayMember = "Nombre";
            cmbEntidadBancaria.ValueMember = "Siglas";
        }

        private void CargarTipoDocumento()
        {
            List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(new BEParametroDetalle() { IdParametro = 8 });
            vLista.Insert(0, new BEParametroDetalle() { Texto = "Seleccione", Valor = "" });
            this.cmbTipoDocumento.DataSource = vLista;
            cmbTipoDocumento.DisplayMember = "Texto";
            cmbTipoDocumento.ValueMember = "Valor";

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();

                documento.IdDocumento = string.IsNullOrEmpty(this.txtIdDocumento.Text) ? 0 :int.Parse( this.txtIdDocumento.Text);
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Observacion = txtObservacion.Text;
                


                BLDP.EliminarCabecera(documento);


            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.bsDocumentos != null )
            //{

            //    BEDocumento compra = (BEDocumento)this.bsDocumentos.Current;
            //    BEClienteProveedor oEntidad = new BEClienteProveedor();
            //    oEntidad.IdCliente = (compra != null)?compra.Cliente.IdCliente: 0 ;

            //    List<BEClienteProveedor> vTemp = new BLClienteProveedor().Listar(oEntidad);
            //    this.cmbClientes.DataSource = vTemp;
            //    this.cmbClientes.DisplayMember = "Nombre";
            //    this.cmbClientes.ValueMember = "IdCliente";
            //    //MostrarMensaje(compra.Proveedor, MessageBoxIcon.Information);


            //}
            


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
                if (this.cmbClientes.Text.Length < 3) return; 
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

        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BEParametroDetalle TipoPago = (BEParametroDetalle)this.cmbTipoDocumento.SelectedItem;
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

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BEParametroDetalle formaPago = (BEParametroDetalle)this.cmbFormaPago.SelectedItem;

                this.cmbEntidadBancaria.SelectedIndex = (this.cmbEntidadBancaria.Items.Count > 0) ? 0 : this.cmbEntidadBancaria.SelectedIndex;
                this.txtCodigoOperacion.Text = string.Empty;

                if (formaPago.Valor == "CHQ" || formaPago.Valor == "DEP" || formaPago.Valor == "DET")
                {
                    this.cmbEntidadBancaria.Enabled = true;
                    this.txtCodigoOperacion.Enabled = true;


                }
                else
                {

                    this.cmbEntidadBancaria.Enabled = false;
                    this.txtCodigoOperacion.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void bindingNavigator1_ItemRemoved(object sender, ToolStripItemEventArgs e)
        {

        }
            
    }
    
}
