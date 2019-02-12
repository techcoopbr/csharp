using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class OperadorCaixa
    {
        public int Id { get; set; }
        public DateTime Dataabertura { get; set; }
        public DateTime Horaabertura { get; set; }
        public DateTime Datafechamento { get; set; }
        public DateTime Horafechamento { get; set; }
        public int Idusuario { get; set; }
        public float Valorentradas { get; set; }
        public float Valorsaidas { get; set; }
        public float Trocoinicial { get; set; }
        public float Trocofinal { get; set; }
        public int Empresa { get; set; }
    }
}
