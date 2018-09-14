CREATE TABLE [dbo].[nb_users]
(
	[nb_user_id] INT NOT NULL PRIMARY KEY, 
    [nb_user_name] VARCHAR(20) NOT NULL, 
    [nb_pass_hash] NCHAR(10) NOT NULL
)
