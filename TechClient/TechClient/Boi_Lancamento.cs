using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Boi_Lancamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public int Idretiro { get; set; }
        public int Idcategoria { get; set; }
        public int Idstatus { get; set; }
        public int Idinvernada { get; set; }
        public int Idbasto { get; set; }
        public int Quantidade { get; set; }
        public string Tipomovimento { get; set; }
        public int Idusuario { get; set; }
        public string Observacao { get; set; }
        public int Idraca { get; set; }
    }
}
