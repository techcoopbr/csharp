using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Veiculos_Itens
    {
        public int Codigo { get; set; }
        public int Idveiculo { get; set; }
        public string Descricao { get; set; }
        public int Idveiculomestre { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public string Tipoitem { get; set; }
        public int Idcteveiculositensweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
