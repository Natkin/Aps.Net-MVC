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
    
    public partial class ElQueue
    {
        public int Id { get; set; }
        public string V_Name { get; set; }
        public string V_Surame { get; set; }
        public string V_Midname { get; set; }
        public string V_Date { get; set; }
        public string V_hour { get; set; }
        public string V_minute { get; set; }
        public Nullable<int> PatFk { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
