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
    public class NfprodutoDAL
    {
        public void Gravar(Nfproduto nfproduto)
        {
            if (nfproduto.Codigo == 0)
                Inserir(nfproduto);
            else
                Atualizar(nfproduto);
        }

        private void Inserir(Nfproduto nfproduto)
        {

        }

        private void Atualizar(Nfproduto nfproduto)
        {

        }

        public void Remover(Nfproduto nfproduto)
        {

        }

        public void PostNfproduto(int Idnf)
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfprodutoig");

            try
            {
                NfprodutoDAL pdal = new NfprodutoDAL();

                FbDataReader dr = conn.DataReader("select p.*, NF.IDNFWEB, PP.IDPRODUTOSWEB, E.IDWEB from NF_PRODUTOS p, empresa E " +
                                                  " INNER JOIN NF ON P.IDNF = NF.CODIGO " +
                                                  " INNER JOIN PRODUTOS PP ON P.CODIGOPRODUTO = PP.CODIGO " +
                                                  " where p.sincronizado = 0 AND P.IDNF = " + Idnf + "");

                Nfproduto p = new Nfproduto();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        int NfprodutoId = 0;
                        // ENCONTRA O ID ACCOUNT DA EMPRESA DONA DA DUPLICATA
                        try
                        {
                            //string webAddrr = "http://localhost:3000/sell_products/" + Convert.ToString(dr["IDNFWEB"]) + "/" + Convert.ToString(dr["codigo"]) + ".json";

                            string webAddrr = "http://apptechcoop.com.br/sell_products/" + Convert.ToString(dr["IDNFWEB"]) + "/" + Convert.ToString(dr["codigo"]) + ".json";

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
                                NfprodutoId = usr["id"];

                                conn.ExecuteQueries("UPDATE NF_PRODUTOS P SET P.IDNFPRODUTOSWEB = " + Convert.ToString(NfprodutoId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                            }
                        }
                        catch (Exception e)
                        {
                            NfprodutoId = 0;
                        }

                        // ENCONTRA O ID FOLK DA PESSOA DA DUPLICATA
                        int DocumentID = 0;
                        try
                        {
                            //string webAddrr = "http://localhost:3000/document_accounts/" + Convert.ToString(dr["idnf"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                            string webAddrr = "http://apptechcoop.com.br/document_accounts/" + Convert.ToString(dr["idnf"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                            var httpWebRequesttD = (HttpWebRequest)WebRequest.Create(webAddrr);
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
                                DocumentID = usrD["id"];

                            }
                        }
                        catch (Exception e)
                        {
                            DocumentID = 0;
                        }

                        //////////////////////////////////////////////
                        try
                        {

                            string webAddr;

                            if (NfprodutoId == 0)
                            {
                                //webAddr = "http://localhost:3000/sells";
                                webAddr = "http://apptechcoop.com.br/sells";
                            }
                            else
                            {
                                //webAddr = "http://localhost:3000/sells/" + Convert.ToString(NfprodutoId) + ".json";
                                webAddr = "http://apptechcoop.com.br/sells/" + Convert.ToString(NfprodutoId) + ".json";
                            }

                            var httpWebRequestnf = (HttpWebRequest)WebRequest.Create(webAddr);
                            httpWebRequestnf.ContentType = "application/json; charset=utf-8";
                            if (NfprodutoId == 0)
                            {
                                httpWebRequestnf.Method = "POST";
                            }
                            else
                            {
                                httpWebRequestnf.Method = "PUT";
                            }

                            Login.login();

                            httpWebRequest.Headers.Add("Authorization", "Basic " + Login.encoded);

                            using (var streamWriternf = new StreamWriter(httpWebRequestnf.GetRequestStream()))
                            {
                                if (Convert.ToInt32(dr["Idweb"]) > 0)
                                {
                                    if (NfprodutoId != 0)
                                    {
                                        p.Id = NfprodutoId;
                                        NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO PRODUTOS EM DOCUMENTOS...");
                                    }
                                    else
                                    {
                                        NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO PRODUTOS EM DOCUMENTOS...");
                                    }

                                    if (dr["codigo"] != DBNull.Value) { p.Codigo = (int)dr["codigo"]; }
                                    if (dr["idnf"] != DBNull.Value) { p.Idnf = (int)dr["idnf"]; }
                                    if (dr["codigoproduto"] != DBNull.Value) { p.Codigoproduto = (int)dr["codigoproduto"]; }
                                    if (dr["cfop"] != DBNull.Value) { p.Cfop = (string)dr["cfop"]; }
                                    if (dr["quantidade"] != DBNull.Value) { p.Quantidade = (decimal)dr["quantidade"]; }
                                    if (dr["valorunitario"] != DBNull.Value) { p.Valorunitario = (decimal)dr["valorunitario"]; }
                                    if (dr["valortotal"] != DBNull.Value) { p.Valortotal = (decimal)dr["valortotal"]; }
                                    if (dr["descontos"] != DBNull.Value) { p.Descontos = (decimal)dr["descontos"]; }
                                    if (dr["csticms"] != DBNull.Value) { p.Csticms = (string)dr["csticms"]; }
                                    if (dr["cstpis"] != DBNull.Value) { p.Cstpis = (string)dr["cstpis"]; }
                                    if (dr["cstcofins"] != DBNull.Value) { p.Cstcofins = (string)dr["cstcofins"]; }
                                    if (dr["cstipi"] != DBNull.Value) { p.Cstipi = (string)dr["cstipi"]; }
                                    if (dr["baseicms"] != DBNull.Value) { p.Baseicms = (decimal)dr["baseicms"]; }
                                    if (dr["baseicmsst"] != DBNull.Value) { p.Baseicmsst = (decimal)dr["baseicmsst"]; }
                                    if (dr["basepis"] != DBNull.Value) { p.Basepis = (decimal)dr["basepis"]; }
                                    if (dr["basecofins"] != DBNull.Value) { p.Basecofins = (decimal)dr["basecofins"]; }
                                    if (dr["baseipi"] != DBNull.Value) { p.Baseipi = (decimal)dr["baseipi"]; }
                                    if (dr["aliquotaicms"] != DBNull.Value) { p.Aliquotaicms = (decimal)dr["aliquotaicms"]; }
                                    if (dr["aliquotaicmsst"] != DBNull.Value) { p.Aliquotaicmsst = (decimal)dr["aliquotaicmsst"]; }
                                    if (dr["aliquotapis"] != DBNull.Value) { p.Aliquotapis = (decimal)dr["aliquotapis"]; }
                                    if (dr["aliquotacofins"] != DBNull.Value) { p.Aliquotacofins = (decimal)dr["aliquotacofins"]; }
                                    if (dr["aliquotaipi"] != DBNull.Value) { p.Aliquotaipi = (decimal)dr["aliquotaipi"]; }
                                    if (dr["valoricms"] != DBNull.Value) { p.Valoricms = (decimal)dr["valoricms"]; }
                                    if (dr["valoricmsst"] != DBNull.Value) { p.Valoricmsst = (decimal)dr["valoricmsst"]; }
                                    if (dr["valorpis"] != DBNull.Value) { p.Valorpis = (decimal)dr["valorpis"]; }
                                    if (dr["valorcofins"] != DBNull.Value) { p.Valorcofins = (decimal)dr["valorcofins"]; }
                                    if (dr["valoripi"] != DBNull.Value) { p.Valoripi = (decimal)dr["valoripi"]; }
                                    if (dr["valorcusto"] != DBNull.Value) { p.Valorcusto = (decimal)dr["valorcusto"]; }
                                    if (dr["descricao"] != DBNull.Value) { p.Descricao = (string)dr["descricao"]; }
                                    if (dr["tipobcicms"] != DBNull.Value) { p.Tipobcicms = (int)dr["tipobcicms"]; }
                                    if (dr["tipobcicmsst"] != DBNull.Value) { p.Tipobcicmsst = (int)dr["tipobcicmsst"]; }
                                    if (dr["coringaicms"] != DBNull.Value) { p.Coringaicms = (decimal)dr["coringaicms"]; }
                                    if (dr["coringaicmsst"] != DBNull.Value) { p.Coringaicmsst = (decimal)dr["coringaicmsst"]; }
                                    if (dr["reducaoicms"] != DBNull.Value) { p.Reducaoicms = (decimal)dr["reducaoicms"]; }
                                    if (dr["desconto_rateio"] != DBNull.Value) { p.Desconto_rateio = (decimal)dr["desconto_rateio"]; }
                                    if (dr["seguro"] != DBNull.Value) { p.Seguro = (decimal)dr["seguro"]; }
                                    if (dr["outros"] != DBNull.Value) { p.Outros = (decimal)dr["outros"]; }
                                    if (dr["frete"] != DBNull.Value) { p.Frete = (decimal)dr["frete"]; }
                                    if (dr["obs"] != DBNull.Value) { p.Obs = (string)dr["obs"]; }
                                    if (dr["baseimportacao"] != DBNull.Value) { p.Baseimportacao = (decimal)dr["baseimportacao"]; }
                                    if (dr["valoraduaneiro"] != DBNull.Value) { p.Valoraduaneiro = (decimal)dr["valoraduaneiro"]; }
                                    if (dr["valorimpostoimportacao"] != DBNull.Value) { p.Valorimpostoimportacao = (decimal)dr["valorimpostoimportacao"]; }
                                    if (dr["valoriof"] != DBNull.Value) { p.Valoriof = (decimal)dr["valoriof"]; }
                                    if (dr["atualizaestoque"] != DBNull.Value) { p.Atualizaestoque = (int)dr["atualizaestoque"]; }
                                    if (dr["tiponf"] != DBNull.Value) { p.Tiponf = (string)dr["tiponf"]; }
                                    if (dr["cancelada"] != DBNull.Value) { p.Cancelada = (int)dr["cancelada"]; }
                                    if (dr["empresa"] != DBNull.Value) { p.Empresa = (int)dr["empresa"]; }
                                    if (dr["comissao"] != DBNull.Value) { p.Comissao = (decimal)dr["comissao"]; }
                                    if (dr["peso"] != DBNull.Value) { p.Peso = (decimal)dr["peso"]; }
                                    if (dr["impostoaprox"] != DBNull.Value) { p.Impostoaprox = (decimal)dr["impostoaprox"]; }
                                    if (dr["unidade"] != DBNull.Value) { p.Unidade = (string)dr["unidade"]; }
                                    if (dr["idtanque"] != DBNull.Value) { p.Idtanque = (int)dr["idtanque"]; }
                                    if (dr["comb_encerrantef"] != DBNull.Value) { p.Comb_encerrantef = (decimal)dr["comb_encerrantef"]; }
                                    if (dr["valorcustomedio"] != DBNull.Value) { p.Valorcustomedio = (decimal)dr["valorcustomedio"]; }
                                    if (dr["idbico"] != DBNull.Value) { p.Idbico = (int)dr["idbico"]; }
                                    if (dr["comb_encerrantei"] != DBNull.Value) { p.Comb_encerrantei = (decimal)dr["comb_encerrantei"]; }
                                    if (dr["idordem"] != DBNull.Value) { p.Idordem = (int)dr["idordem"]; }
                                    if (dr["ncm"] != DBNull.Value) { p.Ncm = (string)dr["ncm"]; }
                                    if (dr["impestimado_nac"] != DBNull.Value) { p.Impestimado_nac = (decimal)dr["impestimado_nac"]; }
                                    if (dr["impestimado_imp"] != DBNull.Value) { p.Impestimado_imp = (decimal)dr["impestimado_imp"]; }
                                    if (dr["impestimado_est"] != DBNull.Value) { p.Impestimado_est = (decimal)dr["impestimado_est"]; }
                                    if (dr["impestimado_mun"] != DBNull.Value) { p.Impestimado_mun = (decimal)dr["impestimado_mun"]; }
                                    if (dr["entregador"] != DBNull.Value) { p.Entregador = (int)dr["entregador"]; }
                                    if (dr["requisitante"] != DBNull.Value) { p.Requisitante = (int)dr["requisitante"]; }
                                    if (dr["md5"] != DBNull.Value) { p.Md5 = (string)dr["md5"]; }
                                    if (dr["chaveexporta"] != DBNull.Value) { p.Chaveexporta = (string)dr["chaveexporta"]; }
                                    if (dr["obsitem"] != DBNull.Value) { p.Obsitem = (string)dr["obsitem"]; }
                                    if (dr["canceladoitem"] != DBNull.Value) { p.Canceladoitem = (int)dr["canceladoitem"]; }
                                    if (dr["piscofinssomado"] != DBNull.Value) { p.Piscofinssomado = (decimal)dr["piscofinssomado"]; }
                                    if (dr["cod_produto"] != DBNull.Value) { p.Cod_produto = (string)dr["cod_produto"]; }
                                    if (dr["datainclusao"] != DBNull.Value) { p.Datainclusao = (DateTime)dr["datainclusao"]; }
                                    if (dr["nre"] != DBNull.Value) { p.Nre = (string)dr["nre"]; }
                                    if (dr["cest"] != DBNull.Value) { p.Cest = (string)dr["cest"]; }
                                    if (dr["origem_cst"] != DBNull.Value) { p.Origem_cst = (string)dr["origem_cst"]; }
                                    if (dr["iditemcomanda"] != DBNull.Value) { p.Iditemcomanda = (int)dr["iditemcomanda"]; }
                                    if (dr["baseiss"] != DBNull.Value) { p.Baseiss = (decimal)dr["baseiss"]; }
                                    if (dr["valoriss"] != DBNull.Value) { p.Valoriss = (decimal)dr["valoriss"]; }
                                    if (dr["aliquotaiss"] != DBNull.Value) { p.Aliquotaiss = (decimal)dr["aliquotaiss"]; }
                                    if (dr["estoqueanterior"] != DBNull.Value) { p.Estoqueanterior = (decimal)dr["estoqueanterior"]; }
                                    if (dr["estoqueanteriornf"] != DBNull.Value) { p.Estoqueanteriornf = (decimal)dr["estoqueanteriornf"]; }
                                    if (dr["saldoanterior"] != DBNull.Value) { p.Saldoanterior = (decimal)dr["saldoanterior"]; }
                                    if (dr["saldoposterior"] != DBNull.Value) { p.Saldoposterior = (decimal)dr["saldoposterior"]; }
                                    if (dr["idnforigem"] != DBNull.Value) { p.Idnforigem = (int)dr["idnforigem"]; }
                                    if (dr["idnfprodutoorigem"] != DBNull.Value) { p.Idnfprodutoorigem = (int)dr["idnfprodutoorigem"]; }
                                    if (dr["unidadetributada"] != DBNull.Value) { p.Unidadetributada = (string)dr["unidadetributada"]; }
                                    if (dr["quantidadetributavel"] != DBNull.Value) { p.Quantidadetributavel = (decimal)dr["quantidadetributavel"]; }
                                    if (dr["valorunitariotributavel"] != DBNull.Value) { p.Valorunitariotributavel = (decimal)dr["valorunitariotributavel"]; }
                                    if (dr["aliquotaufdestino"] != DBNull.Value) { p.Aliquotaufdestino = (decimal)dr["aliquotaufdestino"]; }
                                    if (dr["aliquotafundoprobreza"] != DBNull.Value) { p.Aliquotafundoprobreza = (decimal)dr["aliquotafundoprobreza"]; }
                                    if (dr["baseufdestino"] != DBNull.Value) { p.Baseufdestino = (decimal)dr["baseufdestino"]; }
                                    if (dr["basefundopobreza"] != DBNull.Value) { p.Basefundopobreza = (decimal)dr["basefundopobreza"]; }
                                    if (dr["enquadramentoipi"] != DBNull.Value) { p.Enquadramentoipi = (string)dr["enquadramentoipi"]; }
                                    if (dr["bloq_estoque"] != DBNull.Value) { p.Bloq_estoque = (int)dr["bloq_estoque"]; }
                                    if (dr["bloq_desconto"] != DBNull.Value) { p.Bloq_desconto = (int)dr["bloq_desconto"]; }
                                    if (dr["bloq_validade"] != DBNull.Value) { p.Bloq_validade = (int)dr["bloq_validade"]; }
                                    if (dr["valorliquido"] != DBNull.Value) { p.Valorliquido = (decimal)dr["valorliquido"]; }
                                    if (dr["idnfprodutosweb"] != DBNull.Value) { p.Idnfprodutosweb = (int)dr["idnfprodutosweb"]; }
                                    if (dr["sincronizado"] != DBNull.Value) { p.Sincronizado = (int)dr["sincronizado"]; }

                                    //pdal.PostNfproduto(p);

                                    string json = "{ ";
                                    if (NfprodutoId != 0)
                                    {
                                        json = json + "\"id\" :\"" + NfprodutoId + "\", ";
                                    }
                                    json = json + "\"codigo\":\"" + p.Codigo + "\"," +
                                         "\"codigo\":\"" + p.Codigo + "\"," +
                                          "\"idnf\":\"" + p.Idnf + "\"," +
                                          "\"codigoproduto\":\"" + p.Codigoproduto + "\"," +
                                          "\"cfop\":\"" + p.Cfop + "\"," +
                                          "\"quantidade\":\"" + p.Quantidade + "\"," +
                                          "\"valorunitario\":\"" + p.Valorunitario + "\"," +
                                          "\"valortotal\":\"" + p.Valortotal + "\"," +
                                          "\"descontos\":\"" + p.Descontos + "\"," +
                                          "\"csticms\":\"" + p.Csticms + "\"," +
                                          "\"cstpis\":\"" + p.Cstpis + "\"," +
                                          "\"cstcofins\":\"" + p.Cstcofins + "\"," +
                                          "\"cstipi\":\"" + p.Cstipi + "\"," +
                                          "\"baseicms\":\"" + p.Baseicms + "\"," +
                                          "\"baseicmsst\":\"" + p.Baseicmsst + "\"," +
                                          "\"basepis\":\"" + p.Basepis + "\"," +
                                          "\"basecofins\":\"" + p.Basecofins + "\"," +
                                          "\"baseipi\":\"" + p.Baseipi + "\"," +
                                          "\"aliquotaicms\":\"" + p.Aliquotaicms + "\"," +
                                          "\"aliquotaicmsst\":\"" + p.Aliquotaicmsst + "\"," +
                                          "\"aliquotapis\":\"" + p.Aliquotapis + "\"," +
                                          "\"aliquotacofins\":\"" + p.Aliquotacofins + "\"," +
                                          "\"aliquotaipi\":\"" + p.Aliquotaipi + "\"," +
                                          "\"valoricms\":\"" + p.Valoricms + "\"," +
                                          "\"valoricmsst\":\"" + p.Valoricmsst + "\"," +
                                          "\"valorpis\":\"" + p.Valorpis + "\"," +
                                          "\"valorcofins\":\"" + p.Valorcofins + "\"," +
                                          "\"valoripi\":\"" + p.Valoripi + "\"," +
                                          "\"valorcusto\":\"" + p.Valorcusto + "\"," +
                                          "\"descricao\":\"" + p.Descricao + "\"," +
                                          "\"tipobcicms\":\"" + p.Tipobcicms + "\"," +
                                          "\"tipobcicmsst\":\"" + p.Tipobcicmsst + "\"," +
                                          "\"coringaicms\":\"" + p.Coringaicms + "\"," +
                                          "\"coringaicmsst\":\"" + p.Coringaicmsst + "\"," +
                                          "\"reducaoicms\":\"" + p.Reducaoicms + "\"," +
                                          "\"desconto_rateio\":\"" + p.Desconto_rateio + "\"," +
                                          "\"seguro\":\"" + p.Seguro + "\"," +
                                          "\"outros\":\"" + p.Outros + "\"," +
                                          "\"frete\":\"" + p.Frete + "\"," +
                                          "\"obs\":\"" + p.Obs + "\"," +
                                          "\"baseimportacao\":\"" + p.Baseimportacao + "\"," +
                                          "\"valoraduaneiro\":\"" + p.Valoraduaneiro + "\"," +
                                          "\"valorimpostoimportacao\":\"" + p.Valorimpostoimportacao + "\"," +
                                          "\"valoriof\":\"" + p.Valoriof + "\"," +
                                          "\"atualizaestoque\":\"" + p.Atualizaestoque + "\"," +
                                          "\"tiponf\":\"" + p.Tiponf + "\"," +
                                          "\"cancelada\":\"" + p.Cancelada + "\"," +
                                          "\"empresa\":\"" + p.Empresa + "\"," +
                                          "\"comissao\":\"" + p.Comissao + "\"," +
                                          "\"peso\":\"" + p.Peso + "\"," +
                                          "\"impostoaprox\":\"" + p.Impostoaprox + "\"," +
                                          "\"unidade\":\"" + p.Unidade + "\"," +
                                          "\"idtanque\":\"" + p.Idtanque + "\"," +
                                          "\"comb_encerrantef\":\"" + p.Comb_encerrantef + "\"," +
                                          "\"valorcustomedio\":\"" + p.Valorcustomedio + "\"," +
                                          "\"idbico\":\"" + p.Idbico + "\"," +
                                          "\"comb_encerrantei\":\"" + p.Comb_encerrantei + "\"," +
                                          "\"idordem\":\"" + p.Idordem + "\"," +
                                          "\"ncm\":\"" + p.Ncm + "\"," +
                                          "\"impestimado_nac\":\"" + p.Impestimado_nac + "\"," +
                                          "\"impestimado_imp\":\"" + p.Impestimado_imp + "\"," +
                                          "\"impestimado_est\":\"" + p.Impestimado_est + "\"," +
                                          "\"impestimado_mun\":\"" + p.Impestimado_mun + "\"," +
                                          "\"entregador\":\"" + p.Entregador + "\"," +
                                          "\"requisitante\":\"" + p.Requisitante + "\"," +
                                          "\"md5\":\"" + p.Md5 + "\"," +
                                          "\"chaveexporta\":\"" + p.Chaveexporta + "\"," +
                                          "\"obsitem\":\"" + p.Obsitem + "\"," +
                                          "\"canceladoitem\":\"" + p.Canceladoitem + "\"," +
                                          "\"piscofinssomado\":\"" + p.Piscofinssomado + "\"," +
                                          "\"cod_produto\":\"" + p.Cod_produto + "\"," +
                                          "\"datainclusao\":\"" + p.Datainclusao + "\"," +
                                          "\"nre\":\"" + p.Nre + "\"," +
                                          "\"cest\":\"" + p.Cest + "\"," +
                                          "\"origem_cst\":\"" + p.Origem_cst + "\"," +
                                          "\"iditemcomanda\":\"" + p.Iditemcomanda + "\"," +
                                          "\"baseiss\":\"" + p.Baseiss + "\"," +
                                          "\"valoriss\":\"" + p.Valoriss + "\"," +
                                          "\"aliquotaiss\":\"" + p.Aliquotaiss + "\"," +
                                          "\"estoqueanterior\":\"" + p.Estoqueanterior + "\"," +
                                          "\"estoqueanteriornf\":\"" + p.Estoqueanteriornf + "\"," +
                                          "\"saldoanterior\":\"" + p.Saldoanterior + "\"," +
                                          "\"saldoposterior\":\"" + p.Saldoposterior + "\"," +
                                          "\"idnforigem\":\"" + p.Idnforigem + "\"," +
                                          "\"idnfprodutoorigem\":\"" + p.Idnfprodutoorigem + "\"," +
                                          "\"unidadetributada\":\"" + p.Unidadetributada + "\"," +
                                          "\"quantidadetributavel\":\"" + p.Quantidadetributavel + "\"," +
                                          "\"valorunitariotributavel\":\"" + p.Valorunitariotributavel + "\"," +
                                          "\"aliquotaufdestino\":\"" + p.Aliquotaufdestino + "\"," +
                                          "\"aliquotafundoprobreza\":\"" + p.Aliquotafundoprobreza + "\"," +
                                          "\"baseufdestino\":\"" + p.Baseufdestino + "\"," +
                                          "\"basefundopobreza\":\"" + p.Basefundopobreza + "\"," +
                                          "\"enquadramentoipi\":\"" + p.Enquadramentoipi + "\"," +
                                          "\"bloq_estoque\":\"" + p.Bloq_estoque + "\"," +
                                          "\"bloq_desconto\":\"" + p.Bloq_desconto + "\"," +
                                          "\"bloq_validade\":\"" + p.Bloq_validade + "\"," +
                                          "\"valorliquido\":\"" + p.Valorliquido + "\"," +
                                          "\"idnfprodutosweb\":\"" + p.Idnfprodutosweb + "\"," +
                                          "\"sincronizado\":\"" + p.Sincronizado + "\"," +
                                          "\"document_id\":\"" + DocumentID + "\"," +
                                          "\"account_id\" :\"" + dr["Idweb"] + "\"}";

                                    json = json.Replace("\r\n", "");

                                    streamWriternf.Write(json);
                                    streamWriternf.Flush();

                                    var httpResponsenf = (HttpWebResponse)httpWebRequestnf.GetResponse();

                                    using (var streamReadernf = new StreamReader(httpResponsenf.GetResponseStream()))
                                    {
                                        var responseText = streamReadernf.ReadToEnd();

                                    }

                                    try
                                    {
                                        //string webAddrr = "http://localhost:3000/sell_products/" + Convert.ToString(dr["IDNFWEB"]) + "/" + Convert.ToString(dr["codigo"]) + ".json";

                                        string webAddrr = "http://apptechcoop.com.br/sell_products/" + Convert.ToString(dr["IDNFWEB"]) + "/" + Convert.ToString(dr["codigo"]) + ".json";

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
                                            NfprodutoId = usr["id"];

                                            conn.ExecuteQueries("UPDATE NF_PRODUTOS P SET P.IDNFPRODUTOSWEB = " + Convert.ToString(NfprodutoId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                    }

                                    conn.ExecuteQueries("UPDATE NF_PRODUTOS P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
                                    //conn.CloseConnection();
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
            }
            catch (WebException ex)
            {
                //conn.CloseConnection();
                NewIniFile.IniWriteString("STATUS", "MSG", ex.Message);
                //dr.NextResult();
            }
            conn.CloseConnection();
            NewIniFile.IniWriteString("STATUS", "MSG", "ATUALIZAÇÃO FINALIZADA");
        }

        public Nfproduto ObterPorId(long id)
        {
            var nfproduto = new Nfproduto();

            return nfproduto;
        }
    }
}