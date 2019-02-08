using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Mdfe_Carregamento
    {
        public int Id { get; set; }
        public int Idmdfe { get; set; }
        public int Idcidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Descricao { get; set; }
        public int Idmdfecarregamentoweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
