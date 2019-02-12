using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ncm
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Ncm { get; set; }
        public int Tributacao { get; set; }
        public float Tributoestimadonac { get; set; }
        public float Tributoestimadoimp { get; set; }
        public float Tributoestimadoest { get; set; }
        public float Tributoestimadomun { get; set; }
        public string Versaotabela { get; set; }
        public string Uftabela { get; set; }
        public float Aliquotafixa { get; set; }
        public string Grupo { get; set; }
        public string Subgrupo { get; set; }
        public string Sessao { get; set; }
        public string Sessao1 { get; set; }
        public DateTime Vigenciai { get; set; }
        public DateTime Vigenciaf { get; set; }
        public string Unidadetributada { get; set; }
        public string Unidadedescricao { get; set; }
    }
}
