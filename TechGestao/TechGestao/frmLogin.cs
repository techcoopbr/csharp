﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGestao
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void getKey(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                Close();
            }

            if ((Keys)e.KeyChar == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain TfrmMain = new frmMain();
            this.Visible = false;
            TfrmMain.ShowDialog();
            
            
        }
    }
}
