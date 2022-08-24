const { CallTracker } = require("assert/strict");
const { default: tr } = require("./tr");




var IslemSonucTurleri =
{
    Basarili: "BASARILI",
    Hata: "HATA",
    Uyari: "UYARI",
    Bilgi: "BILGI"
};

var IslemSonucKodlari =
{
    Basarili: 1,
    Hata: 2,
    Uyari: 3,
    Bilgi: 4
};


function MesajKapat() {
    $("#DivSonuc").removeClass();
    $("#DivSonuc").children("strong").text("");
    $("#DivSonuc").children("p").text("");
    //$("#DivSonuc").css({ "display": "none" });
    $("#DivSonuc").hide();
}


function FormGoster(url, modelId) {

    $("#" + modelId).children().children().children(".modal-body").empty();
    $("#" + modelId).children().children().children(".modal-body").load(url, function (data) {
        $("#" + modelId).show("modal");


    });
}



function ChatFormGoster(url, modelId) {

    if ($('#ChatModal').is(':visible')) {
        $("#ChatModal").hide();
    }
    else {
        $("#" + modelId).children().children().children(".modal-body").empty();
        $("#" + modelId).children().children().children(".modal-body").load(url, function (data) {
            $("#" + modelId).show("modal");


        });
    }

    
}
function FormGoster(url, modelId, callback) {
    $("#" + modelId).children().children().children(".modal-body").empty();
    $("#" + modelId).children().children().children(".modal-body").load(url, function (data) {
        $("#" + modelId).show("modal");
     

    });
}






function FormKapat(modelId) {
    $("#" + modelId).hide();
    $("#" + modelId).children().children().children(".modal-body").empty();



}

function ModalGoster(modalId) {
    $("#" + modalId).modal("show");
}

function ModalKapat(modalId) {
    $("#" + modalId).modal("hide");
}

function MesajGoster(islemSonucTuru, baslik, mesaj) {
    $("#DivSonuc").removeClass();
    $("#DivSonuc").children("strong").text("");
    $("#DivSonuc").children("p").text("");

    var _islemSonucTuru = "alert";
    if (islemSonucTuru === IslemSonucTurleri.Basarili) {
        _islemSonucTuru += " alert-success";
    }
    else if (islemSonucTuru === IslemSonucTurleri.Hata) {
        _islemSonucTuru += " alert alert-danger";
    }
    else if (islemSonucTuru === IslemSonucTurleri.Bilgi) {
        _islemSonucTuru += " alert alert-info";
    }
    else if (islemSonucTuru === IslemSonucTurleri.Uyari) {
        _islemSonucTuru += " alert alert-warning";
    }
    _islemSonucTuru += " fade in";
    $("#DivSonuc").addClass(_islemSonucTuru);
    $("#DivSonuc").children("strong").text(baslik);
    $("#DivSonuc").children("p").text(mesaj);
    //$("#DivSonuc").css({ "display": "block" });
    $("#DivSonuc").show();
}
function ModalBilgilendirme(baslik, aciklama) {
    $("#MdlBaslik").text(baslik);
    $("#MdlAciklama").text(aciklama);
    ModalGoster("MdlBilgilendirme");
    $('.modal-backdrop').remove();


}


function ModalUyari(baslik, aciklama) {
    $("#MdlBaslikUyari").text(baslik);
    $("#MdlAciklamaUyari").text(aciklama);
    ModalGoster("MdlUyari");
 


}

function MusteriKaydet() {
    if ($("#FirmaID").val() === "") {
        ModalBilgilendirme("Uyarı", "Müşteri Adı Giriniz.");
        return false;
    }
    if ($("#FirmaKodu").val() === "") {
        ModalBilgilendirme("Uyarı", "Müşteri Türü Giriniz.");
        return false;
    }
    if ($("#FirmaAdi").val() === "") {
        ModalBilgilendirme("Uyarı", "Firma Giriniz.");
        return false;
    }

    if ($("#FirmaGrubuID").val() === "") {
        ModalBilgilendirme("Uyarı", "Müşteri Türü Seçiniz.");
        return false;
    }
    if (($("#VergiKimlikNo").val() === "") || ($("#VergiKimlikNo").val().length!==10)) {
        ModalBilgilendirme("Uyarı", "Vergi Kimlik Numarası Hatalı");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Firma/FirmaKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmFirmaKarti").serialize()
    }).done(function (data) {


        ModalBilgilendirme("Bilgilendirme", data.Aciklama);
        $("#FirmaModal").hide();
        setTimeout(function () {

            location.reload(true);

        }, 2000); 
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}




function EnvanterMarkaSil(EnvanterMarkaID) {

    $.ajax({
        method: 'POST',
        url: '../Kategori/EnvanterMarkaSil',
        content: "application/json;",
        dataType: 'json',


        data: { EnvanterMarkaID: EnvanterMarkaID }
    }).done(function (data) {

        location.reload(true);
        
       

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}




function EnvanterKategoriSil(EnvanterKategoriID) {

    $.ajax({
        method: 'POST',
        url: '../Kategori/EnvanterKategoriSil',
        content: "application/json;",
        dataType: 'json',


        data: { EnvanterKategoriID: EnvanterKategoriID }
    }).done(function (data) {

        location.reload(true);



    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}



function FirmaSil(firmaId) {

    $.ajax({
        method: 'POST',
        url: '../Firma/FirmaSil',
        content: "application/json;",
        dataType: 'json',


        data: { id: firmaId }
    }).done(function (data) {


        if (data.IslemKodu === IslemSonucKodlari.Basarili) {
            ModalBilgilendirme("Bilgilendirme", data.Aciklama);
            //$("#FirmaModal").hide();
            FirmaListele();
        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);
        }

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function TeknikSil(TeknikServisID) {

    $.ajax({
        method: 'POST',
        url: '../Teknik/TeknikSil',
        content: "application/json;",
        dataType: 'json',
        data: { id: TeknikServisID }
    }).done(function (data) {


        if (data.IslemKodu === IslemSonucKodlari.Basarili) {
            ModalBilgilendirme("Bilgilendirme", data.Aciklama);
            $("#TeknikModal").hide();
            $("#TeknikSilModal").hide();
            TeknikListele();


        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);
        }

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
    $("#TeknikSilModal").hide();
    TeknikListele();
}


function TeklifSil(TeklifID) {

    $.ajax({
        method: 'POST',
        url: '../Teklif/TeklifSil',
        content: "application/json;",
        dataType: 'json',
        data: { id: TeklifID }
    }).done(function (data) {


        if (data.IslemKodu === IslemSonucKodlari.Basarili) {
            ModalBilgilendirme("Bilgilendirme", data.Aciklama);
            $("#TeklifModal").hide();
            $("#myModel").hide();
            TeklifListele();


        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);
        }

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}






function TeklifFormuOlustur(id) {

    $.ajax({
        method: 'GET',
        url: '../Teklif/TeklifFormuOlustur/' + id,
        content: "application/html;",
        dataType: 'html'

    });
}
                                               
                                                

function StokKategoriSil(StokKategoriID) {

    $.ajax({
        method: 'POST',
        url: '../Kategori/StokKategoriSil',
        content: "application/json;",
        dataType: 'json',
        data: { StokKategoriID: StokKategoriID }
    }).done(function (data) {

        if (data.IslemKodu === 1) {
            ModalBilgilendirme("İşlem Başarılı!", data.Aciklama);
            setTimeout(function () {
                location.reload(true);
            }, 2000);
          
        }
        else if (data.IslemKodu === 2) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

       

  






    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}



function StokSilMain(StokID) {

    $.ajax({
        method: 'POST',
        url: '../Stok/StokSil',
        content: "application/json;",
        dataType: 'json',
        data: { StokID: StokID }
    }).done(function (data) {

        if (data.IslemKodu === 1) {
            ModalBilgilendirme("İşlem Başarılı!", data.Aciklama);
            setTimeout(function () {
                location.reload(true);
            }, 2000);

        }
        else if (data.IslemKodu === 2) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }










    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function StokMarkaSil(StokMarkaID) {

    $.ajax({
        method: 'POST',
        url: '../Kategori/StokMarkaSil',
        content: "application/json;",
        dataType: 'json',
        data: { StokMarkaID: StokMarkaID }
    }).done(function (data) {



        location.reload(true);






    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function StokTuruSil(StokTuruID) {

    $.ajax({
        method: 'POST',
        url: '../Kategori/StokTuruSil',
        content: "application/json;",
        dataType: 'json',
        data: { StokTuruID: StokTuruID }
    }).done(function (data) {



        location.reload(true);






    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}

function IslemSil(IslemServisID) {

    $.ajax({
        method: 'POST',
        url: '../Anasayfa/IslemSil',
        content: "application/json;",
        dataType: 'json',
        data: { id: IslemServisID }
    }).done(function (data) {


        IslemListele();







    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}

function TeknikReddet(TeknikServisID) {

    $.ajax({
        method: 'POST',
        url: '../Anasayfa/TeknikReddet',
        content: "application/json;",
        dataType: 'json',
        data: { id: TeknikServisID }
    }).done(function (data) {

        ModalBilgilendirme("", "İşlem Reddedildi!");
        setTimeout(function () {
            location.reload(true);
        }, 2000);








    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}

function TeknikOnayla(TeknikServisID) {

    $.ajax({
        method: 'POST',
        url: '../Anasayfa/TeknikOnayla',
        content: "application/json;",
        dataType: 'json',
        data: { id: TeknikServisID }
    }).done(function (data) {

        ModalBilgilendirme("", "İşlem Onaylandı.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
       







    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}




$("#GarantiVar").change(
    function (e) {
        $("#GarantiYok").attr("checked", false);

    })





function ToDoListele(sayfa) {
 
    $.ajax({
        method: 'GET',
        url: '../Anasayfa/ToDoList/' + sayfa,
        content: "application/html;",
        dataType: 'html',
        data: { sayfa: sayfa }
    }).done(function (data) {
        $("#PanelToDoListAnasayfa").html(data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}





function FirmaListele() {
    var kid = $("#FirmaID").val();
    var sadi = $("#FirmaAdi").val();
    $.ajax({
        method: 'GET',
        url: '../Firma/FirmaListesi',
        content: "application/html;",
        dataType: 'html',
        data: { FirmaID: kid, firmaAdi: sadi }
    }).done(function (data) {
        $("#PanelFirmaListe").html(data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


$(document).ready(function () {
    $('.selectpicker').selectpicker({
        liveSearch: true,
        showSubtext: true
    });
});




function TeknikListeleMain() {
    var kid = $("#TeknikServisID").val();

    $.ajax({
        method: 'GET',
        url: '../Teknik/TeknikListesi',
        content: "application/html;",
        dataType: 'html',
        data: { TeknikServisID: kid }
    }).done(function (data) {

        $("#PanelTeknikListe").html(data);




    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }



    )
    $("#PanelTeknikListe").html(data);
    ;
}











function TeknikListele() {
    var kid = $("#TeknikServisID").val();

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/TeknikListesiDB',
        content: "application/html;",
        dataType: 'html',
        data: { TeknikServisID: kid }
    }).done(function (data) {

        $("#PanelTeknikListeAnasayfa").html(data);




    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }



    )
    $("#PanelTeknikListeAnasayfa").html(data);
    ;
}


function IslemListele() {


    $.ajax({
        method: 'GET',
        url: '../Teknik/IslemListesiKart',
        content: "application/html;",
        dataType: 'html',

    }).done(function (data) {



    });


   
}

function TeklifDetayListele() {


    $.ajax({
        method: 'GET',
        url: '../Teklif/TeklifDetayListesi',
        content: "application/html;",
        dataType: 'html',

    }).done(function (data) {

        $("#PanelTeklifDetayListesi").html(data);

    });



}

function IslemListeleKart() {
    var kid = $("#IslemServisID").val();

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/IslemListesiKart',
        content: "application/html;",
        dataType: 'html',
        data: { IslemServisID: kid }
    }).done(function (data) {
        $("#PanelIslemListeKart").html(data);


    })
}


function EnvanterListeleKart() {
    var kid = $("#EnvanterID").val();

    $.ajax({
        method: 'GET',
        url: '../Kullanici/EnvanterListesiKart',
        content: "application/html;",
        dataType: 'html',
        data: { EnvanterID: kid }
    }).done(function (data) {
        $("#PanelEnvanterListeKart").html(data);


    })
}
function EnvanterListeleKartFirma() {
    var kid = $("#EnvanterID").val();

    $.ajax({
        method: 'GET',
        url: '../Firma/EnvanterListesiKart',
        content: "application/html;",
        dataType: 'html',
        data: { EnvanterID: kid }
    }).done(function (data) {
        $("#PanelEnvanterListeKart").html(data);


    })
}



function NotListele() {
    var kid = $("#NotID").val();

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/NotListesi',
        content: "application/html;",
        dataType: 'html',
        data: { NotID: kid }
    }).done(function (data) {
        $("#PanelNotListeAnasayfa").html(data);


    })
}

function TeknikKontrolList() {


    $.ajax({
        method: 'GET',
        url: '../Teknik/IslemKontrolList',
        content: "application/html;",
        dataType: 'html'

    }).done(function (data) {
        $("#PanelIslemListeKartTeknik").html(data);


    })
}

function IslemListeleKartTeknik() {


    $.ajax({
        method: 'GET',
        url: '../Teknik/IslemListesiKart',
        content: "application/html;",
        dataType: 'html'
       
    }).done(function (data) {
        $("#PanelIslemListeKartTeknik").html(data);


    })
}
function IslemListeleSayfa() {
    var kid = $("#IslemServisID").val();

    $.ajax({
        method: 'GET',
        url: '../Islem/IslemListesi',
        content: "application/html;",
        dataType: 'html',
        data: { IslemServisID: kid }
    }).done(function (data) {
        $("#PanelIslemListe").html(data);


    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}


function IslemListele() {
    var kid = $("#IslemServisID").val();

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/IslemListesi',
        content: "application/html;",
        dataType: 'html',
        data: { IslemServisID: kid }
    }).done(function (data) {
        $("#PanelIslemListe").html(data);
        location.reload(true);

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}

function TeklifListele() {
    var kid = $("#TeklifID").val();

    $.ajax({
        method: 'GET',
        url: '../Teklif/TeklifListesi',
        content: "application/html;",
        dataType: 'html',
        data: { TeklifID: kid }
    }).done(function (data) {
        $("#PanelTeklifListe").html(data);
        location.reload(true);

    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}





function GorevKaydet() {

    if ($("#GorevB").val() === "") {
        ModalBilgilendirme("Uyarı", "Görev konusu alanı boş bırakılamaz...");
        return false;
    }
    if ($("#GorevI").val() === "") {
        ModalBilgilendirme("Uyarı", "Gorev İçeriği Alanı Boş Bırakılamaz...");
        return false;
    }




    $.ajax({
        method: 'POST',
        url: '../Gorev/GorevKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmGorevEkleKartiDB").serialize()
    }).done(function (data) {
        $("#gorevkarti").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);


         location.reload(true);

       



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function KullaniciKaydet() {
   
    if ($("#AdSoyad").val() === "") {
        ModalBilgilendirme("Uyarı", "Ad Soyad Giriniz.");
        return false;
    }
    if ($("#EMail").val() === "") {
        ModalBilgilendirme("Uyarı", "E-Posta Giriniz..");
        return false;
    }

    if ($("#cepno").val() === "") {
        ModalBilgilendirme("Uyarı", "Bir Numara Sağlayın.");
        return false;
    }

    if ($("#CalistigiFirma").val() === "") {
        ModalBilgilendirme("Uyarı", "Nerede Çalışıyor Sağlayın.");
        return false;
    }

   

   


    $.ajax({
        method: 'POST',
        url: '../Kullanici/KullaniciKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmKullaniciKarti").serialize()
    }).done(function (data) {
        $("#KullaniciModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}


function StokKaydet() {

   






    $.ajax({
        method: 'POST',
        url: '/Stok/StokKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmStokKarti").serialize()
    }).done(function (data) {
        $("#StokEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}







function EnvanterKategoriKaydet() {




    $.ajax({
        method: 'POST',
        url: '/Kategori/EnvanterKategoriEkleKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmEnvanterKategoriEkleKarti").serialize()
    }).done(function (data) {
        $("#ETurEkleModal").hide();
       

        location.reload(true);



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function EnvanterMarkaKaydet() {




    $.ajax({
        method: 'POST',
        url: '/Kategori/EnvanterMarkaEkleKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmEnvanterMarkaEkleKarti").serialize()
    }).done(function (data) {
        $("#ETurEkleModal").hide();


        location.reload(true);



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function StokMarkaKaydet() {




    $.ajax({
        method: 'POST',
        url: '/Kategori/StokMarkaEkleKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmStokMarkaEkleKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);
       
        

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}



function StokKategoriKaydet() {




    $.ajax({
        method: 'POST',
        url: '/Kategori/StokKategoriEkleKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmStokKategoriEkleKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

  
        location.reload(true);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); 
}



function StokTurKaydet() {




    $.ajax({
        method: 'POST',
        url: '/Kategori/StokTuruEkleKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmStokTuruEkleKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}


function KullaniciSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Kullanici/KullaniciSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmKullaniciSilKarti").serialize()

    }).done(function (data) {

        $("#KullaniciSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Kullanıcı başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}


function IslemSilK(id) {

    $.ajax({
        method: 'GET',
        url: '../Teknik/IslemSil/' + id,
        content: "application/html;",
        dataType: 'html'
      
    });
   

        
}


function EnvanterSilK(id) {

    $.ajax({
        method: 'GET',
        url: '../Kullanici/EnvanterSil/' + id,
        content: "application/html;",
        dataType: 'html'

    });



}


function NotSilK(id) {

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/NotSil/' + id,
        content: "application/html;",
        dataType: 'html'

    });

    NotListele();

}


function removeAnc(id) {

    $.ajax({
        method: 'GET',
        url: '../Anasayfa/NotSil/' + id,
        content: "application/html;",
        dataType: 'html'

    });

    location.reload(true);

}







function KullaniciListele() {

    var kid = $("#KullaniciID").val();

    $.ajax({
        method: 'GET',
        url: '../Kullanici/KullaniciListesi',
        content: "application/html;",
        dataType: 'html',
        data: { KullaniciID: kid }
    }).done(function (data) {
        $("#PanelKullaniciListe").html(data);
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    });
}
function IslemKaydet() {



    $.ajax({
        method: 'POST',
        url: '../Islem/IslemKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmIslemKarti").serialize()

    }).done(function (data) {
        if (data.IslemKodu === IslemSonucKodlari.Basarili) {
            ModalBilgilendirme("Bilgilendirme", data.Aciklama);

            setTimeout(function () {
                location.reload(true);
            }, 2000);
        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);
            setTimeout(function () {
                location.reload(true);
            }, 2000);
        }
        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });

}

function IslemKaydetAnasayfa() {



    $.ajax({
        method: 'POST',
        url: '../Anasayfa/IslemKarti',
        content: "application/json; charset=UTF-8;",
        dataType: 'json',
        data: JSON.stringify($("#FrmIslemKarti").serialize())

    });

}


function EnvanterKaydetKullanici() {



    $.ajax({
        method: 'POST',
        url: '../Kullanici/EnvanterKarti',
        content: "application/json; charset=UTF-8;",
        dataType: 'json',
        data: JSON.stringify($("#FrmEnvanterKarti").serialize())

    });

}













function BildirimKaydet() {



    $.ajax({
        method: 'POST',
        url: '../Bildirim/BildirimKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmBildirimKarti").serialize()

    }).done(function (data) {

        $("#BildirimkEkleModal").hide();

        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        setTimeout(function () {
            location.reload(true);
        }, 2000);




        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}

function TeknikKaydet() {



    $.ajax({
        method: 'POST',
        url: '../Anasayfa/TeknikKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeknikKarti").serialize()

    }).done(function (data) {

        $("#TeknikEkleModal").hide();

        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        setTimeout(function () {
            location.reload(true);
        }, 2000);
       



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}


function EnvanterKaydetMain() {


    if ($("#Aciklama").val() === "") {
        ModalBilgilendirme("Uyarı", "Bir açıklama girin.");
        return false;
    }

    if ($("#Firma").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen firma listesinden bir firma seçin.");
        return false;
    }


    if ($("#Turu").val() === "") {
        ModalBilgilendirme("Uyarı", "Kategori seçmediniz.");
        return false;
    }

    $.ajax({
        method: 'POST',
        url: '../Envanter/EnvanterKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmEnvanterKarti").serialize()

    }).done(function (data) {

        $("#EnvanterModal").hide();

        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        setTimeout(function () {
            location.reload(true);
        }, 2000);




        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}




function TeknikKaydetDB() {
    if ($("#KategoriDB").val() === "") {
        ModalBilgilendirme("Uyarı", "lütfen bir kategori seçiniz. Bu sorunun kaynağını anlamamızı daha da kolaylaştıracaktır...");
        return false;
    }

    if ($("#NotDB").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen talebinize bir açıklama ekleyin.");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Anasayfa/TeknikKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeknikKartiDB").serialize()

    }).done(function (data) {

       

        ModalBilgilendirme("Bilgilendirme", "Destek Talebiniz Başarıyla Oluşturuldu!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);




        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )
        ;
}


function TeknikKaydetMobil() {
    if ($("#Kategorimobil").val() === "") {
        ModalBilgilendirme("Uyarı", "lütfen bir kategori seçiniz. Bu sorunun kaynağını anlamamızı daha da kolaylaştıracaktır...");
        return false;
    }

    if ($("#Notmobil").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen talebinize bir açıklama ekleyin.");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Anasayfa/TeknikKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeknikKartiMobil").serialize()

    }).done(function (data) {



        ModalBilgilendirme("Bilgilendirme", "Destek Talebiniz Başarıyla Oluşturuldu!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);




        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )
        ;
}



function DuyuruKaydet() {


    if ($("#DuyuruB").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen duyurunuza bir başlık ekleyin!.");
        return false;
    }

    if ($("#DuyuruI").val() === "") {
        ModalBilgilendirme("Uyarı", "Duyuru içeriği boş geçilemez!");
        return false;
    }
    


    $.ajax({
        method: 'POST',
        url: '../Anasayfa/NotEkle',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmDuyuruEkleKartiDB").serialize()

    }).done(function (data) {

        ModalBilgilendirme("Bilgilendirme", "Duyuru Başarıyla Eklendi!");


        setTimeout(function () {
            location.reload(true);
        }, 2000);








        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}


function NotKaydetDB() {
    

    if ($("#NotDB2").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen not alanını doldurun.");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Anasayfa/NotEkle',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmNotEkleKartiDB").serialize()

    }).done(function (data) {




        setTimeout(function () {
            location.reload(true);
        }, 2000);


       





        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}

function GorevKaydetDB() {


    if ($("#NotDB3").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen yapılacak alanını doldurun.");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Anasayfa/NotEkle',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmYapilacakEkleKarti").serialize()

    }).done(function (data) {



        setTimeout(function () {
            location.reload(true);
        }, 2000);
   








        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}



function TeknikKaydetMain() {



    $.ajax({
        method: 'POST',
        url: '../Teknik/TeknikKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeknikKarti").serialize()

    }).done(function (data) {

        $("#TeknikModal").hide();

        ModalBilgilendirme("Bilgilendirme", data.Aciklama);
        location.reload(true);



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}



function TeklifKaydetMain() {



    $.ajax({
        method: 'POST',
        url: '../Teklif/TeklifKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeklifKarti").serialize()

    }).done(function (data) {

        $("#TeklifModal").hide();

        ModalBilgilendirme("Bilgilendirme", data.Aciklama);
        location.reload(true);



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    )

        ;
}



function TeklifOlusturNB() {
  
        FormGoster('../Teklif/TeklifKarti', 'TeklifModal');

  


  

   
    

        
}











function TeknikSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Teknik/TeknikSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeknikSilKarti").serialize()

    }).done(function (data) {

        $("#TeknikSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Kayıt başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}




function GorevDegistir(GorevID) {



    $.ajax({
        method: 'POST',
        url: '../Anasayfa/StatuDegistirGorev/',
        content: "application/html;",
        dataType: 'html',
        data: { id: GorevID }


    }).done(function (data) {

        location.reload(true);

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}

function ToDoDegistir(NotID) {



    $.ajax({
        method: 'GET',
        url: '../Anasayfa/StatuDegistir/' + NotID,
        content: "application/html;",
        dataType: 'html'

    }).done(function (data) {



        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}




function ToDoSil(NotID) {



    $.ajax({
        method: 'GET',
        url: '../Anasayfa/ToDoSil/' + NotID,
        content: "application/html;",
        dataType: 'html'

    }).done(function (data) {




    
            location.reload(true);
  





        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}

function StoktanIsleme(StokID) {



    $.ajax({
        method: 'GET',
        url: '../Stok/StokGetir/',
        content: "application/html;",
        dataType: 'html',
        data: { StokID: StokID }

    }).done(function (data) {


       


    






        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    );
}



function TeknikGeriAl(TeknikServisID) {



    $.ajax({
        method: 'GET',
        url: '../Teknik/TeknikGeriAl/',
        content: "application/html;",
        dataType: 'html',
        data: { TeknikServisID: TeknikServisID },

    }).done(function (data) {

        location.reload(true);
;
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");

    }


    );
}

function KullaniciGeriAl(KullaniciID) {



    $.ajax({
        method: 'GET',
        url: '../Kullanici/KullaniciGeriAl/',
        content: "application/html;",
        dataType: 'html',
        data: { KullaniciID: KullaniciID },

    }).done(function (data) {





        location.reload(true);






        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
       
    }


    );
}


function EnvanterSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Envanter/EnvanterSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmEnvanterSilKarti").serialize()

    }).done(function (data) {

        $("#EnvanterSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Envanter kaydı başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}

function TanimSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Teknik/TanimSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTanimSilKarti").serialize()

    }).done(function (data) {

        $("#TanimSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Kategori başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}



function TeknikKontrol(TeknikServisID) {


    $.ajax({
        method: 'GET',
        url: '../Teknik/TeknikIslemCount/',
        content: "application/html;",
        dataType: 'html',
        data: { TeknikServisID: TeknikServisID }
    }).done(function (data) {
   
        console.log(data);
        if (data == 0) {
            FormGoster('../Teknik/TeknikSil/?TeknikServisID='+TeknikServisID, 'TeknikSilModal');

        }
        else {
            FormGoster('../Teknik/TeknikIslemleri/?TeknikServisID='+TeknikServisID, 'TeknikSilModal');
        }
     
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });
}







function ArizaTuruSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Kategori/ArizaTuruSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmArizaTuruSilKarti").serialize()

    }).done(function (data) {

        $("#TanimSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Kategori başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}


function DurumTuruSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Kategori/DurumSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmDurumTuruSilKarti").serialize()

    }).done(function (data) {

        $("#TanimSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Kategori başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}

function FirmaSilMain() {



    $.ajax({
        method: 'POST',
        url: '../Firma/FirmaSil',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmFirmaSilKarti").serialize()

    }).done(function (data) {

        $("#FirmaSilModal").hide();

        ModalBilgilendirme("Bilgilendirme", "Firma başarıyla silindi!");

        setTimeout(function () {
            location.reload(true);
        }, 2000);


        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
        setTimeout(function () {
            location.reload(true);
        }, 2000);
    }


    )

        ;
}










function TeklifKaydet() {

    $.ajax({
        method: 'POST',
        url: '../Teklif/TeklifKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTeklifKarti").serialize()
    }).done(function (data) {
        if (data.IslemKodu === IslemSonucKodlari.Basarili) {
            ModalBilgilendirme("Bilgilendirme", data.Aciklama);
            $("#TeklifModal").hide();
            location.reload(true);
        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);
            location.reload(true);
        }
        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}



function TanimKaydet() {



    if ($("#TanimAdi").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen bir kategori girin...");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Teknik/TanimKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmTanimKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}

function ArizaTuruKaydet() {



    if ($("#ArizaTuru").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen bir Arıza Türü girin...");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Kategori/ArizaKategoriEkleFK',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmArizaTuruEkleKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}


function DurumTuruKaydet() {



    if ($("#DurumAdi").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen bir Durum girin...");
        return false;
    }


    $.ajax({
        method: 'POST',
        url: '../Kategori/DurumKategoriEkleFK',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmDurumEkleKarti").serialize()
    }).done(function (data) {
        $("#TanimEkleModal").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}

















































function sifresifirla() {
    if ($("#EMail").val() === "") {
        ModalBilgilendirme("Uyarı", "Lütfen kutucuğa geçerli bir E-posta adresi girin..");
        return false;
    }
    




    $.ajax({
        method: 'POST',
        url: '../Kullanici/SifreSifirla',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmKullaniciSifirlaKarti").serialize()
    }).done(function (data) {
        $("#KullaniciModalSifirla").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

    

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    });

}





function SifreDegisAnasayfa() {
    
    

    if ($("#Parola").val() !==$("#ParolaTekrar").val() ) {
        ModalBilgilendirme("Uyarı", "Girdiğiniz şifreler uyuşmuyor!");
        return false;
    }


   
   

    $.ajax({
        method: 'POST',
        url: '../Anasayfa/SifreDegisKarti',
        content: "application/json;",
        dataType: 'json',
        data: $("#FrmSifreDegisKarti").serialize()
    }).done(function (data) {
        $("#SifreDegis").hide();
        ModalBilgilendirme("Bilgilendirme", data.Aciklama);

        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('fileInput');
        //Iterating through each files selected in fileInput
        for (i = 0; i < fileInput.files.length; i++) {
            //Appending each file to FormData object
            formdata.append(fileInput.files[i].name, fileInput.files[i]);
        }
        //Creating an XMLHttpRequest and sending
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Anasayfa/DosyaYukle');
        xhr.send(formdata);
        location.reload(true);

        if (data.IslemKodu === IslemSonucKodlari.Basarili) {

        }
        else if (data.IslemKodu === IslemSonucKodlari.Hata) {
            ModalBilgilendirme("Hata", data.Aciklama);

        }

        //alert("Id:" + data.Data);
    }).fail(function () {
        console.log("Hata oluştu.");
        ModalBilgilendirme("Hata", "Hata Oluştu.");
    }); d
}