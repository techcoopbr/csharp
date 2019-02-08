using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Registro_Itens
    {
        public int Id { get; set; }
        public int Idregistro { get; set; }
        public int Codigoitem { get; set; }
        public int Tipoitem { get; set; }
        public float Quantidade { get; set; }
        public float Valorunitario { get; set; }
        public float Valordesconto { get; set; }
        public float Valortotal { get; set; }
        public DateTime Datainicial { get; set; }
        public DateTime Horainicial { get; set; }
        public DateTime Datafinal { get; set; }
        public DateTime Horafinal { get; set; }
        public string Descricao { get; set; }
        public int Empresa { get; set; }
    }
}
