namespace WHTW.Wanda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profile")]
    public partial class Profile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public CivilStatus CivilStatus { get; set; }

        public Racial Racial { get; set; }

        public int QtChildren { get; set; }

        public int SmokeFrequency { get; set; }

        public int DrinkFrequency { get; set; }

        public double Pound { get; set; }

        public double Height { get; set; }

        public bool Diabete { get; set; }

        public bool Hypertension { get; set; }

        public DateTime LastScouting { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Alergies { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Medicaments { get; set; }

        [Required]
        [StringLength(100)]
        public string SUSCardId { get; set; }

        [Required]
        [StringLength(50)]
        public string RG { get; set; }

        [Required]
        [StringLength(255)]
        public string MothersName { get; set; }

        public bool IsPregnant { get; set; }

        public DateTime PregnantDate { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Diseases { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string FamilyHistory { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string AttendenceHistory { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ProcedureHistory { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ExtraInfos { get; set; }

        public int Age { get; set; }

        public virtual User User { get; set; }
    }
}
