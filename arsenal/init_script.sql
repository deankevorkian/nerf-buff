DELETE FROM event_to_user;

DELETE FROM Events;
INSERT INTO [dbo].[Events] ([Id], [Title], [Time], [Author], [Long], [Lat]) VALUES (1, 'deanGame', '2018-12-12 12:12:00', 'dindush', 34.855499, 32.109333)
INSERT INTO [dbo].[Events] ([Id], [Title], [Time], [Author], [Long], [Lat]) VALUES (2, 'a jhgjhgjh', '2018-12-12 15:15:00', 'dindush', 32.222333, 34.11122)
INSERT INTO [dbo].[Events] ([Id], [Title], [Time], [Author], [Long], [Lat]) VALUES (3, 'gilza', '2019-12-13 14:14:00', 'danda', 32.222111, 34.11122)
INSERT INTO [dbo].[Events] ([Id], [Title], [Time], [Author], [Long], [Lat]) VALUES (25, 'dinnnnnnn', '2019-11-15 14:13:00', 'zafig', 32.111112, 34.11112)

DELETE FROM Users;

INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'dindush', '420', 69);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'zafig', '69', 42);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'danda', 'food', 12);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (0, 'dror', 'equal', 4);

INSERT INTO event_to_user (event_id, event_user_name) VALUES (2, 'dindush');
INSERT INTO event_to_user (event_id, event_user_name) VALUES (3, 'zafig');
INSERT INTO event_to_user (event_id, event_user_name) VALUES (25, 'danda');
INSERT INTO event_to_user (event_id, event_user_name) VALUES (2, 'danda');
INSERT INTO event_to_user (event_id, event_user_name) VALUES (3, 'danda');
-- TODO - Insert event-to-user relations