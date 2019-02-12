using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Fatura_Dav
    {
        public int Codigo { get; set; }
        public int Idnf { get; set; }
        public int Parcela { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valor { get; set; }
        public int Idplanovenda { get; set; }
    }
}
