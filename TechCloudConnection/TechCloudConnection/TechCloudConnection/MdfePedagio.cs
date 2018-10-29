using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class MdfePedagio
    {
        public int Id { get; set; }
        public string Idmdfe { get; set; }
        public string Cnpjpedagio { get; set; }
        public string Numerocomprovante { get; set; }
        public string Cnpjpagante { get; set; }
        public int Idweb { get; set; }
    }
}
