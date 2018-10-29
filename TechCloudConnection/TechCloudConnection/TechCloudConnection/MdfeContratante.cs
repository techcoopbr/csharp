using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class MdfeContratante
    {
        public int Id { get; set; }
        public int Idmdfe { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public int Idweb { get; set; }
    }
}
