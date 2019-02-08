using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ComandasItens
    {
        public int Id { get; set; }
        public int Idcomanda { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public int Idvendedor { get; set; }
        public string Obs { get; set; }
    }
}
