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
    
    public partial class Doctors
    {
        public Doctors()
        {
            this.Patient = new HashSet<Patient>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cabinet { get; set; }
        public string MiddleName { get; set; }
    
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
