using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Producao_Itens
    {
        public int Id { get; set; }
        public int Idproducao { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public float Custoreposicao { get; set; }
        public float Customedio { get; set; }
        public float Estoqueanterior { get; set; }
        public DateTime Datainclusao { get; set; }
        public int Idfuncionario { get; set; }
        public float Estoqueanteriornf { get; set; }
    }
}
