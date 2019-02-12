using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class PessoaHorarios
    {
        public int Id { get; set; }
        public int Idpessoa { get; set; }
        public DateTime Data { get; set; }
        public DateTime Dataentrada { get; set; }
        public DateTime Primeiraentrada { get; set; }
        public DateTime Primeirasaida { get; set; }
        public DateTime Segundaentrada { get; set; }
        public DateTime Segundasaida { get; set; }
        public DateTime Terceiraentrada { get; set; }
        public DateTime Terceirasaida { get; set; }
    }
}
