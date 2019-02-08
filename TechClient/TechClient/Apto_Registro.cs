using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Registro
    {
        public int Id { get; set; }
        public int Idapto { get; set; }
        public int Idtarifa { get; set; }
        public int Idpessoa { get; set; }
        public char Status { get; set; }
        public int Ocupante { get; set; }
        public int Alimentacao { get; set; }
        public DateTime Dataentrada { get; set; }
        public DateTime Datasaida { get; set; }
        public DateTime Horaentrada { get; set; }
        public DateTime Horasaida { get; set; }
        public float Valoritens { get; set; }
        public float Valortarifas { get; set; }
        public float Valortotal { get; set; }
        public float Valoralimentacao { get; set; }
        public int Empresa { get; set; }
        public int Limpar { get; set; }
        public int Arrumar { get; set; }
        public int Idfuncionario { get; set; }
        public int Idreserva { get; set; }
        public float Qtdecafe { get; set; }
        public float Qtdealmoco { get; set; }
        public float Qtdejanta { get; set; }
        public float Qtdelanche { get; set; }
    }
}
