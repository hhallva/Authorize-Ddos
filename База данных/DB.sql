USE [Test]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[CandidateId] [int] IDENTITY(1,1) NOT NULL,
	[VacancyId] [int] NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Patronymic] [nvarchar](100) NULL,
	[Resume] [nvarchar](300) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[CandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[FailedAttempts] [int] NOT NULL,
	[LockTime] [datetime2](7) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacancy]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacancy](
	[VcancyId] [int] IDENTITY(1,1) NOT NULL,
	[PositionId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ClosedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Vacancy] PRIMARY KEY CLUSTERED 
(
	[VcancyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VacancySkill]    Script Date: 15.02.2025 23:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VacancySkill](
	[VcancyId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
 CONSTRAINT [PK_VacancySkill] PRIMARY KEY CLUSTERED 
(
	[VcancyId] ASC,
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Candidate] ON 

INSERT [dbo].[Candidate] ([CandidateId], [VacancyId], [Surname], [Name], [Patronymic], [Resume], [CreatedDate]) VALUES (1, 2, N'Екимов ', N'Вячеслав', N'Андреевич', N'№297283', CAST(N'2025-02-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Candidate] ([CandidateId], [VacancyId], [Surname], [Name], [Patronymic], [Resume], [CreatedDate]) VALUES (2, 3, N'Ушаков', N'Дмитрий', N'', N'№123272', CAST(N'2025-02-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Candidate] ([CandidateId], [VacancyId], [Surname], [Name], [Patronymic], [Resume], [CreatedDate]) VALUES (3, 1, N'Попов', N'Глеб', N'', N'№234562', CAST(N'2025-02-16T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Candidate] OFF
GO
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (1, N'Программист')
INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (2, N'Дизайнер')
INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (3, N'Менеджер')
SET IDENTITY_INSERT [dbo].[Position] OFF
GO
SET IDENTITY_INSERT [dbo].[Skill] ON 

INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (1, N'Знание SQL')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (8, N'Знание UX/UI 2025')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (4, N'Знание языка программирования C#')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (3, N'Лидерские качества')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (6, N'Поставленная речь')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (2, N'Умение работать в Excel')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (5, N'Умение работать в Figma')
INSERT [dbo].[Skill] ([SkillId], [Name]) VALUES (7, N'Умение работать с API')
SET IDENTITY_INSERT [dbo].[Skill] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Login], [Password], [FailedAttempts], [LockTime]) VALUES (1, N'1', N'password', 3, CAST(N'2025-02-06T10:59:14.7265035' AS DateTime2))
INSERT [dbo].[User] ([Id], [Login], [Password], [FailedAttempts], [LockTime]) VALUES (2, N'2', N'1213213', 0, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Vacancy] ON 

INSERT [dbo].[Vacancy] ([VcancyId], [PositionId], [Title], [Description], [CreatedDate], [ClosedDate]) VALUES (1, 1, N'Требуется программист в команду опытных разработчиков', N'Опыт работы не требуется. Зарплата сдельная. Команда состоит из 2 человек. ', CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Vacancy] ([VcancyId], [PositionId], [Title], [Description], [CreatedDate], [ClosedDate]) VALUES (2, 2, N'Ищем дизайнера от бога', N'Хотим чтоб нам формочки рисовал', CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Vacancy] ([VcancyId], [PositionId], [Title], [Description], [CreatedDate], [ClosedDate]) VALUES (3, 3, N'Нужен менеджер для руководства', N'Очень нужен', CAST(N'2025-02-15T00:00:00.0000000' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Vacancy] OFF
GO
INSERT [dbo].[VacancySkill] ([VcancyId], [SkillId]) VALUES (1, 1)
INSERT [dbo].[VacancySkill] ([VcancyId], [SkillId]) VALUES (2, 2)
INSERT [dbo].[VacancySkill] ([VcancyId], [SkillId]) VALUES (3, 3)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Skill]    Script Date: 15.02.2025 23:41:06 ******/
ALTER TABLE [dbo].[Skill] ADD  CONSTRAINT [UQ_Skill] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_User]    Script Date: 15.02.2025 23:41:06 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ_User] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_FailedAttempts]  DEFAULT ((0)) FOR [FailedAttempts]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Vacancy] FOREIGN KEY([VacancyId])
REFERENCES [dbo].[Vacancy] ([VcancyId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_Vacancy]
GO
ALTER TABLE [dbo].[Vacancy]  WITH CHECK ADD  CONSTRAINT [FK_Vacancy_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([PositionId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Vacancy] CHECK CONSTRAINT [FK_Vacancy_Position]
GO
ALTER TABLE [dbo].[VacancySkill]  WITH CHECK ADD  CONSTRAINT [FK_VacancySkill_Skill] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([SkillId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[VacancySkill] CHECK CONSTRAINT [FK_VacancySkill_Skill]
GO
ALTER TABLE [dbo].[VacancySkill]  WITH CHECK ADD  CONSTRAINT [FK_VacancySkill_Vacancy] FOREIGN KEY([VcancyId])
REFERENCES [dbo].[Vacancy] ([VcancyId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[VacancySkill] CHECK CONSTRAINT [FK_VacancySkill_Vacancy]
GO
