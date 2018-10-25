using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
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
        public decimal Valor { get; set; }
        public int Idempresa { get; set; }
        public string Nomepessoa { get; set; }
        public int Idacumulador { get; set; }
        public int Idweb { get; set; }
    }
}
