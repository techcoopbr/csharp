using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invernada
{
    public partial class frmRptTeste : Form
    {
        public frmRptTeste()
        {
            InitializeComponent();
        }

        private void formRptTeste_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
