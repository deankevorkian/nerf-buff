CREATE TABLE [dbo].[Users]
(
	[blog_user_name] NCHAR(20) NOT NULL PRIMARY KEY, 
    [blog_user_password] NCHAR(10) NOT NULL, 
    [blog_is_admin] BIT NOT NULL, 
    [blog_user_age] INT NOT NULL 
)
