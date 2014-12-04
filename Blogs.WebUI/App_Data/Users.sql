CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Login] NCHAR(10) NULL, 
    [Password] NCHAR(10) NULL, 
    [RoleID] INT NULL
)
