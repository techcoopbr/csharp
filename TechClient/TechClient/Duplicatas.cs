using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Duplicatas
    {
        public int Codigo { get; set; }
        public int Duplicata { get; set; }
        public int Parcela { get; set; }
        public int Documento { get; set; }
        public int Pessoa { get; set; }
        public string Tipo { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valordocumento { get; set; }
        public float Valorparcela { get; set; }
        public float Saldo { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public int Pedido { get; set; }
        public float Comissao { get; set; }
        public int Totalparcelas { get; set; }
        public int Idnf { get; set; }
        public int Idvendedor { get; set; }
        public string Observacao { get; set; }
        public int Agrupado { get; set; }
        public int Idalteracao { get; set; }
        public int Iduserbaixa { get; set; }
        public float Jurosacumulados { get; set; }
        public string Nossonumero { get; set; }
        public int Idplanovenda { get; set; }
        public int Idcheque { get; set; }
        public DateTime Ultimabaixa { get; set; }
        public float Descontosacumulados { get; set; }
        public float Recebidosacumulado { get; set; }
        public int Idduplicatasweb { get; set; }
        public int Sincronizado { get; set; }
        public int Idboletoremessa { get; set; }
    }
}
