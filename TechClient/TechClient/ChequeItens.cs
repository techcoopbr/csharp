using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ChequeItens
    {
        public int Id { get; set; }
        public int Idcheque { get; set; }
        public int Idconta { get; set; }
        public string Conta { get; set; }
        public string Historico { get; set; }
        public float Debito { get; set; }
        public float Credito { get; set; }
    }
}
