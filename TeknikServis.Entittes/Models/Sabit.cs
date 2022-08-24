namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sabit")]
    public partial class Sabit
    {
        public byte SabitID { get; set; }

        [StringLength(30)]
        public string SabitAdi { get; set; }

        [StringLength(25)]
        public string Tur { get; set; }

        public bool? Aktif { get; set; }

        public int? Sira { get; set; }

        public bool? Banka { get; set; }

        public bool? Fatura { get; set; }
    }
}
