# 📄 Personel Tayin Takip Sistemi

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş, personellerin tayin taleplerini yönetebileceği, yetkili kişilerin ise bu talepleri takip edebileceği bir **web tabanlı sistemdir**.

## 🧰 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- MSSQL Veritabanı
- Bootstrap 5 (responsive tasarım)
- Chart.js (grafikler)
- Session tabanlı kimlik doğrulama
- Loglama (Admin Panelde bütün loglar sayfalı şekilde gözükür)

---

## 👤 Kullanıcı Rolleri

### 👨‍💼 Personel
- Giriş yapar
- Tayin talebi oluşturur
- Kendi taleplerini görür
- Talebi onaylanıncaya ya da reddedilinceye kadar kendi talebini silebilir. 

### 🛡️ Yönetici (Admin)
- Add-Migration Init - Update-Database işleminden sonra Personeller tablosundan kullanıcı ekleyip Admin'i True yaptıktan sonra uygulamaya sicil ve şifre ile girilebilir.
- Yönetici Tüm talepleri görür
- Talepler üzerinde Onaylama ve Reddetme İşlemleri yapar. 
- İstatistik sayfasını görüntüler
- Pasta grafik ile dağılımı görür
- Loglama sayfasına erişebilir

---

## 🔐 Giriş Sistemi

- Sicil No + Şifre ile oturum açılır
- Session üzerinden kimlik doğrulama yapılır
- Admin yetkisi Session üzerinden kontrol edilir

---

## 🧾 Özellik Listesi

| Özellik                          | Açıklama                                   |
|----------------------------------|--------------------------------------------|
| Giriş-Çıkış Sistemi              | Session ile giriş yapılır ve korunur       |
| Tayin Talebi Oluşturma           | Personel kendi talebini oluşturur          |
| Yönetici Paneli                  | Talepler görüntülenebilir, silinir         |
| Filtreleme & Arama               | Ad, adliye, talep türüne göre arama        |
| Talep Durumu Yönetimi            | Onaylandı / Reddedildi / Bekliyor seçimi   |
| Loglama                          | Loglar database'e yazılır ve Panelde gözükür|
| İstatistik Sayfası               | Sayısal ve grafiksel özetler               |
| Pasta Grafik                     | Chart.js ile taleplerin görsel dağılımı    |
| Responsive Tasarım               | Tüm cihazlara uygun görünüm                |

---

## 🧪 Veritabanı

EF Core ile Code First yaklaşımı kullanılmıştır.  
Migration ve DB güncellemeleri için:


Add-Migration Init
Update-Database
