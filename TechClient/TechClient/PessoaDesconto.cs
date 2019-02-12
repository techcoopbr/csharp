using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class PessoaDesconto
    {
        public int Id { get; set; }
        public int Idproduto { get; set; }
        public float Valor { get; set; }
        public string Tipopreco { get; set; }
        public int Idpessoa { get; set; }
    }
}
