//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDoctor
{
    using System;
    using System.Collections.Generic;
    
    public partial class B_C_S_F
    {
        public int Pk { get; set; }
        public string Ampicillin { get; set; }
        public string CLoXacillin { get; set; }
        public string Cefalexin { get; set; }
        public string Cefotaxim { get; set; }
        public string Ceftriaxon { get; set; }
        public string Cefoxitin { get; set; }
        public string Ciprofioxacin { get; set; }
        public string Chloramphenicol { get; set; }
        public string Clindamycin { get; set; }
        public string Tetracyein { get; set; }
        public string Doxicyclin { get; set; }
        public string Ampiclox { get; set; }
        public string Lincoycin { get; set; }
        public string Amoxyeillin { get; set; }
        public string Refampicin { get; set; }
        public string Erthromyein { get; set; }
        public string Azthromycin { get; set; }
        public string Trimcthopim { get; set; }
        public string Amoxiclave { get; set; }
        public string Streptomyein { get; set; }
        public string Gentamyein { get; set; }
        public string Amikacin { get; set; }
        public string Furadantin { get; set; }
        public string Nelidixic_acid { get; set; }
        public string Penicillin { get; set; }
        public string Vancomycin { get; set; }
        public Nullable<int> PatFk { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
