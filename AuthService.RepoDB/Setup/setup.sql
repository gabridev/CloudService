CREATE DATABASE "auth-db";
GO
USE "auth-db";

CREATE TABLE [dbo].[Members]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[FirstName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](128) NOT NULL,
	[EncryptedPassword] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](255) NULL,
	[PostCode] [nvarchar](128) NULL,
	[PhoneNumber] [nvarchar](128) NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL default GETUTCDATE(),
	CONSTRAINT [CRIX_Member_Id] PRIMARY KEY CLUSTERED ([Id] ASC) ON [PRIMARY]
)
ON [PRIMARY];
GO
