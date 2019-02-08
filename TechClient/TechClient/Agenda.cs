using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Agenda
    {
        public int Id { get; set; }
        public int Empresa { get; set; }
        public string Descricao { get; set; }
        public string Descricaolonga { get; set; }
        public DateTime Datainicio { get; set; }
        public DateTime Datafim { get; set; }
        public int Diasaviso { get; set; }
        public int Diasalerta { get; set; }
        public float Kmtrocafeita { get; set; }
        public int Idveiculo { get; set; }
        public int Idpessoa { get; set; }
        public int Idfuncionario { get; set; }
        public int Idequipamento { get; set; }
        public int Tipoagenda { get; set; }
        public DateTime Horainicio { get; set; }
        public DateTime Horafim { get; set; }
        public float Kmtrocaanterior { get; set; }
        public DateTime Dataexecutado { get; set; }
    }
}
