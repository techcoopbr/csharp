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
    class CteCteDAL
    {
        public void Gravar(CteCte cte)
        {
            if (cte.Codigo == 0)
                Inserir(cte);
            else
                Atualizar(cte);
        }

        private void Inserir(CteCte cte)
        {

        }

        private void Atualizar(CteCte cte)
        {

        }

        public void Remover(CteCte cte)
        {

        }


        public void PostCte()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            CteCteDAL ctedal = new CteCteDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from CTE_CTE p, empresa E where p.sincronizado = 0");

            CteCte c = new CteCte();

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

                            conn.ExecuteQueries("UPDATE CTE_CTE P SET P.IDCTECTEWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

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
                                    c.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                c.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { c.Codigo = (int)dr["codigo"]; }
                                if (dr["modelo"] != DBNull.Value) { c.Modelo = (string)dr["modelo"]; }
                                if (dr["serie"] != DBNull.Value) { c.Serie = (int)dr["serie"]; }
                                if (dr["documento"] != DBNull.Value) { c.Documento = (int)dr["documento"]; }
                                if (dr["dataemissao"] != DBNull.Value) { c.Dataemissao = (DateTime)dr["dataemissao"]; }
                                if (dr["cfop"] != DBNull.Value) { c.Cfop = (string)dr["cfop"]; }
                                if (dr["naturezaoperacao"] != DBNull.Value) { c.Naturezaoperacao = (string)dr["naturezaoperacao"]; }
                                if (dr["chave"] != DBNull.Value) { c.Chave = (string)dr["chave"]; }
                                if (dr["chaveref"] != DBNull.Value) { c.Chaveref = (string)dr["chaveref"]; }
                                if (dr["tiposervico"] != DBNull.Value) { c.Tiposervico = (int)dr["tiposervico"]; }
                                if (dr["finalidadeemissao"] != DBNull.Value) { c.Finalidadeemissao = (int)dr["finalidadeemissao"]; }
                                if (dr["formapagamento"] != DBNull.Value) { c.Formapagamento = (int)dr["formapagamento"]; }
                                if (dr["cidadeemissao"] != DBNull.Value) { c.Cidadeemissao = (int)dr["cidadeemissao"]; }
                                if (dr["cidadeinicio"] != DBNull.Value) { c.Cidadeinicio = (int)dr["cidadeinicio"]; }
                                if (dr["cidadefim"] != DBNull.Value) { c.Cidadefim = (int)dr["cidadefim"]; }
                                if (dr["previsaoentrega"] != DBNull.Value) { c.Previsaoentrega = (DateTime)dr["previsaoentrega"]; }
                                if (dr["tomador"] != DBNull.Value) { c.Tomador = (int)dr["tomador"]; }
                                if (dr["idtomador"] != DBNull.Value) { c.Idtomador = (int)dr["idtomador"]; }
                                if (dr["idremetente"] != DBNull.Value) { c.Idremetente = (int)dr["idremetente"]; }
                                if (dr["iddestinatario"] != DBNull.Value) { c.Iddestinatario = (int)dr["iddestinatario"]; }
                                if (dr["idexpedidor"] != DBNull.Value) { c.Idexpedidor = (int)dr["idexpedidor"]; }
                                if (dr["idrecebedor"] != DBNull.Value) { c.Idrecebedor = (int)dr["idrecebedor"]; }
                                if (dr["valorcarga"] != DBNull.Value) { c.Valorcarga = (decimal)dr["valorcarga"]; }
                                if (dr["produtopredominante"] != DBNull.Value) { c.Produtopredominante = (string)dr["produtopredominante"]; }
                                if (dr["outrascaracteristicas"] != DBNull.Value) { c.Outrascaracteristicas = (string)dr["outrascaracteristicas"]; }
                                if (dr["valorfrete"] != DBNull.Value) { c.Valorfrete = (decimal)dr["valorfrete"]; }
                                if (dr["valorreceber"] != DBNull.Value) { c.Valorreceber = (decimal)dr["valorreceber"]; }
                                if (dr["impostosvariavel"] != DBNull.Value) { c.Impostosvariavel = (decimal)dr["impostosvariavel"]; }
                                if (dr["cst_icms"] != DBNull.Value) { c.Cst_icms = (string)dr["cst_icms"]; }
                                if (dr["base_icms"] != DBNull.Value) { c.Base_icms = (decimal)dr["base_icms"]; }
                                if (dr["red_icms"] != DBNull.Value) { c.Red_icms = (decimal)dr["red_icms"]; }
                                if (dr["aliquota_icms"] != DBNull.Value) { c.Aliquota_icms = (int)dr["aliquota_icms"]; }
                                if (dr["valor_icms"] != DBNull.Value) { c.Valor_icms = (decimal)dr["valor_icms"]; }
                                if (dr["credito_icms"] != DBNull.Value) { c.Credito_icms = (decimal)dr["credito_icms"]; }
                                if (dr["informacoes_fisco"] != DBNull.Value) { c.Informacoes_fisco = (string)dr["informacoes_fisco"]; }
                                if (dr["observacoesgerais"] != DBNull.Value) { c.Observacoesgerais = (string)dr["observacoesgerais"]; }
                                if (dr["emissao"] != DBNull.Value) { c.Emissao = (int)dr["emissao"]; }
                                if (dr["ambiente"] != DBNull.Value) { c.Ambiente = (int)dr["ambiente"]; }
                                if (dr["status"] != DBNull.Value) { c.Status = (string)dr["status"]; }
                                if (dr["recibo"] != DBNull.Value) { c.Recibo = (string)dr["recibo"]; }
                                if (dr["protocolo"] != DBNull.Value) { c.Protocolo = (string)dr["protocolo"]; }
                                if (dr["horarecibo"] != DBNull.Value) { c.Horarecibo = (string)dr["horarecibo"]; }
                                if (dr["ciot"] != DBNull.Value) { c.Ciot = (string)dr["ciot"]; }
                                if (dr["lotacao"] != DBNull.Value) { c.Lotacao = (int)dr["lotacao"]; }
                                if (dr["rntrc"] != DBNull.Value) { c.Rntrc = (string)dr["rntrc"]; }
                                if (dr["tipo_data"] != DBNull.Value) { c.Tipo_data = (int)dr["tipo_data"]; }
                                if (dr["tipo_hora"] != DBNull.Value) { c.Tipo_hora = (int)dr["tipo_hora"]; }
                                if (dr["tipo_datai"] != DBNull.Value) { c.Tipo_datai = (DateTime)dr["tipo_datai"]; }
                                if (dr["tipo_dataf"] != DBNull.Value) { c.Tipo_dataf = (DateTime)dr["tipo_dataf"]; }
                                if (dr["tipo_horai"] != DBNull.Value) { c.Tipo_horai = (DateTime)dr["tipo_horai"]; }
                                if (dr["tipo_horaf"] != DBNull.Value) { c.Tipo_horaf = (DateTime)dr["tipo_horaf"]; }
                                if (dr["idviagem"] != DBNull.Value) { c.Idviagem = (int)dr["idviagem"]; }
                                if (dr["data_atualizacao"] != DBNull.Value) { c.Data_atualizacao = (DateTime)dr["data_atualizacao"]; }
                                if (dr["pedagio"] != DBNull.Value) { c.Pedagio = (decimal)dr["pedagio"]; }
                                if (dr["descarga"] != DBNull.Value) { c.Descarga = (decimal)dr["descarga"]; }
                                if (dr["icmsreembolso"] != DBNull.Value) { c.Icmsreembolso = (decimal)dr["icmsreembolso"]; }
                                if (dr["funcionario"] != DBNull.Value) { c.Funcionario = (int)dr["funcionario"]; }
                                if (dr["empresa"] != DBNull.Value) { c.Empresa = (int)dr["empresa"]; }
                                if (dr["acrescimo"] != DBNull.Value) { c.Acrescimo = (decimal)dr["acrescimo"]; }
                                if (dr["idoperacao"] != DBNull.Value) { c.Idoperacao = (int)dr["idoperacao"]; }
                                if (dr["kmincial"] != DBNull.Value) { c.Kmincial = (decimal)dr["kmincial"]; }
                                if (dr["kmfinal"] != DBNull.Value) { c.Kmfinal = (decimal)dr["kmfinal"]; }
                                if (dr["horaemissao"] != DBNull.Value) { c.Horaemissao = (DateTime)dr["horaemissao"]; }
                                if (dr["idmodelo"] != DBNull.Value) { c.Idmodelo = (int)dr["idmodelo"]; }
                                if (dr["retira"] != DBNull.Value) { c.Retira = (int)dr["retira"]; }
                                if (dr["localretirada"] != DBNull.Value) { c.Localretirada = (string)dr["localretirada"]; }
                                if (dr["caracadicional"] != DBNull.Value) { c.Caracadicional = (string)dr["caracadicional"]; }
                                if (dr["caracservico"] != DBNull.Value) { c.Caracservico = (string)dr["caracservico"]; }
                                if (dr["cidadecoleta"] != DBNull.Value) { c.Cidadecoleta = (string)dr["cidadecoleta"]; }
                                if (dr["cidadeentrega"] != DBNull.Value) { c.Cidadeentrega = (string)dr["cidadeentrega"]; }
                                if (dr["globalizado"] != DBNull.Value) { c.Globalizado = (int)dr["globalizado"]; }
                                if (dr["idweb"] != DBNull.Value) { c.Idweb = (int)dr["idweb"]; }

                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + c.Codigo + "\"," +
                                     "\"codigo\":\"" + c.Codigo + "\"," +

                                     "\"codigo\":\"" + c.Codigo + "\"," +
                                     "\"modelo\":\"" + c.Modelo + "\"," +
                                     "\"serie\":\"" + c.Serie + "\"," +
                                     "\"documento\":\"" + c.Documento + "\"," +
                                     "\"dataemissao\":\"" + c.Dataemissao + "\"," +
                                     "\"cfop\":\"" + c.Cfop + "\"," +
                                     "\"naturezaoperacao\":\"" + c.Naturezaoperacao + "\"," +
                                     "\"chave\":\"" + c.Chave + "\"," +
                                     "\"chaveref\":\"" + c.Chaveref + "\"," +
                                     "\"tiposervico\":\"" + c.Tiposervico + "\"," +
                                     "\"finalidadeemissao\":\"" + c.Finalidadeemissao + "\"," +
                                     "\"formapagamento\":\"" + c.Formapagamento + "\"," +
                                     "\"cidadeemissao\":\"" + c.Cidadeemissao + "\"," +
                                     "\"cidadeinicio\":\"" + c.Cidadeinicio + "\"," +
                                     "\"cidadefim\":\"" + c.Cidadefim + "\"," +
                                     "\"previsaoentrega\":\"" + c.Previsaoentrega + "\"," +
                                     "\"tomador\":\"" + c.Tomador + "\"," +
                                     "\"idtomador\":\"" + c.Idtomador + "\"," +
                                     "\"idremetente\":\"" + c.Idremetente + "\"," +
                                     "\"iddestinatario\":\"" + c.Iddestinatario + "\"," +
                                     "\"idexpedidor\":\"" + c.Idexpedidor + "\"," +
                                     "\"idrecebedor\":\"" + c.Idrecebedor + "\"," +
                                     "\"valorcarga\":\"" + c.Valorcarga + "\"," +
                                     "\"produtopredominante\":\"" + c.Produtopredominante + "\"," +
                                     "\"outrascaracteristicas\":\"" + c.Outrascaracteristicas + "\"," +
                                     "\"valorfrete\":\"" + c.Valorfrete + "\"," +
                                     "\"valorreceber\":\"" + c.Valorreceber + "\"," +
                                     "\"impostosvariavel\":\"" + c.Impostosvariavel + "\"," +
                                     "\"cst_icms\":\"" + c.Cst_icms + "\"," +
                                     "\"base_icms\":\"" + c.Base_icms + "\"," +
                                     "\"red_icms\":\"" + c.Red_icms + "\"," +
                                     "\"aliquota_icms\":\"" + c.Aliquota_icms + "\"," +
                                     "\"valor_icms\":\"" + c.Valor_icms + "\"," +
                                     "\"credito_icms\":\"" + c.Credito_icms + "\"," +
                                     "\"informacoes_fisco\":\"" + c.Informacoes_fisco + "\"," +
                                     "\"observacoesgerais\":\"" + c.Observacoesgerais + "\"," +
                                     "\"emissao\":\"" + c.Emissao + "\"," +
                                     "\"ambiente\":\"" + c.Ambiente + "\"," +
                                     "\"status\":\"" + c.Status + "\"," +
                                     "\"recibo\":\"" + c.Recibo + "\"," +
                                     "\"protocolo\":\"" + c.Protocolo + "\"," +
                                     "\"horarecibo\":\"" + c.Horarecibo + "\"," +
                                     "\"ciot\":\"" + c.Ciot + "\"," +
                                     "\"lotacao\":\"" + c.Lotacao + "\"," +
                                     "\"rntrc\":\"" + c.Rntrc + "\"," +
                                     "\"tipo_data\":\"" + c.Tipo_data + "\"," +
                                     "\"tipo_hora\":\"" + c.Tipo_hora + "\"," +
                                     "\"tipo_datai\":\"" + c.Tipo_datai + "\"," +
                                     "\"tipo_dataf\":\"" + c.Tipo_dataf + "\"," +
                                     "\"tipo_horai\":\"" + c.Tipo_horai + "\"," +
                                     "\"tipo_horaf\":\"" + c.Tipo_horaf + "\"," +
                                     "\"idviagem\":\"" + c.Idviagem + "\"," +
                                     "\"data_atualizacao\":\"" + c.Data_atualizacao + "\"," +
                                     "\"pedagio\":\"" + c.Pedagio + "\"," +
                                     "\"descarga\":\"" + c.Descarga + "\"," +
                                     "\"icmsreembolso\":\"" + c.Icmsreembolso + "\"," +
                                     "\"funcionario\":\"" + c.Funcionario + "\"," +
                                     "\"empresa\":\"" + c.Empresa + "\"," +
                                     "\"acrescimo\":\"" + c.Acrescimo + "\"," +
                                     "\"idoperacao\":\"" + c.Idoperacao + "\"," +
                                     "\"kmincial\":\"" + c.Kmincial + "\"," +
                                     "\"kmfinal\":\"" + c.Kmfinal + "\"," +
                                     "\"horaemissao\":\"" + c.Horaemissao + "\"," +
                                     "\"idmodelo\":\"" + c.Idmodelo + "\"," +
                                     "\"retira\":\"" + c.Retira + "\"," +
                                     "\"localretirada\":\"" + c.Localretirada + "\"," +
                                     "\"caracadicional\":\"" + c.Caracadicional + "\"," +
                                     "\"caracservico\":\"" + c.Caracservico + "\"," +
                                     "\"cidadecoleta\":\"" + c.Cidadecoleta + "\"," +
                                     "\"cidadeentrega\":\"" + c.Cidadeentrega + "\"," +
                                     "\"globalizado\":\"" + c.Globalizado + "\"," +
                                     "\"account_id\":\"" + c.Idweb + "\"}";


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

                                        conn.ExecuteQueries("UPDATE CTE_CTE P SET P.IDCTECTEWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

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

                                    conn.ExecuteQueries("UPDATE CTE_CTE P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(c.Codigo));
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
