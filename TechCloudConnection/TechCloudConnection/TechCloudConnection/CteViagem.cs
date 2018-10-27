using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteViagem
    {
        public int Codigo { get; set; }
        public DateTime Saida { get; set; }
        public DateTime Volta { get; set; }
        public int Rota { get; set; }
        public int Idmotorista { get; set; }
        public decimal Adiantamento { get; set; }
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal Outros { get; set; }
        public bool Aberta { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public decimal Kminicial { get; set; }
        public decimal Kmfinal { get; set; }
        public int Idweb { get; set; }
    }
}
