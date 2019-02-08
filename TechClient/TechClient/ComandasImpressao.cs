using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class ComandasImpressao
    {
        public int Id { get; set; }
        public int Idcomanda { get; set; }
        public int Idproduto { get; set; }
        public int Idgrupo { get; set; }
        public string Descricao { get; set; }
        public float Quantidade { get; set; }
        public string Observacao { get; set; }
        public DateTime Datahorachegada { get; set; }
        public DateTime Datahoraenvio { get; set; }
        public int Finalizado { get; set; }
        public int Idvendedor { get; set; }
    }
}
