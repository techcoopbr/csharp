using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Duplicatas_Baixas
    {
        public int Codigo { get; set; }
        public int Idduplicata { get; set; }
        public DateTime Datarecebimento { get; set; }
        public float Valorrecebido { get; set; }
        public float Valorjuros { get; set; }
        public float Valordesconto { get; set; }
        public float Valorsaldo { get; set; }
        public int Operacao { get; set; }
        public int Usuario { get; set; }
        public int Numeroduplicata { get; set; }
        public int Numeroparcela { get; set; }
        public int Banco { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public int Idcaixa { get; set; }
        public float Comissao { get; set; }
        public int Idvendedor { get; set; }
        public int Iduserbaixa { get; set; }
        public int Idrecibo { get; set; }
        public string Obs { get; set; }
    }
}
