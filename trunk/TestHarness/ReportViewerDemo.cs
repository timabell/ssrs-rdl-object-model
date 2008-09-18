using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestHarness
{
    public partial class ReportViewerDemo : Form
    {
        public ReportViewerDemo()
        {
            InitializeComponent();
        }

        private void ReportViewerDemo_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}