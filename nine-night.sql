USE [master]
GO
/****** Object:  Database [nine-night]    Script Date: 2023/4/20 下午 11:12:06 ******/
CREATE DATABASE [nine-night]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ninenight', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ninenight.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ninenight_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ninenight_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [nine-night] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [nine-night].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [nine-night] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [nine-night] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [nine-night] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [nine-night] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [nine-night] SET ARITHABORT OFF 
GO
ALTER DATABASE [nine-night] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [nine-night] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [nine-night] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [nine-night] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [nine-night] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [nine-night] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [nine-night] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [nine-night] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [nine-night] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [nine-night] SET  DISABLE_BROKER 
GO
ALTER DATABASE [nine-night] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [nine-night] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [nine-night] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [nine-night] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [nine-night] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [nine-night] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [nine-night] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [nine-night] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [nine-night] SET  MULTI_USER 
GO
ALTER DATABASE [nine-night] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [nine-night] SET DB_CHAINING OFF 
GO
ALTER DATABASE [nine-night] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [nine-night] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [nine-night] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [nine-night] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [nine-night] SET QUERY_STORE = OFF
GO
USE [nine-night]
GO
/****** Object:  User [constr_user]    Script Date: 2023/4/20 下午 11:12:06 ******/
CREATE USER [constr_user] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [constr_user]
GO
/****** Object:  Table [dbo].[brand]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[brand](
	[brand_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[brand_name] [nvarchar](50) NOT NULL,
	[brand_eng] [varchar](100) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_brand] PRIMARY KEY CLUSTERED 
(
	[brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cart_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[cart_states] [int] NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart_product]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_product](
	[cart_product_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[cart_id] [uniqueidentifier] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[cart_product_amount] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[new]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[new](
	[new_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[new_title] [nvarchar](20) NOT NULL,
	[new_startdate] [nvarchar](20) NOT NULL,
	[new_enddate] [nvarchar](20) NOT NULL,
	[new_content] [nvarchar](max) NOT NULL,
	[new_img] [varchar](max) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_new] PRIMARY KEY CLUSTERED 
(
	[new_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[order_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[cart_id] [uniqueidentifier] NOT NULL,
	[order_name] [nvarchar](15) NOT NULL,
	[order_price] [int] NOT NULL,
	[order_date] [datetime] NOT NULL,
	[order_picktime] [nvarchar](50) NOT NULL,
	[order_pick] [bit] NOT NULL,
	[order_address] [nvarchar](50) NOT NULL,
	[order_phone] [char](10) NOT NULL,
	[order_state] [int] NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[place]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[place](
	[place_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[place_name] [nvarchar](15) NOT NULL,
	[place_eng] [varchar](100) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_place] PRIMARY KEY CLUSTERED 
(
	[place_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[product_id] [uniqueidentifier] NOT NULL,
	[product_num] [varchar](8) NOT NULL,
	[product_name] [nvarchar](50) NOT NULL,
	[product_eng] [varchar](100) NOT NULL,
	[product_like] [int] NOT NULL,
	[product_img] [varchar](max) NOT NULL,
	[brand_id] [uniqueidentifier] NOT NULL,
	[product_price] [int] NOT NULL,
	[place_id] [uniqueidentifier] NOT NULL,
	[product_ml] [int] NOT NULL,
	[product_content] [nvarchar](max) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[store]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[store](
	[store_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[store_name] [nvarchar](30) NOT NULL,
	[store_address] [nvarchar](50) NOT NULL,
	[store_phone] [char](10) NOT NULL,
	[store_email] [nvarchar](50) NOT NULL,
	[store_time] [nvarchar](150) NOT NULL,
	[store_img] [varchar](max) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_store] PRIMARY KEY CLUSTERED 
(
	[store_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[story]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story](
	[story_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[story_title] [nvarchar](20) NOT NULL,
	[story_content] [nvarchar](max) NOT NULL,
	[story_img] [varchar](max) NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_story] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 2023/4/20 下午 11:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[user_account] [varchar](20) NOT NULL,
	[user_pwd] [varchar](20) NOT NULL,
	[user_name] [nvarchar](15) NOT NULL,
	[user_gender] [int] NULL,
	[user_birthday] [date] NULL,
	[user_email] [varchar](50) NULL,
	[user_authcode] [varchar](10) NULL,
	[user_phone] [char](10) NULL,
	[user_address] [nvarchar](50) NULL,
	[user_level] [bit] NOT NULL,
	[isdel] [bit] NOT NULL,
	[create_id] [varchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_id] [varchar](20) NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[brand] ([brand_id], [brand_name], [brand_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'd5c88011-43f0-4c56-ae44-1d5c938cc969', N'虎牌', N'Tiger', 0, N'123', CAST(N'2023-04-12T00:00:00.000' AS DateTime), N'admin', CAST(N'2023-04-12T21:15:00.330' AS DateTime))
GO
INSERT [dbo].[brand] ([brand_id], [brand_name], [brand_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'b5b27652-f26e-4a21-a72f-641a5e50360c', N'海尼根', N'Heineken', 0, N'admin', CAST(N'2023-04-12T21:28:30.847' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[brand] ([brand_id], [brand_name], [brand_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'c7dc5759-6918-46df-b1dc-7d8ba252a20b', N'台灣啤酒', N'Taiwan Beer', 0, N'admin', CAST(N'2023-04-12T21:25:54.673' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[brand] ([brand_id], [brand_name], [brand_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'51ac7ea6-c2e6-47b8-a246-832736dc1152', N'紅馬', N'Red Horse', 0, N'admin', CAST(N'2023-04-18T22:29:26.870' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[brand] ([brand_id], [brand_name], [brand_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'eff50552-1066-476b-bfdd-b7136331268a', N'百威', N'BUDWEISER', 0, N'admin', CAST(N'2023-04-18T18:56:47.537' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cart] ([cart_id], [user_id], [cart_states], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'f487984b-864e-41e0-a294-2dfa183c8656', N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 0, 0, N'xinyu', CAST(N'2023-04-19T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cart_product] ([cart_product_id], [cart_id], [product_id], [cart_product_amount]) VALUES (N'93d5fa0a-6933-435b-84b0-5b3f533b73f9', N'f487984b-864e-41e0-a294-2dfa183c8656', N'd3983586-c91d-4771-ba22-0d889fe9c297', 2)
GO
INSERT [dbo].[cart_product] ([cart_product_id], [cart_id], [product_id], [cart_product_amount]) VALUES (N'93d5fa0a-6933-435b-84b0-5b3f533b73f8', N'f487984b-864e-41e0-a294-2dfa183c8656', N'a83a4b6e-d5e5-4b6d-9796-114c2df9aee0', 1)
GO
INSERT [dbo].[new] ([new_id], [new_title], [new_startdate], [new_enddate], [new_content], [new_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'4b8c9d88-9186-427e-a68d-710e860a2246', N'123', N'2023-04-19', N'2023-04-20', N'123', N'20230419212345p0.png', 0, N'admin', CAST(N'2023-04-19T21:23:46.493' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[new] ([new_id], [new_title], [new_startdate], [new_enddate], [new_content], [new_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'5c2b3be3-617d-4822-9f5d-b700cae8aff0', N'11', N'2023-04-19', N'2023-04-20', N'11', N'20230419160208product1.jpg', 0, N'admin', CAST(N'2023-04-19T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', N'香港', N'Hongkong', 0, N'admin', CAST(N'2023-04-18T22:28:15.753' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'f487984b-864e-41e0-a294-2dfa183c8656', N'中國', N'China', 0, N'admin', CAST(N'2023-04-18T18:57:32.040' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'93d5fa0a-6933-435b-84b0-5b3f533b73f9', N'日本', N'Japan', 0, N'admin', CAST(N'2023-04-12T22:28:03.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'3f1271b6-4eea-47bd-8911-5e69d7c08236', N'台灣', N'Taiwan', 0, N'admin', CAST(N'2023-04-12T22:11:11.153' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'2c771015-768a-4bd8-9183-9ab079a5f04b', N'韓國', N'Korea', 0, N'admin', CAST(N'2023-04-12T22:06:26.180' AS DateTime), N'admin', CAST(N'2023-04-12T22:28:22.373' AS DateTime))
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'18d8fae9-ea0e-4d02-a655-c22735616c40', N'新加坡', N'Singapore', 0, N'admin', CAST(N'2023-04-18T18:53:26.043' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[place] ([place_id], [place_name], [place_eng], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'0d4d517b-0948-49ae-94e8-c81b284e14a5', N'荷蘭', N'Netherlands', 0, N'admin', CAST(N'2023-04-17T20:57:31.973' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'd3983586-c91d-4771-ba22-0d889fe9c297', N'NN9435', N'百威啤酒', N'BUDWEISER BEER', 0, N'NN9435p5.png', N'eff50552-1066-476b-bfdd-b7136331268a', 40, N'f487984b-864e-41e0-a294-2dfa183c8656', 330, N'百威啤酒堅持7個步驟耗時31天的釀造時程，為的就是百威啤酒獨一無二的清醇順暢口感。從大麥、啤酒花、米與酵母的原料挑選開始，到每個釀造過程的監控與品管，及最後裝瓶上市前的品質與口感測試，百威啤酒的釀酒師們以全球統一的嚴格標準，堅持每個釀造細節，確保每個國家都有著相同的清醇順暢口感。', 0, N'admin', CAST(N'2023-04-18T18:58:29.913' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'a83a4b6e-d5e5-4b6d-9796-114c2df9aee0', N'NN2657', N'虎牌-1度C冰釀啤酒', N'TIGER CRYSTAL', 0, N'NN2657p4.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 30, N'18d8fae9-ea0e-4d02-a655-c22735616c40', 330, N'虎牌-1度C冰釀啤酒運用獨特優質 -1°C冰釀過濾技術，讓啤酒清澈明亮，純淨順口，有著冰涼清爽口感；此外，更添加了抗日照的特殊啤酒花，搭配透明流線造型瓶身，就是敢讓每個暢飲時刻更顯潮流時尚，亦創造顛覆想像的極致凍感體驗！', 0, N'admin', CAST(N'2023-04-18T18:55:13.840' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'8e321f1e-d695-4d7c-8af6-183e96834ec2', N'NN3723', N'11', N'11', 0, N'NN3723aboutus.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 11, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 11, N'11', 0, N'admin', CAST(N'2023-04-19T18:09:21.543' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'aeda95b1-0104-4f55-9858-1d6e25f6f33d', N'NN5361', N'海尼根星銀啤酒', N'HEINEKEN SILVER BEER', 0, N'NN5361p2.png', N'b5b27652-f26e-4a21-a72f-641a5e50360c', 75, N'0d4d517b-0948-49ae-94e8-c81b284e14a5', 650, N'順飲型啤酒市場近年持續擴大，帶有鮮明的麥芽香氣、啤酒花的微苦、爽快的氣泡感，讓清爽型拉格啤酒在市場佔比逐年成長。海尼根啤酒近日推出全新口味「Silver星銀啤酒」，以不顯苦味、滑順的全新風味，搶攻夏日商機。', 0, N'admin', CAST(N'2023-04-17T20:57:58.310' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'72826abb-a0a4-4475-9b86-3d84426aeb2a', N'NN2359', N'123', N'123', 0, N'123_20230419155401+warnlogo1.png', N'b5b27652-f26e-4a21-a72f-641a5e50360c', 123, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 123, N'123', 0, N'admin', CAST(N'2023-04-19T15:48:33.580' AS DateTime), N'admin', CAST(N'2023-04-19T15:54:01.240' AS DateTime))
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'f25c5783-744a-4f7c-a047-43f9b2236d81', N'NN1961', N'海尼根星銀啤酒', N'HEINEKEN SILVER BEER', 0, N'NN1961p2.png', N'b5b27652-f26e-4a21-a72f-641a5e50360c', 40, N'0d4d517b-0948-49ae-94e8-c81b284e14a5', 330, N'順飲型啤酒市場近年持續擴大，帶有鮮明的麥芽香氣、啤酒花的微苦、爽快的氣泡感，讓清爽型拉格啤酒在市場佔比逐年成長。海尼根啤酒近日推出全新口味「Silver星銀啤酒」，以不顯苦味、滑順的全新風味，搶攻夏日商機。', 0, N'admin', CAST(N'2023-04-18T18:50:41.790' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'46551596-8632-4478-b44d-791bb07c474e', N'NN7822', N'123', N'123', 0, N'NN7822p5.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 199, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 330, N'123_content', 0, N'admin', CAST(N'2023-04-19T18:15:46.440' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'1107d47c-34da-4963-869a-80aba76dc21b', N'NN3943', N'222', N'222', 0, N'NN3943_20230419155039_warnlogo2.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 222, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 222, N'222', 0, N'admin', CAST(N'2023-04-19T15:50:39.680' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'23345ed6-feab-47ce-a24d-8e1c8e39f9bd', N'NN1358', N'555', N'555', 0, N'NN1358aboutus3.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 555, N'2c771015-768a-4bd8-9183-9ab079a5f04b', 555, N'555', 0, N'admin', CAST(N'2023-04-19T15:55:47.903' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'181dbd3b-a8dd-4d48-a350-acd0e2941c39', N'NN6204', N'1', N'1', 0, N'NN6204p2.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 1, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 1, N'1', 0, N'admin', CAST(N'2023-04-19T18:48:26.810' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'5dbcd0b9-9c02-41dd-9e6d-cd62fe3a7b2f', N'NN2614', N'百威啤酒', N'BUDWEISER BEER', 0, N'NN2614p5.png', N'eff50552-1066-476b-bfdd-b7136331268a', 55, N'f487984b-864e-41e0-a294-2dfa183c8656', 500, N'百威啤酒堅持7個步驟耗時31天的釀造時程，為的就是百威啤酒獨一無二的清醇順暢口感。從大麥、啤酒花、米與酵母的原料挑選開始，到每個釀造過程的監控與品管，及最後裝瓶上市前的品質與口感測試，百威啤酒的釀酒師們以全球統一的嚴格標準，堅持每個釀造細節，確保每個國家都有著相同的清醇順暢口感。', 0, N'admin', CAST(N'2023-04-18T18:58:41.307' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'2869b46f-22ba-4e99-8d73-cdeec7fd5aae', N'NN7863', N'1234', N'1234', 0, N'NN7863p12.png', N'b5b27652-f26e-4a21-a72f-641a5e50360c', 130, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 320, N'content_tt', 0, N'admin', CAST(N'2023-04-19T18:20:04.007' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'fdf19861-7aba-492b-962e-e2fd9ac123a0', N'NN1641', N'虎牌-1度C冰釀啤酒', N'TIGER CRYSTAL', 0, N'NN1641p3.png', N'd5c88011-43f0-4c56-ae44-1d5c938cc969', 60, N'18d8fae9-ea0e-4d02-a655-c22735616c40', 600, N'虎牌-1度C冰釀啤酒運用獨特優質 -1°C冰釀過濾技術，讓啤酒清澈明亮，純淨順口，有著冰涼清爽口感；此外，更添加了抗日照的特殊啤酒花，搭配透明流線造型瓶身，就是敢讓每個暢飲時刻更顯潮流時尚，亦創造顛覆想像的極致凍感體驗！', 0, N'admin', CAST(N'2023-04-18T18:55:00.247' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[product] ([product_id], [product_num], [product_name], [product_eng], [product_like], [product_img], [brand_id], [product_price], [place_id], [product_ml], [product_content], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'923bfd18-e6b3-4286-b373-ee41132f29e9', N'NN7438', N'紅馬烈啤酒', N'RED HORSE BEER', 0, N'NN7438p5.png', N'51ac7ea6-c2e6-47b8-a246-832736dc1152', 45, N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', 500, N'亞太地區銷售最好的烈啤酒─紅馬烈啤酒，在杜拜及沙烏地阿拉伯等地都是銷售成長最快的啤酒，擁有8%的濃烈豪氣！口感強勁有力、個性鮮明，滿足喜歡刺激與冒險精神的啤酒飲用者。  紅馬烈啤酒酒體呈金黃色澤，開罐後撲鼻的濃郁麥芽香，配以啤酒花的苦味與香氣，濃烈又順口；獨樹一格的8%酒精濃度，讓您輕鬆體驗微醺的快意及風味濃厚的口感。', 0, N'admin', CAST(N'2023-04-18T22:30:17.203' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[store] ([store_id], [store_name], [store_address], [store_phone], [store_email], [store_time], [store_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'34a0cb64-c944-44ff-a5f1-61fe4594d3b6', N'22', N'22', N'22        ', N'22', N'2', N'20230419162938aboutus.png', 0, N'admin', CAST(N'2023-04-19T16:29:05.470' AS DateTime), N'admin', CAST(N'2023-04-19T16:29:38.587' AS DateTime))
GO
INSERT [dbo].[store] ([store_id], [store_name], [store_address], [store_phone], [store_email], [store_time], [store_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'eafca637-5b78-4043-a6e1-8bb4b4500c44', N'台中旗艦店', N'門市地址1', N'1234567890', N'NineNight2023@gmail.com', N'24小時營業', N'/image', 0, N'admin', CAST(N'2023-04-16T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[store] ([store_id], [store_name], [store_address], [store_phone], [store_email], [store_time], [store_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'7453f481-d19c-4a73-a363-e8f8a8e36551', N'台北總店', N'門市地址2', N'0987654321', N'NineNight2023@gmail.com', N'24小時營業', N'/image', 0, N'admin', CAST(N'2023-04-16T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'9998e870-2fb5-44ec-b4cf-0594d3449df0', N'品牌故事3', N'內容3', N'12345', 1, N'admin', CAST(N'2023-04-12T02:58:09.977' AS DateTime), N'admin', CAST(N'2023-04-12T03:39:02.793' AS DateTime))
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'8d2b9a88-e9e1-4ee1-9786-0bc7f8b90436', N'品牌故事1', N'內容1', N'string', 0, N'admin', CAST(N'2023-04-12T02:55:41.470' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'221beb4e-7269-46da-bd3d-cd422b45469a', N'22', N'22', N'20230419163401aboutus.png', 0, N'admin', CAST(N'2023-04-19T16:33:09.787' AS DateTime), N'admin', CAST(N'2023-04-19T16:34:01.300' AS DateTime))
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'f7395aed-32fa-4ead-8baa-e8e0b8152eb2', N'品牌故事5', N'品牌故事5', N'/image/', 0, N'admin', CAST(N'2023-04-15T17:15:21.403' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'4f01eda0-29eb-4d43-a0c3-f270857acdfa', N'品牌故事4', N'內容4', N'string', 0, N'admin', CAST(N'2023-04-12T02:58:22.040' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[story] ([story_id], [story_title], [story_content], [story_img], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'86fab91c-cf0f-4dc4-8106-fd61c6fc7b71', N'品牌故事2', N'內容2', N'string', 0, N'admin', CAST(N'2023-04-12T02:57:53.863' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[user] ([user_id], [user_account], [user_pwd], [user_name], [user_gender], [user_birthday], [user_email], [user_authcode], [user_phone], [user_address], [user_level], [isdel], [create_id], [create_time], [update_id], [update_time]) VALUES (N'814aa3a7-f4d7-4a78-9eb5-0aff99d2d003', N'xinyu', N'1234', N'陳欣妤', 2, CAST(N'2002-06-07' AS Date), N'silvia50727@gmail.com', N'1234', N'0937377323', N'台北市內湖區', 1, 0, N'admin', CAST(N'2023-04-19T00:00:00.000' AS DateTime), NULL, NULL)
GO
ALTER TABLE [dbo].[brand] ADD  CONSTRAINT [DF_brand_brand_id]  DEFAULT (newid()) FOR [brand_id]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_cart_id]  DEFAULT (newid()) FOR [cart_id]
GO
ALTER TABLE [dbo].[cart_product] ADD  CONSTRAINT [DF_cart_product_cart_product_id]  DEFAULT (newid()) FOR [cart_product_id]
GO
ALTER TABLE [dbo].[new] ADD  CONSTRAINT [DF_new_new_id]  DEFAULT (newid()) FOR [new_id]
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_order_order_id]  DEFAULT (newid()) FOR [order_id]
GO
ALTER TABLE [dbo].[place] ADD  CONSTRAINT [DF_place_place_id]  DEFAULT (newid()) FOR [place_id]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_brand_id]  DEFAULT (0x) FOR [brand_id]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_place_id]  DEFAULT (0x) FOR [place_id]
GO
ALTER TABLE [dbo].[store] ADD  CONSTRAINT [DF_store_store_id]  DEFAULT (newid()) FOR [store_id]
GO
ALTER TABLE [dbo].[story] ADD  CONSTRAINT [DF_story_story_id]  DEFAULT (newid()) FOR [story_id]
GO
USE [master]
GO
ALTER DATABASE [nine-night] SET  READ_WRITE 
GO
