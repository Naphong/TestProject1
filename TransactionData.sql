USE [TestData]
GO
/****** Object:  Table [dbo].[TransactionData]    Script Date: 10/17/2019 5:54:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[CurrencyCode] [nvarchar](10) NULL,
	[TransactionDate] [datetime] NULL,
	[RawStatus] [nvarchar](10) NULL,
	[FileType] [nvarchar](3) NULL,
	[Status] [nvarchar](1) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_TransactionData_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
