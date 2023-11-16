namespace WindowsFormsApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeaveRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeaveRequestID { get; set; }

        public int EmployeeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime LeaveStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime LeaveEndDate { get; set; }

        [Required]
        [StringLength(255)]
        public string LeaveReason { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
