namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointments
    {
        [Key]
        public int UniqueID { get; set; }

        public int? Type { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndDate { get; set; }

        public bool? AllDay { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public string Description { get; set; }

        public int? Status { get; set; }

        public int? Label { get; set; }

        public int? ResourceID { get; set; }

        public string ResourceIDs { get; set; }

        public string ReminderInfo { get; set; }

        public string RecurrenceInfo { get; set; }

        public string TimeZoneId { get; set; }

        public string CustomField1 { get; set; }
    }
}
