﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public partial class ApplicantResumePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Resume { get; set; }
        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; }

        public virtual ApplicantProfilePoco ApplicantNavigation { get; set; }
    }
}
