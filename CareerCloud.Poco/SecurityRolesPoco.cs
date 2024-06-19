using CareerCloud.Pocos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Roles")]
    public class SecurityRolesPoco : IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }
    }
}
