CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL,
	[DisplayName] VARCHAR(100) NOT NULL, 
)

GO

CREATE INDEX [IX_Users_UsernamePassword] ON [dbo].[Users] ([Username], [Password])
