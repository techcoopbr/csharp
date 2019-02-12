using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Outros
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public int Tipoitem { get; set; }
        public int Codigoitem { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public float Valorunitario { get; set; }
        public float Valortotal { get; set; }
        public int Empresa { get; set; }
    }
}
