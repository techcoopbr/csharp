using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Cte_Docrem
    {
        public int Codigo { get; set; }
        public int Idcte { get; set; }
        public string Modelo { get; set; }
        public int Serie { get; set; }
        public int Numero { get; set; }
        public DateTime Dataemissao { get; set; }
        public string Cfop { get; set; }
        public int Tipodoc { get; set; }
        public int Numeroromaneio { get; set; }
        public int Numeropedido { get; set; }
        public string Chave { get; set; }
        public string Pin { get; set; }
        public int Tipooriginario { get; set; }
        public string Descricao { get; set; }
        public float Base_icms { get; set; }
        public float Valor_icms { get; set; }
        public float Base_icmsst { get; set; }
        public float Valor_icmsst { get; set; }
        public float Pesokg { get; set; }
        public float Valorprodutos { get; set; }
        public float Valortotal { get; set; }
        public int Moddocumento { get; set; }
        public int Empresa { get; set; }
        public int Idctectedocremweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
