USE [master]
GO

/****** Object:  Database [ManymindsBD]    Script Date: 10/04/2024 19:59:38 ******/
CREATE DATABASE [ManymindsBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManymindsBD', FILENAME = N'..\MSSQL\DATA\ManymindsBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManymindsBD_log', FILENAME = N'..\MSSQL\DATA\ManymindsBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ManymindsBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ManymindsBD] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ManymindsBD] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ManymindsBD] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ManymindsBD] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ManymindsBD] SET ARITHABORT OFF 
GO

ALTER DATABASE [ManymindsBD] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ManymindsBD] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ManymindsBD] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ManymindsBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ManymindsBD] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ManymindsBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ManymindsBD] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ManymindsBD] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ManymindsBD] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ManymindsBD] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ManymindsBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ManymindsBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ManymindsBD] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ManymindsBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ManymindsBD] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ManymindsBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ManymindsBD] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ManymindsBD] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ManymindsBD] SET  MULTI_USER 
GO

ALTER DATABASE [ManymindsBD] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ManymindsBD] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ManymindsBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ManymindsBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ManymindsBD] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ManymindsBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [ManymindsBD] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ManymindsBD] SET  READ_WRITE 
GO

