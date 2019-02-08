using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ecf
    {
        public int Id { get; set; }
        public string Numeroserie { get; set; }
        public string Porta { get; set; }
        public string Modelo { get; set; }
        public int Marca { get; set; }
        public float Gt { get; set; }
        public bool Abregaveta { get; set; }
        public int Empresa { get; set; }
        public string Numeroseriemfd { get; set; }
        public string Marcastr { get; set; }
        public string Mfadicional { get; set; }
        public DateTime Horasb { get; set; }
        public DateTime Datasb { get; set; }
        public string Md5 { get; set; }
        public string Versaoecf { get; set; }
        public string Tipoecf { get; set; }
        public DateTime Horaestoque { get; set; }
        public DateTime Dataestoque { get; set; }
        public int Velocidade { get; set; }
        public int Idecfatulizacao { get; set; }
        public string Cnpjecf { get; set; }
        public int Intervalo { get; set; }
        public string Cnpj { get; set; }
    }
}
