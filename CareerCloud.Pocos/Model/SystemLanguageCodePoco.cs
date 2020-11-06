﻿using CareerCloud.Pocos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("System_Language_Codes")]
    public partial class SystemLanguageCodePoco
    {
        public SystemLanguageCodePoco()
        {
            CompanyDescriptions = new HashSet<CompanyDescriptionPoco>();
        }
        [Key]
        public string LanguageID { get; set; }
        public string Name { get; set; }
        [Column("Native_Name")]
        public string NativeName { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}