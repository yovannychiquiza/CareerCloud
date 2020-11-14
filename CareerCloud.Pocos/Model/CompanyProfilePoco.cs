﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Profiles")]
    public partial class CompanyProfilePoco : IPoco
    {
        public CompanyProfilePoco()
        {
            CompanyDescriptions = new HashSet<CompanyDescriptionPoco>();
            CompanyJobs = new HashSet<CompanyJobPoco>();
            CompanyLocations = new HashSet<CompanyLocationPoco>();
        }
        [Key]
        public Guid Id { get; set; }
        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }
        [Column("Company_Website")]
        public string CompanyWebsite { get; set; }
        [Column("Contact_Phone")]
        public string ContactPhone { get; set; }
        [Column("Contact_Name")]
        public string ContactName { get; set; }
        [Column("Company_Logo")]
        public byte[] CompanyLogo { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public virtual ICollection<CompanyJobPoco> CompanyJobs { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
    }
}
