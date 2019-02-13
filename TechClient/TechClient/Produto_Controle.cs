using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Produto_Controle
    {
        public int Id { get; set; }
        public int Idproduto { get; set; }
        public string Idlote { get; set; }
        public DateTime Datafabricacao { get; set; }
        public DateTime Datalote { get; set; }
        public DateTime Datavencimento { get; set; }
        public float Quantidadeporlote { get; set; }
        public float Estoque { get; set; }
        public DateTime Datafim { get; set; }
        public int Idempresa { get; set; }
        public int Status { get; set; }
    }
}
