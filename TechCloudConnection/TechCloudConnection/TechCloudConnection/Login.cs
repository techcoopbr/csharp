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

namespace TechCloudConnection
{
    class Login
    {
        public static void login(HttpWebRequest httpWebRequest)
        {
            String usernamee = "admin@jefferson.com";
            String passwordd = "2311luje2311";
            String encodedd = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(usernamee + ":" + passwordd));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encodedd);
        }

        public static int json(FbDataReader dr, Connection_Query conn, int DocumentId, string url)
        {
            //string webAddrr = "http://localhost:3000/caixa_movimento_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

            string webAddrr = url + Convert.ToString(dr["id"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

            var httpWebRequestt = (HttpWebRequest)WebRequest.Create(webAddrr);
            httpWebRequestt.ContentType = "application/json; charset=utf-8";
            httpWebRequestt.Method = "GET";

            Login.login(httpWebRequestt);

            var httpResponsee = (HttpWebResponse)httpWebRequestt.GetResponse();

            using (var streamReaderr = new StreamReader(httpResponsee.GetResponseStream()))
            {
                var responseTextt = streamReaderr.ReadToEnd();
                var serializerr = new JavaScriptSerializer();
                dynamic usr = serializerr.DeserializeObject(responseTextt);
                DocumentId = usr["id"];

                conn.ExecuteQueries("UPDATE CAIXAMOVIMENTO P SET P.IDCAIXAMOVIMENTOWEB = " + Convert.ToString(DocumentId) + " WHERE P.ID = " + Convert.ToString((int)dr["id"]));

            }

            return DocumentId;
        }
    }
}
