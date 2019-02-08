using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ListaPrecoItens
    {
        public int Id { get; set; }
        public int Idlistapreco { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public float Percentual { get; set; }
    }
}
