using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Registro_Tarifas
    {
        public int Id { get; set; }
        public int Idregistro { get; set; }
        public int Idtipo { get; set; }
        public int Dias { get; set; }
        public float Valorunitario { get; set; }
        public float Valordesconto { get; set; }
        public float Valortotal { get; set; }
        public string Descricao { get; set; }
        public int Empresa { get; set; }
    }
}
