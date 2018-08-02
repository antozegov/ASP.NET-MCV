SET IDENTITY_INSERT dbo.Product OFF UPDATE dbo.Product SET
Name = 'Cola-Cola', Price = 6, CategoryId = 2, IsActive = 1, Modified = GETDATE() WHERE Id = 1
SET IDENTITY_INSERT dbo.Product ON