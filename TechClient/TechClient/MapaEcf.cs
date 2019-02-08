using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class MapaEcf
    {
        public int Id { get; set; }
        public int Idecf { get; set; }
        public int Cro { get; set; }
        public int Crz { get; set; }
        public int Coo { get; set; }
        public float Gt { get; set; }
        public float Vb { get; set; }
        public DateTime Dataimportacao { get; set; }
        public DateTime Dataecf { get; set; }
        public DateTime Hora { get; set; }
        public float Vl { get; set; }
        public float Cancelamentoicms { get; set; }
        public float Cancelamentoiss { get; set; }
        public float Descontoiss { get; set; }
        public float Descontoicms { get; set; }
        public float Acrescimoicms { get; set; }
        public float Acrescimoiss { get; set; }
        public string Md5 { get; set; }
    }
}
