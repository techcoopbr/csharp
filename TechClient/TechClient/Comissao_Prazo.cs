using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Comissao_Prazo
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public int Idbaixa { get; set; }
        public int Idvendedor { get; set; }
        public float Basecomissao { get; set; }
        public float Percentual { get; set; }
        public float Comissao { get; set; }
        public DateTime Data { get; set; }
    }
}
