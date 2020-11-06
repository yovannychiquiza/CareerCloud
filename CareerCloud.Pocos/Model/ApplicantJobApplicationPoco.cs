using CareerCloud.Pocos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Applicant_Job_Applications")]
    public partial class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public Guid Job { get; set; }
        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfilePoco ApplicantNavigation { get; set; }
        public virtual CompanyJobPoco JobNavigation { get; set; }
    }
}
