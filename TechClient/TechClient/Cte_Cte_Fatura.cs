using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Cte_Fatura
    {
        public int Codigo { get; set; }
        public int Idcte { get; set; }
        public int Parcela { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valor { get; set; }
        public int Empresa { get; set; }
        public int Idctectefaturaweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
