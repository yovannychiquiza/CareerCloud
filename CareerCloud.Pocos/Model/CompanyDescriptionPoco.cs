using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public partial class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Language_Id")]
        public string LanguageId { get; set; }
        [Column("Company_Name")]
        public string CompanyName { get; set; }
        [Column("Company_Description")]
        public string CompanyDescription { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyNavigation { get; set; }
        public virtual SystemLanguageCodePoco Language { get; set; }
    }
}
