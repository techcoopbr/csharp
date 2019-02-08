using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_LogTroca
    {
        public int Id { get; set; }
        public int Idaptoorigem { get; set; }
        public int Idaptodestino { get; set; }
        public int Idpessoa { get; set; }
        public DateTime Dataentrada { get; set; }
        public DateTime Datasaida { get; set; }
        public DateTime Datatroca { get; set; }
        public DateTime Horatroca { get; set; }
        public int Idusuario { get; set; }
    }
}
