using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteCteMotorista
    {
        public int Codigo { get; set; }
        public int Idcte { get; set; }
        public int Idmotorista { get; set; }
        public string Nome { get; set; }
        public int Empresa { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public int Parcelas { get; set; }
        public int Idweb { get; set; }
    }
}
