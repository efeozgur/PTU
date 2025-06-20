-- Veritabanı Oluşturma
CREATE DATABASE [PTUDb]
USE [PTUDb]
GO


-- Adliyeler Tablosu
CREATE TABLE [dbo].[Adliyeler](
    [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Ad] [nvarchar](max) NOT NULL
)
GO

-- Loglar Tablosu
CREATE TABLE [dbo].[Loglar](
    [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PersonelId] [int] NULL,
    [Islem] [nvarchar](max) NOT NULL,
      NOT NULL,
    [Aciklama] [nvarchar](max) NOT NULL
)
GO

-- Personeller Tablosu
CREATE TABLE [dbo].[Personeller](
    [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
      NOT NULL,
      NOT NULL,
      NOT NULL,
      NOT NULL,
    [IsAdmin] [bit] NOT NULL DEFAULT 0
)
GO

-- Tayin Talepleri Tablosu
CREATE TABLE [dbo].[TayinTalepleri](
    [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PersonelId] [int] NOT NULL,
    [TalepTuru] [nvarchar](max) NOT NULL,
    [TercihAdliye] [nvarchar](max) NOT NULL,
      NOT NULL,
      NOT NULL,
    [TalepDurumu] [nvarchar](max) NOT NULL
)
GO

-- İlişki
ALTER TABLE [dbo].[TayinTalepleri]  WITH CHECK ADD  CONSTRAINT [FK_TayinTalepleri_Personeller_PersonelId]
FOREIGN KEY([PersonelId]) REFERENCES [dbo].[Personeller] ([Id]) ON DELETE CASCADE
GO

-- Index
CREATE NONCLUSTERED INDEX [IX_TayinTalepleri_PersonelId] ON [dbo].[TayinTalepleri] ([PersonelId])
GO

-- EF Migration Kayıtları
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES
(N'20250606193524_IlkKurulum', N'9.0.5'),
(N'20250606204954_AdminUnvan', N'9.0.5'),
(N'20250607180939_Loglama', N'9.0.5')
GO

-- Personeller
SET IDENTITY_INSERT [dbo].[Personeller] ON
INSERT INTO [dbo].[Personeller] ([Id], [SicilNo], [AdSoyad], [Unvan], [Sifre], [IsAdmin]) VALUES
(6, N'96600', N'Efe Cüneyt ÖZGÜR', N'Yazı İşleri Müdürü', N'1', 1),
(7, N'2', N'Serkan ALTINBAŞ', N'Memur', N'1', 0),
(8, N'3', N'Dudu KARAKAYA', N'Hizmetli', N'1', 0),
(9, N'4', N'Defne KÜPELİ', N'Zabıt Katibi', N'1', 0),
(10, N'5', N'Ramazan KAYIM', N'Mübaşir', N'1', 0)
SET IDENTITY_INSERT [dbo].[Personeller] OFF
GO

-- Tayin Talepleri
SET IDENTITY_INSERT [dbo].[TayinTalepleri] ON
INSERT INTO [dbo].[TayinTalepleri] ([Id], [PersonelId], [TalepTuru], [TercihAdliye], [Aciklama], [BasvuruTarihi], [TalepDurumu]) VALUES
(17, 6, N'Aile Birliği', N'Ağrı Adliyesi', N'sfs sdfdf', '2025-06-07T21:23:46.6548713', N'Reddedildi'),
(20, 9, N'Aile Birliği', N'Adana Adliyesi', N'eş durumu', '2025-06-08T07:23:33.0381006', N'Onaylandı'),
(21, 7, N'Aile Birliği', N'Yalova Adliyesi', N'eş durumu', '2025-06-09T20:43:02.1002578', N'Onaylandı')
SET IDENTITY_INSERT [dbo].[TayinTalepleri] OFF
GO

-- Loglar
SET IDENTITY_INSERT [dbo].[Loglar] ON
INSERT INTO [dbo].[Loglar] ([Id], [PersonelId], [Islem], [Tarih], [Aciklama]) VALUES
(1, 6, N'Tayin Talebi Ekleme', '2025-06-07T21:15:41.2138888', N'Tayin talebi eklendi: Sağlık - Afyonkarahisar Adliyesi'),
(2, 6, N'Tayin Talebi Ekleme', '2025-06-07T21:23:46.6613754', N'Tayin talebi eklendi: Aile Birliği - Ağrı Adliyesi'),
(3, 7, N'Tayin Talebi Ekleme', '2025-06-07T21:27:05.9518605', N'Serkan ALTINBAŞ tarafından tayin talebi eklendi: Kendi İsteği - Adıyaman Adliyesi'),
(19, 7, N'Tayin Talebi Ekleme', '2025-06-09T20:43:02.1006220', N'Serkan ALTINBAŞ tarafından tayin talebi eklendi: Aile Birliği - Yalova Adliyesi')

SET IDENTITY_INSERT [dbo].[Loglar] OFF
GO
