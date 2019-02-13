using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Romaneio
    {
        public int Id { get; set; }
        public DateTime Dataemissao { get; set; }
        public DateTime Datacarregamento { get; set; }
        public DateTime Dataentrega { get; set; }
        public int Frete { get; set; }
        public int Idpessoa { get; set; }
        public int Idveiculo { get; set; }
        public int Idmotorista { get; set; }
        public int Idfuncionario { get; set; }
        public string Observacao { get; set; }
        public string Observacaogeral { get; set; }
        public DateTime Horasaida { get; set; }
        public DateTime Horaentrega { get; set; }
        public string Status { get; set; }
    }
}
