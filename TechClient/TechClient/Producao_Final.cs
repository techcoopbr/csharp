using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Producao_Final
    {
        public int Id { get; set; }
        public int Idproducao { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public float Custo { get; set; }
        public float Customedio { get; set; }
        public float Estoqueatual { get; set; }
        public float Estoqueatualnf { get; set; }
        public DateTime Data { get; set; }
    }
}
