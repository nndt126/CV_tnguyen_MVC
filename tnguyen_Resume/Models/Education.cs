//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tnguyen_Resume.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Education
    {
        public int ID { get; set; }
        public string EducationTitle { get; set; }
        public string EducationInfo { get; set; }
        public Nullable<System.DateTime> EducationDate { get; set; }
        public string EducationDetail { get; set; }
        public Nullable<int> ID_User { get; set; }
    
        public virtual Information Information { get; set; }
    }
}
