USE [master]
GO
/****** Object:  Database [RasshivalovBase]    Script Date: 13.04.2021 8:57:33 ******/
CREATE DATABASE [RasshivalovBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RasshivalovBase', FILENAME = N'D:\DataBases\RasshivalovBase.mdf' , SIZE = 8192KB , MAXSIZE = 8192KB , FILEGROWTH = 3%)
 LOG ON 
( NAME = N'RasshivalovBase_log', FILENAME = N'D:\DataBases\RasshivalovBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 8192KB , FILEGROWTH = 3%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RasshivalovBase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RasshivalovBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RasshivalovBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RasshivalovBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RasshivalovBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RasshivalovBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RasshivalovBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [RasshivalovBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RasshivalovBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RasshivalovBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RasshivalovBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RasshivalovBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RasshivalovBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RasshivalovBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RasshivalovBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RasshivalovBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RasshivalovBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RasshivalovBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RasshivalovBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RasshivalovBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RasshivalovBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RasshivalovBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RasshivalovBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RasshivalovBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RasshivalovBase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RasshivalovBase] SET  MULTI_USER 
GO
ALTER DATABASE [RasshivalovBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RasshivalovBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RasshivalovBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RasshivalovBase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RasshivalovBase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RasshivalovBase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RasshivalovBase] SET QUERY_STORE = OFF
GO
USE [RasshivalovBase]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAverage]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAverage](@Surnm char(15)) 
RETURNS decimal(18,4)   
AS    
BEGIN  
	DECLARE @avg decimal(18,4)
	SELECT @avg = SUM(dbo.Payments.Amount)/COUNT(dbo.Payments.Amount)
    FROM dbo.Payments join dbo.Students on dbo.Payments.StudentId = dbo.Students.StudentId
    WHERE dbo.Students.Surname = @Surnm
    RETURN @avg;  
END;
GO
/****** Object:  UserDefinedFunction [dbo].[StudentsNoPay]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[StudentsNoPay](@PurposeName varchar(20))
RETURNS @studentsTable TABLE(StudentId INT PRIMARY KEY, Surname CHAR(20),Name CHAR(20), MiddleName CHAR(20))
AS
BEGIN
DECLARE @currentDate DATE
DECLARE @currentYear INT
SET @currentDate = GETDATE()
SET @currentYear = YEAR(@currentDate)
INSERT @studentsTable SELECT StudentId,Surname,Name,MiddleName FROM(
SELECT Students.StudentId,Students.Surname,Students.Name,Students.MiddleName 
FROM  Students 
INNER JOIN Payments ON Students.StudentId = Payments.StudentId
INNER JOIN Purposes ON Payments.PurposeId = Purposes.PurposeId
WHERE Purposes.Name = @PurposeName AND YEAR(Payments.PaymentDate) = @currentYear AND Payments.Amount = 0 ) AS newTable
RETURN
END
GO
/****** Object:  Table [dbo].[Streets]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Streets](
	[StreetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[TownId] [int] NOT NULL,
 CONSTRAINT [PK_Streets] PRIMARY KEY CLUSTERED 
(
	[StreetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](20) NOT NULL,
	[Name] [varchar](15) NOT NULL,
	[MiddleName] [varchar](20) NOT NULL,
	[StreetId] [int] NOT NULL,
	[HouseNumber] [smallint] NOT NULL,
	[ApartmentNumber] [smallint] NOT NULL,
	[PhoneNumber] [bigint] NOT NULL,
	[BirthDate] [date] NOT NULL,
	[AdmissionYear] [smallint] NOT NULL,
	[GroupName] [varchar](4) NOT NULL,
	[FacultyId] [int] NOT NULL,
	[Note] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Towns]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[TownId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Towns] PRIMARY KEY CLUSTERED 
(
	[TownId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNumStudents]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetNumStudents](@facultyId INT,@town varchar(20),@townTwo varchar(20),@townThree varchar(20))
RETURNS TABLE
AS
RETURN (SELECT  Students.Surname,Students.MiddleName, Students.FacultyId,Towns.Name, COUNT(Students.StudentId) as CtnStudents
FROM Students
 INNER JOIN Streets ON Students.StreetId = Streets.StreetId 
 INNER JOIN Towns ON Streets.TownId = Towns.TownId
 WHERE Students.FacultyId = @facultyId AND Towns.Name = @town OR Towns.Name = @townTwo OR Towns.Name = @townThree
 GROUP BY Students.MiddleName,Students.Surname,Towns.Name,Students.FacultyId)
GO
/****** Object:  Table [dbo].[Faculties]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculties](
	[FacultyId] [int] IDENTITY(1,1) NOT NULL,
	[ShortName] [varchar](10) NOT NULL,
	[Name] [nchar](30) NOT NULL,
 CONSTRAINT [PK_Faculties] PRIMARY KEY CLUSTERED 
(
	[FacultyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[PaymentDate] [date] NOT NULL,
	[Amount] [decimal](18, 0) NULL,
	[PurposeId] [int] NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purposes]    Script Date: 13.04.2021 8:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purposes](
	[PurposeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Purposes] PRIMARY KEY CLUSTERED 
(
	[PurposeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Faculties] ON 

INSERT [dbo].[Faculties] ([FacultyId], [ShortName], [Name]) VALUES (1, N'MBF', N'Faculty of Mechanical Building')
INSERT [dbo].[Faculties] ([FacultyId], [ShortName], [Name]) VALUES (2, N'ECF', N'Faculty of Econimics          ')
INSERT [dbo].[Faculties] ([FacultyId], [ShortName], [Name]) VALUES (3, N'FI', N'Faculty of Information        ')
INSERT [dbo].[Faculties] ([FacultyId], [ShortName], [Name]) VALUES (4, N'MT', N'Faculty of Mechanics          ')
INSERT [dbo].[Faculties] ([FacultyId], [ShortName], [Name]) VALUES (5, N'EF', N'Faculty of Energetics         ')
SET IDENTITY_INSERT [dbo].[Faculties] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (1, 1, CAST(N'2021-09-12' AS Date), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (2, 1, CAST(N'2019-10-13' AS Date), CAST(100 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (3, 1, CAST(N'2020-09-09' AS Date), CAST(15 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (4, 2, CAST(N'2020-07-09' AS Date), CAST(12 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (5, 2, CAST(N'2021-01-01' AS Date), CAST(231 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (6, 2, CAST(N'2021-01-02' AS Date), CAST(12 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (7, 3, CAST(N'2021-05-15' AS Date), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (8, 3, CAST(N'2020-08-07' AS Date), CAST(82 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (9, 3, CAST(N'2020-09-09' AS Date), CAST(27 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (10, 4, CAST(N'2019-05-27' AS Date), CAST(31 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (11, 4, CAST(N'2020-02-28' AS Date), CAST(12 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (12, 4, CAST(N'2021-02-02' AS Date), CAST(45 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (13, 5, CAST(N'2021-02-03' AS Date), CAST(36 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (15, 5, CAST(N'2021-02-07' AS Date), CAST(31 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (16, 5, CAST(N'2021-02-08' AS Date), CAST(333 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (17, 5, CAST(N'2021-02-10' AS Date), CAST(23 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (18, 6, CAST(N'2020-02-22' AS Date), CAST(21 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (20, 6, CAST(N'2020-03-21' AS Date), CAST(36 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (21, 6, CAST(N'2017-12-13' AS Date), CAST(56 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (22, 6, CAST(N'2017-12-13' AS Date), CAST(58 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (23, 7, CAST(N'2017-12-14' AS Date), CAST(34 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (24, 7, CAST(N'2018-12-21' AS Date), CAST(46 AS Decimal(18, 0)), 3)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (25, 7, CAST(N'2018-12-22' AS Date), CAST(45 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (26, 7, CAST(N'2019-12-25' AS Date), CAST(46 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (27, 8, CAST(N'2019-12-26' AS Date), CAST(48 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (28, 8, CAST(N'2020-01-12' AS Date), CAST(45 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (29, 8, CAST(N'2020-01-12' AS Date), CAST(46 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (30, 9, CAST(N'2021-01-13' AS Date), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (31, 9, CAST(N'2020-01-14' AS Date), CAST(51 AS Decimal(18, 0)), 2)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (32, 9, CAST(N'2020-01-22' AS Date), CAST(512 AS Decimal(18, 0)), 5)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (33, 10, CAST(N'2020-01-23' AS Date), CAST(513 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (34, 10, CAST(N'2020-01-24' AS Date), CAST(345 AS Decimal(18, 0)), 4)
INSERT [dbo].[Payments] ([PaymentId], [StudentId], [PaymentDate], [Amount], [PurposeId]) VALUES (35, 10, CAST(N'2020-01-25' AS Date), CAST(111 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Purposes] ON 

INSERT [dbo].[Purposes] ([PurposeId], [Name]) VALUES (1, N'Food')
INSERT [dbo].[Purposes] ([PurposeId], [Name]) VALUES (2, N'Repair')
INSERT [dbo].[Purposes] ([PurposeId], [Name]) VALUES (3, N'Event')
INSERT [dbo].[Purposes] ([PurposeId], [Name]) VALUES (4, N'Education')
INSERT [dbo].[Purposes] ([PurposeId], [Name]) VALUES (5, N'Other')
SET IDENTITY_INSERT [dbo].[Purposes] OFF
GO
SET IDENTITY_INSERT [dbo].[Streets] ON 

INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (1, N'Central', 5)
INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (2, N'Richevskaja', 6)
INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (3, N'Kirova', 2)
INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (4, N'Lenina', 3)
INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (5, N'Green', 1)
INSERT [dbo].[Streets] ([StreetId], [Name], [TownId]) VALUES (6, N'Red', 4)
SET IDENTITY_INSERT [dbo].[Streets] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (1, N'Ivanov', N'Ivan', N'Ivanovich', 1, 10, 24, 7894598, CAST(N'2001-12-12' AS Date), 2011, N'ITP2', 3, N'Good student                                                                                        ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (2, N'Petrov', N'Petr', N'Petrovich', 2, 9, 21, 7833345, CAST(N'2000-10-20' AS Date), 2010, N'IP1', 3, N'Usefull student                                                                                     ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (3, N'Artemov', N'Artem', N'Artemovich', 6, 2, 1, 6886881, CAST(N'2002-12-12' AS Date), 2011, N'ITP2', 3, N'Funny student                                                                                       ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (4, N'Vasilev', N'Vasiliy', N'Vasilievich', 4, 1, 10, 1112223, CAST(N'1999-06-14' AS Date), 2010, N'EF3', 5, N'Enjoys learning                                                                                     ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (5, N'Igorev', N'Igor', N'Ivanovich', 2, 15, 16, 8889991, CAST(N'2000-10-20' AS Date), 2010, N'EF3', 5, N'Clever student                                                                                      ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (6, N'Nikitov', N'Nikita', N'Nikitovich', 5, 8, 8, 1234548, CAST(N'2003-01-13' AS Date), 2011, N'MT1', 4, N'Good student                                                                                        ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (7, N'Evgeniev', N'Evgeniy', N'Fedorovich', 4, 13, 5, 1551551, CAST(N'2000-04-16' AS Date), 2010, N'EC4', 2, N'Important student                                                                                   ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (8, N'Borisov', N'Boris', N'Borisovich', 3, 12, 3, 5454541, CAST(N'2000-12-12' AS Date), 2010, N'EC3', 2, N'Good student                                                                                        ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (9, N'Pavlova', N'Marina', N'Petrovna', 5, 15, 15, 4545085, CAST(N'2002-05-14' AS Date), 2011, N'MB3', 1, N'Funny student                                                                                       ')
INSERT [dbo].[Students] ([StudentId], [Surname], [Name], [MiddleName], [StreetId], [HouseNumber], [ApartmentNumber], [PhoneNumber], [BirthDate], [AdmissionYear], [GroupName], [FacultyId], [Note]) VALUES (10, N'Fedorova', N'Alexandra', N'Anatolievna', 6, 12, 1, 1115551, CAST(N'2002-05-06' AS Date), 2011, N'MB2', 1, N'Strong student                                                                                      ')
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (1, N'Gomel')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (2, N'Minsk')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (3, N'Mogilev')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (4, N'Vitebsk')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (5, N'Brest')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (6, N'Grodno')
SET IDENTITY_INSERT [dbo].[Towns] OFF
GO
/****** Object:  Index [IX_Amount_Payments]    Script Date: 13.04.2021 8:57:33 ******/
CREATE NONCLUSTERED INDEX [IX_Amount_Payments] ON [dbo].[Payments]
(
	[Amount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StudentsName]    Script Date: 13.04.2021 8:57:33 ******/
CREATE NONCLUSTERED INDEX [IX_StudentsName] ON [dbo].[Students]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Purposes] FOREIGN KEY([PurposeId])
REFERENCES [dbo].[Purposes] ([PurposeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Purposes]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Students]
GO
ALTER TABLE [dbo].[Streets]  WITH CHECK ADD  CONSTRAINT [FK_Streets_Towns] FOREIGN KEY([TownId])
REFERENCES [dbo].[Towns] ([TownId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Streets] CHECK CONSTRAINT [FK_Streets_Towns]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Faculties] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculties] ([FacultyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Faculties]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Streets] FOREIGN KEY([StreetId])
REFERENCES [dbo].[Streets] ([StreetId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Streets]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [CK_Students] CHECK  (([AdmissionYear]<(2012)))
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [CK_Students]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id факультета' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Faculties', @level2type=N'COLUMN',@level2name=N'FacultyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сокращенное имя факультета' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Faculties', @level2type=N'COLUMN',@level2name=N'ShortName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Полное название факультета' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Faculties', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Индекс' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payments', @level2type=N'COLUMN',@level2name=N'PaymentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Внешний ключ для связи с таблицей Students' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payments', @level2type=N'COLUMN',@level2name=N'StudentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payments', @level2type=N'COLUMN',@level2name=N'PaymentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма перечисленная студентом вузу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Внешний ключ для связи с таблицей Purposes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payments', @level2type=N'COLUMN',@level2name=N'PurposeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Фамилия студента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Students', @level2type=N'COLUMN',@level2name=N'Surname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Имя студента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Students', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Отчество студента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Students', @level2type=N'COLUMN',@level2name=N'MiddleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Внешний ключ для связи с таблицей Streets' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Students', @level2type=N'COLUMN',@level2name=N'StreetId'
GO
USE [master]
GO
ALTER DATABASE [RasshivalovBase] SET  READ_WRITE 
GO
