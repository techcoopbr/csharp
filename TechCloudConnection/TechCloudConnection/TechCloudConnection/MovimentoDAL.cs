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
    class MovimentoDAL
    {
        public void Gravar(Movimento movimento)
        {
            if (movimento.Codigo == 0)
                Inserir(movimento);
            else
                Atualizar(movimento);
        }

        private void Inserir(Movimento movimento)
        {

        }

        private void Atualizar(Movimento movimento)
        {

        }

        public void Remover(Movimento movimento)
        {

        }


        public void PostMovimento()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            MovimentoDAL mdal = new MovimentoDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from MOVIMENTO p, empresa E where p.sincronizado = 0");

            Movimento m = new Movimento();

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

                            conn.ExecuteQueries("UPDATE MOVIMENTO P SET P.IDMOVIMENTOWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

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
                            // >>>>>>  não existe o campo idfolk na tabela movimentos  <<<<<<
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
                                    m.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                m.Idweb = (int)Convert.ToInt32(dr["Idweb"]);
                                if (dr["codigo"] != DBNull.Value) { m.Codigo = (int)dr["codigo"]; }
                                if (dr["operacao"] != DBNull.Value) { m.Operacao = (int)dr["codigo"]; }
                                if (dr["origem"] != DBNull.Value) { m.Origem = (string)dr["origem"]; }
                                if (dr["codigo_ref"] != DBNull.Value) { m.Codigo_ref = (int)dr["codigo_ref"]; }
                                if (dr["descricao"] != DBNull.Value) { m.Descricao = (string)dr["descricao"]; }
                                if (dr["datalancamento"] != DBNull.Value) { m.Datalancamento = (DateTime)dr["datalancamento"]; }
                                if (dr["valor"] != DBNull.Value) { m.Valor = (decimal)dr["valor"]; }
                                if (dr["tipomovimento"] != DBNull.Value) { m.Tipomovimento = (int)dr["tipomovimento"]; }
                                if (dr["banco"] != DBNull.Value) { m.Banco = (int)dr["Banco"]; }
                                if (dr["empresa"] != DBNull.Value) { m.Empresa = (int)dr["empresa"]; }
                                if (dr["viagem"] != DBNull.Value) { m.Viagem = (int)dr["viagem"]; }
                                if (dr["idcaixa"] != DBNull.Value) { m.Idcaixa = (int)dr["idcaixa"]; }
                                if (dr["datafatura"] != DBNull.Value) { m.Datafatura = (DateTime)dr["datafatura"]; }
                                if (dr["faturado"] != DBNull.Value) { m.Faturado = (int)dr["faturado"]; }
                                if (dr["idpessoacc"] != DBNull.Value) { m.Idpessoacc = (int)dr["idpessoacc"]; }
                                if (dr["iduserbaixa"] != DBNull.Value) { m.Iduserbaixa = (int)dr["iduserbaixa"]; }
                                if (dr["idecf"] != DBNull.Value) { m.Idecf = (int)dr["idecf"]; }
                                if (dr["coo"] != DBNull.Value) { m.Coo = (int)dr["coo"]; }
                                if (dr["dataecf"] != DBNull.Value) { m.Dataecf = (DateTime)dr["dataecf"]; }
                                if (dr["horaecf"] != DBNull.Value) { m.Horaecf = (DateTime)dr["horaecf"]; }
                                if (dr["crz"] != DBNull.Value) { m.Crz = (int)dr["crz"]; }
                                if (dr["ccf"] != DBNull.Value) { m.Ccf = (int)dr["ccf"]; }
                                if (dr["md5"] != DBNull.Value) { m.Md5 = (string)dr["md5"]; }
                                if (dr["manual"] != DBNull.Value) { m.Manual = (int)dr["manual"]; }
                                if (dr["account_id"] != DBNull.Value) { m.Idweb = (int)dr["account_id"]; }
                                
                                
                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + m.Codigo + "\"," +
                                     "\"codigo\":\"" + m.Codigo + "\"," +

                                     "\"operacao\":\"" + m.Operacao + "\"," +
                                     "\"origem\":\"" + m.Origem + "\"," +
                                     "\"codigo_ref\":\"" + m.Codigo_ref + "\"," +
                                     "\"descricao\":\"" + m.Descricao + "\"," +
                                     "\"datalancamento\":\"" + m.Datalancamento + "\"," +
                                     "\"valor\":\"" + m.Valor + "\"," +
                                     "\"tipomovimento\":\"" + m.Tipomovimento + "\"," +
                                     "\"banco\":\"" + m.Banco + "\"," +
                                     "\"empresa\":\"" + m.Empresa + "\"," +
                                     "\"viagem\":\"" + m.Viagem + "\"," +
                                     "\"idcaixa\":\"" + m.Idcaixa + "\"," +
                                     "\"datafatura\":\"" + m.Datafatura + "\"," +
                                     "\"faturado\":\"" + m.Faturado + "\"," +
                                     "\"idpessoacc\":\"" + m.Idpessoacc + "\"," +
                                     "\"iduserbaixa\":\"" + m.Iduserbaixa + "\"," +
                                     "\"idecf\":\"" + m.Idecf + "\"," +
                                     "\"coo\":\"" + m.Coo + "\"," +
                                     "\"dataecf\":\"" + m.Dataecf + "\"," +
                                     "\"horaecf\":\"" + m.Horaecf + "\"," +
                                     "\"crz\":\"" + m.Crz + "\"," +
                                     "\"ccf\":\"" + m.Ccf + "\"," +
                                     "\"md5\":\"" + m.Md5 + "\"," +
                                     "\"manual\":\"" + m.Manual + "\"," +
                                     "\"account_id\" :\"" + m.Idweb + "\"}";

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

                                        conn.ExecuteQueries("UPDATE MOVIMENTO P SET P.IDMOVIMENTOWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    MovimentoDAL movdal = new MovimentoDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE MOVIMENTO P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(m.Codigo));
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

        public Movimento ObterPorId(long id)
        {
            var mov = new Movimento();

            return mov;
        }
    }
}
