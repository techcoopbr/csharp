using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Comissao
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public float Basecomissao { get; set; }
        public float Vendaavista { get; set; }
        public float Vendaaprazo { get; set; }
        public int Idvendedor { get; set; }
        public int Parcelas { get; set; }
        public float Percavista { get; set; }
        public float Percaprazo { get; set; }
    }
}
