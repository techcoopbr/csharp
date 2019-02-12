using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Energia
    {
        public int Codigo { get; set; }
        public int Idnf { get; set; }
        public int Classeconsumo { get; set; }
        public float Baseicms { get; set; }
        public float Baseicmsst { get; set; }
        public float Valoricms { get; set; }
        public float Valoricmsst { get; set; }
        public float Valorconsumido { get; set; }
        public float Valorfatura { get; set; }
        public float Valordesconto { get; set; }
        public float Valordespesas { get; set; }
        public float Valornaotributado { get; set; }
        public float Valorterceiros { get; set; }
        public float Valorpis { get; set; }
        public float Valorcofins { get; set; }
        public int Tipoligacao { get; set; }
        public int Tipotensao { get; set; }
        public string Cfop { get; set; }
        public float Aliquotaicms { get; set; }
        public int Empresa { get; set; }
    }
}
