using System;

namespace Modelo
{
    class Operacoes
    {
        public int Codigo { get; set; }
        public int Operacao { get; set; }
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int Estoque { get; set; }
        public decimal Descontomaximo { get; set; }
        public decimal Acrescimomaximo { get; set; }
        public decimal Descontoautomatico { get; set; }
        public decimal Acrescimoautomatico { get; set; }
        public int Multiplas { get; set; }
        public int Idoperadora { get; set; }
        public decimal Percentual { get; set; }
        public int Realizatef { get; set; }
        public int Gerenciador { get; set; }
        public decimal Taxajuro { get; set; }
        public string Formapgtoecf { get; set; }
        public string Md5 { get; set; }
        public int Operacaocte { get; set; }
        public int Diasaposvenda { get; set; }
        public int Pagacomissao { get; set; }
        public int Operacaodav { get; set; }
        public int Es_naturezarubrica { get; set; }
    }
}
