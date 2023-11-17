create view [vOrders] AS
select SalesOrderID as order_id,OrderDate as order_date, 
[Customer].FirstName + ' ' + [Customer].LastName as customer_name
from 
[SalesLT].[SalesOrderHeader] inner join [SalesLT].[Customer]
on [SalesOrderHeader].CustomerID = [Customer].CustomerID

GO