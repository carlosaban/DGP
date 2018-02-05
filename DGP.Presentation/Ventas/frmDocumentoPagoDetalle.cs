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
        private BEClienteProveedor Cliente;
        private decimal montoMax;
        private int IdDocumento;
        private decimal sumMonto;
        public frmDocumentoPagoDetalle(BEClienteProveedor Cliente, decimal montoMax, decimal sumMonto, int idDocumento, int IdPersonal)
        {
            InitializeComponent();
            this.Cliente = Cliente;
            this.IdDocumento =idDocumento;
            this.montoMax = montoMax;
            this.sumMonto = sumMonto;
        }


        private void frmDocumentoPagoDetalle_Load(object sender, EventArgs e)
        {
            cargarFormularo();
                      
        }

        private void cargarFormularo()
        {
            try
            {
                BLDocumentoPago BLDP = new BLDocumentoPago();
                txtCliente.Text = Cliente.Nombre;
                this.dgvDetalle.DataSource = BLDP.ListarVentaXCliente(Cliente.IdCliente, this.IdDocumento);

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

        
        private void GrabarDetalle()
        {
            try
            {
                    bool boIndicador = true;
                    int i =0;

                    if (boIndicador)
                    {

                        List<BEAmortizacionVenta> vLista = ObtenerAmortizaciones();
                        if (sumMonto + nudMontoAplicar.Value <= montoMax)
                        {
                            foreach (BEAmortizacionVenta amort in vLista)
                            {
                                if (dgvDetalle.Rows[i].Cells["MontoAAplicar"].Value != null)
                                {
                                    bool bOk = new BLDocumentoPago().InsertarAmortizacionVenta(amort);
                                }
                                i++;
                            }
                            MostrarMensaje("Se registró la amortización correctamente", MessageBoxIcon.Information);

                        }
                        else {
                            MostrarMensaje("La suma de montos de las amortizaciones excede el monto del documento", MessageBoxIcon.Information);
                            cargarFormularo();
                        }
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
                        oBEAmortizacionVenta.Monto = Convert.ToDecimal(vRow.Cells["montoTotalDataGridViewTextBoxColumn"].Value.ToString());
                        oBEAmortizacionVenta.NroDocumento = string.Empty;
                        oBEAmortizacionVenta.Observacion = string.Empty;
                        oBEAmortizacionVenta.IdEstado = "REG";
                        oBEAmortizacionVenta.IdVenta = Convert.ToInt32(vRow.Cells["idVentaDataGridViewTextBoxColumn"].Value.ToString());
                        oBEAmortizacionVenta.IdCliente = this.Cliente.IdCliente;
                        oBEAmortizacionVenta.IdPersonal = VariablesSession.BEUsuarioSession.IdPersonal;
                        oBEAmortizacionVenta.BEUsuarioLogin = VariablesSession.BEUsuarioSession;
                        oBEAmortizacionVenta.Caja = VariablesSession.BECaja;
                        oBEAmortizacionVenta.IdDocumento = this.IdDocumento;
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
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {

                decimal montoTotal = this.nudMontoAplicar.Value;
                decimal montoAcarreo = 0;

                foreach (DataGridViewRow vRow in this.dgvDetalle.Rows)
                {

                    if (montoAcarreo >= montoTotal) break;

                    decimal montoSaldo = 0;
                    bool okParse = decimal.TryParse(vRow.Cells["totalSaldoDataGridViewTextBoxColumn"].Value.ToString(), out montoSaldo);
                    if (!okParse || montoSaldo < 0) continue; //excluir a los negativos
                    decimal delta = montoTotal - montoAcarreo;

                    decimal montoAmortizar = (delta >= montoSaldo) ? montoSaldo : montoTotal - montoAcarreo;
                    vRow.Cells["MontoAAplicar"].Value = montoAmortizar.ToString();
                    montoAcarreo = montoAcarreo + montoAmortizar;


                    
                }



            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, MessageBoxIcon.Error);
            }
            
        }

     

        //private void btnGrabar_Click(object sender, EventArgs e)
        //{
        //    GrabarDetalle();
        //}

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GrabarDetalle();
        }
    }
}
