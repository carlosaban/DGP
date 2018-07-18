using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DGP.Entities.Util;

namespace DGP.Presentation.Reportes
{
    public partial class FrmReporteWeb : Form
    {
        private IReportWeb IReportWeb;
        public FrmReporteWeb( IReportWeb pIReportWeb)
        {

            InitializeComponent();
            IReportWeb = pIReportWeb;
        }

        private void FrmReporteWeb_Load(object sender, EventArgs e)
        {
            this.webReport.Navigate("about:blank");
            try
            {
                if (webReport.Document != null)
                {
                    webReport.Document.Write(string.Empty);
                }
            }
            catch (Exception ex)
            { } // do nothing with this
            string html = IReportWeb.generateHTML();
            
            webReport.DocumentText = html;
            //this.reportViewer1.RefreshReport();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.webReport.Print();
        }
    }
}
