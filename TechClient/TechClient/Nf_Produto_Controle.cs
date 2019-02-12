using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Produto_Controle
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public int Idproduto { get; set; }
        public string Idlote { get; set; }
        public DateTime Datafabricacao { get; set; }
        public DateTime Datalanclote { get; set; }
        public DateTime Datavencimento { get; set; }
        public float Quantidade { get; set; }
        public float Quantidadeporlote { get; set; }
        public int Idempresa { get; set; }
    }
}
