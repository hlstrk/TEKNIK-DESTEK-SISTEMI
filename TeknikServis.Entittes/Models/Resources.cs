namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Resources
    {
        [Key]
        public int UniqueID { get; set; }

        public int ResourceID { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; }

        public int? Color { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public string CustomField1 { get; set; }
    }
}
