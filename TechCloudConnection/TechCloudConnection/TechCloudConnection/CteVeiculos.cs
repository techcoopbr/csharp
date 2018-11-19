using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteVeiculos
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public string Renavan { get; set; }
        public string Ufveiculo { get; set; }
        public decimal Capacidadekg { get; set; }
        public decimal Capacidadem3 { get; set; }
        public int Tiporodado { get; set; }
        public int Tipocarroceria { get; set; }
        public int Tipoveiculo { get; set; }
        public int Tipoproprietario { get; set; }
        public int Idproprietario { get; set; }
        public int Idmotorista { get; set; }
        public decimal Tara { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public int Empresa { get; set; }
        public decimal Kmoleomotor { get; set; }
        public decimal Kmoleodiferencial { get; set; }
        public decimal Kmoleocambio { get; set; }
        public int Ano { get; set; }
        public int Modelo { get; set; }
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public string Marca { get; set; }
        public int Idweb { get; set; }
    }
}
