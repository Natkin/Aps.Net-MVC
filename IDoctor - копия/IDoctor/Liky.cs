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
    
    public partial class Liky
    {
        public int Id { get; set; }
        public string MedName { get; set; }
        public string Dosage { get; set; }
        public string Usage { get; set; }
        public Nullable<int> PatFk { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
