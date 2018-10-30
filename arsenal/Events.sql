CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(10) NULL, 
    [Time] DATETIME NULL, 
    [Location] NCHAR(100) NULL, 
    [Author] NCHAR(10) NULL
)
