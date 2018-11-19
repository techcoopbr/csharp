using System;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using ConexaoDB;
using Persistencia;
using Modelo;
using System.Threading;
using System.Net;

namespace TechCloudConnection
{
    public partial class frmPrincipal : Form
    {

        Thread trd;

        public frmPrincipal()
        {
            IniFile NewIniFile = new IniFile("techconfig");

            InitializeComponent();

            IsConectedToInternet();
            trd = new Thread(SincThread);
            trd.IsBackground = true;
            btnStart.Enabled = false;
            trd.Start();
            NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: SERVIÇO INICIADO");

            Thread trdStatus = new Thread(PrintStatus);
            trdStatus.IsBackground = true;
            trdStatus.Start();
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "Dê duplo clique para verificar o aplicativo";
                notifyIcon1.BalloonTipTitle = "TechCoop Cloud Communication";
                notifyIcon1.ShowBalloonTip(3000);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void SincThread()
        {
            while (1 == 1)
            {
                Thread.Sleep(5000);

                    PessoaDAL pdal = new PessoaDAL();
                    pdal.PostPessoa();
                    

                Thread.Sleep(5000);

                    ItemDAL idal = new ItemDAL();
                    idal.PostItem();


                Thread.Sleep(5000);

                    DuplicataDAL ddal = new DuplicataDAL();
                    ddal.PostDuplicata();


                Thread.Sleep(5000);

                    NfDAL nfdal = new NfDAL();
                    nfdal.PostNf();


                Thread.Sleep(5000);

                    PlanovendaDAL plandal = new PlanovendaDAL();
                    plandal.PostPlanovenda();

            }
        }

        private void PrintStatus()
        {
            while (1 == 1)
            {
                IniFile NewIniFile = new IniFile("techconfig");

                if (lbStatus != null)
                {
                    try
                    {
                        lbStatus.Invoke(new MethodInvoker(delegate { lbStatus.Text = NewIniFile.IniReadString("STATUS", "MSG", "STATUS: NENHUM DADO ENCONTRATO"); }));
                    }
                    catch
                    {
                        try
                        {
                            lbStatus.Text = NewIniFile.IniReadString("STATUS", "MSG", "STATUS: NENHUM DADO ENCONTRATO");
                        }
                        catch
                        {

                        }
                    }
                }


            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IniFile NewIniFile = new IniFile("techconfig");
            NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: SERVIÇO INICIADO");
            pnNuvem1.Visible = false;
            pnNuvem2.Visible = false;
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            trd.Resume();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IniFile NewIniFile = new IniFile("techconfig");
            NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: SERVIÇO PARADO");
            pnNuvem1.Visible = true;
            pnNuvem2.Visible = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            trd.Suspend();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                trd.Abort();
            }
            catch
            {
                trd.Resume();
                trd.Abort();
            }
        }

        void IsConectedToInternet()
        {
            try
            {
                Dns.GetHostEntry("www.apptechcoop.com.br");
                pnNuvem1.Visible = false;
                pnNuvem2.Visible = false;
            }
            catch
            {
                IniFile NewIniFile = new IniFile("techconfig");
                NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: PROBLEMAS DE CONEXÃO COM O SERVIDOR DA TECHCOOP");
                pnNuvem1.Visible = true;
                pnNuvem2.Visible = true;
            }

        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
