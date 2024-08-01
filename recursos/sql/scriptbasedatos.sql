/****** Object:  Schema [HMI]    Script Date: 8/1/2024 12:07:13 PM ******/
CREATE SCHEMA [HMI]
GO
/****** Object:  Table [HMI].[Departamento]    Script Date: 8/1/2024 12:07:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HMI].[Departamento](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Codigo] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HMI].[Municipio]    Script Date: 8/1/2024 12:07:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HMI].[Municipio](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Codigo] [nvarchar](5) NOT NULL,
	[DepartamentoId] [int] NOT NULL,
 CONSTRAINT [PK_Municipio] PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [HMI].[Municipio]  WITH CHECK ADD  CONSTRAINT [FK_Municipio_Departamento_DepartamentoId] FOREIGN KEY([DepartamentoId])
REFERENCES [HMI].[Departamento] ([IdDepartamento])
ON DELETE CASCADE
GO
ALTER TABLE [HMI].[Municipio] CHECK CONSTRAINT [FK_Municipio_Departamento_DepartamentoId]
GO
