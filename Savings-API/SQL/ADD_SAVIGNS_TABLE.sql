/****** Object:  Table [dbo].[Savings]    Script Date: 18.08.2024 13:48:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Savings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[Destination] [varchar](100) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Amount] [int] NOT NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_savings.Savings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Savings] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO