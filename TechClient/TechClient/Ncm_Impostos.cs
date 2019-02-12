using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechClient
{
    class Ncm_Impostos
    {
        public string Ncm { get; set; }
        public string Ufimposto { get; set; }
        public float Aliquotanac { get; set; }
        public float Aliquotainp { get; set; }
        public float Aliquotamun { get; set; }
        public float Aliquotaest { get; set; }
        public DateTime Vigenciainicial { get; set; }
        public DateTime Vigenciafinal { get; set; }
        public string Versao { get; set; }
    }
}
