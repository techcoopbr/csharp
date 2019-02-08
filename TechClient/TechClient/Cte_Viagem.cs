using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Viagem
    {
        public int Codigo { get; set; }
        public DateTime Saida { get; set; }
        public DateTime Volta { get; set; }
        public int Rota { get; set; }
        public int Idmotorista { get; set; }
        public float Adiantamento { get; set; }
        public float Receitas { get; set; }
        public float Despesas { get; set; }
        public float Outros { get; set; }
        public bool Aberta { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public float Kminicial { get; set; }
        public float Kmfinal { get; set; }
        public int Idcteviagemweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
