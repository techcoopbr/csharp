using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class CaixaMovimento
    {
        public int Id { get; set; }
        public int Idcaixa { get; set; }
        public int Idfuncionario { get; set; }
        public DateTime Dataabertura { get; set; }
        public DateTime Horaabertura { get; set; }
        public DateTime Datafechamento { get; set; }
        public DateTime Horafechamento { get; set; }
        public float Valordinheiro { get; set; }
        public float Valorcartaodebito { get; set; }
        public float Valorcartaocredito { get; set; }
        public float Valorcheque { get; set; }
        public float Valorprazo { get; set; }
        public float Totalcaixa { get; set; }
        public float Trocoinicial { get; set; }
        public float Trocofinal { get; set; }
        public int Empresa { get; set; }
        public int Verificado { get; set; }
        public float Valoroutro { get; set; }
        public int Idcaixamovimentoweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
