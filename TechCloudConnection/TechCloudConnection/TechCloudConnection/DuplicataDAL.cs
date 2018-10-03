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

namespace Persistencia
{
    public class DuplicataDAL
    {
        public void Gravar(Duplicata duplicata)
        {
            if (duplicata.Codigo == 0)
                Inserir(duplicata);
            else
                Atualizar(duplicata);
        }

        private void Inserir(Duplicata duplicata)
        {

        }

        private void Atualizar(Duplicata duplicata)
        {

        }

        public void Remover(Duplicata duplicata)
        {

        }

        public void PostDuplicata()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            DuplicataDAL pdal = new DuplicataDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from DUPLICATAS p, empresa E where p.sincronizado = 0");

            Duplicata p = new Duplicata();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int InvoiceId = 0;
                    // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                    try
                    {
                        //string webAddrr = "http://localhost:3000/invoice_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://apptechcoop.com.br/invoice_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            InvoiceId = usr["id"];

                            conn.ExecuteQueries("UPDATE DUPLICATAS P SET P.IDDUPLICATASWEB = " + Convert.ToString(InvoiceId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                        }
                    }
                    catch (Exception e)
                    {
                        InvoiceId = 0;
                    }

                    // ENCONTRA O ID FOLK DA PESSOA DA DUPLICATA
                    try
                    {
                        //string webAddrD = "http://localhost:3000/folk_accounts/" + Convert.ToString(dr["pessoa"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrD = "http://apptechcoop.com.br/folk_accounts/" + Convert.ToString(dr["pessoa"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            p.Idfolk = usrD["id"];

                        }
                    }
                    catch (Exception e)
                    {
                        p.Idfolk = 0;
                    }

                    //////////////////////////////////////////////
                    try
                    {

                        string webAddr;

                        if (InvoiceId == 0)
                        {
                            //webAddr = "http://localhost:3000/invoices";
                            webAddr = "http://apptechcoop.com.br/invoices";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/invoices/" + Convert.ToString(InvoiceId) + ".json";
                            webAddr = "http://apptechcoop.com.br/invoices/" + Convert.ToString(InvoiceId) + ".json";
                        }

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        if (InvoiceId == 0)
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
                                if (InvoiceId != 0)
                                {
                                    p.Id = InvoiceId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DUPLICATA...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DUPLICATA...");
                                }

                                p.Idweb = (int)Convert.ToInt32(dr["Idweb"]);
                                if (dr["codigo"] != DBNull.Value) { p.Codigo = (int)dr["codigo"]; }
                                if (dr["duplicata"] != DBNull.Value) { p.NumeroDuplicata = (int)dr["duplicata"]; }
                                if (dr["parcela"] != DBNull.Value) { p.Parcela = (int)dr["parcela"]; }
                                if (dr["documento"] != DBNull.Value) { p.Documento = (int)dr["documento"]; }
                                if (dr["pessoa"] != DBNull.Value) { p.Pessoa = (int)dr["pessoa"]; }
                                if (dr["tipo"] != DBNull.Value) { p.Tipo = dr["tipo"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["emissao"] != DBNull.Value) { p.Emissao = (DateTime)dr["emissao"]; }
                                if (dr["vencimento"] != DBNull.Value) { p.Vencimento = (DateTime)dr["vencimento"]; }
                                if (dr["valordocumento"] != DBNull.Value) { p.Valordocumento = (decimal)dr["valordocumento"]; }
                                if (dr["valorparcela"] != DBNull.Value) { p.Valorparcela = (decimal)dr["valorparcela"]; }
                                if (dr["saldo"] != DBNull.Value) { p.Saldo = (decimal)dr["saldo"]; }
                                if (dr["data_atualizacao"] != DBNull.Value) { p.Data_atualizacao = (DateTime)dr["data_atualizacao"]; }
                                if (dr["empresa"] != DBNull.Value) { p.Empresa = (int)dr["empresa"]; }
                                if (dr["pedido"] != DBNull.Value) { p.Pedido = (int)dr["pedido"]; }
                                if (dr["comissao"] != DBNull.Value) { p.Comissao = (decimal)dr["comissao"]; }
                                if (dr["totalparcelas"] != DBNull.Value) { p.Totalparcelas = (int)dr["totalparcelas"]; }
                                if (dr["idnf"] != DBNull.Value) { p.Idnf = (int)dr["idnf"]; }
                                if (dr["idvendedor"] != DBNull.Value) { p.Idvendedor = (int)dr["idvendedor"]; }
                                if (dr["observacao"] != DBNull.Value) { p.Observacao = dr["observacao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["agrupado"] != DBNull.Value) { p.Agrupado = (int)dr["agrupado"]; }
                                if (dr["idalteracao"] != DBNull.Value) { p.Idalteracao = (int)dr["idalteracao"]; }
                                if (dr["iduserbaixa"] != DBNull.Value) { p.Iduserbaixa = (int)dr["iduserbaixa"]; }
                                if (dr["jurosacumulados"] != DBNull.Value) { p.Jurosacumulados = (decimal)dr["jurosacumulados"]; }
                                if (dr["nossonumero"] != DBNull.Value) { p.Nossonumero = dr["nossonumero"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["idplanovenda"] != DBNull.Value) { p.Idplanovenda = (int)dr["idplanovenda"]; }
                                if (dr["idcheque"] != DBNull.Value) { p.Idcheque = (int)dr["idcheque"]; }
                                if (dr["ultimabaixa"] != DBNull.Value) { p.Ultimabaixa = (DateTime)dr["ultimabaixa"]; }
                                if (dr["descontosacumulados"] != DBNull.Value) { p.Descontosacumulados = (decimal)dr["descontosacumulados"]; }
                                if (dr["recebidosacumulado"] != DBNull.Value) { p.Recebidosacumulado = (decimal)dr["recebidosacumulado"];}

                                string json = "{ ";
                                if (InvoiceId != 0)
                                {
                                    json = json + "\"id\" :\"" + InvoiceId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + p.Codigo + "\"," +
                                    "\"duplicata\":\"" + p.NumeroDuplicata + "\"," +
                                    "\"parcela\":\"" + p.Parcela + "\"," +
                                    "\"documento\":\"" + p.Documento + "\"," +
                                    "\"pessoa\":\"" + p.Pessoa + "\"," +
                                    "\"tipo\":\"" + p.Tipo + "\"," +
                                    "\"emissao\":\"" + p.Emissao + "\"," +
                                    "\"vencimento\":\"" + p.Vencimento + "\"," +
                                    "\"valordocumento\":\"" + p.Valordocumento + "\"," +
                                    "\"valorparcela\":\"" + p.Valorparcela + "\"," +
                                    "\"saldo\":\"" + p.Saldo + "\"," +
                                    "\"data_atualizacao\":\"" + p.Data_atualizacao + "\"," +
                                    "\"empresa\":\"" + p.Empresa + "\"," +
                                    "\"pedido\":\"" + p.Pedido + "\"," +
                                    "\"comissao\":\"" + p.Comissao + "\"," +
                                    "\"totalparcelas\":\"" + p.Totalparcelas + "\"," +
                                    "\"idnf\":\"" + p.Idnf + "\"," +
                                    "\"idvendedor\":\"" + p.Idvendedor + "\"," +
                                    "\"observacao\":\"" + p.Observacao + "\"," +
                                    "\"agrupado\":\"" + p.Agrupado + "\"," +
                                    "\"idalteracao\":\"" + p.Idalteracao + "\"," +
                                    "\"iduserbaixa\":\"" + p.Iduserbaixa + "\"," +
                                    "\"jurosacumulados\":\"" + p.Jurosacumulados + "\"," +
                                    "\"nossonumero\":\"" + p.Nossonumero + "\"," +
                                    "\"idplanovenda\":\"" + p.Idplanovenda + "\"," +
                                    "\"idcheque\":\"" + p.Idcheque + "\"," +
                                    "\"ultimabaixa\":\"" + p.Ultimabaixa + "\"," +
                                    "\"descontosacumulados\":\"" + p.Descontosacumulados + "\"," +
                                    "\"recebidosacumulado\":\"" + p.Recebidosacumulado + "\"," +
                                    "\"folk_id\":\"" + p.Idfolk + "\"," +
                                    "\"account_id\" :\"" + p.Idweb + "\"}";

                                json = json.Replace("\r\n", "");

                                streamWriter.Write(json);
                                streamWriter.Flush();

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();

                                }

                                //consulta novamente
                                try
                                {
                                    //string webAddrr = "http://localhost:3000/invoice_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/invoice_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                                        InvoiceId = usr["id"];

                                        conn.ExecuteQueries("UPDATE DUPLICATAS P SET P.IDDUPLICATASWEB = " + Convert.ToString(InvoiceId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {

                                }

                                conn.ExecuteQueries("UPDATE DUPLICATAS P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
                                //conn.CloseConnection();
                            }
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        //conn.ExecuteQueries("UPDATE PESSOAS P SET P.SINCRONIZADO = 0 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
                        NewIniFile.IniWriteString("STATUS", "MSG", e.Message);
                        dr.NextResult();
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

        public Duplicata ObterPorId(long id)
        {
            var duplicata = new Duplicata();

            return duplicata;
        }
    }
}
