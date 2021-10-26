USE [master]
GO
/****** Object:  Database [TransportCompany]    Script Date: 25.04.2021 18:15:11 ******/
CREATE DATABASE [TransportCompany]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TransoprtCompany', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TransoprtCompany.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TransoprtCompany_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TransoprtCompany_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TransportCompany] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransportCompany].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransportCompany] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransportCompany] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransportCompany] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransportCompany] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransportCompany] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransportCompany] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TransportCompany] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransportCompany] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransportCompany] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransportCompany] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransportCompany] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransportCompany] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransportCompany] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransportCompany] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransportCompany] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TransportCompany] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransportCompany] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransportCompany] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransportCompany] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransportCompany] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransportCompany] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TransportCompany] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransportCompany] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TransportCompany] SET  MULTI_USER 
GO
ALTER DATABASE [TransportCompany] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransportCompany] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TransportCompany] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TransportCompany] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TransportCompany] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TransportCompany] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TransportCompany] SET QUERY_STORE = OFF
GO
USE [TransportCompany]
GO
/****** Object:  Table [dbo].[Automobiles]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Automobiles](
	[AutomobileId] [int] IDENTITY(1,1) NOT NULL,
	[StampId] [int] NULL,
	[RegNumber] [varchar](50) NULL,
	[WIN] [varchar](50) NULL,
	[EngineNumber] [varchar](50) NULL,
	[Release] [date] NULL,
	[Mileage] [int] NULL,
	[Driver] [varchar](50) NULL,
	[LastTI] [date] NULL,
 CONSTRAINT [PK__Automobi__F0C1697E949BDA41] PRIMARY KEY CLUSTERED 
(
	[AutomobileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyServices]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyServices](
	[CompanyServicesId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyServicesData] [date] NULL,
	[CompanyServicesTime] [time](7) NULL,
	[CompanyServicesPhNumber] [bigint] NULL,
	[DeparturePoint] [varchar](50) NULL,
	[DestinationPoint] [varchar](50) NULL,
	[RateId] [int] NULL,
	[OperatorId] [int] NOT NULL,
	[AutomobileId] [int] NULL,
 CONSTRAINT [PK__CompanyS__3718DC8F280505F9] PRIMARY KEY CLUSTERED 
(
	[CompanyServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employes]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employes](
	[EmployeId] [int] IDENTITY(1,1) NOT NULL,
	[EmplName] [varchar](20) NULL,
	[EmplSurname] [varchar](20) NULL,
	[EmplMiddlename] [varchar](20) NULL,
	[EmplAge] [int] NULL,
	[EmplAdress] [varchar](100) NULL,
	[EmplPhoneNumber] [bigint] NULL,
	[PassportData] [varchar](20) NULL,
	[PositionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[PositionId] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [varchar](20) NULL,
	[Salary] [decimal](18, 1) NULL,
	[Responsibility] [varchar](50) NULL,
	[Requirements] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PositionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rates]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rates](
	[RateId] [int] IDENTITY(1,1) NOT NULL,
	[RateName] [varchar](20) NULL,
	[Specification] [varchar](50) NULL,
	[Cost] [decimal](18, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[RateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stamps]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stamps](
	[StampId] [int] IDENTITY(1,1) NOT NULL,
	[StampName] [varchar](20) NULL,
	[Specification] [varchar](50) NULL,
	[Cost] [decimal](18, 1) NULL,
	[Specificity] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StampId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Stamps] ON 

INSERT [dbo].[Stamps] ([StampId], [StampName], [Specification], [Cost], [Specificity]) VALUES (1, N'Bentley', N'240', CAST(131.0 AS Decimal(18, 1)), N'Обычныая')
SET IDENTITY_INSERT [dbo].[Stamps] OFF
GO
ALTER TABLE [dbo].[Automobiles]  WITH CHECK ADD  CONSTRAINT [FK_Automobiles_Stamps] FOREIGN KEY([StampId])
REFERENCES [dbo].[Stamps] ([StampId])
GO
ALTER TABLE [dbo].[Automobiles] CHECK CONSTRAINT [FK_Automobiles_Stamps]
GO
ALTER TABLE [dbo].[CompanyServices]  WITH CHECK ADD  CONSTRAINT [FC_CompanyServices_Employes] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[Employes] ([EmployeId])
GO
ALTER TABLE [dbo].[CompanyServices] CHECK CONSTRAINT [FC_CompanyServices_Employes]
GO
ALTER TABLE [dbo].[CompanyServices]  WITH CHECK ADD  CONSTRAINT [FK_CompanyServices_Automobiles] FOREIGN KEY([AutomobileId])
REFERENCES [dbo].[Automobiles] ([AutomobileId])
GO
ALTER TABLE [dbo].[CompanyServices] CHECK CONSTRAINT [FK_CompanyServices_Automobiles]
GO
ALTER TABLE [dbo].[CompanyServices]  WITH CHECK ADD  CONSTRAINT [FK_CompanyServices_Rates] FOREIGN KEY([RateId])
REFERENCES [dbo].[Rates] ([RateId])
GO
ALTER TABLE [dbo].[CompanyServices] CHECK CONSTRAINT [FK_CompanyServices_Rates]
GO
ALTER TABLE [dbo].[Employes]  WITH CHECK ADD  CONSTRAINT [FK_Employess_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([PositionId])
GO
ALTER TABLE [dbo].[Employes] CHECK CONSTRAINT [FK_Employess_Positions]
GO
/****** Object:  StoredProcedure [dbo].[InsertAutomobile]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertAutomobile]
    @StampId int,
	@RegNumber varchar(50),
	@WIN VARCHAR(50),
	@EngineNumber varchar(50),
	@Release date,
	@Mileage int,
	@Driver varchar(50),
	@LastTI DATE
AS
    INSERT INTO Automobiles(StampId, RegNumber,WIN,EngineNumber,Release,Mileage,Driver,LastTI)
    VALUES (@StampId, @RegNumber,@WIN,@EngineNumber,@Release,@Mileage,@Driver,@LastTI)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[InsertCompanyServices]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCompanyServices]
    @CompanyServicesData date,
	@CompanyServicesTime time(7),
	@CompanyServicesPhNumber bigint,
	@DeparturePoint varchar(50),
	@DestinationPoint varchar(50),
	@RateId int,
	@OperatorId int,
	@AutomobileId int
AS
    INSERT INTO CompanyServices(CompanyServicesData, CompanyServicesTime,CompanyServicesPhNumber,DeparturePoint,DestinationPoint,RateId,OperatorId,AutomobileId)
    VALUES (@CompanyServicesData,@CompanyServicesTime,@CompanyServicesPhNumber,@DeparturePoint,@DestinationPoint,@RateId,@OperatorId,@AutomobileId)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployes]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertEmployes]
    @EmplName varchar(20),
	@EmplSurname varchar(20),
	@EmplMiddleName varchar(20),
	@EmplAge int,
	@EmplAdress varchar(50),
	@EmplPhoneNumber bigint,
	@PassportData varchar(20),
	@PositionId int
AS
    INSERT INTO Employes(EmplName,EmplSurname,EmplMiddleName,EmplAge,EmplAdress,EmplPhoneNumber,PassportData,PositionId)
    VALUES (@EmplName,@EmplSurname,@EmplMiddleName,@EmplAge,@EmplAdress,@EmplPhoneNumber,@PassportData,@PositionId)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[InsertPositions]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPositions]
    @PositionName varchar(20),
	@Salary decimal(18,1),
	@Responsibility varchar(50),
	@Requirements varchar(50)
AS
    INSERT INTO Positions(PositionName,Salary,Responsibility,Requirements)
    VALUES (@PositionName,@Salary,@Responsibility,@Requirements)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[InsertRates]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRates]
    @RateName varchar(20),
	@Specification varchar(50),
	@Cost decimal(18,1)
AS
    INSERT INTO Rates(RateName,Specification,Cost)
    VALUES (@RateName,@Specification,@Cost)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[InsertStamps]    Script Date: 25.04.2021 18:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertStamps]
    @StampName varchar(20),
	@Specification varchar(50),
	@Cost decimal(18,1),
	@Specificity varchar(50)
AS
    INSERT INTO Stamps(StampName,Specification,Cost,Specificity)
    VALUES (@StampName,@Specification,@Cost,@Specificity)
  
    SELECT SCOPE_IDENTITY()
GO
USE [master]
GO
ALTER DATABASE [TransportCompany] SET  READ_WRITE 
GO
