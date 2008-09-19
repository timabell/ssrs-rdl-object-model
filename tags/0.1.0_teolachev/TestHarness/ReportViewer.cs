using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Microsoft.Reporting.WinForms;
using System.Diagnostics;
using System.Reflection;
using System.Net;

namespace TestHarness
{

    public partial class ReportViewer : Microsoft.Reporting.WinForms.ReportViewer
    {
        public enum SecurityMode
        {
            SSPI,
            TrustedAccount,
            CustomSecurity
        }

        #region Private Variables

        private SecurityMode _securityMode = SecurityMode.SSPI;
        #endregion

        #region Constructors

        public ReportViewer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            this.LocalReport.ReportEmbeddedResource = null;
            this.LocalReport.ReportPath = null;
            this.Name = "ReportViewer";
            this.Size = new System.Drawing.Size(650, 414);
            this.ResumeLayout(false);

        }

        #endregion

        #region Public Properties

        public new ServerReport ServerReport
        {
            get
            {

                // TODO: Call to security service here to get security mode,
                // e.g. Windows Integrated or Basic
                this.ConfigureSecurity();
                return base.ServerReport;
            }
        }
  
        #endregion

        #region Public Methods

        public new void RefreshReport()
        {
            base.RefreshReport();
        }

        public new void Reset()
        {
            base.Reset();

        }

        #endregion

        #region Private Methods


        private void ConfigureSecurity()
        {
            switch (_securityMode)
            {
                case SecurityMode.SSPI:
                    base.ServerReport.ReportServerCredentials.NetworkCredentials = System.Net.CredentialCache.DefaultCredentials;
                    break;

                case SecurityMode.TrustedAccount:
                    NetworkCredential credentials = new NetworkCredential("user", "password");
                    base.ServerReport.ReportServerCredentials.NetworkCredentials = credentials;
                    break;

            }
        }

        #endregion
    }
}
