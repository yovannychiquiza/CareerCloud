using CareerCloud.Pocos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Security_Logins_Roles")]
    public partial class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public Guid Role { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }

        public virtual SecurityLoginPoco LoginNavigation { get; set; }
        public virtual SecurityRolePoco RoleNavigation { get; set; }
    }
}
