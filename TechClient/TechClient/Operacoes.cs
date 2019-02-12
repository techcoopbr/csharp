using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Operacoes
    {
        public int Codigo { get; set; }
        public int Operacao { get; set; }
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int Estoque { get; set; }
        public float Descontomaximo { get; set; }
        public float Acrescimomaximo { get; set; }
        public float Descontoautomatico { get; set; }
        public float Acrescimoautomatico { get; set; }
        public int Multiplas { get; set; }
        public int Idoperadora { get; set; }
        public float Percentual { get; set; }
        public int Realizatef { get; set; }
        public int Gerenciador { get; set; }
        public float Taxajuro { get; set; }
        public string Formapgtoecf { get; set; }
        public string Md5 { get; set; }
        public int Operacaocte { get; set; }
        public int Diasaposvenda { get; set; }
        public int Pagacomissao { get; set; }
        public int Operacaodav { get; set; }
        public int Es_naturezarubrica { get; set; }
        public int Idoperacoesweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
