using CareerCloud.Pocos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("System_Country_Codes")]
    public partial class SystemCountryCodePoco
    {
        public SystemCountryCodePoco()
        {
            ApplicantProfiles = new HashSet<ApplicantProfilePoco>();
            ApplicantWorkHistory = new HashSet<ApplicantWorkHistoryPoco>();
        }
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
    }
}
