using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Devolucao
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public int Idnfproduto { get; set; }
        public int Idproduto { get; set; }
        public DateTime Datadevolucao { get; set; }
        public float Quantidade { get; set; }
        public float Quantidadesaldo { get; set; }
        public int Idnforigem { get; set; }
        public int Idnfprodutoorigem { get; set; }
    }
}
