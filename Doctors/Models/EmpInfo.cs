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
    
    public partial class EmpInfo
    {
        public int ID { get; set; }
        public Nullable<int> DeptID { get; set; }
        public Nullable<int> JopID { get; set; }
        public int CostCenter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NatID { get; set; }
        public System.DateTime HireDate { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> MonthDays { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string EduGrade { get; set; }
        public Nullable<System.DateTime> GradeDate { get; set; }
        public string Degree { get; set; }
        public string Photo { get; set; }
        public string MState { get; set; }
        public Nullable<bool> MaritalState { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Shift { get; set; }
        public string Usr { get; set; }
    }
}
