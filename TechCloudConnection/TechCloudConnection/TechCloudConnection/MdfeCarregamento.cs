using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class MdfeCarregamento
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Idmdfe { get; set; }
        public int Idcidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Descricao { get; set; }
        public int Idweb { get; set; }
    }
}
