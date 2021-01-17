using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public partial class SystemCountryCodePoco :  IPoco
    {
        public SystemCountryCodePoco()
        {
            ApplicantProfiles = new HashSet<ApplicantProfilePoco>();
            ApplicantWorkHistories = new HashSet<ApplicantWorkHistoryPoco>();
        }
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        [NotMapped]
        public Guid Id { get ; set ; }
    }
}
