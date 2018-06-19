CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT IDENTITY(1,1), 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL
	CONSTRAINT PK_CustomerID PRIMARY KEY ([CustomerID])
)
GO
CREATE NONCLUSTERED INDEX CustomerIndex ON [Customer] (Email);