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
GO

CREATE TABLE [StatusNotaFiscal] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] varchar(50) NOT NULL,
    CONSTRAINT [PK_StatusNotaFiscal] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NotasFiscais] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [NomePagador] varchar(100) NOT NULL,
    [NumeroIdentificacao] varchar(100) NOT NULL,
    [DataEmissao] datetime2 NOT NULL,
    [DataCobranca] datetime2 NULL,
    [DataPagamento] datetime2 NULL,
    [Valor] decimal(18,2) NOT NULL,
    [DocumentoNotaFiscal] varchar(100) NULL,
    [DocumentoBoletoBancario] varchar(100) NULL,
    [StatusId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_NotasFiscais] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotasFiscais_StatusNotaFiscal_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [StatusNotaFiscal] ([Id])
);
GO

CREATE INDEX [IX_NotasFiscais_StatusId] ON [NotasFiscais] ([StatusId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240930142223_InitialCreate', N'8.0.8');
GO

COMMIT;
GO

