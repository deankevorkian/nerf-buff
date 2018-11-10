DELETE FROM Users;

INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'dindush', '420', 69);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'zafig', '69', 42);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (1, 'danda', 'food', 12);
INSERT INTO Users (blog_is_admin, blog_user_name, blog_user_password, blog_user_age) VALUES (0, 'dror', 'equal', 4);

DELETE FROM Events;
--Dana - todo - insert events here

DELETE FROM event_to_user;

-- TODO - Insert event-to-user relations