USE [master]
GO

--drop database [TriviaGameDB]
--Go

CREATE DATABASE [TriviaGameDB]
GO

USE [TriviaGameDB]
GO


CREATE TABLE [AccessLV](
	[AccessId] int IDENTITY(1,1) NOT NULL,
	[Access_level] nvarchar(10) not NULL,
 CONSTRAINT PK_AccessLV PRIMARY KEY (AccessId)
)
CREATE TABLE [Users](
	[UserID] int IDENTITY(1,1) NOT NULL,
	[UserMail] nvarchar(90)  unique NOT NULL,
	[UserName] nvarchar(20) NOT NULL,
	[Password] nvarchar(30) NOT NULL,
	[AccessId] int NOT null,
	[Score] int null,
	[TotalScore] int NULL,
	[QuastionsAdded] int NULL,
 CONSTRAINT PK_Users PRIMARY KEY (UserID),
 CONSTRAINT FK_AccessId FOREIGN KEY (AccessId)
    REFERENCES AccessLV(AccessId)
)

INSERT INTO [AccessLV] ([Access_level]) VALUES ('user')
INSERT INTO [AccessLV] ([Access_level]) VALUES ('master')
INSERT INTO [AccessLV] ([Access_level]) VALUES ('admin')
GO

INSERT INTO [Users] ([UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('yelansimp@gmail.com','yelansimp', 'pass123', 2, 0, 2400, 24)
INSERT INTO [Users] ([UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('ganyusimp@gmail.com','steponme24', 'please2007', 1, 40, 200, 1)
INSERT INTO [Users] ([UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('jingliusimp@gmail.com','ofek', 'o2016', 1, 90, 999990, 9942)
INSERT INTO [Users] ([UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('arlenchinosimp@gmail.com','yarden', 'SDVFHC', 3, 0, 0, 0)
INSERT INTO [Users] ([UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('luminesimp@gmail.com','shahar', '43isthetruth', 3, 10, 120, 1)


CREATE TABLE [QuestionStatus](
	[StatusId] int IDENTITY(1,1) NOT NULL,
	[Status] nvarchar(10) not NULL,
 CONSTRAINT PK_QuestionStatus PRIMARY KEY (StatusId),
)



INSERT INTO [QuestionStatus] ([Status]) VALUES ('accepted')
INSERT INTO [QuestionStatus] ([Status]) VALUES ('pending')
INSERT INTO [QuestionStatus] ([Status]) VALUES ('denied')
GO

CREATE TABLE [QuestionSubjects](
	[SubjectId] int IDENTITY(1,1) NOT NULL,
	[Subject] nvarchar(50) not NULL,
 CONSTRAINT PK_QuestionSubjects PRIMARY KEY (SubjectId),
)

INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('sport')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('politics')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('history')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('science')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('high School ramon')
GO

CREATE TABLE [Questions](
	[QuestionID] int IDENTITY(1,1) NOT NULL,
	[Question] nvarchar(100) unique NOT NULL,
	[UserID] int NOT NULL,
	[SubjectID] int NOT NULL,
	[StatusID] int NOT null,
 CONSTRAINT PK_Questions PRIMARY KEY (QuestionID),

 CONSTRAINT FK_UserID FOREIGN KEY (UserID)
    REFERENCES Users(UserID),

 CONSTRAINT FK_QuestionSubject FOREIGN KEY (SubjectID)
    REFERENCES QuestionSubjects([SubjectID]),

 CONSTRAINT FK_QuestionStatus FOREIGN KEY (StatusID)
    REFERENCES QuestionStatus(StatusID)
)

INSERT INTO [Questions] ([Question], [UserID], [SubjectID], [StatusID]) VALUES ('When did WW2 start?',1, 3, 1)
INSERT INTO [Questions] ([Question], [UserID], [SubjectID], [StatusID]) VALUES ('how much time did the current prime minister of isreal worked as a prime minister?',2, 2, 2)
INSERT INTO [Questions] ([Question], [UserID], [SubjectID], [StatusID]) VALUES ('which of those is not an olympic sport?',3, 1, 3)
INSERT INTO [Questions] ([Question], [UserID], [SubjectID], [StatusID]) VALUES ('What is the power of gravity? in (m/s^2)',4, 4, 1)
INSERT INTO [Questions] ([Question], [UserID], [SubjectID], [StatusID]) VALUES ('how many floors are in the school?',5, 5, 1)



CREATE TABLE [Answers](
	[AnswerID] int IDENTITY(1,1) NOT NULL,
	[Answer] nvarchar(100) NOT NULL,
	[QuestionID] int NOT NULL,
	[true_false] bit not null,
 CONSTRAINT PK_Answers PRIMARY KEY (AnswerID),

 CONSTRAINT FK_QuestionID FOREIGN KEY (QuestionID)
    REFERENCES Questions(QuestionID)
)

INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('September 1, 1939',1,1)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('September 30, 1936',1,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('august 1, 1948',1,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('may 17, 1939',1,2)

INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('not enough',2,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('16 years',2,1)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('15 years',2,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('21 years',2,2)

INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('baseball',3,1)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('cricket',3,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('golf',3,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('football',3,2)

INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('9,8',4,1)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('10',4,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('9.98',4,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('9.74',4,2)

INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('4',5,1)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('3',5,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('3.5',5,2)
INSERT INTO [Answers] ([Answer], [QuestionID], [true_false]) VALUES ('2',5,2)
