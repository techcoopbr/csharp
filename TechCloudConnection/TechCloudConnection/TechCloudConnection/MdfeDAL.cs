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
    class MdfeDAL
    {
        public void Gravar(Mdfe mdfe)
        {
            if (mdfe.Codigo == 0)
                Inserir(mdfe);
            else
                Atualizar(mdfe);
        }

        private void Inserir(Mdfe mdfe)
        {

        }

        private void Atualizar(Mdfe mdfe)
        {

        }

        public void Remover(Mdfe mdfe)
        {

        }


        public void PostMdfe()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            MdfeDAL mdal = new MdfeDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from NF p, empresa E where p.sincronizado = 0");

            Mdfe m = new Mdfe();

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
                                    m.Codigo = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                m.Idweb = (int)Convert.ToInt32(dr["Idweb"]);

                                if (dr["codigo"] != DBNull.Value) { m.Codigo = (int)dr["codigo"]; }
                                if (dr["chave"] != DBNull.Value) { m.Chave = (string)dr["chave"]; }
                                if (dr["serie"] != DBNull.Value) { m.Serie = (int)dr["serie"]; }
                                if (dr["documento"] != DBNull.Value) { m.Documento = (int)dr["documento"]; }
                                if (dr["dataemissao"] != DBNull.Value) { m.Dataemissao = (DateTime)dr["dataemissao"]; }
                                if (dr["horaemissao"] != DBNull.Value) { m.Horaemissao = (DateTime)dr["horaemissao"]; }
                                if (dr["datainicio"] != DBNull.Value) { m.Datainicio = (DateTime)dr["dataincio"]; }
                                if (dr["horainicio"] != DBNull.Value) { m.Horainicio = (DateTime)dr["horainicio"]; }
                                if (dr["informarcodigo"] != DBNull.Value) { m.Informarcodigo = (int)dr["informarcodigo"]; }
                                if (dr["tipoemitente"] != DBNull.Value) { m.Tipoemitente = (int)dr["tipoemitente"]; }
                                if (dr["ufemitente"] != DBNull.Value) { m.Ufemitente = (string)dr["ufemitente"]; }
                                if (dr["formaemissao"] != DBNull.Value) { m.Formaemissao = (int)dr["formaemissao"]; }
                                if (dr["ufdescarregamento"] != DBNull.Value) { m.Ufdescarregamento = (string)dr["ufdescarregamento"]; }
                                if (dr["codporto"] != DBNull.Value) { m.Codporto = (string)dr["codporto"]; }
                                if (dr["idveiculo"] != DBNull.Value) { m.Idveiculo = (int)dr["idveiculo"]; }
                                if (dr["ufveiculo"] != DBNull.Value) { m.Ufveiculo = (string)dr["ufveiculo"]; }
                                if (dr["tipocarroceria"] != DBNull.Value) { m.Tipocarroceria = (int)dr["tipocarroceria"]; }
                                if (dr["tiporodado"] != DBNull.Value) { m.Tiporodado = (int)dr["tiporodado"]; }
                                if (dr["placa"] != DBNull.Value) { m.Placa = (string)dr["placa"]; }
                                if (dr["capacidadekg"] != DBNull.Value) { m.Capacidadekg = (decimal)dr["capacidadekg"]; }
                                if (dr["tarakg"] != DBNull.Value) { m.Tarakg = (decimal)dr["tarakg"]; }
                                if (dr["capacidadem3"] != DBNull.Value) { m.Capacidadem3 = (decimal)dr["capacidadem3"]; }
                                if (dr["renavam"] != DBNull.Value) { m.Renavam = (string)dr["renavam"]; }
                                if (dr["proprietario"] != DBNull.Value) { m.Proprietario = (int)dr["proprietario"]; }
                                if (dr["rntrc"] != DBNull.Value) { m.Rntrc = (string)dr["rntrc"]; }
                                if (dr["tipoproprietario"] != DBNull.Value) { m.Tipoproprietario = (int)dr["tipoproprietario"]; }
                                if (dr["ufproprietario"] != DBNull.Value) { m.Ufproprietario = (string)dr["ufproprietario"]; }
                                if (dr["cpfcnpj"] != DBNull.Value) { m.Cpfcnpj = (string)dr["cpfcnpj"]; }
                                if (dr["nome"] != DBNull.Value) { m.Nome = (string)dr["nome"]; }
                                if (dr["ie"] != DBNull.Value) { m.Ie = (string)dr["ie"]; }
                                if (dr["valortotalmercadoria"] != DBNull.Value) { m.Valortotalmercadoria = (decimal)dr["valortotalmercadoria"]; }
                                if (dr["unidademedida"] != DBNull.Value) { m.Unidademedida = (string)dr["unidademedida"]; }
                                if (dr["valortotalpesobruto"] != DBNull.Value) { m.Valortotalpesobruto = (decimal)dr["valortotalpesobruto"]; }
                                if (dr["obs"] != DBNull.Value) { m.Obs = (string)dr["obs"]; }
                                if (dr["obsfisco"] != DBNull.Value) { m.Obsfisco = (string)dr["obsfisco"]; }
                                if (dr["tipoambiente"] != DBNull.Value) { m.Tipoambiente = (int)dr["tipoambiente"]; }
                                if (dr["versao"] != DBNull.Value) { m.Versao = (string)dr["versao"]; }
                                if (dr["versaosistema"] != DBNull.Value) { m.Versaosistema = (string)dr["versaosistema"]; }
                                if (dr["status"] != DBNull.Value) { m.Status = (int)dr["status"]; }
                                if (dr["cancelado"] != DBNull.Value) { m.Cancelado = (int)dr["cancelado"]; }
                                if (dr["ufretorno"] != DBNull.Value) { m.Ufretorno = (string)dr["ufretorno"]; }
                                if (dr["xmotivo"] != DBNull.Value) { m.Xmotivo = (string)dr["xmotivo"]; }
                                if (dr["xmsg"] != DBNull.Value) { m.Xmsg = (string)dr["xmsg"]; }
                                if (dr["recibo"] != DBNull.Value) { m.Recibo = (string)dr["recibo"]; }
                                if (dr["protocolo"] != DBNull.Value) { m.Protocolo = (string)dr["protocolo"]; }
                                if (dr["idfuncionario"] != DBNull.Value) { m.Idfuncionario = (int)dr["idfuncionario"]; }
                                if (dr["idmodelo"] != DBNull.Value) { m.Idmodelo = (int)dr["idmodelo"]; }
                                if (dr["ciot"] != DBNull.Value) { m.Ciot = (string)dr["ciot"]; }
                                if (dr["rntrcempresa"] != DBNull.Value) { m.Rntrcempresa = (string)dr["rntrcempresa"]; }
                                if (dr["idempresa"] != DBNull.Value) { m.Idempresa = (int)dr["idempresa"]; }
                                if (dr["ufcarregamento"] != DBNull.Value) { m.Ufcarregamento = (string)dr["ufcarregamento"]; }
                                if (dr["cnpjcontratante"] != DBNull.Value) { m.Cnpjcontratante = (string)dr["cnpjcontratante"]; }
                                if (dr["emissao"] != DBNull.Value) { m.Emissao = (int)dr["emissao"]; }
                                if (dr["account_id"] != DBNull.Value) { m.Idweb = (int)dr["account_id"]; }



                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + m.Codigo + "\"," +
                                     "\"codigo\":\"" + m.Codigo + "\"," +
                                     
                                     "\"chave\":\"" + m.Chave + "\"," +
                                     "\"serie\":\"" + m.Serie + "\"," +
                                     "\"documento\":\"" + m.Documento + "\"," +
                                     "\"dataemissao\":\"" + m.Dataemissao + "\"," +
                                     "\"horaemissao\":\"" + m.Horaemissao + "\"," +
                                     "\"datainicio\":\"" + m.Datainicio + "\"," +
                                     "\"horainicio\":\"" + m.Horainicio + "\"," +
                                     "\"informarcodigo\":\"" + m.Informarcodigo + "\"," +
                                     "\"tipoemitente\":\"" + m.Tipoemitente + "\"," +
                                     "\"ufemitente\":\"" + m.Ufemitente + "\"," +
                                     "\"formaemissao\":\"" + m.Formaemissao + "\"," +
                                     "\"ufdescarregamento\":\"" + m.Ufdescarregamento + "\"," +
                                     "\"codporto\":\"" + m.Codporto + "\"," +
                                     "\"idveiculo\":\"" + m.Idveiculo + "\"," +
                                     "\"ufveiculo\":\"" + m.Ufveiculo + "\"," +
                                     "\"tipocarroceria\":\"" + m.Tipocarroceria + "\"," +
                                     "\"tiporodado\":\"" + m.Tiporodado + "\"," +
                                     "\"placa\":\"" + m.Placa + "\"," +
                                     "\"capacidadekg\":\"" + m.Capacidadekg + "\"," +
                                     "\"tarakg\":\"" + m.Tarakg + "\"," +
                                     "\"capacidadem3\":\"" + m.Capacidadem3 + "\"," +
                                     "\"renavam\":\"" + m.Renavam + "\"," +
                                     "\"proprietario\":\"" + m.Proprietario + "\"," +
                                     "\"rntrc\":\"" + m.Rntrc + "\"," +
                                     "\"tipoproprietario\":\"" + m.Tipoproprietario + "\"," +
                                     "\"ufproprietario\":\"" + m.Ufproprietario + "\"," +
                                     "\"cpfcnpj\":\"" + m.Cpfcnpj + "\"," +
                                     "\"nome\":\"" + m.Nome + "\"," +
                                     "\"ie\":\"" + m.Ie + "\"," +
                                     "\"valortotalmercadoria\":\"" + m.Valortotalmercadoria + "\"," +
                                     "\"unidademedida\":\"" + m.Unidademedida + "\"," +
                                     "\"valortotalpesobruto\":\"" + m.Valortotalpesobruto + "\"," +
                                     "\"obs\":\"" + m.Obs + "\"," +
                                     "\"obsfisco\":\"" + m.Obsfisco + "\"," +
                                     "\"tipoambiente\":\"" + m.Tipoambiente + "\"," +
                                     "\"versao\":\"" + m.Versao + "\"," +
                                     "\"versaosistema\":\"" + m.Versaosistema + "\"," +
                                     "\"status\":\"" + m.Status + "\"," +
                                     "\"cancelado\":\"" + m.Cancelado + "\"," +
                                     "\"ufretorno\":\"" + m.Ufretorno + "\"," +
                                     "\"xmotivo\":\"" + m.Xmotivo + "\"," +
                                     "\"xmsg\":\"" + m.Xmsg + "\"," +
                                     "\"recibo\":\"" + m.Recibo + "\"," +
                                     "\"protocolo\":\"" + m.Protocolo + "\"," +
                                     "\"idfuncionario\":\"" + m.Idfuncionario + "\"," +
                                     "\"idmodelo\":\"" + m.Idmodelo + "\"," +
                                     "\"ciot\":\"" + m.Ciot + "\"," +
                                     "\"rntrcempresa\":\"" + m.Rntrcempresa + "\"," +
                                     "\"idempresa\":\"" + m.Idempresa + "\"," +
                                     "\"ufcarregamento\":\"" + m.Ufcarregamento + "\"," +
                                     "\"cnpjcontratante\":\"" + m.Cnpjcontratante + "\"," +
                                     "\"emissao\":\"" + m.Emissao + "\"," +
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

                                        conn.ExecuteQueries("UPDATE NF P SET P.IDNFWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    MdfeDAL mdfedal = new MdfeDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE NF P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(m.Codigo));
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

        public Mdfe ObterPorId(long id)
        {
            var mdfe = new Mdfe();

            return mdfe;
        }
    }
}
