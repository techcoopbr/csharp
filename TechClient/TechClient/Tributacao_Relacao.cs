using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Tributacao_Relacao
    {
        public int Id { get; set; }
        public string Cst_icms_origem { get; set; }
        public string Cst_icms_destino { get; set; }
        public string Cst_ipi_origem { get; set; }
        public string Cst_ipi_destino { get; set; }
        public string Cst_pis_origem { get; set; }
        public string Cst_pis_destino { get; set; }
        public string Cst_cofins_origem { get; set; }
        public string Cst_cofins_destino { get; set; }
        public int Zeraicms { get; set; }
        public int Zeraipi { get; set; }
        public int Zerapis { get; set; }
        public int Zeracofins { get; set; }
        public string Formulaicms { get; set; }
        public string Formulaipi { get; set; }
        public string Formulapiscofins { get; set; }
        public float Aliquotaicms { get; set; }
        public float Aliquotaipi { get; set; }
        public float Aliquotapis { get; set; }
        public float Aliquotacofins { get; set; }
    }
}
