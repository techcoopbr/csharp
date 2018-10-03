using System;

namespace Modelo
{

    public class Duplicata
    {
        //dados básicos
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int NumeroDuplicata { get; set; }
        public int Parcela { get; set; }
        public int Documento { get; set; }
        public int Pessoa { get; set; }
        public string Tipo { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valordocumento { get; set; }
        public decimal Valorparcela { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public int Pedido { get; set; }
        public decimal Comissao { get; set; }
        public int Totalparcelas { get; set; }
        public int Idnf { get; set; }
        public int Idvendedor { get; set; }
        public string Observacao { get; set; }
        public int Agrupado { get; set; }
        public int Idalteracao { get; set; }
        public int Iduserbaixa { get; set; }
        public decimal Jurosacumulados { get; set; }
        public string Nossonumero { get; set; }
        public int Idplanovenda { get; set; }
        public int Idcheque { get; set; }
        public DateTime Ultimabaixa { get; set; }
        public decimal Descontosacumulados { get; set; }
        public decimal Recebidosacumulado { get; set; }
        public int Idweb { get; set; }
        public int Idfolk { get; set; }
    }
}