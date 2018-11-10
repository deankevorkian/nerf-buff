CREATE TABLE [dbo].[event_to_user]
(
	[event_id] INT NOT NULL , 
    [event_user_name] NCHAR(20) NOT NULL, 
    CONSTRAINT [FK_event_to_user_username] FOREIGN KEY ([event_user_name]) REFERENCES [dbo].[Users], 
    CONSTRAINT [FK_event_to_user_eventid] FOREIGN KEY ([event_id]) REFERENCES [dbo].[Events], 
    CONSTRAINT [PK_event_to_user] PRIMARY KEY ([event_id], [event_user_name])
)
