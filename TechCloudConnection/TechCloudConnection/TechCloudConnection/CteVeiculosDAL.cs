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
    class CteVeiculosDAL
    {
        public void Gravar(CteVeiculos vei)
        {
            if (vei.Codigo == 0)
                Inserir(vei);
            else
                Atualizar(vei);
        }

        private void Inserir(CteVeiculos vei)
        {

        }

        private void Atualizar(CteVeiculos vei)
        {

        }

        public void Remover(CteVeiculos vei)
        {

        }


        public void PostCteCce()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            CteVeiculosDAL vdal = new CteVeiculosDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from CTE_VEICULOS p, empresa E where p.sincronizado = 0");

            CteVeiculos c = new CteVeiculos();

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

                            conn.ExecuteQueries("UPDATE CTE_VEICULOS P SET P.IDCTEVEICULOSWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

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
                                if (dr["descricao"] != DBNull.Value) { c.Descricao = (string)dr["descricao"]; }
                                if (dr["placa"] != DBNull.Value) { c.Placa = (string)dr["placa"]; }
                                if (dr["renavan"] != DBNull.Value) { c.Renavan = (string)dr["renavan"]; }
                                if (dr["ufveiculo"] != DBNull.Value) { c.Ufveiculo = (string)dr["ufveiculo"]; }
                                if (dr["capacidadekg"] != DBNull.Value) { c.Capacidadekg = (decimal)dr["capacidadekg"]; }
                                if (dr["capacidadem3"] != DBNull.Value) { c.Capacidadem3 = (decimal)dr["capacidadem3"]; }
                                if (dr["tiporodado"] != DBNull.Value) { c.Tiporodado = (int)dr["tiporodado"]; }
                                if (dr["tipocarroceria"] != DBNull.Value) { c.Tipocarroceria = (int)dr["tipocarroceria"]; }
                                if (dr["tipoveiculo"] != DBNull.Value) { c.Tipoveiculo = (int)dr["tipoveiculo"]; }
                                if (dr["tipoproprietario"] != DBNull.Value) { c.Tipoproprietario = (int)dr["tipoproprietario"]; }
                                if (dr["idproprietario"] != DBNull.Value) { c.Idproprietario = (int)dr["idproprietario"]; }
                                if (dr["idmotorista"] != DBNull.Value) { c.Idmotorista = (int)dr["idmotorista"]; }
                                if (dr["tara"] != DBNull.Value) { c.Tara = (decimal)dr["tara"]; }
                                if (dr["data_atualizacao"] != DBNull.Value) { c.Data_atualizacao = (DateTime)dr["data_atualizacao"]; }
                                if (dr["empresa"] != DBNull.Value) { c.Empresa = (int)dr["empresa"]; }
                                if (dr["kmoleomotor"] != DBNull.Value) { c.Kmoleomotor = (decimal)dr["kmoleomotor"]; }
                                if (dr["kmoleodiferencial"] != DBNull.Value) { c.Kmoleodiferencial = (decimal)dr["kmoleodiferencial"]; }
                                if (dr["kmoleocambio"] != DBNull.Value) { c.Kmoleocambio = (decimal)dr["kmoleocambio"]; }
                                if (dr["ano"] != DBNull.Value) { c.Ano = (int)dr["ano"]; }
                                if (dr["modelo"] != DBNull.Value) { c.Modelo = (int)dr["modelo"]; }
                                if (dr["chassi"] != DBNull.Value) { c.Chassi = (string)dr["chassi"]; }
                                if (dr["cor"] != DBNull.Value) { c.Cor = (string)dr["cor"]; }
                                if (dr["marca"] != DBNull.Value) { c.Marca = (string)dr["marca"]; }
                                if (dr["account_id"] != DBNull.Value) { c.Idweb = (int)dr["account_id"]; }



                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + c.Codigo + "\"," +
                                     "\"codigo\":\"" + c.Codigo + "\"," +

                                     "\"descricao\":\"" + c.Descricao + "\"," +
                                     "\"placa\":\"" + c.Placa + "\"," +
                                     "\"renavan\":\"" + c.Renavan + "\"," +
                                     "\"ufveiculo\":\"" + c.Ufveiculo + "\"," +
                                     "\"capacidadekg\":\"" + c.Capacidadekg + "\"," +
                                     "\"capacidadem3\":\"" + c.Capacidadem3 + "\"," +
                                     "\"tiporodado\":\"" + c.Tiporodado + "\"," +
                                     "\"tipocarroceria\":\"" + c.Tipocarroceria + "\"," +
                                     "\"tipoveiculo\":\"" + c.Tipoveiculo + "\"," +
                                     "\"tipoproprietario\":\"" + c.Tipoproprietario + "\"," +
                                     "\"idproprietario\":\"" + c.Idproprietario + "\"," +
                                     "\"idmotorista\":\"" + c.Idmotorista + "\"," +
                                     "\"tara\":\"" + c.Tara + "\"," +
                                     "\"data_atualizacao\":\"" + c.Data_atualizacao + "\"," +
                                     "\"empresa\":\"" + c.Empresa + "\"," +
                                     "\"kmoleomotor\":\"" + c.Kmoleomotor + "\"," +
                                     "\"kmoleodiferencial\":\"" + c.Kmoleodiferencial + "\"," +
                                     "\"kmoleocambio\":\"" + c.Kmoleocambio + "\"," +
                                     "\"ano\":\"" + c.Ano + "\"," +
                                     "\"modelo\":\"" + c.Modelo + "\"," +
                                     "\"chassi\":\"" + c.Chassi + "\"," +
                                     "\"cor\":\"" + c.Cor + "\"," +
                                     "\"marca\":\"" + c.Marca + "\"," +
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

                                        conn.ExecuteQueries("UPDATE CTE_VEICULOS P SET P.IDCTEVEICULOSWEB = " + Convert.ToString(DocumentId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                    }
                                }
                                catch (Exception e)
                                {
                                    DocumentId = 0;
                                }

                                try
                                {
                                    CteVeiculosDAL veidal = new CteVeiculosDAL();
                                    //movdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE CTE_VEICULOS P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(c.Codigo));
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

        public CteVeiculos ObterPorId(long id)
        {
            var vei = new CteVeiculos();

            return vei;
        }
    }
}
