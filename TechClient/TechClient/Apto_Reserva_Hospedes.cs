using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Apto_Reserva_Hospedes
    {
        public int Id { get; set; }
        public int Idreserva { get; set; }
        public int Idpessoa { get; set; }
        public string Nome { get; set; }
        public int Idempresa { get; set; }
    }
}
