﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column("login")]
        public Guid Login { get; set; }

        [Column("Current_Salary")]
        public decimal? CurrentSalary { get; set; }

        [Column("Current_Rate")]
        public decimal? CurrentRate { get; set;}

        [Column("Currency")]
        public string Currency { get; set; }

        [Column("Country_Code")]
        public string Country { get; set; }

        [Column("State_Province_Code")]
        public string Province { get; set; }

        [Column("Street_Address")]
        public string Street { get; set; }

        [Column("City_Town")]
        public string City { get; set; }

        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; }

        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }


    }
}
