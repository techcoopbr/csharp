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
    class CteCceDAL
    {
        public void Gravar(CteCce cce)
        {
            if (cce.Codigo == 0)
                Inserir(cce);
            else
                Atualizar(cce);
        }

        private void Inserir(CteCce cce)
        {

        }

        private void Atualizar(CteCce cce)
        {

        }

        public void Remover(CteCce cce)
        {

        }


        public void PostCteCce()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            CteCceDAL mdal = new CteCceDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from NF p, empresa E where p.sincronizado = 0");

            CteCce c = new CteCce();

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
                                    c.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                c.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { c.Codigo = (int)dr["codigo"]; }
                                if (dr["idcte"] != DBNull.Value) { c.Idcte = (int)dr["idcte"]; }
                                if (dr["arquivo"] != DBNull.Value) { c.Arquivo = (string)dr["arquivo"]; }
                                if (dr["status"] != DBNull.Value) { c.Status = (int)dr[""]; }
                                if (dr["sequencia"] != DBNull.Value) { c.Sequencia = (int)dr[""]; }
                                if (dr["texto"] != DBNull.Value) { c.Texto = (string)dr["texto"]; }
                                if (dr["chaveevento"] != DBNull.Value) { c.Chaveevento = (string)dr["chaveevento"]; }
                                if (dr["dataevento"] != DBNull.Value) { c.Dataevento = (DateTime)dr["dataevento"]; }
                                if (dr["grupoalterado"] != DBNull.Value) { c.Grupoalterado = (string)dr["grupoalterado"]; }
                                if (dr["nomecampo"] != DBNull.Value) { c.Nomecampo = (string)dr["nomecampo"]; }
                                if (dr["numeroitem"] != DBNull.Value) { c.Numeroitem = (int)dr["numeroitem"]; }
                                if (dr["account_id"] != DBNull.Value) { c.Idweb = (int)dr["account_id"]; }


                                
                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + c.Codigo + "\"," +
                                     "\"codigo\":\"" + c.Codigo + "\"," +

                                     "\"idcte\":\"" + c.Idcte + "\"," +
                                     "\"arquivo\":\"" + c.Arquivo + "\"," +
                                     "\"status\":\"" + c.Status + "\"," +
                                     "\"sequencia\":\"" + c.Sequencia + "\"," +
                                     "\"texto\":\"" + c.Texto + "\"," +
                                     "\"chaveevento\":\"" + c.Chaveevento + "\"," +
                                     "\"dataevento\":\"" + c.Dataevento + "\"," +
                                     "\"grupoalterado\":\"" + c.Grupoalterado + "\"," +
                                     "\"nomecampo\":\"" + c.Nomecampo + "\"," +
                                     "\"numeroitem\":\"" + c.Numeroitem + "\"," +
                                     "\"account_id\" :\"" + c.Idweb + "\"}";

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
                                    CteCceDAL ccedal = new CteCceDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE NF P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(c.Codigo));
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

        public CteCce ObterPorId(long id)
        {
            var cce = new CteCce();

            return cce;
        }
    }
}
