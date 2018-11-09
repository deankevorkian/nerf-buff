CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(10) NOT NULL, 
    [Time] DATETIME NOT NULL, 
    [Location] NCHAR(100) NOT NULL, 
    [Author] NCHAR(10) NOT NULL
)
