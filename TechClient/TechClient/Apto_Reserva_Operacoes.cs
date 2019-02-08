using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Reserva_Operacoes
    {
        public int Id { get; set; }
        public int Idreserva { get; set; }
        public int Idoperacao { get; set; }
        public float Valortotal { get; set; }
        public string Descricao { get; set; }
        public int Tipoop { get; set; }
        public DateTime Datalancamento { get; set; }
    }
}
