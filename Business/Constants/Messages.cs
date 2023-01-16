using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string BrandsListed = "Markalar Listelendi";
        public static string CheckCarNameOrDailyPrice = "Ürün İsmi > 2 ve Günlük Fiyatı > 0 olmalıdır.";
        public static string UsersListed = "Kullanıcılar Listelendi";
        public static string CustomersListed = "Müşteriler Listelendi";
        public static string RentalsListed = "Kiralamalar Listelendi";
        public static string CarIsRented = "Araba Kiradadır.";
        public static string CarsListed = "Arabalar Listelendi";
        public static string CarCountOfBrandError = "Bir Markadan En Fazla 10 Araç Olabilir.";
        public static string CarAlreadyExists = "Araba İsmi Zaten Mevcuttur.";
        public static string BrandLimitExceeded = "Marka Limiti Aşıldı!";
        public static string CarImagesListed = "Araba Resimleri Yüklendi";
        public static string CarImageLimitExceded = "Araba Resim Limiti Aşıldı!";
        public static string CarImagesNotFound = "Araba Resmi Mevcut Değildir.";
        public static string AuthorizationDenided = "Yetkiniz Yok!";

        public static string UserRegistered = "Kullanıcı Oluşturuldu.";

        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Şifre Hatası";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı Mevcut.";
        public static string AccessTokenCreated = "Token Oluşturuldu.";
    }
}
