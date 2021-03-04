USE [master]
GO

/****** Object:  Database [db_desafiositemercado]    Script Date: 04/03/2021 18:16:16 ******/
CREATE DATABASE [db_desafiositemercado]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_desafiositemercado', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_desafiositemercado.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_desafiositemercado_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_desafiositemercado_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_desafiositemercado].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [db_desafiositemercado] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET ARITHABORT OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [db_desafiositemercado] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [db_desafiositemercado] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET  DISABLE_BROKER 
GO

ALTER DATABASE [db_desafiositemercado] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [db_desafiositemercado] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET RECOVERY FULL 
GO

ALTER DATABASE [db_desafiositemercado] SET  MULTI_USER 
GO

ALTER DATABASE [db_desafiositemercado] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [db_desafiositemercado] SET DB_CHAINING OFF 
GO

ALTER DATABASE [db_desafiositemercado] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [db_desafiositemercado] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [db_desafiositemercado] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [db_desafiositemercado] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [db_desafiositemercado] SET QUERY_STORE = OFF
GO

ALTER DATABASE [db_desafiositemercado] SET  READ_WRITE 
GO


