using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace ConexaoDB
{
    public class Connection_Query
    {
        private static string StrConnection()
        {
            IniFile NewIniFile = new IniFile("techconfig");


            string strConn =
            @"User=" + NewIniFile.IniReadString("BANCO_DADOS", "USERBD", "SYSDBA") + ";" +
            "Password=" + NewIniFile.IniReadString("BANCO_DADOS", "PASSBD", "masterkey") + ";" +
            "Database=" + NewIniFile.IniReadString("BANCO_DADOS", "CAMINHO", "masterkey") + ";";
            if (NewIniFile.IniReadString("BANCO_DADOS", "IP", "127.0.0.1") == "127.0.0.1")
            {
                strConn = strConn + "DataSource=;";
            }
            else
            {
                strConn = strConn + "DataSource=" + NewIniFile.IniReadString("BANCO_DADOS", "IP", "127.0.0.1") + ";";
            }

            //"Port=3050;" +
            strConn = strConn + "Dialect=3;" +
            "Charset=ISO8859_1;" +
            "Role=;" +
            "Connection lifetime=15;" +
            "Pooling=true;" +
            //"MinPoolSize=0;" +
            //"MaxPoolSize=50;" +
            //"Packet Size=8192;" +
            "ServerType=0";

            return strConn;
        }

        private string strConn = StrConnection();//"DataSource=localhost; Database=D:\Fontes\DADOS\ROSA\DADOS.FDB; username= SYSDBA; password = masterkey";
        //string de conexao. preencher os dados minimos para conectar

        FbConnection conn;

        public void OpenConection()
        {
            conn = new FbConnection(strConn); //conecta utilizando a stri
            conn.Open();
        }


        public void CloseConnection()
        {
            conn.Close();
        }


        public void ExecuteQueries(string Query_)
        {
            FbCommand cmd = new FbCommand(Query_, conn);
            cmd.ExecuteNonQuery();
        }

        public FbDataReader DataReader(string Query_)
        {
            FbCommand cmd = new FbCommand(Query_, conn);
            FbDataReader dr = cmd.ExecuteReader();
            return dr;
        }


        public object ShowDataInGridView(string Query_)
        {
            FbDataAdapter dr = new FbDataAdapter(Query_, strConn);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}
