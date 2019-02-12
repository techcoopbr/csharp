using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class MovimentoGerencial
    {
        public int Id { get; set; }
        public int Idmovimento { get; set; }
        public string Idplanog { get; set; }
        public int Idpessoas { get; set; }
        public DateTime Datalancamento { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public int Idempresa { get; set; }
        public string Nomepessoa { get; set; }
        public int Idacumulador { get; set; }
        public int Idmovimentogerencialweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
