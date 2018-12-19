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
    class MovimentoGerencialDAL
    {
        public void Gravar(MovimentoGerencial mov)
        {
            if (mov.Codigo == 0)
                Inserir(mov);
            else
                Atualizar(mov);
        }

        private void Inserir(MovimentoGerencial mov)
        {

        }

        private void Atualizar(MovimentoGerencial mov)
        {

        }

        public void Remover(MovimentoGerencial mov)
        {

        }


        public void PostMovimentoGerencial()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            MovimentoGerencialDAL mdal = new MovimentoGerencialDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from MOVIMENTO_GERENCIAL p, empresa E where p.sincronizado = 0");

            MovimentoGerencial m = new MovimentoGerencial();

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

                        Login.login();

                        httpWebRequestt.Headers.Add("Authorization", "Basic " + Login.encoded);



                        var httpResponsee = (HttpWebResponse)httpWebRequestt.GetResponse();

                        using (var streamReaderr = new StreamReader(httpResponsee.GetResponseStream()))
                        {
                            var responseTextt = streamReaderr.ReadToEnd();
                            var serializerr = new JavaScriptSerializer();
                            dynamic usr = serializerr.DeserializeObject(responseTextt);
                            DocumentId = usr["id"];

                            conn.ExecuteQueries("UPDATE MOVIMENTO_GERENCIAL P SET P.IDMOVIMENTOGERENCIALWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

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

                        Login.login();

                        httpWebRequesttD.Headers.Add("Authorization", "Basic " + Login.encoded);


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

                        Login.login();

                        httpWebRequest.Headers.Add("Authorization", "Basic " + Login.encoded);

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            if (Convert.ToInt32(dr["Idweb"]) > 0)
                            {
                                if (DocumentId != 0)
                                {
                                    m.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                m.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { m.Codigo = (int)dr["codigo"]; }
                                if (dr["idmovimento"] != DBNull.Value) { m.Idmovimento = (int)dr["idmovimento"]; }
                                if (dr["idplanog"] != DBNull.Value) { m.Idplanog = (string)dr["idplanog"]; }
                                if (dr["idpessoas"] != DBNull.Value) { m.Idpessoas = (int)dr["idpessoas"]; }
                                if (dr["datalancamento"] != DBNull.Value) { m.Datalancamento = (DateTime)dr["datalancamento"]; }
                                if (dr["data"] != DBNull.Value) { m.Data = (DateTime)dr["data"]; }
                                if (dr["descricao"] != DBNull.Value) { m.Descricao = (string)dr["descricao"]; }
                                if (dr["valor"] != DBNull.Value) { m.Valor = (decimal)dr["valor"]; }
                                if (dr["idempresa"] != DBNull.Value) { m.Idempresa = (int)dr["idempresa"]; }
                                if (dr["nomepessoa"] != DBNull.Value) { m.Nomepessoa = (string)dr["nomepessoa"]; }
                                if (dr["idacumulador"] != DBNull.Value) { m.Idacumulador = (int)dr["idacumulador"]; }
                                if (dr["account_id"] != DBNull.Value) { m.Idweb = (int)dr["account_id"]; }



                                //mdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + m.Codigo + "\"," +
                                     "\"codigo\":\"" + m.Codigo + "\"," +

                                     "\"idmovimento\":\"" + m.Idmovimento + "\"," +
                                     "\"idplanog\":\"" + m.Idplanog + "\"," +
                                     "\"idpessoas\":\"" + m.Idpessoas + "\"," +
                                     "\"datalancamento\":\"" + m.Datalancamento + "\"," +
                                     "\"data\":\"" + m.Data + "\"," +
                                     "\"descricao\":\"" + m.Descricao + "\"," +
                                     "\"valor\":\"" + m.Valor + "\"," +
                                     "\"idempresa\":\"" + m.Idempresa + "\"," +
                                     "\"nomepessoa\":\"" + m.Nomepessoa + "\"," +
                                     "\"idacumulador\":\"" + m.Idacumulador + "\"," +
                                     "\"account_id\":\"" + m.Idweb + "\"}";

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

                                        conn.ExecuteQueries("UPDATE MOVIMENTO_GERENCIAL P SET P.IDMOVIMENTOGERENCIALWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    MovimentoGerencialDAL movdal = new MovimentoGerencialDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE MOVIMENTO_GERENCIAL P SET P.SINCRONIZADO = 1 WHERE P.ID = " + Convert.ToString(m.Codigo));
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

        public MovimentoGerencial ObterPorId(long id)
        {
            var mov = new MovimentoGerencial();

            return mov;
        }
    }
}
