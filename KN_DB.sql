USE [master]
GO

CREATE DATABASE [KN_DB]
GO

USE [KN_DB]
GO

CREATE TABLE [dbo].[Perfil](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Usuario](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Contrasenna] [varchar](15) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
	[IdPerfil] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([IdPerfil])
REFERENCES [dbo].[Perfil] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@Identificacion varchar(15),
	@Contrasenna varchar(15)
AS
BEGIN
	
	SELECT Id, Identificacion, Contrasenna, Nombre, Correo, Estado, IdPerfil
	FROM dbo.Usuario
	WHERE	Identificacion = @Identificacion
		AND Contrasenna = @Contrasenna
		AND Estado = 1
END
GO

CREATE PROCEDURE [dbo].[RegistrarCuenta]
	@Identificacion varchar(15),
	@Contrasenna varchar(15),
	@Nombre varchar(200),
	@Correo varchar(200)
AS
BEGIN
	
	INSERT INTO dbo.Usuario (Identificacion,Contrasenna,Nombre,Correo,Estado,IdPerfil)
	VALUES (@Identificacion,@Contrasenna,@Nombre,@Correo,1,2)

END
GO