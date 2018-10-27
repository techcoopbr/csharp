using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteCte
    {
        public int Codigo { get; set; }
        public string Modelo { get; set; }
        public int Serie { get; set; }
        public int Documento { get; set; }
        public DateTime Dataemissao { get; set; }
        public string Cfop { get; set; }
        public string Naturezaoperacao { get; set; }
        public string Chave { get; set; }
        public string Chaveref { get; set; }
        public int Tiposervico { get; set; }
        public int Finalidadeemissao { get; set; }
        public int Formapagamento { get; set; }
        public int Cidadeemissao { get; set; }
        public int Cidadeinicio { get; set; }
        public int Cidadefim { get; set; }
        public DateTime Previsaoentrega { get; set; }
        public int Tomador { get; set; }
        public int Idtomador { get; set; }
        public int Idremetente { get; set; }
        public int Iddestinatario { get; set; }
        public int Idexpedidor { get; set; }
        public int Idrecebedor { get; set; }
        public decimal Valorcarga { get; set; }
        public string Produtopredominante { get; set; }
        public string Outrascaracteristicas { get; set; }
        public decimal Valorfrete { get; set; }
        public decimal Valorreceber { get; set; }
        public decimal Impostosvariavel { get; set; }
        public string Cst_icms { get; set; }
        public decimal Base_icms { get; set; }
        public decimal Red_icms { get; set; }
        public int Aliquota_icms { get; set; }
        public decimal Valor_icms { get; set; }
        public decimal Credito_icms { get; set; }
        public string Informacoes_fisco { get; set; }
        public string Observacoesgerais { get; set; }
        public int Emissao { get; set; }
        public int Ambiente { get; set; }
        public string Status { get; set; }
        public string Recibo { get; set; }
        public string Protocolo { get; set; }
        public string Horarecibo { get; set; }
        public string Ciot { get; set; }
        public int Lotacao { get; set; }
        public string Rntrc { get; set; }
        public int Tipo_data { get; set; }
        public int Tipo_hora { get; set; }
        public DateTime Tipo_datai { get; set; }
        public DateTime Tipo_dataf { get; set; }
        public DateTime Tipo_horai { get; set; }
        public DateTime Tipo_horaf { get; set; }
        public int Idviagem { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public decimal Pedagio { get; set; }
        public decimal Descarga { get; set; }
        public decimal Icmsreembolso { get; set; }
        public int Funcionario { get; set; }
        public int Empresa { get; set; }
        public decimal Acrescimo { get; set; }
        public int Idoperacao { get; set; }
        public decimal Kmincial { get; set; }
        public decimal Kmfinal { get; set; }
        public DateTime Horaemissao { get; set; }
        public int Idmodelo { get; set; }
        public int Retira { get; set; }
        public string Localretirada { get; set; }
        public string Caracadicional { get; set; }
        public string Caracservico { get; set; }
        public string Cidadecoleta { get; set; }
        public string Cidadeentrega { get; set; }
        public int Globalizado { get; set; }
        public int Idweb { get; set; }
    }
}
