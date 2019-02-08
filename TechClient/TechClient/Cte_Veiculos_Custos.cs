using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Veiculos_Custos
    {
        public int Id { get; set; }
        public int Idveiculo { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public float Valorunitario { get; set; }
        public float Valortotal { get; set; }
        public int Idnf { get; set; }
        public int Idequipamento { get; set; }
        public float Kmhora { get; set; }
        public int Idcteveiculoscustosweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
