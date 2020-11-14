using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Roles")]
    public partial class SecurityRolePoco : IPoco
    {
        public SecurityRolePoco()
        {
            SecurityLoginsRoles = new HashSet<SecurityLoginsRolePoco>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Role { get; set; }
        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }

        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}
