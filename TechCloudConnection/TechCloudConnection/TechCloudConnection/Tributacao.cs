using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Tributacao
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cst { get; set; }
        public decimal Aliquotaref { get; set; }
        public string Aliquotaecf { get; set; }
        public string Origem { get; set; }
        public int Idweb { get; set; }
    }
}
