CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NCHAR(10) NOT NULL, 
    [Time] DATETIME NOT NULL, 
    [Author] NCHAR(10) NOT NULL, 
    [Long] FLOAT NOT NULL, 
    [Lat] FLOAT NOT NULL
)
