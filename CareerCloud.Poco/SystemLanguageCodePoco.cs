﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco: IPoco
    {
        
        [Column("id")]

        public Guid Id { get; set; }

        [Key]
        [Column("LanguageID")]
        public string LanguageID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Native_Name")]
        public string NativeName { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

    }
}
