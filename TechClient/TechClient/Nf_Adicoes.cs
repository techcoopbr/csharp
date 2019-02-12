using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Nf_Adicoes
    {
        public int Id { get; set; }
        public int Idnf { get; set; }
        public int Idproduto { get; set; }
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public DateTime Datadespacho { get; set; }
        public string Codigoexportador { get; set; }
        public string Ufdespacho { get; set; }
        public int Empresa { get; set; }
        public int Tipotransporte { get; set; }
        public int Tipoimportacao { get; set; }
        public string Cnpjimportador { get; set; }
        public string Ufimportador { get; set; }
        public int Numerodrawback { get; set; }
        public float Afrmm { get; set; }
    }
}
