CREATE TABLE Order_Products (
    order_id INT,
    product_id INT,
    PRIMARY KEY (order_id, product_id),
    --FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    --FOREIGN KEY (product_id) REFERENCES Products(product_id)
);