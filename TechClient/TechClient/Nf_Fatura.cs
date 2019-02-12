using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Fatura
    {
        public int Codigo { get; set; }
        public int Idnf { get; set; }
        public int Parcela { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valor { get; set; }
        public int Empresa { get; set; }
        public float Comissao { get; set; }
        public float Basecomissao { get; set; }
        public string Md5 { get; set; }
        public int Idplanovenda { get; set; }
    }
}
