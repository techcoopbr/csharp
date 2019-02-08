using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Reserva_Duplicatas
    {
        public int Id { get; set; }
        public int Idreserva { get; set; }
        public int Parcela { get; set; }
        public DateTime Vencimento { get; set; }
        public float Valor { get; set; }
    }
}
