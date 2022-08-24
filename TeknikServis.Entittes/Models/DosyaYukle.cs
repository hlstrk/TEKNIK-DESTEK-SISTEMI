using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.Models
{
    public class DosyaYukleModal
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Resim Ekle")]
        [Required(ErrorMessage = "Eklenecek resmi seçin...")]
        public string dosya { get; set; }

    }
}
