using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public partial class CompanyJobPoco : IPoco
    {
        public CompanyJobPoco()
        {
            ApplicantJobApplications = new HashSet<ApplicantJobApplicationPoco>();
            CompanyJobEducations = new HashSet<CompanyJobEducationPoco>();
            CompanyJobSkills = new HashSet<CompanyJobSkillPoco>();
            CompanyJobsDescriptions = new HashSet<CompanyJobDescriptionPoco>();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public bool IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyNavigation { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
    }
}
