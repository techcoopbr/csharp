using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Servico
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public float Custo { get; set; }
        public float Valor { get; set; }
        public float Percentualcomissao { get; set; }
        public bool Ativo { get; set; }
        public float Percentualirf { get; set; }
        public string Cst_pis { get; set; }
        public float Percentual_pis { get; set; }
        public float Valor_pauta_pis { get; set; }
        public string Cst_cofins { get; set; }
        public float Percentual_cofins { get; set; }
        public float Valor_pauta_cofins { get; set; }
        public DateTime Data_atualizacao { get; set; }
    }
}
