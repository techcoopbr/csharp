using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Comb_Medicao
    {
        public int Id { get; set; }
        public int Idtanque { get; set; }
        public int Idcombustivel { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public float Quantidade { get; set; }
        public int Empresa { get; set; }
        public int Idfuncionario { get; set; }
        public float Quantidadeinicial { get; set; }
        public float Compras { get; set; }
        public float Saidas { get; set; }
    }
}
