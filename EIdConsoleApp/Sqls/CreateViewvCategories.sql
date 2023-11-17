create view [vCategories] AS
SELECT ProductCategoryID as category_id, 
[Name] as category_name
from [SalesLT].[ProductCategory]
GO