using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Servicos
    {
        public int Codigo { get; set; }
        public int Idnf { get; set; }
        public int Codigoservico { get; set; }
        public string Cfop { get; set; }
        public float Quantidade { get; set; }
        public float Valorunitario { get; set; }
        public float Valortotal { get; set; }
        public float Baseicms { get; set; }
        public float Baseicmsst { get; set; }
        public float Basepis { get; set; }
        public float Basecofins { get; set; }
        public float Valoricms { get; set; }
        public float Valoricmsst { get; set; }
        public float Valorpis { get; set; }
        public float Valorcofins { get; set; }
        public string Csticms { get; set; }
        public string Cstpis { get; set; }
        public string Cstcofins { get; set; }
        public float Aliquotaicms { get; set; }
        public float Aliquotaicmsst { get; set; }
        public float Aliquotapis { get; set; }
        public float Aliquotacofins { get; set; }
        public float Descontos { get; set; }
        public float Valorcusto { get; set; }
        public int Empresa { get; set; }
        public int Idfuncionario { get; set; }
        public string Nomefuncionario { get; set; }
        public string Descricao { get; set; }
        public string Md5 { get; set; }
        public string Observacao { get; set; }
    }
}
