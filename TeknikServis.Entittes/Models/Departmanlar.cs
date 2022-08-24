namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Departmanlar")]
    public partial class Departmanlar
    {
        [Key]
        public int DepartmanID { get; set; }


        public string DepartmanAdi { get; set; }


        public int FirmaID { get; set; }

        public string FirmaAdi { get; set; }


        public virtual ICollection<AltDepartmanlar> AltDepartmanlar { get; set; }





    }
}
