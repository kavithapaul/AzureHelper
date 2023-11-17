SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [vProducts] AS
SELECT ProductID as product_id,
[Name] as product_name, 
ProductCategoryID as category_id,
ListPrice as price,
'https://images.com/gifs/' + ThumbnailPhotoFileName as image_url,
ModifiedDate as date_added,
[Name]+': sample desc' as [description]
FROM [SalesLT].[Product]
GO