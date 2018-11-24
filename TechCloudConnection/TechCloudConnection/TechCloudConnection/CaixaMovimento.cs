using System;

namespace Modelo
{
    class CaixaMovimento
    {
        public int Idmovimentocaixa { get; set; }
        public int Idcaixa { get; set; }
        public int Idfuncionario { get; set; }
        public DateTime Dataabertura { get; set; }
        public DateTime Horaabertura { get; set; }
        public DateTime Datafechamento { get; set; }
        public DateTime Horafechamento { get; set; }
        public decimal Valordinheiro { get; set; }
        public decimal Valorcartaodebito { get; set; }
        public decimal Valorcartaocredito { get; set; }
        public decimal Valorcheque { get; set; }
        public decimal Valorprazo { get; set; }
        public decimal Totalcaixa { get; set; }
        public decimal Trocoinicial { get; set; }
        public decimal Trocofinal { get; set; }
        public int Empresa { get; set; }
        public int Verificado { get; set; }
        public decimal Valoroutro { get; set; }
        public int Idweb { get; set; }
    }
}
