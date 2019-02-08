using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ApartamentoTipo_Tarifas
    {
        public int Id { get; set; }
        public int Idtipo { get; set; }
        public float Valor { get; set; }
        public int Idtarifa { get; set; }
        public string Descricao { get; set; }
        public int Empresa { get; set; }
    }
}
