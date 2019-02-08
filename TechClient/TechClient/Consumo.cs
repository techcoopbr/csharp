using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Consumo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public int Idlocal { get; set; }
        public int Iddepartamento { get; set; }
        public int Idfuncionario { get; set; }
        public int Idrequisitante { get; set; }
        public float Customedio { get; set; }
        public float Custoreposicao { get; set; }
        public float Precovenda { get; set; }
        public int Cancelada { get; set; }
        public int Idempresa { get; set; }
        public string Obs { get; set; }
    }
}
