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
    class OperacoesDAL
    {
        public void Gravar(Operacoes operacoes)
        {
            if (operacoes.Codigo == 0)
                Inserir(operacoes);
            else
                Atualizar(operacoes);
        }

        private void Inserir(Operacoes operacoes)
        {

        }

        private void Atualizar(Operacoes operacoes)
        {

        }

        public void Remover(Operacoes operacoes)
        {

        }


        public void PostOperacoes()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            OperacoesDAL odal = new OperacoesDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from OPERACOES p, empresa E where p.sincronizado = 0");

            Operacoes o = new Operacoes();

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

                            conn.ExecuteQueries("UPDATE OPERACOES P SET P.IDOPERACOESWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

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
                                    o.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                o.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { o.Codigo = (int)dr["codigo"]; }
                                if (dr["operacao"] != DBNull.Value) { o.Operacao = (int)dr["operacao"]; }
                                if (dr["tipo"] != DBNull.Value) { o.Tipo = (int)dr["tipo"]; }
                                if (dr["descricao"] != DBNull.Value) { o.Descricao = (string)dr["descricao"]; }
                                if (dr["ativo"] != DBNull.Value) { o.Ativo = (bool)dr["ativo"]; }
                                if (dr["estoque"] != DBNull.Value) { o.Estoque = (int)dr["estoque"]; }
                                if (dr["descontomaximo"] != DBNull.Value) { o.Descontomaximo = (decimal)dr["descontomaximo"]; }
                                if (dr["acrescimomaximo"] != DBNull.Value) { o.Acrescimomaximo = (decimal)dr["acrescimomaximo"]; }
                                if (dr["descontoautomatico"] != DBNull.Value) { o.Descontoautomatico = (decimal)dr["descontoautomatico"]; }
                                if (dr["acrescimoautomatico"] != DBNull.Value) { o.Acrescimoautomatico = (decimal)dr["acrescimoautomatico"]; }
                                if (dr["multiplas"] != DBNull.Value) { o.Multiplas = (int)dr["multiplas"]; }
                                if (dr["idoperadora"] != DBNull.Value) { o.Idoperadora = (int)dr["idoperadora"]; }
                                if (dr["percentual"] != DBNull.Value) { o.Percentual = (decimal)dr["percentual"]; }
                                if (dr["realizatef"] != DBNull.Value) { o.Realizatef = (int)dr["realizatef"]; }
                                if (dr["gerenciador"] != DBNull.Value) { o.Gerenciador = (int)dr["gerenciador"]; }
                                if (dr["taxajuro"] != DBNull.Value) { o.Taxajuro = (decimal)dr["taxajuro"]; }
                                if (dr["formapgtoecf"] != DBNull.Value) { o.Formapgtoecf = (string)dr["formapgtoecf"]; }
                                if (dr["md5"] != DBNull.Value) { o.Md5 = (string)dr["md5"]; }
                                if (dr["operacaocte"] != DBNull.Value) { o.Operacaocte = (int)dr["operacaocte"]; }
                                if (dr["diasposvenda"] != DBNull.Value) { o.Diasaposvenda = (int)dr["diasposvenda"]; }
                                if (dr["pagacocmissao"] != DBNull.Value) { o.Pagacomissao = (int)dr["pagacomissao"]; }
                                if (dr["operacaodav"] != DBNull.Value) { o.Operacaodav = (int)dr["operacaodav"]; }
                                if (dr["es_naturezarubrica"] != DBNull.Value) { o.Es_naturezarubrica = (int)dr["es_naturezarubrica"]; }
                                if (dr["account_id"] != DBNull.Value) { o.Idweb = (int)dr["account_id"]; }

        //pdal.PostNf(p);

        string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + o.Codigo + "\"," +
                                     "\"codigo\":\"" + o.Codigo + "\"," +

                                     "\"operacao\":\"" + o.Operacao + "\"," +
                                     "\"tipo\":\"" + o.Tipo + "\"," +
                                     "\"descricao\":\"" + o.Descricao + "\"," +
                                     "\"ativo\":\"" + o.Ativo + "\"," +
                                     "\"estoque\":\"" + o.Estoque + "\"," +
                                     "\"descontomaximo\":\"" + o.Descontomaximo + "\"," +
                                     "\"acrescimomaximo\":\"" + o.Acrescimomaximo + "\"," +
                                     "\"descontoautomatico\":\"" + o.Descontoautomatico + "\"," +
                                     "\"acrescimoautomatico\":\"" + o.Acrescimoautomatico + "\"," +
                                     "\"multiplas\":\"" + o.Multiplas + "\"," +
                                     "\"idoperadora\":\"" + o.Idoperadora + "\"," +
                                     "\"percentual\":\"" + o.Percentual + "\"," +
                                     "\"realizatef\":\"" + o.Realizatef + "\"," +
                                     "\"gerenciador\":\"" + o.Gerenciador + "\"," +
                                     "\"taxajuro\":\"" + o.Taxajuro + "\"," +
                                     "\"formapgtoecf\":\"" + o.Formapgtoecf + "\"," +
                                     "\"md5\":\"" + o.Md5 + "\"," +
                                     "\"operacaocte\":\"" + o.Operacaocte + "\"," +
                                     "\"diasposvenda\":\"" + o.Diasaposvenda + "\"," +
                                     "\"pagacomissao\":\"" + o.Pagacomissao + "\"," +
                                     "\"operacaodav\":\"" + o.Operacaodav + "\"," +
                                     "\"es_naturezarubrica\":\"" + o.Es_naturezarubrica + "\"," +
                                     "\"account_id\" :\"" + o.Idweb + "\"}";

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

                                        conn.ExecuteQueries("UPDATE OPERACOES P SET P.IDOPERACOESWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    OperacoesDAL opedal = new OperacoesDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE OPERACOES P SET P.SINCRONIZADO = 1 WHERE P.ID = " + Convert.ToString(o.Codigo));
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

        public Operacoes ObterPorId(long id)
        {
            var op = new Operacoes();

            return op;
        }
    }
}
