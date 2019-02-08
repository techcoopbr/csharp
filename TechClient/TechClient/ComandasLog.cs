using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ComandasLog
    {
        public int Id { get; set; }
        public int Idcomanda { get; set; }
        public int Idusuario { get; set; }
        public string Descricao { get; set; }
        public DateTime Datahora { get; set; }
        public float Totaldeitens { get; set; }
        public float Totalvalor { get; set; }
        public string Motivo { get; set; }
    }
}
