using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CareerCloud.Pocos
{
    internal class ApplicantProfilePoco : IPoco
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
        public string CountryCode { get; set; }

        [Column("State_Province_Code")]
        public string StateProvinceCode { get; set; }

        [Column("Street_Address")]
        public string StreetAddress { get; set; }

        [Column("City_Town")]
        public string CityTown { get; set; }

        [Column("Zip_Postal_Code")]
        public string ZipPostalCode { get; set; }

        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }


    }
}
