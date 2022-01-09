USE [master]
GO
/****** Object:  Database [Hospital]    Script Date: 1/9/2022 6:30:39 PM ******/
CREATE DATABASE [Hospital]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hospital', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Hospital.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hospital_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Hospital_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Hospital] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hospital].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hospital] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hospital] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hospital] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hospital] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hospital] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hospital] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hospital] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hospital] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hospital] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hospital] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hospital] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hospital] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hospital] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hospital] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hospital] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Hospital] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hospital] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hospital] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hospital] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hospital] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hospital] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hospital] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hospital] SET RECOVERY FULL 
GO
ALTER DATABASE [Hospital] SET  MULTI_USER 
GO
ALTER DATABASE [Hospital] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hospital] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hospital] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hospital] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hospital] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hospital] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hospital', N'ON'
GO
ALTER DATABASE [Hospital] SET QUERY_STORE = OFF
GO
USE [Hospital]
GO
/****** Object:  Table [dbo].[Appoinment]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appoinment](
	[AppointId] [nvarchar](64) NOT NULL,
	[PatientId] [int] NOT NULL,
	[DoctorId] [nvarchar](64) NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentTime] [time](7) NOT NULL,
	[AppointmentStatus] [nvarchar](64) NOT NULL,
	[Symptom] [nvarchar](64) NOT NULL,
	[Medication] [nvarchar](128) NOT NULL,
	[Diesis] [nvarchar](64) NULL,
	[Prescription] [nvarchar](128) NULL,
	[IsVisited] [int] NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL,
 CONSTRAINT [PK_Appoinment] PRIMARY KEY CLUSTERED 
(
	[AppointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[OId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Address] [nvarchar](64) NOT NULL,
	[Balance] [decimal](19, 2) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[OId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[DrId] [nvarchar](64) NOT NULL,
	[UserId] [int] NOT NULL,
	[SpecializationId] [int] NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[Status] [nvarchar](64) NOT NULL,
	[Fee] [decimal](19, 2) NOT NULL,
	[Balance] [decimal](19, 2) NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[DrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Secretary]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Secretary](
	[SecId] [nvarchar](64) NOT NULL,
	[UserId] [int] NOT NULL,
	[DrId] [nvarchar](64) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[OId] [int] IDENTITY(1,1) NOT NULL,
	[Specialization] [nvarchar](64) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[OId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransId] [nvarchar](64) NOT NULL,
	[PatientId] [int] NOT NULL,
	[DoctorId] [nvarchar](64) NOT NULL,
	[Amount] [decimal](19, 2) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[OId] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Address] [nvarchar](128) NULL,
	[Gender] [nvarchar](64) NULL,
	[DOB] [date] NOT NULL,
	[Phone] [nvarchar](64) NOT NULL,
	[Email] [nvarchar](64) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[OId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 1/9/2022 6:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[OId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](64) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Created_by] [nvarchar](64) NOT NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [nvarchar](64) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[OId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Appoinment] ([AppointId], [PatientId], [DoctorId], [AppointmentDate], [AppointmentTime], [AppointmentStatus], [Symptom], [Medication], [Diesis], [Prescription], [IsVisited], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (N'app20220109-124013471', 4, N'dr5', CAST(N'2022-01-09' AS Date), CAST(N'16:39:00' AS Time), N'Pending', N'fever', N'napa', NULL, NULL, 0, CAST(N'2022-01-09T12:41:23.097' AS DateTime), N'abrar', NULL, NULL)
INSERT [dbo].[Appoinment] ([AppointId], [PatientId], [DoctorId], [AppointmentDate], [AppointmentTime], [AppointmentStatus], [Symptom], [Medication], [Diesis], [Prescription], [IsVisited], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (N'app20220109-124219370', 4, N'dr5', CAST(N'2022-01-09' AS Date), CAST(N'16:42:00' AS Time), N'Pending', N'Fever', N'napa', NULL, NULL, 0, CAST(N'2022-01-09T12:42:20.480' AS DateTime), N'abrar', NULL, NULL)
INSERT [dbo].[Appoinment] ([AppointId], [PatientId], [DoctorId], [AppointmentDate], [AppointmentTime], [AppointmentStatus], [Symptom], [Medication], [Diesis], [Prescription], [IsVisited], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (N'app20220109-125513203', 4, N'dr5', CAST(N'2022-01-09' AS Date), CAST(N'16:42:00' AS Time), N'Pending', N'fever', N'napa', NULL, NULL, 0, CAST(N'2022-01-09T12:55:13.400' AS DateTime), N'abrar', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([OId], [Name], [Address], [Balance]) VALUES (1, N'Bhutan Hospital', N'Bhutan', CAST(100.00 AS Decimal(19, 2)))
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
INSERT [dbo].[Doctor] ([DrId], [UserId], [SpecializationId], [JoinDate], [Status], [Fee], [Balance], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (N'dr5', 5, 1, CAST(N'2000-01-04T00:00:00.000' AS DateTime), N'Active', CAST(2500.00 AS Decimal(19, 2)), NULL, CAST(N'2022-01-06T17:32:43.173' AS DateTime), N'Admin', NULL, NULL)
GO
INSERT [dbo].[Secretary] ([SecId], [UserId], [DrId], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (N'se6', 6, N'dr5', CAST(N'2022-01-09T10:57:43.643' AS DateTime), N'Admin', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Specialization] ON 

INSERT [dbo].[Specialization] ([OId], [Specialization], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (1, N'Cardio', CAST(N'2022-01-06T17:28:46.087' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[Specialization] ([OId], [Specialization], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (2, N'Skin', CAST(N'2022-01-06T17:28:52.277' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[Specialization] ([OId], [Specialization], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (3, N'Dental', CAST(N'2022-01-06T17:28:58.500' AS DateTime), N'Admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Specialization] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (1, 1, N'Admin', N'Dhaka', N'Male', CAST(N'1980-01-04' AS Date), N'01713059609', N'admin@hosp.com', N'Admin123456', CAST(N'2022-01-04T12:09:34.047' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (2, 2, N'Adib', N'Dhaka, Bangladesh', N'Male', CAST(N'1995-01-10' AS Date), N'015342145667', N'patient1@patient.com', N'Pa123456', CAST(N'2022-01-04T18:25:55.687' AS DateTime), N'Adib', NULL, NULL)
INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (3, 2, N'Azad', N'dhanmondi', N'Male', CAST(N'2000-02-02' AS Date), N'01874928765', N'azad@g.com', N'Azad123456', CAST(N'2022-01-05T15:35:05.870' AS DateTime), N'Azad', NULL, NULL)
INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (4, 2, N'abrar', N'azimpur', N'Male', CAST(N'1990-05-16' AS Date), N'01785683975', N'abrar@g.com', N'Abrar123456', CAST(N'2022-01-05T16:00:54.853' AS DateTime), N'abrar', NULL, NULL)
INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (5, 3, N'Doctor', N'Gulshan', N'Female', CAST(N'1995-02-10' AS Date), N'01874928765', N'doc@hosp.com', N'Doc123456', CAST(N'2022-01-06T16:17:50.740' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[User] ([OId], [TypeId], [Name], [Address], [Gender], [DOB], [Phone], [Email], [Password], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (6, 4, N'Secretary', N'Badda', N'Female', CAST(N'1995-02-15' AS Date), N'01874928765', N'sec@hosp.com', N'Sec123456', CAST(N'2022-01-06T16:18:47.060' AS DateTime), N'Admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([OId], [Type], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (1, N'Admin', CAST(N'2022-01-04T12:06:10.660' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[UserType] ([OId], [Type], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (2, N'Patient', CAST(N'2022-01-04T12:06:31.987' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[UserType] ([OId], [Type], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (3, N'Doctor', CAST(N'2022-01-04T12:06:38.350' AS DateTime), N'Admin', NULL, NULL)
INSERT [dbo].[UserType] ([OId], [Type], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (4, N'Secretary', CAST(N'2022-01-06T15:18:21.953' AS DateTime), N'Admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
ALTER TABLE [dbo].[Appoinment]  WITH CHECK ADD  CONSTRAINT [FK_Appoinment_Appoinment] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([DrId])
GO
ALTER TABLE [dbo].[Appoinment] CHECK CONSTRAINT [FK_Appoinment_Appoinment]
GO
ALTER TABLE [dbo].[Appoinment]  WITH CHECK ADD  CONSTRAINT [FK_Appoinment_User] FOREIGN KEY([PatientId])
REFERENCES [dbo].[User] ([OId])
GO
ALTER TABLE [dbo].[Appoinment] CHECK CONSTRAINT [FK_Appoinment_User]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([OId])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Specialization]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([OId])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_User]
GO
ALTER TABLE [dbo].[Secretary]  WITH CHECK ADD  CONSTRAINT [FK_Secretary_Doctor] FOREIGN KEY([DrId])
REFERENCES [dbo].[Doctor] ([DrId])
GO
ALTER TABLE [dbo].[Secretary] CHECK CONSTRAINT [FK_Secretary_Doctor]
GO
ALTER TABLE [dbo].[Secretary]  WITH CHECK ADD  CONSTRAINT [FK_Secretary_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([OId])
GO
ALTER TABLE [dbo].[Secretary] CHECK CONSTRAINT [FK_Secretary_User]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Transaction] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([DrId])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Transaction]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([PatientId])
REFERENCES [dbo].[User] ([OId])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[UserType] ([OId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
USE [master]
GO
ALTER DATABASE [Hospital] SET  READ_WRITE 
GO
