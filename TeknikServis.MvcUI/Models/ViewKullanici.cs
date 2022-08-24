using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeknikServis.MvcUI.Models
{
    public class ViewKullanici
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage ="{0} Kodu Boş Geçilemez."),
        MinLength(4,ErrorMessage = "{0} En Az {1} Karakterden Oluşmalıdır."),
        MaxLength(64,ErrorMessage = "{0} En Fazla {1} Karakterden Oluşmalıdır.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "{0} boş geçilemez."),
        MinLength(4, ErrorMessage = "{0} En Az {1} Karakterden Oluşmalıdır."),
        MaxLength(32, ErrorMessage = "{0} En Fazla {1} Karakterden Oluşmalıdır."),
        DataType(DataType.Password)]
        public string Parola{ get; set; }

        public bool? Aktif { get; set; }
    }
}