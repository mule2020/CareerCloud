using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco : IPoco
    {

        [Column("Id")]
        public Guid Id { get; set; }

        [Key]
        [Column("Code")]
        [StringLength(10)] 
        public string Code { get; set; }

        [Column("Name")]
        [StringLength(100)] 
        public string Name { get; set; }
    }
}
