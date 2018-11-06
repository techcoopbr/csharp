using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class MdfeSeguro
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Idmdfe { get; set; }
        public int Responsavel { get; set; }
        public string Cpfcnpj { get; set; }
        public string Nomeseuradora { get; set; }
        public string Cnpj { get; set; }
        public string Apolice { get; set; }
        public string Averbacao { get; set; }
        public int Idweb { get; set; }
    }
}
