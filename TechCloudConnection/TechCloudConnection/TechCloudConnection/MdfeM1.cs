using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class MdfeM1
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Idmdfe { get; set; }
        public string Cnpjemitente { get; set; }
        public string Ufdestinatario { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
        public DateTime Dataemissao { get; set; }
        public decimal Valortotal { get; set; }
        public string Suframa { get; set; }
        public int Idweb { get; set; }
    }
}
