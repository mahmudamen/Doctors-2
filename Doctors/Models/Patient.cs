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
    
    public partial class Patient
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<int> Serv { get; set; }
        public string ServName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<int> PatienState { get; set; }
        public Nullable<System.DateTime> StartWait { get; set; }
        public int Sorted { get; set; }
        public int ShiftID { get; set; }
    
        public virtual ServList ServList { get; set; }
        public virtual ShiftList ShiftList { get; set; }
        public virtual StateList StateList { get; set; }
    }
}
