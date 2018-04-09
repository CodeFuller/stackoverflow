DROP TABLE [dbo].[Entries]

CREATE TABLE [dbo].[Entries] (
	[Id] [int] NOT NULL PRIMARY KEY,
	[Name] [nvarchar](max) NOT NULL,
)

INSERT INTO [dbo].[Entries](Id, Name) VALUES (1, 'John Smith');
INSERT INTO [dbo].[Entries](Id, Name) VALUES (2, 'Jeff Goldwin');
