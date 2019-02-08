using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Log
    {
        public int Id { get; set; }
        public string Tabela { get; set; }
        public string Valorantigo { get; set; }
        public string Valornovo { get; set; }
        public int Idusuario { get; set; }
        public int Idregistro { get; set; }
        public string Iporigem { get; set; }
        public string Campo { get; set; }
        public string Acao { get; set; }
        public DateTime Dataevento { get; set; }
    }
}
