namespace WHTW.Wanda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }

        public virtual User User { get; set; }
    }
}
