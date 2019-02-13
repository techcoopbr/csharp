using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class RestricaoVenda
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public string Msg { get; set; }
        public int Lib_clientebloqueado { get; set; }
        public int Lib_estoquenegativo { get; set; }
        public int Lib_parcelabaixo { get; set; }
        public int Lib_parcelaacima { get; set; }
        public int Lib_descontocliente { get; set; }
        public int Lib_descontoproduto { get; set; }
        public int Lib_validade { get; set; }
        public int Lib_datavencimento { get; set; }
        public int Lib_descontototal { get; set; }
        public int Status { get; set; }
        public DateTime Databloqueio { get; set; }
        public DateTime Horabloqueio { get; set; }
        public DateTime Dataliberacao { get; set; }
        public DateTime Horaliberacao { get; set; }
        public int Usuario { get; set; }
        public string Msgretorno { get; set; }
    }
}
