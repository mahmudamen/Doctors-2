//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Doctors.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usr
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int EmpID { get; set; }
        public int Role { get; set; }
        public string UserName { get; set; }
        public string UserKey { get; set; }
        public string EntryKey { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.TimeSpan CreatedTime { get; set; }
    }
}
