using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Tributacao_Itens
    {
        public int Codigo { get; set; }
        public int Idtributacao { get; set; }
        public string Estado { get; set; }
        public string Cst_icmssaida { get; set; }
        public int Tipo_baseicms { get; set; }
        public int Tipo_baseicmsst { get; set; }
        public float Coringa_icms { get; set; }
        public float Reducao_icmssaida { get; set; }
        public float Aliquota_icmssaida { get; set; }
        public float Aliquota_icmsstsaida { get; set; }
        public string Cst_pissaida { get; set; }
        public string Cst_pisentrada { get; set; }
        public float Aliquota_pissaida { get; set; }
        public float Aliquota_pisentrada { get; set; }
        public string Cst_ipisaida { get; set; }
        public string Cst_ipientrada { get; set; }
        public float Aliquota_ipisaida { get; set; }
        public float Aliquota_ipientrada { get; set; }
        public float Pauta_federalentrada { get; set; }
        public float Pauta_federalsaida { get; set; }
        public string Formula_valoricms { get; set; }
        public string Formula_valoricmsst { get; set; }
        public string Formula_baseicms { get; set; }
        public string Formula_baseicmsst { get; set; }
        public string Formula_totalnf { get; set; }
        public int Tipo_pessoapfpj { get; set; }
        public int Tipo_pessoaconsumidor { get; set; }
        public int Tipo_pessoacontribuinte { get; set; }
        public float Coringa_icmsst { get; set; }
        public float Aliquota_cofinssaida { get; set; }
        public float Aliquota_cofinsentrada { get; set; }
        public string Cst_cofinssaida { get; set; }
        public string Cst_cofinsentrada { get; set; }
        public float Cargaicms { get; set; }
        public int Somapiscofins { get; set; }
        public string Origem { get; set; }
        public float Aliquotaufdestino { get; set; }
        public float Aliquotafundoprobreza { get; set; }
        public float Baseufdestino { get; set; }
        public float Basefundopobreza { get; set; }
        public string Enquadramentoipi { get; set; }
    }
}
