using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Boi_Transferencias
    {
        public int Id { get; set; }
        public int Idretiro_o { get; set; }
        public int Idinvernada_o { get; set; }
        public int Idcategoria_o { get; set; }
        public int Idraca_o { get; set; }
        public int Idstatus_o { get; set; }
        public int Idretiro_d { get; set; }
        public int Idinvernada_d { get; set; }
        public int Idcategoria_d { get; set; }
        public int Idraca_d { get; set; }
        public int Idstatus_d { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public int Idusuario { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public string Tipo { get; set; }
        public int Idboilancamento_o { get; set; }
        public int Idboilancamento_d { get; set; }
    }
}
