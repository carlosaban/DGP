using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.Entities;
using DGP.Entities.Seguridad;
using DGP.Entities.Ventas;
using DGP.BusinessLogic;
using DGP.BusinessLogic.Seguridad;
using DGP.BusinessLogic.Ventas;

namespace DGP.Presentation.Ventas
{
    public partial class frmDocumentoPagoDetalle : Form
    {
        BEClienteProveedor Cliente;
        decimal montoAplicar;
        int idDocumento;

        public frmDocumentoPagoDetalle(BEClienteProveedor Cliente, decimal montoAplicar, int idDocumento)
        {
            InitializeComponent();
            this.Cliente = Cliente;
            this.montoAplicar = montoAplicar;
            this.idDocumento =idDocumento;
            inicializarFormulario();
        }

        private void inicializarFormulario()
        {
            CargarUsuarios();
            cbUsuario.SelectedValue = VariablesSession.BEUsuarioSession.IdPersonal;
        }

        private void frmDocumentoPagoDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDetalle.AutoGenerateColumns = false;
                BLDocumentoPago BLDP = new BLDocumentoPago();
                txtCliente.Text = Cliente.Nombre;
                cmbIdVenta.DataSource = BLDP.ListarVentaXCliente(Cliente.IdCliente);

                cmbIdVenta.ValueMember = "MontoTotal";
                cmbIdVenta.DisplayMember = "IdVenta";
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }


           
        }

        private void MostrarMensaje(string pMensaje, MessageBoxIcon pMsgBoxicon)
        {
            MessageBox.Show(pMensaje, "DGP", MessageBoxButtons.OK, pMsgBoxicon);
        }

        private void CargarUsuarios()
        {
            List<BEPersonal> vLista = new BLPersonal().ListarPersonal(new BEPersonal());
            cbUsuario.DataSource = vLista;
            cbUsuario.DisplayMember = "Login";
            cbUsuario.ValueMember = "IdPersonal";
        }
        private void GrabarDetalle()
        {
            try
            {
                    int intIdUsuario = 0;
                    bool boIndicador = true;
                    int.TryParse(cbUsuario.SelectedValue.ToString(), out intIdUsuario);
                    if (intIdUsuario > 0 && intIdUsuario != VariablesSession.BEUsuarioSession.IdPersonal)
                    {
                        boIndicador = (MessageBox.Show("La amortización se va a registrar con otro usuario, desea continuar?", "DGP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                    }
                    if (boIndicador)
                    {

                        List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones();

                        foreach (BEAmortizacionVenta amort in vLista)
                        {
                            bool bOk = new BLDocumentoPago().InsertarAmortizacionVenta(amort);

                        } 
                        MostrarMensaje("Se registró la amortización correctamente", MessageBoxIcon.Information);
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
                        oBEAmortizacionVenta.NroDocumento = vRow.Cells["NumeroDocumento"].Value.ToString();
                        oBEAmortizacionVenta.Observacion = vRow.Cells["Observacion"].Value.ToString();
                        oBEAmortizacionVenta.IdEstado = vRow.Cells["IdEstado"].Value.ToString();
                        oBEAmortizacionVenta.IdVenta = Convert.ToInt32(vRow.Cells["IdVenta"].Value.ToString());
                        oBEAmortizacionVenta.IdCliente = Convert.ToInt32(vRow.Cells["idCliente"].Value.ToString());
                        oBEAmortizacionVenta.IdPersonal = Convert.ToInt32(cbUsuario.SelectedValue.ToString());
                        oBEAmortizacionVenta.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                        oBEAmortizacionVenta.Caja = VariablesSession.BECaja;
                        oBEAmortizacionVenta.IdDocumento = idDocumento;
                        vLista.Add(oBEAmortizacionVenta);
                    
                
            }
            return vLista;
        }

        private void cmbIdVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdVenta_DisplayMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbIdVenta_MouseClick(object sender, MouseEventArgs e)
        {
            numMontoVenta.Value = Convert.ToDecimal(cmbIdVenta.SelectedValue.ToString());
        }

        private void numMonto_ValueChanged(object sender, EventArgs e)
        {
            if (numMonto.Value > montoAplicar)
            {
                MessageBox.Show("DGP", "El monto debe ser menor o igual que el monto a aplicar");
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            dgvDetalle.Rows.Add(new object[]{ numMonto.Value.ToString(), txtNroDocumento.Text, "REG", cmbIdVenta.Text, Cliente.IdCliente.ToString(), txtObservacion.Text });

            
            
        }

        private void cmbIdVenta_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            GrabarDetalle();
        }
    }
}
