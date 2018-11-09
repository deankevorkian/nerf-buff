CREATE TABLE [dbo].[Comments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PostID] INT NOT NULL, 
    [Author] NCHAR(30) NOT NULL, 
    [Content] NCHAR(500) NOT NULL, 
    [Date] DATETIME NOT NULL, 
	CONSTRAINT [FK_Comments_ToPost] FOREIGN KEY ([PostID]) REFERENCES [dbo].[Posts]
)
