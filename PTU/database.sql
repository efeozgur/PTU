IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO
 
BEGIN TRANSACTION;
CREATE TABLE [Adliyeler] (
    [Id] int NOT NULL IDENTITY,
    [Ad] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Adliyeler] PRIMARY KEY ([Id])
);

CREATE TABLE [Personeller] (
    [Id] int NOT NULL IDENTITY,
    [SicilNo] nvarchar(50) NOT NULL,
    [AdSoyad] nvarchar(100) NOT NULL,
    [Unvan] nvarchar(50) NOT NULL,
    [Sifre] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Personeller] PRIMARY KEY ([Id])
);

CREATE TABLE [TayinTalepleri] (
    [Id] int NOT NULL IDENTITY,
    [PersonelId] int NOT NULL,
    [TalepTuru] nvarchar(max) NOT NULL,
    [TercihAdliye] nvarchar(max) NOT NULL,
    [Aciklama] nvarchar(500) NOT NULL,
    [BasvuruTarihi] datetime2 NOT NULL,
    [TalepDurumu] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TayinTalepleri] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TayinTalepleri_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_TayinTalepleri_PersonelId] ON [TayinTalepleri] ([PersonelId]);

INSERT INTO Personeller (AdSoyad, SicilNo, Unvan, Sifre, IsAdmin)
VALUES ('Yönetici', 'admin', '1', '1', 1);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250606193524_IlkKurulum', N'9.0.5');

ALTER TABLE [Personeller] ADD [IsAdmin] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250606204954_AdminUnvan', N'9.0.5');

COMMIT;
GO

