using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteVeiculosCusto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Idveiculo { get; set; }
        public int Idproduto { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valorunitario { get; set; }
        public decimal Valortotal { get; set; }
        public int Idnf { get; set; }
        public int Idequipamento { get; set; }
        public decimal Kmhora { get; set; }
        public int Idweb { get; set; }
    }
}
