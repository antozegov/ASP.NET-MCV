SET IDENTITY_INSERT dbo.Product OFF INSERT INTO dbo.Product
(
	Name,
	Price,
	CategoryId,
	IsActive,
	Created,
	Modified
)
VALUES
(
	'Coca-Cola',        -- Name - varchar(50)
	6.99,       -- Price - float
	2,         -- CategoryId - int
	1,      -- IsActive - bit
	GETDATE(), -- Created - datetime
	GETDATE()  -- Modified - datetime
	)
SET IDENTITY_INSERT dbo.Product ON