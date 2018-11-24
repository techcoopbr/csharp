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
    class CaixaMovimentoDAL
    {
        public void Gravar(CaixaMovimento caixamovimentos)
        {
            if (caixamovimentos.Idmovimentocaixa == 0)
                Inserir(caixamovimentos);
            else
                Atualizar(caixamovimentos);
        }

        private void Inserir(CaixaMovimento caixamovimentos)
        {

        }

        private void Atualizar(CaixaMovimento caixamovimentos)
        {

        }

        public void Remover(CaixaMovimento caixamovimentos)
        {

        }


        public void PostCaixaMovimento()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            CaixaMovimentoDAL odal = new CaixaMovimentoDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from CAIXAMOVIMENTO p, empresa E where p.sincronizado = 0");

            CaixaMovimento o = new CaixaMovimento();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int CaixaMovimentoId = 0;
                    // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                    try
                    {
                        //string webAddrr = "http://localhost:3000/caixa_movimento_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://apptechcoop.com.br/caixa_movimento_accounts/" + Convert.ToString(dr["id"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            CaixaMovimentoId = usr["id"];

                            conn.ExecuteQueries("UPDATE CAIXAMOVIMENTO P SET P.IDCAIXAMOVIMENTOWEB = " + Convert.ToString(CaixaMovimentoId) + " WHERE P.ID = " + Convert.ToString((int)dr["id"]));

                        }
                    }
                    catch (Exception e)
                    {
                        CaixaMovimentoId = 0;
                    }

                    try
                    {

                        string webAddr;

                        if (CaixaMovimentoId == 0)
                        {
                            //webAddr = "http://localhost:3000/caixa_movimentos";
                            webAddr = "http://apptechcoop.com.br/caixa_movimentos";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/caixa_movimentos/" + Convert.ToString(DocumentId) + ".json";
                            webAddr = "http://apptechcoop.com.br/caixa_movimentos/" + Convert.ToString(CaixaMovimentoId) + ".json";
                        }

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        if (CaixaMovimentoId == 0)
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
                                if (CaixaMovimentoId != 0)
                                {
                                    o.Idmovimentocaixa = CaixaMovimentoId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO CAIXAS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO CAIXAS...");
                                }

                                o.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["id"] != DBNull.Value) { o.Idmovimentocaixa = (int)dr["id"]; }
                                if (dr["Idcaixa"] != DBNull.Value) { o.Idcaixa = (int)dr["Idcaixa"]; }
                                if (dr["Idfuncionario"] != DBNull.Value) { o.Idfuncionario = (int)dr["Idfuncionario"]; }
                                if (dr["Dataabertura"] != DBNull.Value) { o.Dataabertura = (DateTime)dr["Dataabertura"]; }
                                if (dr["Horaabertura"] != DBNull.Value) { o.Horaabertura = (DateTime)dr["Horaabertura"]; }
                                if (dr["Datafechamento"] != DBNull.Value) { o.Datafechamento = (DateTime)dr["Datafechamento"]; }
                                if (dr["Horafechamento"] != DBNull.Value) { o.Horafechamento = (DateTime)dr["Horafechamento"]; }
                                if (dr["Valordinheiro"] != DBNull.Value) { o.Valordinheiro = (decimal)dr["Valordinheiro"]; }
                                if (dr["Valorcartaodebito"] != DBNull.Value) { o.Valorcartaodebito = (decimal)dr["Valorcartaodebito"]; }
                                if (dr["Valorcartaocredito"] != DBNull.Value) { o.Valorcartaocredito = (decimal)dr["Valorcartaocredito"]; }
                                if (dr["Valorcheque"] != DBNull.Value) { o.Valorcheque = (decimal)dr["Valorcheque"]; }
                                if (dr["Valorprazo"] != DBNull.Value) { o.Valorprazo = (decimal)dr["Valorprazo"]; }
                                if (dr["Totalcaixa"] != DBNull.Value) { o.Totalcaixa = (decimal)dr["Totalcaixa"]; }
                                if (dr["Trocoinicial"] != DBNull.Value) { o.Trocoinicial = (decimal)dr["Trocoinicial"]; }
                                if (dr["Trocofinal"] != DBNull.Value) { o.Trocofinal = (decimal)dr["Trocofinal"]; }
                                if (dr["Empresa"] != DBNull.Value) { o.Empresa = (int)dr["Empresa"]; }
                                if (dr["Verificado"] != DBNull.Value) { o.Verificado = (int)dr["Verificado"]; }
                                if (dr["Valoroutro"] != DBNull.Value) { o.Valoroutro = (decimal)dr["Valoroutro"]; }


                                string json = "{ ";
                                if (CaixaMovimentoId != 0)
                                {
                                    json = json + "\"id\" :\"" + CaixaMovimentoId + "\", ";
                                }
                                json = json + "\"Idmovimentocaixa\":\"" + o.Idmovimentocaixa + "\"," +
                                     "\"Idcaixa\":\"" + o.Idcaixa + "\"," +
                                     "\"Idfuncionario\":\"" + o.Idfuncionario + "\"," +
                                     "\"Dataabertura\":\"" + o.Dataabertura + "\"," +
                                     "\"Horaabertura\":\"" + o.Horaabertura + "\"," +
                                     "\"Datafechamento\":\"" + o.Datafechamento + "\"," +
                                     "\"Horafechamento\":\"" + o.Horafechamento + "\"," +
                                     "\"Valordinheiro\":\"" + o.Valordinheiro + "\"," +
                                     "\"Valorcartaodebito\":\"" + o.Valorcartaodebito + "\"," +
                                     "\"Valorcartaocredito\":\"" + o.Valorcartaocredito + "\"," +
                                     "\"Valorcheque\":\"" + o.Valorcheque + "\"," +
                                     "\"Valorprazo\":\"" + o.Valorprazo + "\"," +
                                     "\"Totalcaixa\":\"" + o.Totalcaixa + "\"," +
                                     "\"Trocoinicial\":\"" + o.Trocoinicial + "\"," +
                                     "\"Trocofinal\":\"" + o.Trocofinal + "\"," +
                                     "\"Empresa\":\"" + o.Empresa + "\"," +
                                     "\"Verificado\":\"" + o.Verificado + "\"," +
                                     "\"Valoroutro\":\"" + o.Valoroutro + "\"," +
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
                                    //string webAddrr = "http://localhost:3000/caixa_movimento_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/caixa_movimento_accounts/" + Convert.ToString(dr["id"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                                        CaixaMovimentoId = usr["id"];

                                        conn.ExecuteQueries("UPDATE CAIXAMOVIMENTO P SET P.IDCAIXAMOVIMENTOWEB = " + Convert.ToString(CaixaMovimentoId) + " WHERE P.ID = " + Convert.ToString((int)dr["id"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    CaixaMovimentoId = 0;
                                }

                                try
                                {
                                    OperacoesDAL opedal = new OperacoesDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE CAIXAMOVIMENTO P SET P.SINCRONIZADO = 1 WHERE P.ID = " + Convert.ToString(o.Idmovimentocaixa));
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

        public CaixaMovimento ObterPorId(long id)
        {
            var Caixa = new CaixaMovimento();

            return Caixa;
        }
    }
}
