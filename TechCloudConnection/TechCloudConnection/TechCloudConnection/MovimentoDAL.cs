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

                    int CashId = 0;
                    // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                    try
                    {
                        //string webAddrr = "http://www.techcoop.com.br/cash_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://www.techcoop.com.br/cash_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            CashId = usr["id"];

                            conn.ExecuteQueries("UPDATE MOVIMENTO P SET P.IDMOVIMENTOWEB = " + Convert.ToString(CashId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                        }
                    }
                    catch (Exception e)
                    {
                        CashId = 0;
                    }

                    //////////////////////////////////////////////
                    try
                    {

                        string webAddr;

                        if (CashId == 0)
                        {
                            //webAddr = "http://localhost:3000/cashes";
                            webAddr = "http://apptechcoop.com.br/cashes";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/cashes/" + Convert.ToString(DocumentId) + ".json";
                            webAddr = "http://apptechcoop.com.br/cashes/" + Convert.ToString(CashId) + ".json";
                        }

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        if (CashId == 0)
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
                                if (CashId != 0)
                                {
                                    m.Codigo = CashId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO MOVIMENTO...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO MOVIMENTO...");
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
                                                                
                                
                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (CashId != 0)
                                {
                                    json = json + "\"id\" :\"" + CashId + "\", ";
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

                                //teste

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();

                                }

                                // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                                try
                                {
                                    //string webAddrr = "http://localhost:3000/cash_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/cash_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                                        CashId = usr["id"];

                                        conn.ExecuteQueries("UPDATE MOVIMENTO P SET P.IDMOVIMENTOWEB = " + Convert.ToString(CashId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    CashId = 0;
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
