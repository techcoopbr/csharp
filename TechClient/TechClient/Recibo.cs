using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Recibo
    {
        public int Id { get; set; }
        public int Idpessoa { get; set; }
        public DateTime Datalancamento { get; set; }
        public float Valorrecebido { get; set; }
        public float Valorsaldo { get; set; }
        public float Valordesconto { get; set; }
        public float Valorjuros { get; set; }
        public float Valortroco { get; set; }
    }
}
