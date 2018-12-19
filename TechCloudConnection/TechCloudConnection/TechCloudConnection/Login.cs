using System;

namespace Modelo
{
    public class Login
    {
        
        public static String encoded = null;

        public static void login()
        {
            String username = "admin@jefferson.com";
            String password = "2311luje2311";
            encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("utf-8").GetBytes(username + ":" + password));
        }
    }
}
