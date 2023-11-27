USE [master]
GO

CREATE DATABASE [TriviaGameDB]
GO

USE [TriviaGameDB]
GO

CREATE TABLE [Users](
	[UserID] int IDENTITY(1,1) NOT NULL,
	[UserMail] nvarchar(90)  unique NOT NULL,
	[UserName] nvarchar(20) NOT NULL,
	[Password] nvarchar(30) NOT NULL,
	[AccessId] int NOT null,
	[Score] int null,
	[TotalScore] int NULL,
	[QuastionsAdded] int NULL,
 CONSTRAINT PK_Users PRIMARY KEY (UsersID),
 CONSTRAINT FK_AccessId FOREIGN KEY (AccessId)
    REFERENCES AccessLV(AccessId)
)

CREATE TABLE [AccessLV](
	[AccessId] int IDENTITY(1,1) NOT NULL,
	[Access_level] nvarchar(10) not NULL,
 CONSTRAINT PK_AccessLV PRIMARY KEY (AccessId),
)

INSERT INTO [AccessLV] ([Access_level]) VALUES ('user')
INSERT INTO [AccessLV] ([Access_level]) VALUES ('master')
INSERT INTO [AccessLV] ([Access_level]) VALUES ('admin')
GO

INSERT INTO [Users] ([UserID], [UserMail], [UserName], [Password], [AccessId], [Score], [TotalScore], [QuastionsAdded]) VALUES ('yelansimp@gmail.com','yelansimp', 'pass123', 2, 0, 2400, 24)




CREATE TABLE [Questions](
	[QuestionID] int IDENTITY(1,1) NOT NULL,
	[Question] nvarchar(100) unique NOT NULL,
	[UserID] int NOT NULL,
	[QuestionSubject] nvarchar(30) NOT NULL,
	[QuestionStatus] int NOT null,
 CONSTRAINT PK_Questions PRIMARY KEY (QuestionID),

 CONSTRAINT FK_UserID FOREIGN KEY (UserID)
    REFERENCES Users(UserID),

 CONSTRAINT FK_QuestionSubject FOREIGN KEY (QuestionSubject)
    REFERENCES QuestionSubjects(QuestionSubject),

 CONSTRAINT FK_QuestionStatus FOREIGN KEY (QuestionStatus)
    REFERENCES QuestionStatus(QuestionStatus)
)

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
	[Subject] nvarchar(10) not NULL,
 CONSTRAINT PK_QuestionSubjects PRIMARY KEY (SubjectId),
)

INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('sport')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('politics')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('history')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('science')
INSERT INTO [QuestionSubjects] ([Subject]) VALUES ('high School ramon')
GO


CREATE TABLE [Answers](
	[AnswerID] int IDENTITY(1,1) NOT NULL,
	[Answer] nvarchar(100) NOT NULL,
	[QuestionID] int NOT NULL,
	[true_false] bit not null,
 CONSTRAINT PK_Answers PRIMARY KEY (AnswerID),

 CONSTRAINT FK_QuestionID FOREIGN KEY (QuestionID)
    REFERENCES Questions(QuestionID)
)

