using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Reserva
    {
        public int Id { get; set; }
        public int Idapto { get; set; }
        public int Idpessoa { get; set; }
        public string Pessoanome { get; set; }
        public string Pessoacpfcnpj { get; set; }
        public int Pessoacidade { get; set; }
        public DateTime Dataentrada { get; set; }
        public DateTime Horaentrada { get; set; }
        public DateTime Datasaida { get; set; }
        public DateTime Horasaida { get; set; }
        public string Observacao { get; set; }
        public float Valoritens { get; set; }
        public float Valortarifas { get; set; }
        public float Valordescontos { get; set; }
        public float Valortotal { get; set; }
        public float Adiantamento { get; set; }
        public int Tiporeserva { get; set; }
        public string Pessoafone { get; set; }
        public int Empresa { get; set; }
        public float Qtdecafe { get; set; }
        public float Qtdealmoco { get; set; }
        public float Qtdejanta { get; set; }
        public float Qtdelanche { get; set; }
        public int Incluiralimentacao { get; set; }
    }
}
