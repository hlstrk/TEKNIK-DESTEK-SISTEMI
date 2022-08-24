namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TanimGrup")]
    public partial class TanimGrup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TanimGrup()
        {
            Tanim = new HashSet<Tanim>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TanimGrupID { get; set; }

        [StringLength(50)]
        public string TanimGrupAdi { get; set; }

        public bool? Aktif { get; set; }

        public int? Sira { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tanim> Tanim { get; set; }
    }
}
