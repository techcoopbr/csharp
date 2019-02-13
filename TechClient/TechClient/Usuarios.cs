using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Usuarios
    {
        public int Codigo { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Admin { get; set; }
        public int Idpessoa { get; set; }
        public int Operadorcaixa { get; set; }
        public bool Ativo { get; set; }
        public int Trocavendedor { get; set; }
        public int Vendaemcaixa { get; set; }
        public int Permitereimpressao { get; set; }
        public int Permiteopcoesnf { get; set; }
        public int Permitefinanceironf { get; set; }
        public int Permiteprodutocontrole { get; set; }
        public int Mostrapendencias { get; set; }
        public int Permitesemsenha { get; set; }
        public int Usuarioweb { get; set; }
        public int Sincronizado { get; set; }
        public int Permitevalorcomanda { get; set; }
        public int Visualizaestoque { get; set; }
        public int Mostradadosfinanceiro { get; set; }
        public int Mostradavfaturados { get; set; }
    }
}
