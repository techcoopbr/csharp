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
    class CteCteDocremDAL
    {
        public void Gravar(CteCteDocrem doc)
        {
            if (doc.Codigo == 0)
                Inserir(doc);
            else
                Atualizar(doc);
        }

        private void Inserir(CteCteDocrem doc)
        {

        }

        private void Atualizar(CteCteDocrem doc)
        {

        }

        public void Remover(CteCteDocrem doc)
        {

        }


        public void PostCteCteDocremm()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            CteCteDocremDAL ddal = new CteCteDocremDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from CTE_CTE_DOCREM p, empresa E where p.sincronizado = 0");

            CteCteDocrem c = new CteCteDocrem();

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

                            conn.ExecuteQueries("UPDATE CTE_CTE_DOCREM P SET P.IDCTECTEDOCREMWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

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
                                if (dr["modelo"] != DBNull.Value) { c.Modelo = (string)dr["modelo"]; }
                                if (dr["serie"] != DBNull.Value) { c.Serie = (int)dr["serie"]; }
                                if (dr["numero"] != DBNull.Value) { c.Numero = (int)dr["numero"]; }
                                if (dr["dataemissao"] != DBNull.Value) { c.Dataemissao = (DateTime)dr["dataemissao"]; }
                                if (dr["cfop"] != DBNull.Value) { c.Cfop = (string)dr["cfop"]; }
                                if (dr["tipodoc"] != DBNull.Value) { c.Tipodoc = (int)dr["tipodoc"]; }
                                if (dr["numeroromaneio"] != DBNull.Value) { c.Numeroromaneio = (int)dr["numeroromaneio"]; }
                                if (dr["numeropedido"] != DBNull.Value) { c.Numeropedido = (int)dr["numeropedido"]; }
                                if (dr["chave"] != DBNull.Value) { c.Chave = (string)dr["chave"]; }
                                if (dr["pin"] != DBNull.Value) { c.Pin = (string)dr["pin"]; }
                                if (dr["tipooriginario"] != DBNull.Value) { c.Tipooriginario = (int)dr["tipooriginario"]; }
                                if (dr["descricao"] != DBNull.Value) { c.Descricao = (string)dr["descricao"]; }
                                if (dr["base_icms"] != DBNull.Value) { c.Base_icms = (decimal)dr["base_icms"]; }
                                if (dr["valor_icms"] != DBNull.Value) { c.Valor_icms = (decimal)dr["valor_icms"]; }
                                if (dr["base_icmsst"] != DBNull.Value) { c.Base_icmsst = (decimal)dr["base_icmsst"]; }
                                if (dr["valor_icmsst"] != DBNull.Value) { c.Valor_icmsst = (decimal)dr["valor_icmsst"]; }
                                if (dr["pesokg"] != DBNull.Value) { c.Pesokg = (decimal)dr["pesokg"]; }
                                if (dr["valorprodutos"] != DBNull.Value) { c.Valorprodutos = (decimal)dr["valorprodutos"]; }
                                if (dr["valortotal"] != DBNull.Value) { c.Valortotal = (decimal)dr["valortotal"]; }
                                if (dr["moddocumento"] != DBNull.Value) { c.Moddocumento = (int)dr["moddocumento"]; }
                                if (dr["empresa"] != DBNull.Value) { c.Empresa = (int)dr["empresa"]; }
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
                                     "\"modelo\":\"" + c.Modelo + "\"," +
                                     "\"serie\":\"" + c.Serie + "\"," +
                                     "\"numero\":\"" + c.Numero + "\"," +
                                     "\"dataemissao\":\"" + c.Dataemissao + "\"," +
                                     "\"cfop\":\"" + c.Cfop + "\"," +
                                     "\"tipodoc\":\"" + c.Tipodoc + "\"," +
                                     "\"numeroromaneio\":\"" + c.Numeroromaneio + "\"," +
                                     "\"numeropedido\":\"" + c.Numeropedido + "\"," +
                                     "\"chave\":\"" + c.Chave + "\"," +
                                     "\"pin\":\"" + c.Pin + "\"," +
                                     "\"tipooriginario\":\"" + c.Tipooriginario + "\"," +
                                     "\"descricao\":\"" + c.Descricao + "\"," +
                                     "\"base_icms\":\"" + c.Base_icms + "\"," +
                                     "\"valor_icms\":\"" + c.Valor_icms + "\"," +
                                     "\"base_icmsst\":\"" + c.Base_icmsst + "\"," +
                                     "\"valor_icmsst\":\"" + c.Valor_icmsst + "\"," +
                                     "\"pesokg\":\"" + c.Pesokg + "\"," +
                                     "\"valorprodutos\":\"" + c.Valorprodutos + "\"," +
                                     "\"valortotal\":\"" + c.Valortotal + "\"," +
                                     "\"moddocumento\":\"" + c.Moddocumento + "\"," +
                                     "\"empresa\":\"" + c.Empresa + "\"," +
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

                                        conn.ExecuteQueries("UPDATE CTE_CTE_DOCREM P SET P.IDCTECTEDOCREMWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    CteCteDocremDAL docdal = new CteCteDocremDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE CTE_CTE_DOCREM P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(c.Codigo));
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

        public CteCteDocrem ObterPorId(long id)
        {
            var doc = new CteCteDocrem();

            return doc;
        }
    }
}
