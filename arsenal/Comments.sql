CREATE TABLE [dbo].[Comments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PostID] INT NULL, 
    [Title] NCHAR(10) NULL, 
    [Author] NCHAR(10) NULL, 
    [Content] NCHAR(10) NULL, 
    [Date] DATETIME NULL
)
