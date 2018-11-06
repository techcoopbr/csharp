using System;
using FirebirdSql.Data.FirebirdClient;
using ConexaoDB;
using Modelo;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace Modelo
{
    class MdfeReboqueDAL
    {
        public void Gravar(MdfeReboque reb)
        {
            if (reb.Codigo == 0)
                Inserir(reb);
            else
                Atualizar(reb);
        }

        private void Inserir(MdfeReboque reb)
        {

        }

        private void Atualizar(MdfeReboque reb)
        {

        }

        public void Remover(MdfeReboque reb)
        {

        }


        public void PostMdfeReboque()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            MdfeReboqueDAL rdal = new MdfeReboqueDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from NF p, empresa E where p.sincronizado = 0");

            MdfeReboque r = new MdfeReboque();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int DocumentId = 0;
                    // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                    try
                    {
                        //string webAddrr = "http://localhost:3000/document_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://apptechcoop.com.br/document_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        var httpWebRequestt = (HttpWebRequest)WebRequest.Create(webAddrr);
                        httpWebRequestt.ContentType = "application/json; charset=utf-8";
                        httpWebRequestt.Method = "GET";

                        String usernamee = "admin@jefferson.com";
                        String passwordd = "2311luje2311";
                        String encodedd = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(usernamee + ":" + passwordd));
                        httpWebRequestt.Headers.Add("Authorization", "Basic " + encodedd);



                        var httpResponsee = (HttpWebResponse)httpWebRequestt.GetResponse();

                        using (var streamReaderr = new StreamReader(httpResponsee.GetResponseStream()))
                        {
                            var responseTextt = streamReaderr.ReadToEnd();
                            var serializerr = new JavaScriptSerializer();
                            dynamic usr = serializerr.DeserializeObject(responseTextt);
                            DocumentId = usr["id"];

                            conn.ExecuteQueries("UPDATE NF P SET P.IDNFWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                        }
                    }
                    catch (Exception e)
                    {
                        DocumentId = 0;
                    }

                    // ENCONTRA O ID FOLK DA PESSOA DA DUPLICATA
                    try
                    {
                        //string webAddrD = "http://localhost:3000/folk_accounts/" + Convert.ToString(dr["idpessoas"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrD = "http://apptechcoop.com.br/folk_accounts/" + Convert.ToString(dr["idpessoas"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        var httpWebRequesttD = (HttpWebRequest)WebRequest.Create(webAddrD);
                        httpWebRequesttD.ContentType = "application/json; charset=utf-8";
                        httpWebRequesttD.Method = "GET";

                        String usernameeD = "admin@jefferson.com";
                        String passworddD = "2311luje2311";
                        String encodeddD = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(usernameeD + ":" + passworddD));
                        httpWebRequesttD.Headers.Add("Authorization", "Basic " + encodeddD);



                        var httpResponseeD = (HttpWebResponse)httpWebRequesttD.GetResponse();

                        using (var streamReaderrD = new StreamReader(httpResponseeD.GetResponseStream()))
                        {
                            var responseTexttD = streamReaderrD.ReadToEnd();
                            var serializerD = new JavaScriptSerializer();
                            dynamic usrD = serializerD.DeserializeObject(responseTexttD);
                            //p.Idfolk = usrD["id"];
                            // >>>>>>  não existe o campo idfolk na tabela operacoes  <<<<<<
                        }
                    }
                    catch (Exception e)
                    {
                        //p.Idfolk = 0;
                    }

                    //////////////////////////////////////////////
                    try
                    {

                        string webAddr;

                        if (DocumentId == 0)
                        {
                            //webAddr = "http://localhost:3000/documents";
                            webAddr = "http://apptechcoop.com.br/documents";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/documents/" + Convert.ToString(DocumentId) + ".json";
                            webAddr = "http://apptechcoop.com.br/documents/" + Convert.ToString(DocumentId) + ".json";
                        }

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        if (DocumentId == 0)
                        {
                            httpWebRequest.Method = "POST";
                        }
                        else
                        {
                            httpWebRequest.Method = "PUT";
                        }

                        String username = "admin@jefferson.com";
                        String password = "2311luje2311";
                        String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(username + ":" + password));
                        httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            if (Convert.ToInt32(dr["Idweb"]) > 0)
                            {
                                if (DocumentId != 0)
                                {
                                    r.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                r.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { r.Codigo = (int)dr["codigo"]; }
                                if (dr["idmdfe"] != DBNull.Value) { r.Idmdfe = (int)dr["idmdfe"]; }
                                if (dr["idveiculo"] != DBNull.Value) { r.Idveiculo = (int)dr["idveiculo"]; }
                                if (dr["placa"] != DBNull.Value) { r.Placa = (string)dr["placa"]; }
                                if (dr["tipocarroceria"] != DBNull.Value) { r.Tipocarroceria = (int)dr["tipocarroceria"]; }
                                if (dr["ufveiculo"] != DBNull.Value) { r.Ufveiculo = (string)dr["ufveiculo"]; }
                                if (dr["capacidadekg"] != DBNull.Value) { r.Capacidadekg = (decimal)dr["capacidadekg"]; }
                                if (dr["renavam"] != DBNull.Value) { r.Renavam = (string)dr["renavam"]; }
                                if (dr["capacidadem3"] != DBNull.Value) { r.Capacidadem3 = (decimal)dr["capacidadem3"]; }
                                if (dr["tara"] != DBNull.Value) { r.Tara = (decimal)dr["tara"]; }
                                if (dr["proprietario"] != DBNull.Value) { r.Proprietario = (int)dr["proprietario"]; }
                                if (dr["tipoproprietario"] != DBNull.Value) { r.Tipoproprietario = (int)dr["tipoproprietario"]; }
                                if (dr["cpfcnpj"] != DBNull.Value) { r.Cpfcnpj = (string)dr["cpfcnpj"]; }
                                if (dr["nome"] != DBNull.Value) { r.Nome = (string)dr["nome"]; }
                                if (dr["ie"] != DBNull.Value) { r.Ie = (string)dr["ie"]; }
                                if (dr["rntrc"] != DBNull.Value) { r.Rntrc = (string)dr["rntrc"]; }
                                if (dr["uf"] != DBNull.Value) { r.Uf = (string)dr["uf"]; }
                                if (dr["account_id"] != DBNull.Value) { r.Idweb = (int)dr["account_id"]; }



                                //rdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + r.Codigo + "\"," +
                                     "\"codigo\":\"" + r.Codigo + "\"," +

                                     "\"idmdfe\":\"" + r.Idmdfe + "\"," +
                                     "\"idveiculo\":\"" + r.Idveiculo + "\"," +
                                     "\"placa\":\"" + r.Placa + "\"," +
                                     "\"tipocarroceria\":\"" + r.Tipocarroceria + "\"," +
                                     "\"ufveiculo\":\"" + r.Ufveiculo + "\"," +
                                     "\"capacidadekg\":\"" + r.Capacidadekg + "\"," +
                                     "\"renavam\":\"" + r.Renavam + "\"," +
                                     "\"capacidadem3\":\"" + r.Capacidadem3 + "\"," +
                                     "\"tara\":\"" + r.Tara + "\"," +
                                     "\"proprietario\":\"" + r.Proprietario + "\"," +
                                     "\"tipoproprietario\":\"" + r.Tipoproprietario + "\"," +
                                     "\"cpfcnpj\":\"" + r.Cpfcnpj + "\"," +
                                     "\"nome\":\"" + r.Nome + "\"," +
                                     "\"ie\":\"" + r.Ie + "\"," +
                                     "\"rntrc\":\"" + r.Rntrc + "\"," +
                                     "\"uf\":\"" + r.Uf + "\"," +
                                     "\"account_id\":\"" + r.Idweb + "\"}";

                                json = json.Replace("\r\n", "");

                                streamWriter.Write(json);
                                streamWriter.Flush();

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();

                                }

                                // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                                try
                                {
                                    //string webAddrr = "http://localhost:3000/document_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/document_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    var httpWebRequestt = (HttpWebRequest)WebRequest.Create(webAddrr);
                                    httpWebRequestt.ContentType = "application/json; charset=utf-8";
                                    httpWebRequestt.Method = "GET";

                                    String usernamee = "admin@jefferson.com";
                                    String passwordd = "2311luje2311";
                                    String encodedd = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(usernamee + ":" + passwordd));
                                    httpWebRequestt.Headers.Add("Authorization", "Basic " + encodedd);



                                    var httpResponsee = (HttpWebResponse)httpWebRequestt.GetResponse();

                                    using (var streamReaderr = new StreamReader(httpResponsee.GetResponseStream()))
                                    {
                                        var responseTextt = streamReaderr.ReadToEnd();
                                        var serializerr = new JavaScriptSerializer();
                                        dynamic usr = serializerr.DeserializeObject(responseTextt);
                                        DocumentId = usr["id"];

                                        conn.ExecuteQueries("UPDATE NF P SET P.IDNFWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    MdfeReboqueDAL perdal = new MdfeReboqueDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE NF P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(r.Codigo));
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        //conn.ExecuteQueries("UPDATE PESSOAS P SET P.SINCRONIZADO = 0 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
                        NewIniFile.IniWriteString("STATUS", "MSG", e.Message);
                        dr.NextResult();
                        //conn.CloseConnection();
                    }
                }
            }
            else
            {
                NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: NENHUM REGISTRO NOVO");
            }

            conn.CloseConnection();
            NewIniFile.IniWriteString("STATUS", "MSG", "ATUALIZAÇÃO FINALIZADA");
        }

        public MdfeReboque ObterPorId(long id)
        {
            var per = new MdfeReboque();

            return per;
        }
    }
}
