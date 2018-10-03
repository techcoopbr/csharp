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
    public class NfDAL
    {
        public void Gravar(Nf nf)
        {
            if (nf.Codigo == 0)
                Inserir(nf);
            else
                Atualizar(nf);
        }

        private void Inserir(Nf nf)
        {

        }

        private void Atualizar(Nf nf)
        {

        }

        public void Remover(Nf nf)
        {

        }

        public void PostNf()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");

            NfDAL pdal = new NfDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb from NF p, empresa E where p.sincronizado = 0");

            Nf p = new Nf();

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
                                    p.Id = DocumentId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO DOCUMENTOS...");
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO DOCUMENTOS...");
                                }

                                p.Idweb = (int)Convert.ToInt32(dr["Idweb"]);
                                if (dr["codigo"] != DBNull.Value) { p.Codigo = (int)dr["codigo"]; }
                                if (dr["documento"] != DBNull.Value) { p.Documento = (int)dr["documento"]; }
                                if (dr["idpessoas"] != DBNull.Value) { p.Idpessoas = (int)dr["idpessoas"]; }
                                if (dr["dataentrada"] != DBNull.Value) { p.Dataentrada = (DateTime)dr["dataentrada"]; }
                                if (dr["dataemissao"] != DBNull.Value) { p.Dataemissao = (DateTime)dr["dataemissao"]; }
                                if (dr["hora"] != DBNull.Value) { p.Hora = (DateTime)dr["hora"]; }
                                if (dr["tipo"] != DBNull.Value) { p.Tipo = (string)dr["tipo"]; }
                                if (dr["emissor"] != DBNull.Value) { p.Emissor = (int)dr["emissor"]; }
                                if (dr["chavenfe"] != DBNull.Value) { p.Chavenfe = (string)dr["chavenfe"]; }

                                if (dr["xml"] != DBNull.Value) { p.Xml = (string)dr["xml"]; } else { p.Xml = ""; }

                                if (dr["modelo"] != DBNull.Value) { p.Modelo = (string)dr["modelo"]; }
                                if (dr["operacao"] != DBNull.Value) { p.Operacao = (int)dr["operacao"]; }
                                if (dr["valorconsumo"] != DBNull.Value) { p.Valorconsumo = (decimal)dr["valorconsumo"]; }
                                if (dr["valorenergia"] != DBNull.Value) { p.Valorenergia = (decimal)dr["valorenergia"]; }
                                if (dr["valorservico"] != DBNull.Value) { p.Valorservico = (decimal)dr["valorservico"]; }
                                if (dr["valorproduto"] != DBNull.Value) { p.Valorproduto = (decimal)dr["valorproduto"]; }
                                if (dr["valortotal"] != DBNull.Value) { p.Valortotal = (decimal)dr["valortotal"]; }
                                if (dr["baseicms"] != DBNull.Value) { p.Baseicms = (decimal)dr["baseicms"]; }
                                if (dr["baseicmsst"] != DBNull.Value) { p.Baseicmsst = (decimal)dr["baseicmsst"]; }
                                if (dr["basepis"] != DBNull.Value) { p.Basepis = (decimal)dr["basepis"]; }
                                if (dr["basecofins"] != DBNull.Value) { p.Basecofins = (decimal)dr["basecofins"]; }
                                if (dr["baseipi"] != DBNull.Value) { p.Baseipi = (decimal)dr["baseipi"]; }
                                if (dr["valoricms"] != DBNull.Value) { p.Valoricms = (decimal)dr["valoricms"]; }
                                if (dr["valoricmsst"] != DBNull.Value) { p.Valoricmsst = (decimal)dr["valoricmsst"]; }
                                if (dr["valorpis"] != DBNull.Value) { p.Valorpis = (decimal)dr["valorpis"]; }
                                if (dr["valorcofins"] != DBNull.Value) { p.Valorcofins = (decimal)dr["valorcofins"]; }
                                if (dr["valoripi"] != DBNull.Value) { p.Valoripi = (decimal)dr["valoripi"]; }
                                if (dr["valorfrete_somado"] != DBNull.Value) { p.Valorfrete_somado = (decimal)dr["valorfrete_somado"]; }
                                if (dr["valorfrete"] != DBNull.Value) { p.Valorfrete = (decimal)dr["valorfrete"]; }
                                if (dr["valoroutros_somado"] != DBNull.Value) { p.Valoroutros_somado = (decimal)dr["valoroutros_somado"]; }
                                if (dr["valoroutros"] != DBNull.Value) { p.Valoroutros = (decimal)dr["valoroutros"]; }
                                if (dr["valordesconto"] != DBNull.Value) { p.Valordesconto = (decimal)dr["valordesconto"]; }
                                if (dr["valoracrescimo"] != DBNull.Value) { p.Valoracrescimo = (decimal)dr["valoracrescimo"]; }
                                if (dr["serie"] != DBNull.Value) { p.Serie = (int)dr["serie"]; }
                                if (dr["ambiente"] != DBNull.Value) { p.Ambiente = (int)dr["ambiente"]; }
                                if (dr["emissao"] != DBNull.Value) { p.Emissao = (int)dr["emissao"]; }
                                if (dr["status"] != DBNull.Value) { p.Status = (string)dr["status"]; }
                                if (dr["protocolo"] != DBNull.Value) { p.Protocolo = (string)dr["protocolo"]; }
                                if (dr["recibo"] != DBNull.Value) { p.Recibo = (string)dr["recibo"]; }
                                if (dr["horarecibo"] != DBNull.Value) { p.Horarecibo = (string)dr["horarecibo"]; }
                                if (dr["observacao"] != DBNull.Value) { p.Observacao = dr["observacao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["observacaofisco"] != DBNull.Value) { p.Observacaofisco = dr["observacaofisco"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["tipofrete"] != DBNull.Value) { p.Tipofrete = (int)dr["tipofrete"]; }
                                if (dr["idtransportador"] != DBNull.Value) { p.Idtransportador = (int)dr["idtransportador"]; }
                                if (dr["placa"] != DBNull.Value) { p.Placa = (string)dr["placa"]; }
                                if (dr["ufveiculo"] != DBNull.Value) { p.Ufveiculo = (string)dr["ufveiculo"]; }
                                if (dr["rntc"] != DBNull.Value) { p.Rntc = (string)dr["rntc"]; }
                                if (dr["quantidadevolume"] != DBNull.Value) { p.Quantidadevolume = (decimal)dr["quantidadevolume"]; }
                                if (dr["especievolume"] != DBNull.Value) { p.Especievolume = dr["especievolume"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["marcavolume"] != DBNull.Value) { p.Marcavolume = dr["marcavolume"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["numerovolume"] != DBNull.Value) { p.Numerovolume = dr["numerovolume"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["pesolvolume"] != DBNull.Value) { p.Pesolvolume = (decimal)dr["pesolvolume"]; }
                                if (dr["pesobvolume"] != DBNull.Value) { p.Pesobvolume = (decimal)dr["pesobvolume"]; }
                                if (dr["data_atualizacao"] != DBNull.Value) { p.Data_atualizacao = (DateTime)dr["data_atualizacao"]; }
                                if (dr["ufdesembarque"] != DBNull.Value) { p.Ufdesembarque = (string)dr["ufdesembarque"]; }
                                if (dr["localdesembarque"] != DBNull.Value) { p.Localdesembarque = dr["localdesembarque"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["funcionario"] != DBNull.Value) { p.Funcionario = (int)dr["funcionario"]; }
                                if (dr["idapto"] != DBNull.Value) { p.Idapto = (int)dr["idapto"]; }
                                if (dr["empresa"] != DBNull.Value) { p.Empresa = (int)dr["empresa"]; }
                                if (dr["idviagem"] != DBNull.Value) { p.Idviagem = (int)dr["idviagem"]; }
                                if (dr["idcaixa"] != DBNull.Value) { p.Idcaixa = (int)dr["idcaixa"]; }
                                if (dr["idvendedor"] != DBNull.Value) { p.Idvendedor = (int)dr["idvendedor"]; }
                                if (dr["nomepessoa"] != DBNull.Value) { p.Nomepessoa = dr["nomepessoa"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["localentrega"] != DBNull.Value) { p.Localentrega = dr["localentrega"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["comissao"] != DBNull.Value) { p.Comissao = (decimal)dr["comissao"]; }
                                if (dr["requisitante"] != DBNull.Value) { p.Requisitante = (string)dr["requisitante"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["idplanovenda"] != DBNull.Value) { p.Idplanovenda = (int)dr["idplanovenda"]; }
                                if (dr["idorigem"] != DBNull.Value) { p.Idorigem = (int)dr["idorigem"]; }
                                if (dr["entrega"] != DBNull.Value) { p.Entrega = (int)dr["entrega"]; }
                                if (dr["retirada"] != DBNull.Value) { p.Retirada = (int)dr["retirada"]; }
                                if (dr["nfpropria"] != DBNull.Value) { p.Nfpropria = (int)dr["nfpropria"]; }
                                if (dr["horasaida"] != DBNull.Value) { p.Horasaida = (DateTime)dr["horasaida"]; }
                                if (dr["idecf"] != DBNull.Value) { p.Idecf = (int)dr["idecf"]; }
                                if (dr["cnpjdigitado"] != DBNull.Value) { p.Cnpjdigitado = dr["cnpjdigitado"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["percentualimp"] != DBNull.Value) { p.Percentualimp = (decimal)dr["percentualimp"]; }
                                if (dr["comb_placa"] != DBNull.Value) { p.Comb_placa = (string)dr["comb_placa"]; }
                                if (dr["comb_kmplaca"] != DBNull.Value) { p.Comb_kmplaca = (int)dr["comb_kmplaca"]; }
                                if (dr["impostos_nac"] != DBNull.Value) { p.Impostos_nac = (decimal)dr["impostos_nac"]; }
                                if (dr["impostos_imp"] != DBNull.Value) { p.Impostos_imp = (decimal)dr["impostos_imp"]; }
                                if (dr["impostos_est"] != DBNull.Value) { p.Impostos_est = (decimal)dr["impostos_est"]; }
                                if (dr["impostos_mun"] != DBNull.Value) { p.Impostos_mun = (decimal)dr["impostos_mun"]; }
                                if (dr["idnfbaseada"] != DBNull.Value) { p.Idnfbaseada = (int)dr["idnfbaseada"]; }
                                if (dr["oskminicial"] != DBNull.Value) { p.Oskminicial = (decimal)dr["oskminicial"]; }
                                if (dr["oskmfinal"] != DBNull.Value) { p.Oskmfinal = (decimal)dr["oskmfinal"]; }
                                if (dr["cancelada"] != DBNull.Value) { p.Cancelada = (int)dr["cancelada"]; }
                                if (dr["md5"] != DBNull.Value) { p.Md5 = (string)dr["md5"]; }
                                if (dr["crz"] != DBNull.Value) { p.Crz = (int)dr["crz"]; }
                                if (dr["coo"] != DBNull.Value) { p.Coo = (int)dr["coo"]; }
                                if (dr["ccf"] != DBNull.Value) { p.Ccf = (int)dr["ccf"]; }
                                if (dr["finalidade"] != DBNull.Value) { p.Finalidade = (int)dr["finalidade"]; }
                                if (dr["idmodelo"] != DBNull.Value) { p.Idmodelo = (int)dr["idmodelo"]; }
                                if (dr["tipocarteira"] != DBNull.Value) { p.Tipocarteira = (string)dr["tipocarteira"]; }
                                if (dr["piscofinssomado"] != DBNull.Value) { p.Piscofinssomado = (decimal)dr["piscofinssomado"]; }
                                if (dr["idveiculo"] != DBNull.Value) { p.Idveiculo = (int)dr["idveiculo"]; }
                                if (dr["emedicao"] != DBNull.Value) { p.Emedicao = (int)dr["emedicao"]; }
                                if (dr["totalitens"] != DBNull.Value) { p.Totalitens = (decimal)dr["totalitens"]; }
                                if (dr["datafiscal"] != DBNull.Value) { p.Datafiscal = (DateTime)dr["datafiscal"]; }
                                if (dr["idrepresentante"] != DBNull.Value) { p.Idrepresentante = (int)dr["idrepresentante"]; }
                                if (dr["valordescontos_prod"] != DBNull.Value) { p.Valordescontos_prod = (decimal)dr["valordescontos_prod"]; }
                                if (dr["valordescontos_nf"] != DBNull.Value) { p.Valordescontos_nf = (decimal)dr["valordescontos_nf"]; }
                                if (dr["impostostotais"] != DBNull.Value) { p.Impostostotais = (decimal)dr["impostostotais"]; }
                                if (dr["valordinheiro"] != DBNull.Value) { p.Valordinheiro = (decimal)dr["valordinheiro"]; }
                                if (dr["valorprazo"] != DBNull.Value) { p.Valorprazo = (decimal)dr["valorprazo"]; }
                                if (dr["valorcdebito"] != DBNull.Value) { p.Valorcdebito = (decimal)dr["valorcdebito"]; }
                                if (dr["valorccredito"] != DBNull.Value) { p.Valorccredito = (decimal)dr["valorccredito"]; }
                                if (dr["valorccorrente"] != DBNull.Value) { p.Valorccorrente = (decimal)dr["valorccorrente"]; }
                                if (dr["idequipamento"] != DBNull.Value) { p.Idequipamento = (int)dr["idequipamento"]; }
                                if (dr["equipamentodescricao"] != DBNull.Value) { p.Equipamentodescricao = (string)dr["equipamentodescricao"]; }
                                if (dr["idcomanda"] != DBNull.Value) { p.Idcomanda = (int)dr["idcomanda"]; }
                                if (dr["aliquotaiss"] != DBNull.Value) { p.Aliquotaiss = (decimal)dr["aliquotaiss"]; }
                                if (dr["baseiss"] != DBNull.Value) { p.Baseiss = (decimal)dr["baseiss"]; }
                                if (dr["valoriss"] != DBNull.Value) { p.Valoriss = (decimal)dr["valoriss"]; }
                                if (dr["datamanifesto"] != DBNull.Value) { p.Datamanifesto = (DateTime)dr["datamanifesto"]; }
                                if (dr["statusmanifesto"] != DBNull.Value) { p.Statusmanifesto = (int)dr["statusmanifesto"]; }
                                if (dr["eventomanifesto"] != DBNull.Value) { p.Eventomanifesto = dr["eventomanifesto"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["xeventomanifesto"] != DBNull.Value) { p.Xeventomanifesto = dr["xeventomanifesto"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["protocolomanifesto"] != DBNull.Value) { p.Protocolomanifesto = (string)dr["protocolomanifesto"]; }
                                if (dr["bloq_clientebloqueado"] != DBNull.Value) { p.Bloq_clientebloqueado = (int)dr["bloq_clientebloqueado"]; }
                                if (dr["bloq_parcelaabaixo"] != DBNull.Value) { p.Bloq_parcelaabaixo = (int)dr["bloq_parcelaabaixo"]; }
                                if (dr["bloq_parcelaacima"] != DBNull.Value) { p.Bloq_parcelaacima = (int)dr["bloq_parcelaacima"]; }
                                if (dr["bloq_descontocliente"] != DBNull.Value) { p.Bloq_descontocliente = (int)dr["bloq_descontocliente"]; }
                                if (dr["bloq_descontototal"] != DBNull.Value) { p.Bloq_descontototal = (int)dr["bloq_descontototal"]; }
                                if (dr["xmotivo"] != DBNull.Value) { p.Xmotivo = dr["xmotivo"].ToString().Trim().Replace(@"""", @"\"""); }

                                //pdal.PostNf(p);

                                string json = "{ ";
                                if (DocumentId != 0)
                                {
                                    json = json + "\"id\" :\"" + DocumentId + "\", ";
                                }
                                json = json + "\"codigo\":\"" + p.Codigo + "\"," +
                                     "\"codigo\":\"" + p.Codigo + "\"," +

                                     "\"xml\":\"" + p.Xml.Replace(@"""", @"\""") + "\"," +
                                     "\"documento\":\"" + p.Documento + "\"," +
                                     "\"idpessoas\":\"" + p.Idpessoas + "\"," +
                                     "\"dataentrada\":\"" + p.Dataentrada + "\"," +
                                     "\"dataemissao\":\"" + p.Dataemissao + "\"," +
                                     "\"hora\":\"" + p.Hora + "\"," +
                                     "\"tipo\":\"" + p.Tipo + "\"," +
                                     "\"emissor\":\"" + p.Emissor + "\"," +
                                     "\"chavenfe\":\"" + p.Chavenfe + "\"," +
                                     "\"modelo\":\"" + p.Modelo + "\"," +
                                     "\"operacao\":\"" + p.Operacao + "\"," +
                                     "\"valorconsumo\":\"" + p.Valorconsumo + "\"," +
                                     "\"valorenergia\":\"" + p.Valorenergia + "\"," +
                                     "\"valorservico\":\"" + p.Valorservico + "\"," +
                                     "\"valorproduto\":\"" + p.Valorproduto + "\"," +
                                     "\"valortotal\":\"" + p.Valortotal + "\"," +
                                     "\"baseicms\":\"" + p.Baseicms + "\"," +
                                     "\"baseicmsst\":\"" + p.Baseicmsst + "\"," +
                                     "\"basepis\":\"" + p.Basepis + "\"," +
                                     "\"basecofins\":\"" + p.Basecofins + "\"," +
                                     "\"baseipi\":\"" + p.Baseipi + "\"," +
                                     "\"valoricms\":\"" + p.Valoricms + "\"," +
                                     "\"valoricmsst\":\"" + p.Valoricmsst + "\"," +
                                     "\"valorpis\":\"" + p.Valorpis + "\"," +
                                     "\"valorcofins\":\"" + p.Valorcofins + "\"," +
                                     "\"valoripi\":\"" + p.Valoripi + "\"," +
                                     "\"valorfrete_somado\":\"" + p.Valorfrete_somado + "\"," +
                                     "\"valorfrete\":\"" + p.Valorfrete + "\"," +
                                     "\"valoroutros_somado\":\"" + p.Valoroutros_somado + "\"," +
                                     "\"valoroutros\":\"" + p.Valoroutros + "\"," +
                                     "\"valordesconto\":\"" + p.Valordesconto + "\"," +
                                     "\"valoracrescimo\":\"" + p.Valoracrescimo + "\"," +
                                     "\"serie\":\"" + p.Serie + "\"," +
                                     "\"ambiente\":\"" + p.Ambiente + "\"," +
                                     "\"emissao\":\"" + p.Emissao + "\"," +
                                     "\"status\":\"" + p.Status + "\"," +
                                     "\"protocolo\":\"" + p.Protocolo + "\"," +
                                     "\"recibo\":\"" + p.Recibo + "\"," +
                                     "\"horarecibo\":\"" + p.Horarecibo + "\"," +
                                     "\"observacao\":\"" + p.Observacao + "\"," +
                                     "\"observacaofisco\":\"" + p.Observacaofisco + "\"," +
                                     "\"tipofrete\":\"" + p.Tipofrete + "\"," +
                                     "\"idtransportador\":\"" + p.Idtransportador + "\"," +
                                     "\"placa\":\"" + p.Placa + "\"," +
                                     "\"ufveiculo\":\"" + p.Ufveiculo + "\"," +
                                     "\"rntc\":\"" + p.Rntc + "\"," +
                                     "\"quantidadevolume\":\"" + p.Quantidadevolume + "\"," +
                                     "\"especievolume\":\"" + p.Especievolume + "\"," +
                                     "\"marcavolume\":\"" + p.Marcavolume + "\"," +
                                     "\"numerovolume\":\"" + p.Numerovolume + "\"," +
                                     "\"pesolvolume\":\"" + p.Pesolvolume + "\"," +
                                     "\"pesobvolume\":\"" + p.Pesobvolume + "\"," +
                                     "\"data_atualizacao\":\"" + p.Data_atualizacao + "\"," +
                                     "\"ufdesembarque\":\"" + p.Ufdesembarque + "\"," +
                                     "\"localdesembarque\":\"" + p.Localdesembarque + "\"," +
                                     "\"funcionario\":\"" + p.Funcionario + "\"," +
                                     "\"idapto\":\"" + p.Idapto + "\"," +
                                     "\"empresa\":\"" + p.Empresa + "\"," +
                                     "\"idviagem\":\"" + p.Idviagem + "\"," +
                                     "\"idcaixa\":\"" + p.Idcaixa + "\"," +
                                     "\"idvendedor\":\"" + p.Idvendedor + "\"," +
                                     "\"nomepessoa\":\"" + p.Nomepessoa + "\"," +
                                     "\"localentrega\":\"" + p.Localentrega + "\"," +
                                     "\"comissao\":\"" + p.Comissao + "\"," +
                                     "\"requisitante\":\"" + p.Requisitante + "\"," +
                                     "\"idplanovenda\":\"" + p.Idplanovenda + "\"," +
                                     "\"idorigem\":\"" + p.Idorigem + "\"," +
                                     "\"entrega\":\"" + p.Entrega + "\"," +
                                     "\"retirada\":\"" + p.Retirada + "\"," +
                                     "\"nfpropria\":\"" + p.Nfpropria + "\"," +
                                     "\"horasaida\":\"" + p.Horasaida + "\"," +
                                     "\"idecf\":\"" + p.Idecf + "\"," +
                                     "\"cnpjdigitado\":\"" + p.Cnpjdigitado + "\"," +
                                     "\"percentualimp\":\"" + p.Percentualimp + "\"," +
                                     "\"comb_placa\":\"" + p.Comb_placa + "\"," +
                                     "\"comb_kmplaca\":\"" + p.Comb_kmplaca + "\"," +
                                     "\"impostos_nac\":\"" + p.Impostos_nac + "\"," +
                                     "\"impostos_imp\":\"" + p.Impostos_imp + "\"," +
                                     "\"impostos_est\":\"" + p.Impostos_est + "\"," +
                                     "\"impostos_mun\":\"" + p.Impostos_mun + "\"," +
                                     "\"idnfbaseada\":\"" + p.Idnfbaseada + "\"," +
                                     "\"oskminicial\":\"" + p.Oskminicial + "\"," +
                                     "\"oskmfinal\":\"" + p.Oskmfinal + "\"," +
                                     "\"cancelada\":\"" + p.Cancelada + "\"," +
                                     "\"md5\":\"" + p.Md5 + "\"," +
                                     "\"crz\":\"" + p.Crz + "\"," +
                                     "\"coo\":\"" + p.Coo + "\"," +
                                     "\"ccf\":\"" + p.Ccf + "\"," +
                                     "\"finalidade\":\"" + p.Finalidade + "\"," +
                                     "\"idmodelo\":\"" + p.Idmodelo + "\"," +
                                     "\"tipocarteira\":\"" + p.Tipocarteira + "\"," +
                                     "\"piscofinssomado\":\"" + p.Piscofinssomado + "\"," +
                                     "\"idveiculo\":\"" + p.Idveiculo + "\"," +
                                     "\"emedicao\":\"" + p.Emedicao + "\"," +
                                     "\"totalitens\":\"" + p.Totalitens + "\"," +
                                     "\"datafiscal\":\"" + p.Datafiscal + "\"," +
                                     "\"idrepresentante\":\"" + p.Idrepresentante + "\"," +
                                     "\"valordescontos_prod\":\"" + p.Valordescontos_prod + "\"," +
                                     "\"valordescontos_nf\":\"" + p.Valordescontos_nf + "\"," +
                                     "\"impostostotais\":\"" + p.Impostostotais + "\"," +
                                     "\"valordinheiro\":\"" + p.Valordinheiro + "\"," +
                                     "\"valorprazo\":\"" + p.Valorprazo + "\"," +
                                     "\"valorcdebito\":\"" + p.Valorcdebito + "\"," +
                                     "\"valorccredito\":\"" + p.Valorccredito + "\"," +
                                     "\"valorccorrente\":\"" + p.Valorccorrente + "\"," +
                                     "\"idequipamento\":\"" + p.Idequipamento + "\"," +
                                     "\"equipamentodescricao\":\"" + p.Equipamentodescricao + "\"," +
                                     "\"idcomanda\":\"" + p.Idcomanda + "\"," +
                                     "\"aliquotaiss\":\"" + p.Aliquotaiss + "\"," +
                                     "\"baseiss\":\"" + p.Baseiss + "\"," +
                                     "\"valoriss\":\"" + p.Valoriss + "\"," +
                                     "\"datamanifesto\":\"" + p.Datamanifesto + "\"," +
                                     "\"statusmanifesto\":\"" + p.Statusmanifesto + "\"," +
                                     "\"eventomanifesto\":\"" + p.Eventomanifesto + "\"," +
                                     "\"xeventomanifesto\":\"" + p.Xeventomanifesto + "\"," +
                                     "\"protocolomanifesto\":\"" + p.Protocolomanifesto + "\"," +
                                     "\"bloq_clientebloqueado\":\"" + p.Bloq_clientebloqueado + "\"," +
                                     "\"bloq_parcelaabaixo\":\"" + p.Bloq_parcelaabaixo + "\"," +
                                     "\"bloq_parcelaacima\":\"" + p.Bloq_parcelaacima + "\"," +
                                     "\"bloq_descontocliente\":\"" + p.Bloq_descontocliente + "\"," +
                                     "\"bloq_descontototal\":\"" + p.Bloq_descontototal + "\"," +
                                     "\"xmotivo\":\"" + p.Xmotivo + "\"," +
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
                                    NfprodutoDAL nfpdal = new NfprodutoDAL();
                                    nfpdal.PostNfproduto((int)dr["codigo"]);

                                    conn.ExecuteQueries("UPDATE NF P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
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

        public Nf ObterPorId(long id)
        {
            var nf = new Nf();

            return nf;
        }
    }
}