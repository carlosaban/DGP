using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.BusinessLogic.Ventas;
using DGP.BusinessLogic;
using DGP.Entities.Ventas;
using DGP.Entities;
using System.Threading;
using DGP.Entities.DataSet;
using DGP.Entities.DataSet.DSEntitiesTableroTableAdapters;
namespace DGP.Presentation.Ventas
{
    partial class frmTableroElectronico
    {
        private DSEntitiesTablero.tb_gastoDataTable dtGastos;
        private int filaAgregada = -1;
        //private DSEntitiesCliente.Tb_Cliente_ProveedorRow rowClienteProveedor;
        private int filaGastoAgregada = -1;

        private void loadTipoGasto()
        {
            Tb_Tipo_GastoTableAdapter tb_Tipo_GastoTableAdapter = new Tb_Tipo_GastoTableAdapter();
            
            this.bdsTipoGasto.DataSource = tb_Tipo_GastoTableAdapter.GetData();
            

        }
        private void loadGastos()
        {
            tb_gastoTableAdapter gastoTableAdapter = new tb_gastoTableAdapter();
            dtGastos = gastoTableAdapter.GetData(VariablesSession.BECaja.IdCaja);

            this.bdsGasto.DataSource = dtGastos;
            
         

        }
        private void cmbTipoGasto_Enter(object sender, EventArgs e)
        {
            Tb_Tipo_GastoTableAdapter tb_Tipo_GastoTableAdapter = new Tb_Tipo_GastoTableAdapter();

            this.bdsTipoGasto.DataSource = tb_Tipo_GastoTableAdapter.GetData();
            

        }


        //private bool validarEdicionFila(ref string mensaje, int rowIndex)
        //{
            
        //    object objMonto = this.dgvGastos.Rows[rowIndex].Cells["montoDataGridViewTextBoxColumn"].Value;
        //    object objConcepto = this.dgvGastos.Rows[rowIndex].Cells["conceptoDataGridViewTextBoxColumn"].Value;
        //    object IdTipoGasto = this.dgvGastos.Rows[rowIndex].Cells["idtipoGastoDataGridViewTextBoxColumn"].Value;

        //    string strFila = string.Empty;
        //    decimal monto =0;
        //    bool flag = false;
        //    //if (objIdPersonal == null || objIdPersonal.ToString() == string.Empty) strFila += ", Personal";
        //    //if (objIdCaja == null || objIdCaja.ToString() == string.Empty) strFila += ", Caja";
        //    if (objMonto == null || objMonto.ToString() == string.Empty) strFila += ", Monto";
        //    else
        //    {
        //        if (!decimal.TryParse(objMonto.ToString(), out monto) || monto < 0) strFila += ", Monto debe ser mayor a cero";

        //    }

        //    if (objConcepto == null || objConcepto.ToString() == string.Empty) strFila += ", Concepto";
        //    if (IdTipoGasto == null || IdTipoGasto.ToString() == string.Empty) strFila += ", Tipo Gasto";

        
        //    if (strFila == string.Empty)
        //    {
        //        mensaje = string.Empty;
        //        return true;

        //    }
        //    else
        //    {

        //        mensaje = "Los Siguientes campos son obligatorios" + strFila + ".";
        //        return false;

        //    }



        //}

        

    }
}
