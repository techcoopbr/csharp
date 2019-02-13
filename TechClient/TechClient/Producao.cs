using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Producao
    {
        public int Id { get; set; }
        public DateTime Dataabertura { get; set; }
        public DateTime Datafechamento { get; set; }
        public DateTime Dataprevisao { get; set; }
        public string Status { get; set; }
        public int Idfuncionario { get; set; }
        public DateTime Horaabertura { get; set; }
        public DateTime Horaprevisao { get; set; }
        public DateTime Horafechamento { get; set; }
    }
}
