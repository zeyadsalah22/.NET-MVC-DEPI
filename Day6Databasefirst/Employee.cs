using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Day6Mydemo.Models { 

    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Please Enter Your Name ")]
        [MaxLength(50, ErrorMessage = "Must Enter Name Less than 50 letters")]
        public string EmployeeName { get; set; } = null!;

        public string Job { get; set; } = null!;
        [Required(ErrorMessage = "Please Enter Your Salary")]
        public decimal Salary { get; set; }

        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        //[ForeignKey(nameof(Depart))] //
        [ForeignKey("Depart")]
        public int? DepartId { get; set; }

        public virtual Department? Depart { get; set; }
    }
}
