﻿using CareerCloud.Pocos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Profiles")]
    public class CompanyProfilesPoco : IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }

        [Column("Company_Website")]
        public string CompanyWebsite { get; set; }

        [Column("Contact_Phone")]
        public string ContactPhone { get; set; }

        [Column("Contact_Name")]
        public string ContactName { get; set; }

        [Column("Company_Logo")]
        public byte[] CompanyLogo { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
