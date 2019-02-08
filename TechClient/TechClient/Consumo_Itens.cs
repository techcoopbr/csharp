using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Consumo_Itens
    {
        public int Id { get; set; }
        public int Idconsumo { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public string Unidade { get; set; }
        public float Customedioun { get; set; }
        public float Custoreposicaoun { get; set; }
        public float Precovendaun { get; set; }
        public float Customediototal { get; set; }
        public float Custoreposicaototal { get; set; }
        public float Precovendatotal { get; set; }
    }
}
