using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ajuste
    {
        public int Id { get; set; }
        public DateTime Datainicial { get; set; }
        public DateTime Datafinal { get; set; }
        public int Idpessoa { get; set; }
        public DateTime Hora { get; set; }
        public int Empresa { get; set; }
        public string Md5 { get; set; }
    }
}
