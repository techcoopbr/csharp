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
    class PlanovendaDAL
    {
        public void Gravar(Planovenda pla)
        {
            if (pla.Codigo == 0)
                Inserir(pla);
            else
                Atualizar(pla);
        }

        private void Inserir(Planovenda pla)
        {

        }

        private void Atualizar(Planovenda pla)
        {

        }

        public void Remover(Planovenda pla)
        {

        }


        public void PostPlanovenda()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            PlanovendaDAL pdal = new PlanovendaDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from PLANOVENDA p, empresa E where p.sincronizado = 0");

            Planovenda p = new Planovenda();

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

                            conn.ExecuteQueries("UPDATE PLANOVENDA P SET P.IDPLANOVENDAWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

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
                            webAddr = "http://apptechcoop.com.br/planovenda";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/documents/" + Convert.ToString(DocumentId) + ".json";
                            webAddr = "http://apptechcoop.com.br/planovenda/" + Convert.ToString(DocumentId) + ".json";
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
                                    p.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO PLANOS DE VENDA...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO PLANOS DE VENDA...");
                                }

                                p.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["id"] != DBNull.Value) { p.Codigo = (int)dr["id"]; }
                                if (dr["descricao"] != DBNull.Value) { p.Descricao = (string)dr["descricao"]; }
                                if (dr["parcelas"] != DBNull.Value) { p.Parcelas = (int)dr["parcelas"]; }
                                if (dr["intervalo"] != DBNull.Value) { p.Intervalo = (string)dr["intevalo"]; }
                                if (dr["tipocobranca"] != DBNull.Value) { p.Tipocobranca = (int)dr["tipocobranca"]; }
                                if (dr["descricaocobranca"] != DBNull.Value) { p.Descricaocobranca = (string)dr["descricaocobranca"]; }
                                if (dr["relatorio"] != DBNull.Value) { p.Relatorio = (int)dr["relatorio"]; }
                                if (dr["parcelaminima"] != DBNull.Value) { p.Parcelaminima = (decimal)dr["parcelaminima"]; }
                                if (dr["ativo"] != DBNull.Value) { p.Ativo = (int)dr["ativo"]; }
                                if (dr["diavencimento"] != DBNull.Value) { p.Diavencimento = (int)dr["diavencimento"]; }
                                if (dr["tododia"] != DBNull.Value) { p.Tododia = (int)dr["tododia"]; }
                                if (dr["idboleto"] != DBNull.Value) { p.Idboleto = (int)dr["idboleto"]; }
                                if (dr["empresa"] != DBNull.Value) { p.Empresa = (int)dr["empresa"]; }
                                if (dr["imprimecontrato"] != DBNull.Value) { p.Imprimecontrato = (int)dr["imprimecontrato"]; }
                                if (dr["relatoriocontrato"] != DBNull.Value) { p.Relatoriocontrato = (string)dr["relatoriocontrato"]; }
                                if (dr["imprimeromaneio"] != DBNull.Value) { p.Imprimeromaneio = (int)dr["imprimeromaneio"]; }
                                if (dr["relatorioromaneio"] != DBNull.Value) { p.Relatorioromaneio = (string)dr["relatorioromaneio"]; }
                                if (dr["pagacomissao"] != DBNull.Value) { p.Pagacomissao = (int)dr["pagacomissao"]; }
                                if (dr["account_id"] != DBNull.Value) { p.Idweb = (int)dr["account_id"]; }



                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + p.Codigo + "\"," +
                                     "\"codigo\":\"" + p.Codigo + "\"," +

                                     "\"descricao\":\"" + p.Descricao+ "\"," +
                                     "\"parcelas\":\"" + p.Parcelas + "\"," +
                                     "\"intervalo\":\"" + p.Intervalo + "\"," +
                                     "\"tipocobranca\":\"" + p.Tipocobranca + "\"," +
                                     "\"descricaocobranca\":\"" + p.Descricaocobranca + "\"," +
                                     "\"descricao\":\"" + p.Descricao + "\"," +
                                     "\"parcelaminima\":\"" + p.Parcelaminima + "\"," +
                                     "\"ativo\":\"" + p.Ativo + "\"," +
                                     "\"diavencimento\":\"" + p.Diavencimento + "\"," +
                                     "\"tododia\":\"" + p.Tododia + "\"," +
                                     "\"idboleto\":\"" + p.Idboleto + "\"," +
                                     "\"empresa\":\"" + p.Empresa + "\"," +
                                     "\"imprimecontrato\":\"" + p.Imprimecontrato + "\"," +
                                     "\"relatoriocontrato\":\"" + p.Relatoriocontrato + "\"," +
                                     "\"imprimeromaneio\":\"" + p.Imprimeromaneio + "\"," +
                                     "\"relatorioromaneio\":\"" + p.Relatorioromaneio + "\"," +
                                     "\"pagacomissao\":\"" + p.Pagacomissao + "\"," +
                                     "\"account_id\":\"" + p.Idweb + "\"}";

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

                                        conn.ExecuteQueries("UPDATE PLANOVENDA P SET P.IDPLANOVENDAWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    PlanovendaDAL pladal = new PlanovendaDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE PLANOVENDA P SET P.SINCRONIZADO = 1 WHERE P.ID = " + Convert.ToString(p.Codigo));
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
            NewIniFile.IniWriteString("STATUS", "MSG", "ATUALIZAÇÃO PLANOS DE VENDA FINALIZADA");
        }

        public Planovenda ObterPorId(long id)
        {
            var pla = new Planovenda();

            return pla;
        }
    }
}
