using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class PlanoVenda
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Parcelas { get; set; }
        public string Intervalo { get; set; }
        public int Tipocobranca { get; set; }
        public string Descricaocobranca { get; set; }
        public string Relatorio { get; set; }
        public float Parcelaminima { get; set; }
        public int Ativo { get; set; }
        public int Diavencimento { get; set; }
        public int Tododia { get; set; }
        public int Idboleto { get; set; }
        public int Empresa { get; set; }
        public int Imprimecontrato { get; set; }
        public string Relatoriocontrato { get; set; }
        public int Imprimeromaneio { get; set; }
        public string Relatorioromaneio { get; set; }
        public int Pagacomissao { get; set; }
        public int Idplanovendaweb { get; set; }
        public int Sincronizado { get; set; }
    }
}
