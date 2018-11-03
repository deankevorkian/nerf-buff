CREATE TABLE [dbo].[Comments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PostID] INT NULL, 
    [Title] NCHAR(30) NULL, 
    [Author] NCHAR(30) NULL, 
    [Content] NCHAR(500) NULL, 
    [Date] DATETIME NULL, 
	CONSTRAINT [FK_Comments_ToPost] FOREIGN KEY ([PostID]) REFERENCES [dbo].[Posts]
)
