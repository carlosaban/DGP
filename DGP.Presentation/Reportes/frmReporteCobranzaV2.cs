using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DGP.BusinessLogic.Ventas;
using DGP.Entities.DataSet;
using DGP.Entities.Reportes;
using DGP.Entities.Ventas;

namespace DGP.Presentation.Reportes {

    public partial class frmReporteCobranzaV2 : Form {
        public frmReporteCobranzaV2() {
            InitializeComponent();
            CargarReporteCobranza();
        }

        private void frmReporteCobranza_Load(object sender, EventArgs e) {

        }

        private void CargarReporteCobranza() {
            DSReporteCuentasPorCobrar oDSHojaCobranza = new DSReporteCuentasPorCobrar();
            
            oDSHojaCobranza = new BLVenta().ReporteHojaCobranzaV2();
            CRHojaCobranzaV2 oCRHojaCobranza = new CRHojaCobranzaV2();
            
            oCRHojaCobranza.Refresh();
            oCRHojaCobranza.SetDataSource(oDSHojaCobranza);

            crvCobranza.ReportSource = oCRHojaCobranza;
            crvCobranza.RefreshReport();
        }

    }
}