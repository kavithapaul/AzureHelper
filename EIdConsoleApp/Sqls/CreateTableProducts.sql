CREATE TABLE [Products] (
    [product_id]   INT            NOT NULL,
    [product_name] NVARCHAR (350)  NULL,
    [category_id]  INT            NULL,
    [price]        MONEY            NULL,
    [description]  NVARCHAR (350) NULL,
    [image_url]    NVARCHAR (350)  NULL,
    [date_added]   DATETIME       NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([product_id] ASC), 
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([category_id]) REFERENCES [Categories]([category_id])
);