using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ajuste_Itens
    {
        public int Id { get; set; }
        public int Idajuste { get; set; }
        public int Idproduto { get; set; }
        public float Descricao { get; set; }
        public float Estoqueatual { get; set; }
        public float Estoqueajuste { get; set; }
        public float Diferenca { get; set; }
        public int Idtanque { get; set; }
        public string Md5 { get; set; }
    }
}
