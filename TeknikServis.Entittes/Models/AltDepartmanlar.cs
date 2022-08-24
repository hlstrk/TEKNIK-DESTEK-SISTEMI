namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AltDepartmanlar")]
    public partial class AltDepartmanlar
    {
        [Key]
        public int AltDepartmanID { get; set; }


        public string AltDepartmanAdi { get; set; }


        public int DepartmanID { get; set; }



        public virtual ICollection<Bolumler> Bolumler { get; set; }




    }
}
