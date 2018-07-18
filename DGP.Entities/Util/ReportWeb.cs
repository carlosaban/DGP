using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DGP.Entities.Util
{
    public class ReportWeb : IReportWeb
    {
        Hashtable parametros;
        string rutaReporte;
        public ReportWeb(Hashtable parametros , string rutaReporte ) {

            this.parametros = parametros;
            this.rutaReporte = rutaReporte;
        
        
        }

        #region IReportWeb Members

        string IReportWeb.generateHTML()
        {
            return "<html><body>hola mundo<body></html>";
        }

        #endregion
    }
}
