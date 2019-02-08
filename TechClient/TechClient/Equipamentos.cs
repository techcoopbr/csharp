using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Equipamentos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Disponivel { get; set; }
        public float Valor { get; set; }
        public int Empresa { get; set; }
        public float Oleohoras { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }
        public DateTime Labeldata1 { get; set; }
        public DateTime Labeldata2 { get; set; }
        public int Idproprietario { get; set; }
        public string Nomedono { get; set; }
    }
}
