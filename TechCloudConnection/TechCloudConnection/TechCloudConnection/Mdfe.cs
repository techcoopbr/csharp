using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Mdfe
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public int Serie { get; set; }
        public int Documento { get; set; }
        public DateTime Dataemissao { get; set; }
        public DateTime Horaemissao { get; set; }
        public DateTime Datainicio { get; set; }
        public DateTime Horainicio { get; set; }
        public int Informarcodigo { get; set; }
        public int Tipoemitente { get; set; }
        public string Ufemitente { get; set; }
        public int Modalidade { get; set; }
        public int Formaemissao { get; set; }
        public string Ufdescarregamento { get; set; }
        public string Codporto { get; set; }
        public int Idveiculo { get; set; }
        public string Ufveiculo { get; set; }
        public int Tipocarroceria { get; set; }
        public int Tiporodado { get; set; }
        public string Placa { get; set; }
        public decimal Capacidadekg { get; set; }
        public decimal Tarakg { get; set; }
        public decimal Capacidadem3 { get; set; }
        public string Renavam { get; set; }
        public int Proprietario { get; set; }
        public string Rntrc { get; set; }
        public int Tipoproprietario { get; set; }
        public string Ufproprietario { get; set; }
        public string Cpfcnpj { get; set; }
        public string Nome { get; set; }
        public string Ie { get; set; }
        public decimal Valortotalmercadoria { get; set; }
        public string Unidademedida { get; set; }
        public decimal Valortotalpesobruto { get; set; }
        public string Obs { get; set; }
        public string Obsfisco { get; set; }
        public int Tipoambiente { get; set; }
        public string Versao { get; set; }
        public string Versaosistema { get; set; }
        public int Status { get; set; }
        public int Cancelado { get; set; }
        public string Ufretorno { get; set; }
        public string Xmotivo { get; set; }
        public string Xmsg { get; set; }
        public string Recibo { get; set; }
        public string Protocolo { get; set; }
        public int Idfuncionario { get; set; }
        public int Idmodelo { get; set; }
        public string Ciot { get; set; }
        public string Rntrcempresa { get; set; }
        public int Idempresa { get; set; }
        public string Ufcarregamento { get; set; }
        public string Cnpjcontratante { get; set; }
        public int Emissao { get; set; }
        public int Idweb { get; set; }
    }
}
