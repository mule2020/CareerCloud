using CareerCloud.Pocos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginsPoco : IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Login")]
        public string Login { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; }

        [Column("Password_Update_Date")]
        public DateTime? PasswordUpdateDate { get; set; }

        [Column("Agreement_Accepted_Date")]
        public DateTime? AgreementAcceptedDate { get; set; }

        [Column("Is_Locked")]
        public bool IsLocked { get; set; }

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }

        [Column("Email_Address")]
        public string EmailAddress { get; set; }

        [Column("Phone_Number")]
        public string PhoneNumber { get; set; }

        [Column("Full_Name")]
        public string FullName { get; set; }

        [Column("Force_Change_Password")]
        public bool ForceChangePassword { get; set; }

        [Column("Prefferred_Language")]
        public string PreferredLanguage { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
