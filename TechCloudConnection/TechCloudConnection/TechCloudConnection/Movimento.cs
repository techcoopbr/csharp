using System;

namespace Modelo

{
    class Movimento
    {
        public int Codigo { get; set; }
        public int Operacao { get; set; }
        public string Origem { get; set; }
        public int Codigo_ref { get; set; }
        public string Descricao { get; set; }
        public DateTime Datalancamento { get; set; }
        public decimal Valor { get; set; }
        public int Tipomovimento { get; set; }
        public int Banco { get; set; }
        public int Empresa { get; set; }
        public int Viagem { get; set; }
        public int Idcaixa { get; set; }
        public DateTime Datafatura { get; set; }
        public int Faturado { get; set; }
        public int Idpessoacc { get; set; }
        public int Iduserbaixa { get; set; }
        public int Idecf { get; set; }
        public int Coo { get; set; }
        public DateTime Dataecf { get; set; }
        public DateTime Horaecf { get; set; }
        public int Crz { get; set; }
        public int Ccf { get; set; }
        public string Md5 { get; set; }
        public int Manual { get; set; }
        public int Idweb { get; set; }
    }
}
