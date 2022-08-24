using Newtonsoft.Json;

namespace TeknikServis.MvcUI.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IslemSonucModel
    {
        [JsonProperty]
        public int IslemKodu { get; set; }
        [JsonProperty]
        public string Baslik { get; set; }
        [JsonProperty]
        public string Aciklama { get; set; }
        [JsonProperty]
        public string AciklamaDetay { get; set; }
        [JsonProperty]
        public object Data { get; set; }
        [JsonProperty]
        public object Data2 { get; set; }

        public IslemSonucModel()
        {

        }



        public IslemSonucModel(int islemKodu, string baslik, string aciklama, object data)
        {
            this.IslemKodu = islemKodu;
            this.Baslik = baslik;
            this.Aciklama = aciklama;
           
            this.Data = data;
        }

        public IslemSonucModel(IslemSonucuEnum.IslemSonucKodlari islemSonucKodu, string aciklama = "", string aciklamaDetay = "", object data = null, object data2=null)
        {
            this.IslemKodu = (int)islemSonucKodu;
            this.Baslik = islemSonucKodu.ToDescriptionString();
            this.Aciklama = aciklama;
            this.AciklamaDetay = aciklamaDetay;
            this.Data = data;
            this.Data2 = data2;
        }



    }
}