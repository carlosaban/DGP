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
            
            CargarUsuarios();
            CargarFormaDePago();
            CargarEntidadBancaria();

            this.bsDocumentos.DataSource = bs.DataSource;
            this.accion = accion;
            this.Cliente = cliente;
            bindingNavigatorPositionItem.Text = cell.ToString();
        }
        public frmDocumentoPago(BindingSource bs, string accion, BEClienteProveedor cliente)
        {
            InitializeComponent();

            CargarUsuarios();
            CargarFormaDePago();
            CargarEntidadBancaria();

            this.bsDocumentos.DataSource = bs.DataSource;
            this.accion = accion;
            this.Cliente = cliente;
        }
       

        private void frmDocumentoPago_Load(object sender, EventArgs e)
        {
            if (accion.Equals("actualizar"))
            {
                txtIdDocumento.DataBindings.Add("Text", bsDocumentos, "IdDocumento");
                dtFecha.DataBindings.Add("Text", bsDocumentos, "Fecha");
                numMonto.DataBindings.Add("Text", bsDocumentos, "Monto");
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
            Actualizar(accion);
        }


        private void Actualizar(String accion)
        {
            if (accion.Equals("actualizar"))
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
                documento.Banco.IdEntidadBancaria = (int)this.cmbEntidadBancaria.SelectedValue;
                documento.CodigoReferencia = this.txtCodigoReferencia.Text;
                documento.IdTipoPago = this.cmbTipoPago.SelectedItem.ToString();

                BLDP.ActualizarCabecera(documento);
            }

            if (accion.Equals("insertar"))
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                BEDocumento documento = new BEDocumento();
                documento.IdTipoDocumento = cmbTipoDocumento.SelectedItem.ToString();
                documento.Fecha = dtFecha.Value.Date;
                documento.Monto = numMonto.Value;
                documento.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                documento.Cliente.IdCliente = Cliente.IdCliente;
                documento.Observacion = txtObservacion.Text;
                BLDP.InsertarCabecera(documento);
            }
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
            montoAplicar = 100; //numMontoAplica.Value;
            int idDocumento = Convert.ToInt32(txtIdDocumento.Text);
            frmDocumentoPagoDetalle from = new frmDocumentoPagoDetalle(Cliente, montoAplicar, idDocumento, (int)this.cmbPersonal.SelectedValue);
            from.Show();
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

                    List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones();

                    foreach (BEAmortizacionVenta amort in vLista)
                    {
                        bool bOk = new BLDocumentoPago().ActualizarAmortizacionVenta(amort);

                    }
                    MostrarMensaje("Se actualizo la amortización correctamente", MessageBoxIcon.Information);
                

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
        private void CargarFormaDePago()
        {
            List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(new BEParametroDetalle() { IdParametro = 7 } );
            this.cmbTipoPago.DataSource = vLista ;
            cmbTipoPago.DisplayMember = "Texto";
            cmbTipoPago.ValueMember = "Valor";
        }

        private void CargarEntidadBancaria()
        {
            List<BEEntidadBancaria> vLista = new BLEntidadBancaria().Listar(new BEEntidadBancaria());
            this.cmbEntidadBancaria.DataSource = vLista;
            cmbEntidadBancaria.DisplayMember = "Nombre";
            cmbEntidadBancaria.ValueMember = "Siglas";
        }

        private void CargarTipoDocumento()
        {
            List<BEParametroDetalle> vLista = new BLParametroDetalle().Listar(new BEParametroDetalle() { IdParametro = 8 });
            this.cmbTipoDocumento.DataSource = vLista;
            cmbTipoDocumento.DisplayMember = "Texto";
            cmbTipoDocumento.ValueMember = "Valor";
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }
    }
    
}
