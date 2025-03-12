USE [master]
GO

CREATE DATABASE [KN_DB]
GO

USE [KN_DB]
GO

CREATE TABLE [dbo].[Error](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[Mensaje] [varchar](max) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Origen] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Error] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Oferta](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdPuesto] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Salario] [decimal](10, 2) NOT NULL,
	[Horario] [varchar](255) NOT NULL,
	[Disponible] [bit] NOT NULL,
 CONSTRAINT [PK_Oferta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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

CREATE TABLE [dbo].[Puesto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Puesto] PRIMARY KEY CLUSTERED 
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

CREATE TABLE [dbo].[UsuariosOferta](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[IdOferta] [bigint] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_UsuariosOferta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Error] ON 
GO
INSERT [dbo].[Error] ([Id], [IdUsuario], [Mensaje], [FechaHora], [Origen]) VALUES (1, 0, N'Referencia a objeto no establecida como instancia de un objeto.', CAST(N'2025-03-11T20:16:18.503' AS DateTime), N'Get ConsultarOfertasDisponibles')
GO
INSERT [dbo].[Error] ([Id], [IdUsuario], [Mensaje], [FechaHora], [Origen]) VALUES (2, 4, N'Referencia a objeto no establecida como instancia de un objeto.', CAST(N'2025-03-11T20:18:45.107' AS DateTime), N'Get ConsultarOfertasDisponibles')
GO
SET IDENTITY_INSERT [dbo].[Error] OFF
GO

SET IDENTITY_INSERT [dbo].[Oferta] ON 
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (1, 1, 3, CAST(1500.00 AS Decimal(10, 2)), N'Lunes a Viernes de 8:00 a 17:00', 1)
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (2, 2, 3, CAST(1800.00 AS Decimal(10, 2)), N'Lunes a Domingo de 08:00 a 18:00', 1)
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (3, 4, 5, CAST(2000.00 AS Decimal(10, 2)), N'Lunes, Miércoles y Viernes de 8:00 am - 6:00 pm / presencial ', 0)
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (4, 1, 2, CAST(2121.00 AS Decimal(10, 2)), N'21321321', 1)
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (5, 4, 1, CAST(5000.00 AS Decimal(10, 2)), N'Lunes a Viernes', 1)
GO
INSERT [dbo].[Oferta] ([Id], [IdPuesto], [Cantidad], [Salario], [Horario], [Disponible]) VALUES (6, 6, 10, CAST(100.00 AS Decimal(10, 2)), N'LV', 1)
GO
SET IDENTITY_INSERT [dbo].[Oferta] OFF
GO

SET IDENTITY_INSERT [dbo].[Perfil] ON 
GO
INSERT [dbo].[Perfil] ([Id], [Nombre]) VALUES (1, N'Reclutador(a)')
GO
INSERT [dbo].[Perfil] ([Id], [Nombre]) VALUES (2, N'Usuario(a)')
GO
SET IDENTITY_INSERT [dbo].[Perfil] OFF
GO

SET IDENTITY_INSERT [dbo].[Puesto] ON 
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (1, N'Programador', N'Tareas principales de desarrollo')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (2, N'Asistente de Base de Datos', N'Tareas principalles de base de datos')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (3, N'Asistente de Base de Datos', N'Tareas principalles de base de datos')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (4, N'QA Senior', N'Encargado de realizar pruebas de los sistemas.
Encargado de documentar las pruebas.
Encargado de automatizar escenarios.
Supervisar el trabajo del equipo.')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (5, N'Encargado de Mercadeo', N'Se encargará de las funcionalidades más importantes de mercadeo')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (6, N'Encargado de RH', N'Procesos de reclutamiento y selección')
GO
INSERT [dbo].[Puesto] ([Id], [Nombre], [Descripcion]) VALUES (7, N'b', N'bc')
GO
SET IDENTITY_INSERT [dbo].[Puesto] OFF
GO

SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Id], [Identificacion], [Contrasenna], [Nombre], [Correo], [Estado], [IdPerfil]) VALUES (4, N'304590415', N'90415', N'Eduardo Calvo Castillo', N'ecalvo90415@ufide.ac.cr', 1, 2)
GO
INSERT [dbo].[Usuario] ([Id], [Identificacion], [Contrasenna], [Nombre], [Correo], [Estado], [IdPerfil]) VALUES (6, N'305440468', N'S63P4', N'Fabricio Arce Salas', N'farce40468@ufide.ac.cr', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UK_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UK_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Oferta]  WITH CHECK ADD  CONSTRAINT [FK_Oferta_Puesto] FOREIGN KEY([IdPuesto])
REFERENCES [dbo].[Puesto] ([Id])
GO
ALTER TABLE [dbo].[Oferta] CHECK CONSTRAINT [FK_Oferta_Puesto]
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([IdPerfil])
REFERENCES [dbo].[Perfil] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO

ALTER TABLE [dbo].[UsuariosOferta]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosOferta_Oferta] FOREIGN KEY([IdOferta])
REFERENCES [dbo].[Oferta] ([Id])
GO
ALTER TABLE [dbo].[UsuariosOferta] CHECK CONSTRAINT [FK_UsuariosOferta_Oferta]
GO

ALTER TABLE [dbo].[UsuariosOferta]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosOferta_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[UsuariosOferta] CHECK CONSTRAINT [FK_UsuariosOferta_Usuario]
GO

CREATE PROCEDURE [dbo].[ConsultarOfertas]

AS
BEGIN

	SELECT	O.Id,
			IdPuesto,
			P.Nombre,
			P.Descripcion,
			Cantidad,
			Salario,
			Horario,
			Disponible
	  FROM	dbo.Oferta O
	  INNER JOIN dbo.Puesto P ON O.IdPuesto = P.Id

END
GO

CREATE PROCEDURE [dbo].[ConsultarPuestos]

AS
BEGIN

	SELECT	Id,
			Nombre,
			Descripcion
	FROM	dbo.Puesto

END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@Identificacion varchar(15),
	@Contrasenna varchar(15)
AS
BEGIN
	
	SELECT	U.Id, 
			Identificacion, 
			Contrasenna, 
			U.Nombre 'NombreUsuario', 
			Correo, 
			Estado, 
			IdPerfil, 
			P.Nombre 'NombrePerfil'
	FROM	dbo.Usuario U
	INNER	JOIN dbo.Perfil P ON U.IdPerfil = P.Id
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
	
	IF NOT EXISTS(SELECT 1 FROM dbo.Usuario
				  WHERE Identificacion = @Identificacion
					 OR Correo = @Correo)
	BEGIN

		INSERT INTO dbo.Usuario (Identificacion,Contrasenna,Nombre,Correo,Estado,IdPerfil)
		VALUES (@Identificacion,@Contrasenna,@Nombre,@Correo,1,2)

	END

END
GO

CREATE PROCEDURE [dbo].[RegistrarError]
	@IdUsuario bigint,
	@Mensaje   varchar(max),
	@Origen	   varchar(500)
AS
BEGIN
	
	INSERT INTO dbo.Error (IdUsuario,Mensaje,FechaHora,Origen)
    VALUES (@IdUsuario, @Mensaje, GETDATE(), @Origen)

END
GO