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
    public class PessoaDAL
    {
        public void Gravar(Pessoa pessoa)
        {
            if (pessoa.Codigo == 0)
                Inserir(pessoa);
            else
                Atualizar(pessoa);
        }

        private void Inserir(Pessoa pessoa)
        {

        }

        private void Atualizar(Pessoa pessoa)
        {

        }

        public void Remover(Pessoa pessoa)
        {

        }

        public void PostPessoa()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");


            PessoaDAL pdal = new PessoaDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb, c.IBGE from pessoas p, empresa e inner join cidade c on p.CIDADE = c.CODIGO where p.sincronizado = 0");

            Pessoa p = new Pessoa();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int FolkId = 0;
                    //////////////////////////////////////////////
                    try
                    {
                        //string webAddrr = "http://localhost:3000/folk_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://apptechcoop.com.br/folk_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            var serializer = new JavaScriptSerializer();
                            dynamic usr = serializer.DeserializeObject(responseTextt);
                            FolkId = usr["id"];

                            conn.ExecuteQueries("UPDATE PESSOAS P SET P.IDPESSOASWEB = " + Convert.ToString(FolkId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));
                        }
                    }
                    catch (Exception e)
                    {
                        FolkId = 0;
                    }

                    //////////////////////////////////////////////
                    try
                    {

                        string webAddr;

                        if (FolkId == 0)
                        {
                            //webAddr = "http://localhost:3000/folks";
                            webAddr = "http://apptechcoop.com.br/folks";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/folks/" + Convert.ToString(FolkId) + ".json";
                            webAddr = "http://apptechcoop.com.br/folks/" + Convert.ToString(FolkId) + ".json";
                        }

                        //verifica se vai inserir ou atualizar
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        if (FolkId == 0)
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
                                if (FolkId != 0)
                                {
                                    p.Id = FolkId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO CLIENTE...: " + dr["nome"].ToString().Trim());
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO CLIENTE...: " + dr["nome"].ToString().Trim());
                                }

                                if (dr["nome"] != DBNull.Value) { p.Nome = dr["nome"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["apelido"] != DBNull.Value) { p.Apelido = dr["apelido"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["endereco"] != DBNull.Value) { p.Endereco = dr["endereco"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["idweb"] != DBNull.Value) { p.Idweb = (int)dr["idweb"]; }
                                if (dr["codigo"] != DBNull.Value) { p.Codigo = (int)dr["codigo"]; }
                                if (dr["numerorua"] != DBNull.Value) { p.Numerorua = dr["numerorua"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["bairro"] != DBNull.Value) { p.Bairro = dr["bairro"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["complemento"] != DBNull.Value) { p.Complemento = dr["complemento"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["ibge"] != DBNull.Value) { p.Cidade = Convert.ToInt32(dr["ibge"]); }
                                if (dr["cep"] != DBNull.Value) { p.Cep = dr["cep"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["cxpostal"] != DBNull.Value) { p.Cxpostal = (int)dr["cxpostal"]; }
                                if (dr["fone01"] != DBNull.Value) { p.Fone01 = dr["fone01"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["fone02"] != DBNull.Value) { p.Fone02 = dr["fone02"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["fone03"] != DBNull.Value) { p.Fone03 = dr["fone03"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["fone04"] != DBNull.Value) { p.Fone04 = dr["fone04"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["contato"] != DBNull.Value) { p.Contato = dr["contato"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["celular"] != DBNull.Value) { p.Celular = dr["celular"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["cpfcnpj"] != DBNull.Value) { p.Cpfcnpj = dr["cpfcnpj"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["pessoafisicajuridica"] != DBNull.Value) { p.Pessoafisicajuridica = Convert.ToChar(dr["pessoafisicajuridica"].ToString().Trim().Replace(@"""", @"\""")); } else { p.Sexo = Convert.ToChar("F"); }
                                if (dr["rgie"] != DBNull.Value) { p.Rgie = dr["rgie"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["orgaoemissorrg"] != DBNull.Value) { p.Orgaoemissorrg = dr["orgaoemissorrg"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["email"] != DBNull.Value) { p.Email = dr["email"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["homepage"] != DBNull.Value) { p.Homepage = dr["homepage"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["msn"] != DBNull.Value) { p.Msn = dr["msn"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["nomepai"] != DBNull.Value) { p.Nomepai = dr["nomepai"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["nomemae"] != DBNull.Value) { p.Nomemae = dr["nomemae"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["conjuge"] != DBNull.Value) { p.Conjuge = dr["conjuge"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["referenciacomercial"] != DBNull.Value) { p.Referenciacomercial = dr["referenciacomercial"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["restricaomotivo"] != DBNull.Value) { p.Restricaomotivo = dr["restricaomotivo"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["dataemissaorrg"] != DBNull.Value) { p.Dataemissaorg = Convert.ToDateTime(dr["dataemissaorrg"]); }
                                if (dr["datanascimento"] != DBNull.Value) { p.Datanascimento = Convert.ToDateTime(dr["datanascimento"]); }
                                if (dr["estadocivil"] != DBNull.Value) { p.Estadocivil = Convert.ToChar(dr["estadocivil"].ToString().Trim().Replace(@"""", @"\""")); } else { p.Estadocivil = Convert.ToChar("S"); }
                                if (dr["sexo"] != DBNull.Value) { p.Sexo = Convert.ToChar(dr["sexo"]); } else { p.Sexo = Convert.ToChar("M"); }
                                if (dr["bloquearvenda"] != DBNull.Value) { p.Bloquearvenda = Convert.ToByte(dr["bloquearvenda"]); } else { p.Bloquearvenda = 1; }
                                if (dr["inscricaomunicipal"] != DBNull.Value) { p.Inscricaomunicipal = dr["inscricaomunicipal"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["contribuinteicms"] != DBNull.Value) { p.Contribuinteicms = Convert.ToByte(dr["contribuinteicms"]); } else { p.Contribuinteicms = 0; }
                                if (dr["contribuinteipi"] != DBNull.Value) { p.Contribuinteipi = Convert.ToByte(dr["contribuinteipi"]); } else { p.Contribuinteipi = 0; }
                                if (dr["suframa"] != DBNull.Value) { p.Suframa = (int)dr["suframa"]; }
                                if (dr["datahora_atualizacao"] != DBNull.Value) { p.Datahora_atualizacao = (DateTime)dr["datahora_atualizacao"]; }
                                if (dr["dataultcompra"] != DBNull.Value) { p.Dataultimacompra = (DateTime)dr["dataultcompra"]; }
                                if (dr["cliente"] != DBNull.Value) { p.Cliente = Convert.ToByte(dr["cliente"]); } else { p.Cliente = 1; }
                                if (dr["funcionario"] != DBNull.Value) { p.Funcionario = Convert.ToByte(dr["funcionario"]); } else { p.Funcionario = 0; }
                                if (dr["fornecedor"] != DBNull.Value) { p.Fornecedor = Convert.ToByte(dr["fornecedor"]); } else { p.Fornecedor = 0; }
                                if (dr["motorista"] != DBNull.Value) { p.Motorista = Convert.ToByte(dr["motorista"]); } else { p.Motorista = 0; }
                                if (dr["transportador"] != DBNull.Value) { p.Transportador = Convert.ToByte(dr["transportador"]); } else { p.Transportador = 0; }
                                if (dr["entregapecas"] != DBNull.Value) { p.Entregapecas = Convert.ToByte(dr["entregapecas"]); } else { p.Entregapecas = 0; }
                                if (dr["representante"] != DBNull.Value) { p.Representante = Convert.ToByte(dr["representante"]); } else { p.Representante = 0; }
                                if (dr["vendedor"] != DBNull.Value) { p.Vendedor = Convert.ToByte(dr["vendedor"]); } else { p.Vendedor = 0; }
                                if (dr["operadoracartao"] != DBNull.Value) { p.Operadoracartao = Convert.ToByte(dr["operadoracartao"]); } else { p.Operadoracartao = 0; }
                                if (dr["consumidorfinal"] != DBNull.Value) { p.Consumidorfinal = Convert.ToByte(dr["consumidorfinal"]); } else { p.Consumidorfinal = 0; }
                                if (dr["ativo"] != DBNull.Value) { p.Ativo = Convert.ToByte(dr["ativo"]); } else { p.Ativo = 1; }
                                if (dr["consultor"] != DBNull.Value) { p.Consultor = Convert.ToByte(dr["consultor"]); } else { p.Consultor = 1; }
                                if (dr["tributacao"] != DBNull.Value) { p.Tributacao = Convert.ToByte(dr["tributacao"]); } else { p.Tributacao = 0; }
                                if (dr["cte_rntrc"] != DBNull.Value) { p.Cte_rntrc = dr["cte_rntrc"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_cnpj"] != DBNull.Value) { p.Trab_cnpj = dr["trab_cnpj"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_ie"] != DBNull.Value) { p.Trab_ie = dr["trab_ie"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_razaosocial"] != DBNull.Value) { p.Trab_razaosocial = dr["trab_razaosocial"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_fantasia"] != DBNull.Value) { p.Trab_fantasia = dr["trab_fantasia"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_endereco"] != DBNull.Value) { p.Trab_endereco = dr["trab_endereco"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_numero"] != DBNull.Value) { p.Trab_numero = dr["trab_numero"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_bairro"] != DBNull.Value) { p.Trab_bairro = dr["trab_bairro"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["trab_cidadeuf"] != DBNull.Value) { p.Trab_cidadeuf = dr["trab_cidadeuf"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["classificacao"] != DBNull.Value) { p.Classificacao = Convert.ToByte(dr["classificacao"]); } else { p.Classificacao = 0; }
                                if (dr["atacado"] != DBNull.Value) { p.Atacado = Convert.ToByte(dr["atacado"]); } else { p.Atacado = 0; }
                                if (dr["iesustituto"] != DBNull.Value) { p.Iesustituto = dr["iesustituto"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["ididentifid"] != DBNull.Value) { p.Ididentifid = dr["ididentifid"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["comissao"] != DBNull.Value) { p.Comissao = (decimal)dr["comissao"]; }
                                if (dr["comissaoavista"] != DBNull.Value) { p.Comissaoavista = (decimal)dr["comissaoavista"]; }
                                if (dr["comissaoaprazo"] != DBNull.Value) { p.Comissaoaprazo = (decimal)dr["comissaoaprazo"]; }
                                if (dr["limitecredito"] != DBNull.Value) { p.Limitecredito = (decimal)dr["limitecredito"]; }
                                if (dr["bloqueialimite"] != DBNull.Value) { p.Bloqueialimite = Convert.ToByte(dr["bloqueialimite"]); } else { p.Bloqueialimite = 1; }
                                if (dr["bloqueiadebito"] != DBNull.Value) { p.Bloqueiadebito = Convert.ToByte(dr["bloqueiadebito"]); } else { p.Bloqueiadebito = 1; }
                                if (dr["msglimite"] != DBNull.Value) { p.Msglimite = Convert.ToByte(dr["msglimite"]); } else { p.Msglimite = 0; }
                                if (dr["msgdebito"] != DBNull.Value) { p.Msgdebito = Convert.ToByte(dr["msgdebito"]); } else { p.Msgdebito = 0; }
                                if (dr["operacaopadrao"] != DBNull.Value) { p.Operacaopadrao = Convert.ToByte(dr["operacaopadrao"]); }
                                if (dr["origemrg"] != DBNull.Value) { p.Origemrg = dr["origemrg"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["opservico"] != DBNull.Value) { p.Opservico = (byte)Convert.ToByte(dr["opservico"]); } else { p.Opservico = 0; }
                                if (dr["totalhorasmensais"] != DBNull.Value) { p.Totalhorasmensais = (byte)Convert.ToByte(dr["totalhorasmensais"]); } else { p.Totalhorasmensais = 0; }
                                if (dr["hora1"] != DBNull.Value) { p.Hora1 = (DateTime)dr["hora1"]; }
                                if (dr["hora2"] != DBNull.Value) { p.Hora2 = (DateTime)dr["hora2"]; }
                                if (dr["hora3"] != DBNull.Value) { p.Hora3 = (DateTime)dr["hora3"]; }
                                if (dr["hora4"] != DBNull.Value) { p.Hora4 = (DateTime)dr["hora4"]; }
                                if (dr["hora5"] != DBNull.Value) { p.Hora5 = (DateTime)dr["hora5"]; }
                                if (dr["hora6"] != DBNull.Value) { p.Hora6 = (DateTime)dr["hora6"]; }
                                if (dr["hora7"] != DBNull.Value) { p.Hora7 = (DateTime)dr["hora7"]; }
                                if (dr["hora8"] != DBNull.Value) { p.Hora8 = (DateTime)dr["hora8"]; }
                                if (dr["hora9"] != DBNull.Value) { p.Hora9 = (DateTime)dr["hora9"]; }
                                if (dr["hora10"] != DBNull.Value) { p.Hora10 = (DateTime)dr["hora10"]; }
                                if (dr["hora11"] != DBNull.Value) { p.Hora11 = (DateTime)dr["hora11"]; }
                                if (dr["hora12"] != DBNull.Value) { p.Hora12 = (DateTime)dr["hora12"]; }
                                if (dr["hora13"] != DBNull.Value) { p.Hora13 = (DateTime)dr["hora13"]; }
                                if (dr["hora14"] != DBNull.Value) { p.Hora14 = (DateTime)dr["hora14"]; }
                                if (dr["hora15"] != DBNull.Value) { p.Hora15 = (DateTime)dr["hora15"]; }
                                if (dr["hora16"] != DBNull.Value) { p.Hora16 = (DateTime)dr["hora16"]; }
                                if (dr["hora17"] != DBNull.Value) { p.Hora17 = (DateTime)dr["hora17"]; }
                                if (dr["hora18"] != DBNull.Value) { p.Hora18 = (DateTime)dr["hora18"]; }
                                if (dr["hora19"] != DBNull.Value) { p.Hora19 = (DateTime)dr["hora19"]; }
                                if (dr["hora20"] != DBNull.Value) { p.Hora20 = (DateTime)dr["hora20"]; }
                                if (dr["hora21"] != DBNull.Value) { p.Hora21 = (DateTime)dr["hora21"]; }
                                if (dr["hora22"] != DBNull.Value) { p.Hora22 = (DateTime)dr["hora22"]; }
                                if (dr["hora23"] != DBNull.Value) { p.Hora23 = (DateTime)dr["hora23"]; }
                                if (dr["hora24"] != DBNull.Value) { p.Hora24 = (DateTime)dr["hora24"]; }
                                if (dr["hora25"] != DBNull.Value) { p.Hora25 = (DateTime)dr["hora25"]; }
                                if (dr["hora26"] != DBNull.Value) { p.Hora26 = (DateTime)dr["hora26"]; }
                                if (dr["hora27"] != DBNull.Value) { p.Hora27 = (DateTime)dr["hora27"]; }
                                if (dr["hora28"] != DBNull.Value) { p.Hora28 = (DateTime)dr["hora28"]; }
                                if (dr["parcelamaxima"] != DBNull.Value) { p.Parcelamaxima = (decimal)dr["parcelamaxima"]; }
                                if (dr["desc_avista"] != DBNull.Value) { p.Desc_avista = (decimal)dr["desc_avista"]; }
                                if (dr["desc_aprazo"] != DBNull.Value) { p.Desc_aprazo = (decimal)dr["desc_aprazo"]; }
                                if (dr["desc_adebito"] != DBNull.Value) { p.Desc_adebito = (decimal)dr["desc_adebito"]; }
                                if (dr["desc_acredito"] != DBNull.Value) { p.Desc_acredito = (decimal)dr["desc_acredito"]; }
                                if (dr["desc_acc"] != DBNull.Value) { p.Desc_acc = (decimal)dr["desc_acc"]; }
                                if (dr["usabarras"] != DBNull.Value) { p.Usabarras = Convert.ToByte(dr["usabarras"]); } else { p.Usabarras = 0; }
                                if (dr["regimetributario"] != DBNull.Value) { p.Regimetributario = Convert.ToByte(dr["regimetributario"]); }
                                if (dr["bloqueiadebitosenha"] != DBNull.Value) { p.Bloqueiadebitosenha = Convert.ToByte(dr["bloqueiadebitosenha"]); } else { p.Bloqueiadebitosenha = 1; }
                                if (dr["bloqueialimitesenha"] != DBNull.Value) { p.Bloqueialimitesenha = Convert.ToByte(dr["bloqueialimitesenha"]); } else { p.Bloqueialimitesenha = 1; }
                                if (dr["diasaposvencido"] != DBNull.Value) { p.Diasaposvencido = Convert.ToByte(dr["diasaposvencido"]); }
                                if (dr["idvendedor"] != DBNull.Value) { p.Idvendedor = Convert.ToByte(dr["idvendedor"]); }
                                if (dr["pagacomissao"] != DBNull.Value) { p.Pagacomissao = Convert.ToByte(dr["pagacomissao"]); } else { p.Pagacomissao = 1; }
                                if (dr["documento"] != DBNull.Value) { p.Documento = Convert.ToInt32(dr["documento"]); }
                                if (dr["mostraimpostoibpt"] != DBNull.Value) { p.Mostraimpostoibpt = Convert.ToByte(dr["mostraimpostoibpt"]); } else { p.Mostraimpostoibpt = 1; }
                                if (dr["conj_localtrab"] != DBNull.Value) { p.Conj_localtrab = dr["conj_localtrab"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["conj_profissao"] != DBNull.Value) { p.Conj_profissao = dr["conj_profissao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["conj_cpf"] != DBNull.Value) { p.Conj_cpf = dr["conj_cpf"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["conj_rg"] != DBNull.Value) { p.Conj_rg = dr["conj_rg"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["conj_tempotrabalho"] != DBNull.Value) { p.Conj_tempotrabalho = dr["conj_tempotrabalho"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["profissao"] != DBNull.Value) { p.Profissao = dr["profissao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["tempotrabalho"] != DBNull.Value) { p.Tempotrabalho = dr["tempotrabalho"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["sincronizado"] != DBNull.Value) { p.Sincronizado = Convert.ToByte(dr["sincronizado"]); }
                                if (dr["imprimenota"] != DBNull.Value) { p.Imprimenota = Convert.ToByte(dr["imprimenota"]); } else { p.Imprimenota = 0; }
                                if (dr["valorservico"] != DBNull.Value) { p.Valorservico = (decimal)dr["valorservico"]; }
                                if (dr["comissaototalnavenda"] != DBNull.Value) { p.Comissaototalnavenda = Convert.ToByte(dr["comissaototalnavenda"]); } else { p.Comissaototalnavenda = 0; }
                                if (dr["ultimacompra"] != DBNull.Value) { p.Ultimacompra = (DateTime)dr["ultimacompra"]; }
                                if (dr["valorultimacompra"] != DBNull.Value) { p.Valorultimacompra = (decimal)dr["valorultimacompra"]; }
                                if (dr["salario"] != DBNull.Value) { p.Salario = (decimal)dr["salario"]; }
                                if (dr["conj_salario"] != DBNull.Value) { p.Conj_salario = (decimal)dr["conj_salario"]; }
                                if (dr["conj_nascimento"] != DBNull.Value) { p.Conj_nascimento = (DateTime)dr["conj_nascimento"]; }
                                if (dr["idlistapreco"] != DBNull.Value) { p.Idlistapreco = (byte)Convert.ToByte(dr["idlistapreco"]); } else { p.Idlistapreco = 0; }


                                //pdal.PostPessoa(p);

                                string json = "{ ";
                                if (FolkId != 0)
                                {
                                    json = json + "\"id\" :\"" + FolkId + "\", ";
                                }
                                json = json + "\"nome\" :\"" + p.Nome + "\", " +
                                    "\"apelido\" :\"" + p.Apelido + "\", " +
                                    "\"endereco\" :\"" + p.Endereco + "\", " +
                                    "\"codigo\" :\"" + p.Codigo + "\", " +
                                    "\"numerorua\" :\"" + p.Numerorua + "\", " +
                                    "\"bairro\" :\"" + p.Bairro + "\", " +
                                    "\"cxpostal\" :\"" + p.Cxpostal + "\", " +
                                    "\"complemento\" :\"" + p.Complemento + "\", " +
                                    "\"cidade\" :\"" + p.Cidade + "\", " +
                                    "\"cep\" :\"" + p.Cep + "\", " +
                                    "\"fone01\" :\"" + p.Fone01 + "\", " +
                                    "\"fone02\" :\"" + p.Fone02 + "\", " +
                                    "\"fone03\" :\"" + p.Fone03 + "\", " +
                                    "\"fone04\" :\"" + p.Fone04 + "\", " +
                                    "\"contato\" :\"" + p.Contato + "\", " +
                                    "\"celular\" :\"" + p.Celular + "\", " +
                                    "\"p.fisicajuridica\" :\"" + p.Pessoafisicajuridica + "\", " +
                                    "\"cpfcnpj\" :\"" + p.Cpfcnpj + "\", " +
                                    "\"rgie\" :\"" + p.Rgie + "\", " +
                                    "\"orgaoemissorrg\" :\"" + p.Orgaoemissorrg + "\", " +
                                    "\"dataemissaorg\" :\"" + p.Dataemissaorg + "\", " +
                                    "\"datanascimento\" :\"" + p.Datanascimento + "\", " +
                                    "\"email\" :\"" + p.Email + "\", " +
                                    "\"homepage\" :\"" + p.Homepage + "\", " +
                                    "\"msn\" :\"" + p.Msn + "\", " +
                                    "\"nomepai\" :\"" + p.Nomepai + "\", " +
                                    "\"nomemae\" :\"" + p.Nomemae + "\", " +
                                    "\"conjuge\" :\"" + p.Conjuge + "\", " +
                                    "\"estadocivil\" :\"" + p.Estadocivil + "\", " +
                                    "\"sexo\" :\"" + p.Sexo + "\", " +
                                    "\"referenciacomercial\" :\"" + p.Referenciacomercial + "\", " +
                                    "\"restricaomotivo\" :\"" + p.Restricaomotivo + "\", " +
                                    "\"bloquearvenda\" :\"" + p.Bloquearvenda + "\", " +
                                    "\"inscricaomunicipal\" :\"" + p.Inscricaomunicipal + "\", " +
                                    "\"contribuinteicms\" :\"" + p.Contribuinteicms + "\", " +
                                    "\"contribuinteipi\" :\"" + p.Contribuinteipi + "\", " +
                                    "\"suframa\" :\"" + p.Suframa + "\", " +
                                    "\"datahora_atualizacao\" :\"" + p.Datahora_atualizacao + "\", " +
                                    "\"dataultimacompra\" :\"" + p.Dataultimacompra + "\", " +
                                    "\"cliente\" :\"" + p.Cliente + "\", " +
                                    "\"funcionario\" :\"" + p.Funcionario + "\", " +
                                    "\"fornecedor\" :\"" + p.Fornecedor + "\", " +
                                    "\"motorista\" :\"" + p.Motorista + "\", " +
                                    "\"transportador\" :\"" + p.Transportador + "\", " +
                                    "\"consultor\" :\"" + p.Consultor + "\", " +
                                    "\"entregapecas\" :\"" + p.Entregapecas + "\", " +
                                    "\"representante\" :\"" + p.Representante + "\", " +
                                    "\"vendedor\" :\"" + p.Vendedor + "\", " +
                                    "\"operadoracartao\" :\"" + p.Operadoracartao + "\", " +
                                    "\"consumidorfinal\" :\"" + p.Consumidorfinal + "\", " +
                                    "\"ativo\" :\"" + p.Ativo + "\", " +
                                    "\"tributacao\" :\"" + p.Tributacao + "\", " +
                                    "\"cte_rntrc\" :\"" + p.Cte_rntrc + "\", " +
                                    "\"trab_cnpj\" :\"" + p.Trab_cnpj + "\", " +
                                    "\"trab_ie\" :\"" + p.Trab_ie + "\", " +
                                    "\"trab_razaosocial\" :\"" + p.Trab_razaosocial + "\", " +
                                    "\"trab_fantasia\" :\"" + p.Trab_fantasia + "\", " +
                                    "\"trab_endereco\" :\"" + p.Trab_endereco + "\", " +
                                    "\"trab_numero\" :\"" + p.Trab_numero + "\", " +
                                    "\"trab_bairro\" :\"" + p.Trab_bairro + "\", " +
                                    "\"trab_cidadeuf\" :\"" + p.Trab_cidadeuf + "\", " +
                                    "\"classificacao\" :\"" + p.Classificacao + "\", " +
                                    "\"atacado\" :\"" + p.Atacado + "\", " +
                                    "\"iesustituto\" :\"" + p.Iesustituto + "\", " +
                                    "\"comissao\" :\"" + p.Comissao + "\", " +
                                    "\"comissaoavista\" :\"" + p.Comissaoavista + "\", " +
                                    "\"comissaoaprazo\" :\"" + p.Comissaoaprazo + "\", " +
                                    "\"ididentifid\" :\"" + p.Ididentifid + "\", " +
                                    "\"limitecredito\" :\"" + p.Limitecredito + "\", " +
                                    "\"bloqueialimite\" :\"" + p.Bloqueialimite + "\", " +
                                    "\"bloqueiadebito\" :\"" + p.Bloqueiadebito + "\", " +
                                    "\"msglimite\" :\"" + p.Msglimite + "\", " +
                                    "\"msgdebito\" :\"" + p.Msgdebito + "\", " +
                                    "\"operacaopadrao\" :\"" + p.Operacaopadrao + "\", " +
                                    "\"origemrg\" :\"" + p.Origemrg + "\", " +
                                    "\"opservico\" :\"" + p.Opservico + "\", " +
                                    "\"totalhorasmensais\" :\"" + p.Totalhorasmensais + "\", " +
                                    "\"hora1\" :\"" + p.Hora1 + "\", " +
                                    "\"hora2\" :\"" + p.Hora2 + "\", " +
                                    "\"hora3\" :\"" + p.Hora3 + "\", " +
                                    "\"hora4\" :\"" + p.Hora4 + "\", " +
                                    "\"hora5\" :\"" + p.Hora5 + "\", " +
                                    "\"hora6\" :\"" + p.Hora6 + "\", " +
                                    "\"hora7\" :\"" + p.Hora7 + "\", " +
                                    "\"hora8\" :\"" + p.Hora8 + "\", " +
                                    "\"hora9\" :\"" + p.Hora9 + "\", " +
                                    "\"hora10\" :\"" + p.Hora10 + "\", " +
                                    "\"hora11\" :\"" + p.Hora11 + "\", " +
                                    "\"hora12\" :\"" + p.Hora12 + "\", " +
                                    "\"hora13\" :\"" + p.Hora13 + "\", " +
                                    "\"hora14\" :\"" + p.Hora14 + "\", " +
                                    "\"hora15\" :\"" + p.Hora15 + "\", " +
                                    "\"hora16\" :\"" + p.Hora16 + "\", " +
                                    "\"hora17\" :\"" + p.Hora17 + "\", " +
                                    "\"hora18\" :\"" + p.Hora18 + "\", " +
                                    "\"hora19\" :\"" + p.Hora19 + "\", " +
                                    "\"hora20\" :\"" + p.Hora20 + "\", " +
                                    "\"hora21\" :\"" + p.Hora21 + "\", " +
                                    "\"hora22\" :\"" + p.Hora22 + "\", " +
                                    "\"hora23\" :\"" + p.Hora23 + "\", " +
                                    "\"hora24\" :\"" + p.Hora24 + "\", " +
                                    "\"hora25\" :\"" + p.Hora25 + "\", " +
                                    "\"hora26\" :\"" + p.Hora26 + "\", " +
                                    "\"hora27\" :\"" + p.Hora27 + "\", " +
                                    "\"hora28\" :\"" + p.Hora28 + "\", " +
                                    "\"parcelamaxima\" :\"" + p.Parcelamaxima + "\", " +
                                    "\"desc_avista\" :\"" + p.Desc_avista + "\", " +
                                    "\"desc_aprazo\" :\"" + p.Desc_aprazo + "\", " +
                                    "\"desc_adebito\" :\"" + p.Desc_adebito + "\", " +
                                    "\"desc_acredito\" :\"" + p.Desc_acredito + "\", " +
                                    "\"desc_acc\" :\"" + p.Desc_acc + "\", " +
                                    "\"usabarras\" :\"" + p.Usabarras + "\", " +
                                    "\"regimetributario\" :\"" + p.Regimetributario + "\", " +
                                    "\"imprimenota\" :\"" + p.Imprimenota + "\", " +
                                    "\"bloqueiadebitosenha\" :\"" + p.Bloqueiadebitosenha + "\", " +
                                    "\"bloqueialimitesenha\" :\"" + p.Bloqueialimitesenha + "\", " +
                                    "\"diasaposvencido\" :\"" + p.Diasaposvencido + "\", " +
                                    "\"valorservico\" :\"" + p.Valorservico + "\", " +
                                    "\"idvendedor\" :\"" + p.Idvendedor + "\", " +
                                    "\"pagacomissao\" :\"" + p.Pagacomissao + "\", " +
                                    "\"comissaototalnavenda\" :\"" + p.Comissaototalnavenda + "\", " +
                                    "\"ultimacompra\" :\"" + p.Ultimacompra + "\", " +
                                    "\"valorultimacompra\" :\"" + p.Valorultimacompra + "\", " +
                                    "\"documento\" :\"" + p.Documento + "\", " +
                                    "\"mostraimpostoibpt\" :\"" + p.Mostraimpostoibpt + "\", " +
                                    "\"salario\" :\"" + p.Salario + "\", " +
                                    "\"conj_localtrab\" :\"" + p.Conj_localtrab + "\", " +
                                    "\"conj_profissao\" :\"" + p.Conj_profissao + "\", " +
                                    "\"conj_salario\" :\"" + p.Conj_salario + "\", " +
                                    "\"conj_cpf\" :\"" + p.Conj_cpf + "\", " +
                                    "\"conj_rg\" :\"" + p.Conj_rg + "\", " +
                                    "\"conj_nascimento\" :\"" + p.Conj_nascimento + "\", " +
                                    "\"conj_tempotrabalho\" :\"" + p.Conj_tempotrabalho + "\", " +
                                    "\"profissao\" :\"" + p.Profissao + "\", " +
                                    "\"tempotrabalho\" :\"" + p.Tempotrabalho + "\", " +
                                    "\"idlistapreco\" :\"" + p.Idlistapreco + "\", " +
                                    "\"sincronizado\" :\"" + p.Sincronizado + "\", " +
                                    "\"account_id\" :\"" + p.Idweb + "\"}";

                                json = json.Replace("\r\n", "");

                                streamWriter.Write(json);
                                streamWriter.Flush();

                                //MessageBox.Show(json);

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();
                                }

                                //pesquisa após inserir
                                try
                                {
                                    //string webAddrr = "http://localhost:3000/folk_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/folk_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                                        var serializer = new JavaScriptSerializer();
                                        dynamic usr = serializer.DeserializeObject(responseTextt);
                                        FolkId = usr["id"];

                                        conn.ExecuteQueries("UPDATE PESSOAS P SET P.IDPESSOASWEB = " + Convert.ToString(FolkId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));
                                    }
                                }
                                catch (Exception e)
                                {
                                    FolkId = 0;
                                }

                                conn.ExecuteQueries("UPDATE PESSOAS P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
                                //conn.CloseConnection();
                            }
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        NewIniFile.IniWriteString("STATUS", "MSG", e.Message);
                        dr.NextResult();
                    }
                }
            }

            conn.CloseConnection();
            NewIniFile.IniWriteString("STATUS", "MSG", "CADASTRO DE PESSOAS ATUALIZADO");
        }

        public Pessoa ObterPorId(long id)
        {
            var pessoa = new Pessoa();

            return pessoa;
        }
    }
}