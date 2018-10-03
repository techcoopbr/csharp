using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FirebirdSql.Data.FirebirdClient;
//bliblioteca utilizada para conectgar ao firebird client.



namespace TechCloud
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private string strConn = @"DataSource=localhost; Database=D:\Fontes\DADOS\ROSA\DADOS.FDB; username= SYSDBA; password = masterkey";
        //string de conexao. preencher os dados minimos para conectar

        FbConnection conn;
      


        private void bt_conectar_Click(object sender, EventArgs e)
        {
            conn = new FbConnection(strConn); //conecta utilizando a string    
            lbl_status.Text="CONECTADO";
        }

        private void btn_listar_Click(object sender, EventArgs e)
        {
            FbCommand cmd = new FbCommand("SELECT * FROM PESSOAS", conn); //executa o SQL
            FbDataAdapter DA = new FbDataAdapter(cmd); //cria uma variavel para coletar o resultado

            DataSet DS = new DataSet(); //cria uma variavel DATASET para distribuir o resultado
            conn.Open(); // abre conexao

            //jogar os resultados no grid.

            DA.Fill(DS, "PESSOAS"); //Diz para o DATASET qual a tabela 
            dataGridView1.DataSource = DS; // Linca o GRID com o DATASET
            dataGridView1.DataMember = "PESSOAS"; //especifica nome da base para grid

            conn.Close(); //fecha conecxao
        }

        private void bt_inserir_Click(object sender, EventArgs e)
        { 
            string sqlIncluir = "INSERT INTO PESSOAS (CODIGO, NOME)"
+           "Values (" + textBox1.Text + ", ' " + textBox2.Text + " ')";

            FbCommand cmd = new FbCommand(sqlIncluir, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close(); //fecha conecxao

        }

        private void bt_list_unico_Click(object sender, EventArgs e)
        {
            FbCommand cmd = new FbCommand("SELECT * FROM PESSOAS WHERE CODIGO = " + txt_listar.Text, conn); //executa o SQL
            FbDataAdapter DA = new FbDataAdapter(cmd); //cria uma variavel para coletar o resultado
            DataSet DS = new DataSet(); //cria uma variavel DATASET para distribuir o resultado
            conn.Open(); // abre conexao
            DA.Fill(DS, "PESSOAS"); //Diz para o DATASET qual a tabela 
            dataGridView1.DataSource = DS; // Linca o GRID com o DATASET
            dataGridView1.DataMember = "PESSOAS"; //especifica nome da base para grid

            conn.Close(); //fecha conecxao
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
