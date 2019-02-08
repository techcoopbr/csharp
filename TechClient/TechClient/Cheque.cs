using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cheque
    {
        public int Id { get; set; }
        public int Documento { get; set; }
        public float Valor { get; set; }
        public DateTime Dataemissao { get; set; }
        public string Nominal { get; set; }
        public int Idempresa { get; set; }
        public int Idbanco { get; set; }
        public string Banco { get; set; }
        public int Numerocheque { get; set; }
        public string Historico { get; set; }
        public DateTime Datalancamento { get; set; }
        public string Empresa { get; set; }
        public string Cidade { get; set; }
        public int Idpessoa { get; set; }
        public string Tipo { get; set; }
        public DateTime Datavencimento { get; set; }
        public DateTime Databaixa { get; set; }
        public int Baixado { get; set; }
        public int Idcidade { get; set; }
        public int Idduplicata { get; set; }
    }
}
