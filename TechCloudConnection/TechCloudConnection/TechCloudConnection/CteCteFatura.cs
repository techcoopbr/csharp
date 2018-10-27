using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteCteFatura
    {
        public int Codigo { get; set; }
        public int Idcte { get; set; }
        public int Parcela { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public int Empresa { get; set; }
        public int Idweb { get; set; }
    }
}
