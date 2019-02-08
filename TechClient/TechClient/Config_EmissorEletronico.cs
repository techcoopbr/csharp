using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Config_EmissorEletronico
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Emite_cte { get; set; }
        public string Emite_nfe { get; set; }
        public string Emite_spedicms { get; set; }
        public string Emite_spedcontrib { get; set; }
        public string Certificado_numero { get; set; }
        public DateTime Certificado_validade { get; set; }
        public string Ativo { get; set; }
        public string Path_emitidos { get; set; }
        public string Path_cancelados { get; set; }
        public string Path_inutilizados { get; set; }
        public string Path_consultas { get; set; }
        public string Path_log { get; set; }
        public bool Emissao { get; set; }
        public bool Ambiente { get; set; }
        public int Empresa { get; set; }
        public int Usanovoqr { get; set; }
        public string Path_pdf { get; set; }
        public string Path_eventos { get; set; }
        public string Path_schemas { get; set; }
        public int Timeoutnfe { get; set; }
        public int Intervaloconsultanfe { get; set; }
        public int Tentativasnfe { get; set; }
        public int Diasantesvencimento { get; set; }
        public string Certificado_senha { get; set; }
        public int Layoutnfe { get; set; }
        public int Layoutcte { get; set; }
        public int Layoutmdfe { get; set; }
        public int Emissao_cte { get; set; }
        public int Emissao_mdfe { get; set; }
        public int Emissao_nfce { get; set; }
        public string Path_download { get; set; }
    }
}
