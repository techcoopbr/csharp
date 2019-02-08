using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ecf_DocumentosEmitidos
    {
        public int Idecf { get; set; }
        public int Coo { get; set; }
        public int Gnf { get; set; }
        public int Grg { get; set; }
        public int Cdc { get; set; }
        public string Denominacao { get; set; }
        public DateTime Datafinal { get; set; }
        public DateTime Horafinal { get; set; }
        public DateTime Datamovimento { get; set; }
        public string Meiopgto { get; set; }
        public float Valorpagamento { get; set; }
        public string Indicador { get; set; }
        public float Valorestorno { get; set; }
        public int Id { get; set; }
        public int Ccf { get; set; }
        public string Md5 { get; set; }
    }
}
