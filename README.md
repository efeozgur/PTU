# 📄 Personel Tayin Takip Sistemi

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş, personellerin tayin taleplerini yönetebileceği, yetkili kişilerin ise bu talepleri takip edebileceği bir **web tabanlı sistemdir**.

## 🧰 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- MSSQL Veritabanı
- Bootstrap 5 (responsive tasarım)
- Chart.js (grafikler)
- BCrypt.Net (şifre güvenliği)
- Session tabanlı kimlik doğrulama
- Loglama (TXT dosyasına)

---

## 👤 Kullanıcı Rolleri

### 👨‍💼 Personel
- Giriş yapar
- Tayin talebi oluşturur
- Kendi taleplerini görür

### 🛡️ Yönetici (Admin)
- Varsayılan olarak admin sicili 96600 sifre 1'dir.
- Tüm talepleri görür
- Talep durumlarını değiştirir ve talepleri siler
- İstatistik sayfasını görüntüler
- Pasta grafik ile dağılımı görür

---

## 🔐 Giriş Sistemi

- Sicil No + Şifre ile oturum açılır
- Şifreler veritabanında **BCrypt ile hashlenmiş** olarak saklanır
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
| Şifre Hashleme                   | BCrypt ile güvenli parola saklama          |
| Loglama                          | Önemli işlemler `log.txt` dosyasına yazılır|
| İstatistik Sayfası               | Sayısal ve grafiksel özetler               |
| Pasta Grafik                     | Chart.js ile taleplerin görsel dağılımı    |
| Responsive Tasarım               | Tüm cihazlara uygun görünüm                |

---

## 🧪 Veritabanı

EF Core ile Code First yaklaşımı kullanılmıştır.  
Migration ve DB güncellemeleri için:


Add-Migration Init
Update-Database
