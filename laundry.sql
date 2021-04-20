USE [master]
GO
/****** Object:  Database [laundry]    Script Date: 20/04/2021 14.06.50 ******/
CREATE DATABASE [laundry]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'laundry', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\laundry.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'laundry_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\laundry_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [laundry] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [laundry].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [laundry] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [laundry] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [laundry] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [laundry] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [laundry] SET ARITHABORT OFF 
GO
ALTER DATABASE [laundry] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [laundry] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [laundry] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [laundry] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [laundry] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [laundry] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [laundry] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [laundry] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [laundry] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [laundry] SET  DISABLE_BROKER 
GO
ALTER DATABASE [laundry] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [laundry] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [laundry] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [laundry] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [laundry] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [laundry] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [laundry] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [laundry] SET RECOVERY FULL 
GO
ALTER DATABASE [laundry] SET  MULTI_USER 
GO
ALTER DATABASE [laundry] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [laundry] SET DB_CHAINING OFF 
GO
ALTER DATABASE [laundry] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [laundry] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [laundry] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [laundry] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'laundry', N'ON'
GO
ALTER DATABASE [laundry] SET QUERY_STORE = OFF
GO
USE [laundry]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Address] [text] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailDeposit]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailDeposit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDeposit] [int] NOT NULL,
	[IdService] [int] NOT NULL,
	[IdPrepaidPackage] [int] NULL,
	[PriceUnit] [int] NOT NULL,
	[TotalUnit] [float] NOT NULL,
	[CompletedDatetime] [datetime] NULL,
 CONSTRAINT [PK_DetailDeposit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [text] NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[DateofBirth] [date] NOT NULL,
	[IdJob] [int] NOT NULL,
	[Salary] [money] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HeaderDeposit]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HeaderDeposit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomer] [int] NOT NULL,
	[IdEmployee] [int] NOT NULL,
	[TransactionDatetime] [datetime] NOT NULL,
	[CompleteEstimationDatetime] [datetime] NULL,
 CONSTRAINT [PK_HeaderDeposit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdService] [int] NOT NULL,
	[TotalUnit] [int] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrepaidPackage]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrepaidPackage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomer] [int] NOT NULL,
	[IdPackage] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[StartDatetime] [datetime] NOT NULL,
	[CompletedDatetime] [datetime] NULL,
 CONSTRAINT [PK_PrepaidPackage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IdCategory] [int] NOT NULL,
	[IdUnit] [int] NOT NULL,
	[PriceUnit] [int] NOT NULL,
	[EstimationDuration] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 20/04/2021 14.06.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [laundry] SET  READ_WRITE 
GO
