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
    public class Login
    {
        //HttpWebRequest httpWebRequest = new HttpWebRequest();

        //public static HttpWebRequest httpWebRequest { get => httpWebRequest; set => httpWebRequest = value; }
        //public static HttpWebRequest httpWebRequestt { get => httpWebRequestt; set => httpWebRequestt = value }

        public static String encoded = null;

        public static void login()
        {
            String username = "admin@jefferson.com";
            String password = "2311luje2311";
            encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(username + ":" + password));
        }

        /*public static int json(FbDataReader dr, Connection_Query conn, int DocumentId, string url)
        {
            //string webAddrr = "http://localhost:3000/caixa_movimento_accounts/" + Convert.ToString(dr["codigo"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";

            string webAddrr = url + Convert.ToString(dr["id"]) + "/" + Convert.ToString(dr["Idweb"]) + ".json";
            
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
        }*/
    }
}
