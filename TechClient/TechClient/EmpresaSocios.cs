using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class EmpresaSocios
    {
        public int Id { get; set; }
        public int Idempresa { get; set; }
        public int Idpessoa { get; set; }
        public string Nome { get; set; }
        public int Respondereceita { get; set; }
        public float Percentual { get; set; }
        public float Cota { get; set; }
    }
}
