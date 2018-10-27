using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class CteCteDocrem
    {
        public int Codigo { get; set; }
        public int Idcte { get; set; }
        public string Modelo { get; set; }
        public int Serie { get; set; }
        public int Numero { get; set; }
        public DateTime Dataemissao { get; set; }
        public string Cfop { get; set; }
        public int Tipodoc { get; set; }
        public int Numeroromaneio { get; set; }
        public int Numeropedido { get; set; }
        public string Chave { get; set; }
        public string Pin { get; set; }
        public int Tipooriginario { get; set; }
        public string Descricao { get; set; }
        public decimal Base_icms { get; set; }
        public decimal Valor_icms { get; set; }
        public decimal Base_icmsst { get; set; }
        public decimal Valor_icmsst { get; set; }
        public decimal Pesokg { get; set; }
        public decimal Valorprodutos { get; set; }
        public decimal Valortotal { get; set; }
        public int Moddocumento { get; set; }
        public int Empresa { get; set; }
        public int Idweb { get; set; }
    }
}
