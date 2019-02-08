using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Cte_Composicao
    {
        public int Id { get; set; }
        public int Idmotorista { get; set; }
        public int Idveiculo1 { get; set; }
        public int Idveiculo2 { get; set; }
        public int Idveiculo3 { get; set; }
        public int Idveiculo4 { get; set; }
        public DateTime Datainicial { get; set; }
        public DateTime Datafinal { get; set; }
        public int Empresa { get; set; }
        public int Idviagem { get; set; }
        public int Idctecomposicaoweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
