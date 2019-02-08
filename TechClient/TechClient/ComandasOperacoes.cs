using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ComandasOperacoes
    {
        public int Id { get; set; }
        public int Idcomanda { get; set; }
        public int Idoperacao { get; set; }
        public float Valortotal { get; set; }
        public string Descricao { get; set; }
        public int Tipoop { get; set; }
        public int Idplanovenda { get; set; }
        public string Md5 { get; set; }
        public string Cnpjoperadora { get; set; }
        public int Bandeira { get; set; }
        public string Autorizacao { get; set; }
    }
}
