using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class CarteiraBoleto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Msglocalpgto { get; set; }
        public string Especiedocumento { get; set; }
        public string Especiemoeda { get; set; }
        public int Aceite { get; set; }
        public string Carteira { get; set; }
        public float Juros { get; set; }
        public float Desconto { get; set; }
        public float Abatimento { get; set; }
        public float Multa { get; set; }
        public int Ultimadpl { get; set; }
        public int Diasjuros { get; set; }
        public int Diaslimite { get; set; }
        public int Diasdesconto { get; set; }
        public int Diasabatimento { get; set; }
        public int Diasprotesto { get; set; }
        public string Msg { get; set; }
        public string Instrucao1 { get; set; }
        public string Instrucao2 { get; set; }
        public int Layout { get; set; }
        public int Banco { get; set; }
        public int Numerobanco { get; set; }
        public int Digito { get; set; }
        public int Geraremessa { get; set; }
        public int Registrado { get; set; }
        public int Protesta { get; set; }
        public int Empresa { get; set; }
        public int Tamanhonossonumero { get; set; }
        public int Idbanco { get; set; }
        public string Codigocedente { get; set; }
        public string Variacao { get; set; }
        public string Codigotransmissao { get; set; }
        public string Modalidade { get; set; }
        public string Convenio { get; set; }
        public int Caractitulo { get; set; }
        public string Instrucao3 { get; set; }
        public int Responsavel { get; set; }
        public string Relatorio { get; set; }
        public int Colorido { get; set; }
        public int Layoutarquivo { get; set; }
    }
}
