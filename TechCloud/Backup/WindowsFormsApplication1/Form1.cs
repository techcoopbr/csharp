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



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string strConn = @"DataSource=localhost; Database=C:\arq.fdb; username= SYSDBA; password = masterkey";
        //string de conexao. preencher os dados minimos para conectar

        FbConnection conn;



       


        private void bt_conectar_Click(object sender, EventArgs e)
        {
            conn = new FbConnection(strConn); //conecta utilizando a string    
            lbl_status.Text="CONECTADO";
        }

        private void btn_listar_Click(object sender, EventArgs e)
        {
            FbCommand cmd = new FbCommand("SELECT * FROM teste", conn); //executa o SQL
            FbDataAdapter DA = new FbDataAdapter(cmd); //cria uma variavel para coletar o resultado

            DataSet DS = new DataSet(); //cria uma variavel DATASET para distribuir o resultado
            conn.Open(); // abre conexao

            //jogar os resultados no grid.

            DA.Fill(DS, "teste"); //Diz para o DATASET qual a tabela 
            dataGridView1.DataSource = DS; // Linca o GRID com o DATASET
            dataGridView1.DataMember = "teste"; //especifica nome da base para grid

            conn.Close(); //fecha conecxao
        }

        private void bt_inserir_Click(object sender, EventArgs e)
        { 
            string sqlIncluir = "INSERT INTO teste (num, nome)"
+ "Values (" + textBox1.Text + ", ' " + textBox2.Text + " ')";

            FbCommand cmd = new FbCommand(sqlIncluir, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close(); //fecha conecxao

        }

        private void bt_list_unico_Click(object sender, EventArgs e)
        {
            FbCommand cmd = new FbCommand("SELECT * FROM teste WHERE num=" + txt_listar.Text, conn); //executa o SQL
            FbDataAdapter DA = new FbDataAdapter(cmd); //cria uma variavel para coletar o resultado
            DataSet DS = new DataSet(); //cria uma variavel DATASET para distribuir o resultado
            conn.Open(); // abre conexao
            DA.Fill(DS, "teste"); //Diz para o DATASET qual a tabela 
            conn.Close(); //fecha conecxao

            //textBox1.DataBindings.Add("text", DS.Tables["teste"], "nome");
            //associa o textbox a o dataset.

            lbl_nome.Text = DS.Tables["teste"].Rows[0][1].ToString();
            //jogar o resultado em um texbox.
        }



        
    }
}
