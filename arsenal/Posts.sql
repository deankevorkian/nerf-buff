CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(30) NULL, 
    [Author] NCHAR(30) NULL, 
    [Date] DATETIME NULL, 
    [Content] NCHAR(500) NULL
)
