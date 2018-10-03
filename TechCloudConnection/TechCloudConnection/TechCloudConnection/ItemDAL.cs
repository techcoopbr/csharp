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
    public class ItemDAL
    {
        public void Gravar(Item item)
        {
            if (item.Codigo == 0)
                Inserir(item);
            else
                Atualizar(item);
        }

        private void Inserir(Item item)
        {

        }

        private void Atualizar(Item item)
        {

        }

        public void Remover(Item item)
        {

        }

        public void PostItem()
        {
            Connection_Query conn = new Connection_Query();
            conn.OpenConection();

            IniFile NewIniFile = new IniFile("techconfig");


            ItemDAL pdal = new ItemDAL();

            FbDataReader dr = conn.DataReader("select p.*, e.idweb, n.NCM as ncmstring from produtos p, empresa e inner join ncm n on p.NCM = n.CODIGO where p.sincronizado = 0");

            Item p = new Item();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int ProductId = 0;
                    //////////////////////////////////////////////
                    try
                    {
                        //string webAddrr = "http://localhost:3000/product_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                        string webAddrr = "http://apptechcoop.com.br/product_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                            //Console.WriteLine(responseText);
                            //MessageBox.Show(responseTextt);
                            var serializer = new JavaScriptSerializer();
                            dynamic usr = serializer.DeserializeObject(responseTextt);
                            ProductId = usr["id"];

                            conn.ExecuteQueries("UPDATE PRODUTOS P SET P.IDPRODUTOSWEB = " + Convert.ToString(ProductId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));
                            //MessageBox.Show(Convert.ToString(UserId));

                            //Now you have your response.
                            //or false depending on information in the response     
                        }
                    }
                    catch (Exception e)
                    {
                        ProductId = 0;
                    }

                    //////////////////////////////////////////////
                    try
                    {

                        string webAddr;

                        if (ProductId == 0)
                        {
                            //webAddr = "http://localhost:3000/products";
                            webAddr = "http://apptechcoop.com.br/products";
                        }
                        else
                        {
                            //webAddr = "http://localhost:3000/products/" + Convert.ToString(ProductId) + ".json";
                            webAddr = "http://apptechcoop.com.br/products/" + Convert.ToString(ProductId) + ".json";
                        }

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequest.ContentType = "application/json; charset=utf-8";

                        //determina de vai inserir ou atualizar
                        if (ProductId == 0)
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
                                if (ProductId != 0)
                                {
                                    p.Id = ProductId;
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: ATUALIZANDO ITEM...: " + (string)dr["descricao"].ToString().Trim());
                                }
                                else
                                {
                                    NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: INSERINDO ITEM...: " + (string)dr["descricao"].ToString().Trim());
                                }

                                p.Idweb = (int)Convert.ToInt32(dr["Idweb"]);
                                if (dr["codigoproduto"] != DBNull.Value) { p.Codigoproduto = dr["codigoproduto"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["codigo"] != DBNull.Value) { p.Codigo = (int)dr["codigo"]; }
                                if (dr["descricao"] != DBNull.Value) { p.Descricao = dr["descricao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["unidade"] != DBNull.Value) { p.Unidade = dr["unidade"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["customedio"] != DBNull.Value) { p.Customedio = (decimal)dr["customedio"]; }
                                if (dr["custovenda"] != DBNull.Value) { p.Custovenda = (decimal)dr["custovenda"]; }
                                if (dr["preco"] != DBNull.Value) { p.Preco = (decimal)dr["preco"]; }
                                if (dr["estoque"] != DBNull.Value) { p.Estoque = (decimal)dr["estoque"]; }
                                if (dr["ncm"] != DBNull.Value) { p.Ncm = (int)dr["ncm"]; }
                                if (dr["ncmstring"] != DBNull.Value) { p.Ncmstring = dr["ncmstring"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["tributacao"] != DBNull.Value) { p.Tributacao = (int)dr["tributacao"]; }
                                if (dr["codigobarras"] != DBNull.Value) { p.Codigobarras = dr["codigobarras"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["tipoproduto"] != DBNull.Value) { p.Tipoproduto = (dr["tipoproduto"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["lubrificante"] != DBNull.Value) { p.Lubrificante = (int)dr["lubrificante"]; }
                                if (dr["codigoanp"] != DBNull.Value) { p.Codigoanp = dr["codigoanp"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["codigogerencial"] != DBNull.Value) { p.Codigogerencial = dr["codigogerencial"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["empresa"] != DBNull.Value) { p.Empresa = (int)dr["empresa"]; }
                                if (dr["grupo"] != DBNull.Value) { p.Grupo = (int)dr["grupo"]; }
                                if (dr["subgrupo"] != DBNull.Value) { p.Subgrupo = (int)dr["subgrupo"]; }
                                if (dr["marca"] != DBNull.Value) { p.Marca = (int)dr["marca"]; }
                                if (dr["unsaida"] != DBNull.Value) { p.Unsaida = dr["unsaida"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["fatorconversao"] != DBNull.Value) { p.Fatorconversao = (decimal)dr["fatorconversao"]; }
                                if (dr["precoprazo"] != DBNull.Value) { p.Precoprazo = (decimal)dr["precoprazo"]; }
                                if (dr["precoatacado"] != DBNull.Value) { p.Precoatacado = (decimal)dr["precoatacado"]; }
                                if (dr["margemlucro"] != DBNull.Value) { p.Margemlucro = (decimal)dr["margemlucro"]; }
                                if (dr["produtosecundario"] != DBNull.Value) { p.Produtosecundario = (int)dr["produtosecundario"]; }
                                if (dr["fatorsecundario"] != DBNull.Value) { p.Fatorsecundario = (decimal)dr["fatorsecundario"]; }
                                if (dr["tipagem"] != DBNull.Value) { p.Tipagem = (dr["tipagem"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["comissao"] != DBNull.Value) { p.Comissao = (decimal)dr["comissao"]; }
                                if (dr["tributacaosuperior"] != DBNull.Value) { p.Tributacaosuperior = (int)dr["tributacaosuperior"]; }
                                if (dr["isentoipi"] != DBNull.Value) { p.Isentoipi = (int)dr["isentoipi"]; }
                                if (dr["peso"] != DBNull.Value) { p.Peso = (decimal)dr["peso"]; }
                                if (dr["item"] != DBNull.Value) { p.Tipoitem = (dr["item"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["estoquef"] != DBNull.Value) { p.Estoquef = (decimal)dr["estoquef"]; }
                                if (dr["estoqueg"] != DBNull.Value) { p.Estoqueg = (decimal)dr["estoqueg"]; }
                                if (dr["g_pontocompra"] != DBNull.Value) { p.G_pontocompra = (decimal)dr["g_pontocompra"]; }
                                if (dr["g_minimo"] != DBNull.Value) { p.G_minimo = (decimal)dr["g_minimo"]; }
                                if (dr["g_maximo"] != DBNull.Value) { p.G_maximo = (decimal)dr["g_maximo"]; }
                                if (dr["locacao"] != DBNull.Value) { p.Locacao = dr["locacao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["descricaoalongada"] != DBNull.Value) { p.Descricaoalongada = dr["descricaoalongada"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["aplicacao"] != DBNull.Value) { p.Aplicacao = dr["aplicacao"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["margem1pp"] != DBNull.Value) { p.Margem1pp = (decimal)dr["margem1pp"]; }
                                if (dr["margem2pp"] != DBNull.Value) { p.Margem2pp = (decimal)dr["margem2pp"]; }
                                if (dr["margem1pa"] != DBNull.Value) { p.Margem1pa = (decimal)dr["margem1pa"]; }
                                if (dr["margem2pa"] != DBNull.Value) { p.Margem2pa = (decimal)dr["margem2pa"]; }
                                if (dr["margemlucro2"] != DBNull.Value) { p.Margemlucro2 = (decimal)dr["margemlucro2"]; }
                                if (dr["ativo"] != DBNull.Value) { p.Ativo = (int)dr["ativo"]; }
                                if (dr["grade_pai"] != DBNull.Value) { p.Grade_pai = (int)dr["grade_pai"]; }
                                if (dr["grade_filho"] != DBNull.Value) { p.Grade_filho = (int)dr["grade_filho"]; }
                                if (dr["d_cor"] != DBNull.Value) { p.D_cor = dr["d_cor"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["d_tamanho"] != DBNull.Value) { p.D_tamanho = dr["d_tamanho"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["d_largura"] != DBNull.Value) { p.D_largura = dr["d_largura"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["d_altura"] != DBNull.Value) { p.D_altura = dr["d_altura"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["d_profundidade"] != DBNull.Value) { p.D_profundidade = dr["d_profundidade"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["d_capacidade"] != DBNull.Value) { p.D_capacidade = dr["d_capacidade"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["md5"] != DBNull.Value) { p.Md5 = dr["md5"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["enquadramentoipi"] != DBNull.Value) { p.Enquadramentoipi = (dr["enquadramentoipi"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["controlelote"] != DBNull.Value) { p.Controlelote = (int)dr["controlelote"]; }
                                if (dr["pacote"] != DBNull.Value) { p.Pacote = (int)dr["pacote"]; }
                                if (dr["data_atualizacao"] != DBNull.Value) { p.Data_atualizacao = (DateTime)dr["data_atualizacao"]; }
                                if (dr["iat"] != DBNull.Value) { p.Iat = (dr["iat"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["ippt"] != DBNull.Value) { p.Ippt = (dr["ippt"].ToString().Trim().Replace(@"""", @"\""")); }
                                if (dr["stecf"] != DBNull.Value) { p.Stecf = dr["stecf"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["cest"] != DBNull.Value) { p.Cest = dr["cest"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["descontomaximo"] != DBNull.Value) { p.Descontomaximo = (decimal)dr["descontomaximo"]; }
                                if (dr["codigobalanca"] != DBNull.Value) { p.Codigobalanca = dr["codigobalanca"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["permitemudarvalor"] != DBNull.Value) { p.Permitemudarvalor = (int)dr["permitemudarvalor"]; }
                                if (dr["idgrupoimpressao"] != DBNull.Value) { p.Idgrupoimpressao = (int)dr["idgrupoimpressao"]; }
                                if (dr["temobscomanda"] != DBNull.Value) { p.Temobscomanda = (int)dr["temobscomanda"]; }
                                if (dr["usaprecobalanca"] != DBNull.Value) { p.Usaprecobalanca = (int)dr["usaprecobalanca"]; }
                                if (dr["validadebalanca"] != DBNull.Value) { p.Validadebalanca = (int)dr["validadebalanca"]; }
                                if (dr["codigoservico"] != DBNull.Value) { p.Codigoservico = dr["codigoservico"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["produtocompermissao"] != DBNull.Value) { p.Produtocompermissao = (int)dr["produtocompermissao"]; }
                                if (dr["sincronizado"] != DBNull.Value) { p.Sincronizado = (int)dr["sincronizado"]; }
                                if (dr["estoquesecundario"] != DBNull.Value) { p.Estoquesecundario = (int)dr["estoquesecundario"]; }
                                if (dr["untributada"] != DBNull.Value) { p.Untributada = dr["untributada"].ToString().Trim().Replace(@"""", @"\"""); }
                                if (dr["fatoruntributada"] != DBNull.Value) { p.Fatoruntributada = (decimal)dr["fatoruntributada"]; }
                                if (dr["precopacote"] != DBNull.Value) { p.Precopacote = (decimal)dr["precopacote"]; }
                                if (dr["quantidadepacote"] != DBNull.Value) { p.Quantidadepacote = (decimal)dr["quantidadepacote"]; }

                                //pdal.PostItem(p);

                                string json = "{ ";
                                if (ProductId != 0)
                                {
                                    json = json + "\"id\" :\"" + ProductId + "\", ";
                                }
                                json = json + "\"codigoproduto\":\"" + p.Codigoproduto + "\"," +
                                    "\"codigo\":\"" + p.Codigo + "\"," +
                                    "\"descricao\":\"" + p.Descricao + "\"," +
                                    "\"unidade\":\"" + p.Unidade + "\"," +
                                    "\"customedio\":\"" + p.Customedio + "\"," +
                                    "\"custovenda\":\"" + p.Custovenda + "\"," +
                                    "\"preco\":\"" + p.Preco + "\"," +
                                    "\"estoque\":\"" + p.Estoque + "\"," +
                                    "\"ncm\":\"" + p.Ncm + "\"," +
                                    "\"tributacao\":\"" + p.Tributacao + "\"," +
                                    "\"codigobarras\":\"" + p.Codigobarras + "\"," +
                                    "\"tipoproduto\":\"" + p.Tipoproduto + "\"," +
                                    "\"lubrificante\":\"" + p.Lubrificante + "\"," +
                                    "\"codigoanp\":\"" + p.Codigoanp + "\"," +
                                    "\"codigogerencial\":\"" + p.Codigogerencial + "\"," +
                                    "\"empresa\":\"" + p.Empresa + "\"," +
                                    "\"grupo\":\"" + p.Grupo + "\"," +
                                    "\"subgrupo\":\"" + p.Subgrupo + "\"," +
                                    "\"marca\":\"" + p.Marca + "\"," +
                                    "\"unsaida\":\"" + p.Unsaida + "\"," +
                                    "\"fatorconversao\":\"" + p.Fatorconversao + "\"," +
                                    "\"precoprazo\":\"" + p.Precoprazo + "\"," +
                                    "\"precoatacado\":\"" + p.Precoatacado + "\"," +
                                    "\"margemlucro\":\"" + p.Margemlucro + "\"," +
                                    "\"produtosecundario\":\"" + p.Produtosecundario + "\"," +
                                    "\"fatorsecundario\":\"" + p.Fatorsecundario + "\"," +
                                    "\"tipagem\":\"" + p.Tipagem + "\"," +
                                    "\"comissao\":\"" + p.Comissao + "\"," +
                                    "\"tributacaosuperior\":\"" + p.Tributacaosuperior + "\"," +
                                    "\"isentoipi\":\"" + p.Isentoipi + "\"," +
                                    "\"peso\":\"" + p.Peso + "\"," +
                                    "\"item\":\"" + p.Tipoitem + "\"," +
                                    "\"estoquef\":\"" + p.Estoquef + "\"," +
                                    "\"estoqueg\":\"" + p.Estoqueg + "\"," +
                                    "\"g_pontocompra\":\"" + p.G_pontocompra + "\"," +
                                    "\"g_minimo\":\"" + p.G_minimo + "\"," +
                                    "\"g_maximo\":\"" + p.G_maximo + "\"," +
                                    "\"locacao\":\"" + p.Locacao + "\"," +
                                    "\"descricaoalongada\":\"" + p.Descricaoalongada + "\"," +
                                    "\"aplicacao\":\"" + p.Aplicacao + "\"," +
                                    "\"margem1pp\":\"" + p.Margem1pp + "\"," +
                                    "\"margem2pp\":\"" + p.Margem2pp + "\"," +
                                    "\"margem1pa\":\"" + p.Margem1pa + "\"," +
                                    "\"margem2pa\":\"" + p.Margem2pa + "\"," +
                                    "\"margemlucro2\":\"" + p.Margemlucro2 + "\"," +
                                    "\"ativo\":\"" + p.Ativo + "\"," +
                                    "\"grade_pai\":\"" + p.Grade_pai + "\"," +
                                    "\"grade_filho\":\"" + p.Grade_filho + "\"," +
                                    "\"d_cor\":\"" + p.D_cor + "\"," +
                                    "\"d_tamanho\":\"" + p.D_tamanho + "\"," +
                                    "\"d_largura\":\"" + p.D_largura + "\"," +
                                    "\"d_altura\":\"" + p.D_altura + "\"," +
                                    "\"d_profundidade\":\"" + p.D_profundidade + "\"," +
                                    "\"d_capacidade\":\"" + p.D_capacidade + "\"," +
                                    "\"md5\":\"" + p.Md5 + "\"," +
                                    "\"enquadramentoipi\":\"" + p.Enquadramentoipi + "\"," +
                                    "\"controlelote\":\"" + p.Controlelote + "\"," +
                                    "\"pacote\":\"" + p.Pacote + "\"," +
                                    "\"data_atualizacao\":\"" + p.Data_atualizacao + "\"," +
                                    "\"iat\":\"" + p.Iat + "\"," +
                                    "\"ippt\":\"" + p.Ippt + "\"," +
                                    "\"stecf\":\"" + p.Stecf + "\"," +
                                    "\"cest\":\"" + p.Cest + "\"," +
                                    "\"descontomaximo\":\"" + p.Descontomaximo + "\"," +
                                    "\"codigobalanca\":\"" + p.Codigobalanca + "\"," +
                                    "\"permitemudarvalor\":\"" + p.Permitemudarvalor + "\"," +
                                    "\"idgrupoimpressao\":\"" + p.Idgrupoimpressao + "\"," +
                                    "\"temobscomanda\":\"" + p.Temobscomanda + "\"," +
                                    "\"usaprecobalanca\":\"" + p.Usaprecobalanca + "\"," +
                                    "\"validadebalanca\":\"" + p.Validadebalanca + "\"," +
                                    "\"codigoservico\":\"" + p.Codigoservico + "\"," +
                                    "\"produtocompermissao\":\"" + p.Produtocompermissao + "\"," +
                                    "\"sincronizado\":\"" + p.Sincronizado + "\"," +
                                    "\"estoquesecundario\":\"" + p.Estoquesecundario + "\"," +
                                    "\"untributada\":\"" + p.Untributada + "\"," +
                                    "\"fatoruntributada\":\"" + p.Fatoruntributada + "\"," +
                                    "\"precopacote\":\"" + p.Precopacote + "\"," +
                                    "\"quantidadepacote\":\"" + p.Quantidadepacote + "\"," +
                                    "\"idprodutosweb\":\"" + p.Idprodutosweb + "\"," +
                                    "\"account_id\" :\"" + p.Idweb + "\"}";

                                json = json.Replace("\r\n", "");

                                streamWriter.Write(json);
                                streamWriter.Flush();

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responseText = streamReader.ReadToEnd();
                                }


                                //verifica novamente se existe
                                try
                                {
                                    //string webAddrr = "http://localhost:3000/product_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

                                    string webAddrr = "http://apptechcoop.com.br/product_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

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
                                        ProductId = usr["id"];

                                        conn.ExecuteQueries("UPDATE PRODUTOS P SET P.IDPRODUTOSWEB = " + Convert.ToString(ProductId) + " WHERE P.CODIGO = " + Convert.ToString((int)dr["codigo"]));
                                    }
                                }
                                catch
                                {

                                }

                                conn.ExecuteQueries("UPDATE PRODUTOS P SET P.SINCRONIZADO = 1 WHERE P.CODIGO = " + Convert.ToString(p.Codigo));
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
            else
            {
                NewIniFile.IniWriteString("STATUS", "MSG", "STATUS: NENHUM REGISTRO NOVO");
            }

            conn.CloseConnection();
            NewIniFile.IniWriteString("STATUS", "MSG", "ATUALIZAÇÃO DE PRODUTOS FINALIZADA");
        }

        public Item ObterPorId(long id)
        {
            var item = new Item();
            /*var command = new SqlCommand(
                "select disciplinaid, nome, cargahoraria from DISCIPLINAS where disciplinaid = @disciplinaid",
                this.connection);
            command.Parameters.AddWithValue("@disciplinaid", id);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    disciplina.DisciplinaId = reader.GetInt32(0);
                    disciplina.Nome = reader.GetString(1);
                    disciplina.CargaHoraria = reader.GetInt32(2);
                }
            }
            connection.Close();*/
            return item;
        }
    }
}