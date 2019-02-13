using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Saldo
    {
        public int Id { get; set; }
        public int Idpessoa { get; set; }
        public int Idoperacao { get; set; }
        public DateTime Data { get; set; }
        public int Idfuncionario { get; set; }
        public int Idempresa { get; set; }
        public float Valor { get; set; }
        public float saldo { get; set; }
    }
}
