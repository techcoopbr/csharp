using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Mdfe_M1
    {
        public int Id { get; set; }
        public int Idmdfe { get; set; }
        public string Cnpjemitente { get; set; }
        public string Ufdestinatario { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
        public DateTime Dataemissao { get; set; }
        public float Valortotal { get; set; }
        public string Suframa { get; set; }
        public int Idmdfem1web { get; set; }
        public int Sincronizado { get; set; }
    }
}
