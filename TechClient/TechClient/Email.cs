using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Email
    {
        public int Id { get; set; }
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public string Html { get; set; }
        public DateTime Dataenvio { get; set; }
        public int Emlote { get; set; }
        public int Cco { get; set; }
    }
}
