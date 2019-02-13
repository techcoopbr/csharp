using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Produto_Pacote
    {
        public int Idproduto { get; set; }
        public float Quantidade { get; set; }
        public float Valorunitario { get; set; }
        public float Valortotal { get; set; }
        public string Descricao { get; set; }
        public int Idprodutoproduto { get; set; }
    }
}
