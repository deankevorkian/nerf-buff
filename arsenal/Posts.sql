CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(30) NOT NULL, 
    [Author] NCHAR(30) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [Content] NCHAR(500) NOT NULL, 
)
