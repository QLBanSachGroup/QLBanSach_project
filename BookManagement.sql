CREATE DATABASE BookManagement;
USE BookManagement;

-- Book Category Table
CREATE TABLE book_category (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(255) NOT NULL,
    code VARCHAR(100) NOT NULL,
    PRIMARY KEY (code)
);

-- Publisher Table
CREATE TABLE publisher (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    phone CHAR(10) NOT NULL,
    email VARCHAR(255) NOT NULL
);

-- Book Table
CREATE TABLE book (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    barcode VARCHAR(255) NOT NULL UNIQUE,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    status TINYINT NOT NULL,
    description NVARCHAR(255),
    priority TINYINT DEFAULT 0,
    number_of_purchases INT DEFAULT 0,
    number_of_views INT DEFAULT 0,
    create_date DATE NOT NULL,
    create_by NVARCHAR(255) NOT NULL,
    modified_date DATE,
    modified_by NVARCHAR(255),
    id_publisher INT NOT NULL,
    code_category VARCHAR(100) NOT NULL,
    FOREIGN KEY (id_publisher) REFERENCES publisher (id),
    FOREIGN KEY (code_category) REFERENCES book_category (code)
);

-- Detail Bill Table
CREATE TABLE detail_bill (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    VAT VARCHAR(255),
    id_book INT NOT NULL,
    FOREIGN KEY (id_book) REFERENCES book (id)
);

-- Author Table
CREATE TABLE author (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    bio VARCHAR(255),
    phone CHAR(10) NOT NULL,
    email VARCHAR(255) NOT NULL
);

-- Role Table
CREATE TABLE role (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(255) NOT NULL,
    code VARCHAR(100) NOT NULL PRIMARY KEY
);

-- User Table
CREATE TABLE [user] (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    full_name NVARCHAR(255) NOT NULL,
    phone CHAR(10) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    email VARCHAR(255),
    image VARCHAR(255),
    image64bit VARCHAR(255),
    user_name VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    gender NCHAR(5) NOT NULL,
    date_of_birth DATE,
    status TINYINT NOT NULL,
    create_date DATE NOT NULL,
    create_by NVARCHAR(255) NOT NULL,
    modified_date DATE,
    modified_by NVARCHAR(255),
    code_role VARCHAR(100) NOT NULL,
    FOREIGN KEY (code_role) REFERENCES role (code)
);

-- Evaluate Table
CREATE TABLE evaluate (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    rate NVARCHAR(100),
    comment NVARCHAR(255),
    create_date DATE,
    id_customer INT NOT NULL,
    id_book INT NOT NULL,
    FOREIGN KEY (id_customer) REFERENCES [user] (id),
    FOREIGN KEY (id_book) REFERENCES book (id)
);

-- Method Table
CREATE TABLE method (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(255),
    processing_fee DECIMAL(10,2),
    currency VARCHAR(10),
    active NVARCHAR(50),
    create_date DATE,
    modified_date DATE,
    created_by NVARCHAR(50),
    modified_by NVARCHAR(50)
);

-- Shipping Table
CREATE TABLE shipping (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    method_name NVARCHAR(20),
    description NVARCHAR(255),
    cost_ship DECIMAL(10,2),
    delivery_time INT, -- Estimated delivery in days
    carrier NVARCHAR(100),
    tracking_url VARCHAR(255),
    active TINYINT,
    create_date DATE,
    modified_date DATE,
    created_by NVARCHAR(50),
    modified_by NVARCHAR(50)
);

-- Bill Table
CREATE TABLE bill (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    total_price DECIMAL(10,2) NOT NULL,
    id_method INT NOT NULL,
    id_shipping INT,
    create_date DATE NOT NULL,
    FOREIGN KEY (id_method) REFERENCES method (id),
    FOREIGN KEY (id_shipping) REFERENCES shipping (id)
);

-- Bill Detail Table (Linking multiple books with a bill)
CREATE TABLE bill_detail (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_bill INT NOT NULL,
    id_book INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    vat DECIMAL(5,2),
    FOREIGN KEY (id_bill) REFERENCES bill (id),
    FOREIGN KEY (id_book) REFERENCES book (id)
);

-- Purchase History Table
CREATE TABLE purchase_history (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_user INT NOT NULL,
    id_book INT NOT NULL,
    purchase_date DATE NOT NULL,
    quantity INT NOT NULL,
    total_price DECIMAL(10,2) NOT NULL,
    id_payment_method INT NOT NULL,
    id_shipping INT NOT NULL,
    status TINYINT NOT NULL,
    FOREIGN KEY (id_book) REFERENCES book (id),
    FOREIGN KEY (id_user) REFERENCES [user] (id),
    FOREIGN KEY (id_payment_method) REFERENCES method (id),
    FOREIGN KEY (id_shipping) REFERENCES shipping (id)
);

-- Book Join Author Table
CREATE TABLE book_join_author (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    role NVARCHAR(255),
    id_book INT NOT NULL,
    id_author INT NOT NULL,
    FOREIGN KEY (id_book) REFERENCES book (id),
    FOREIGN KEY (id_author) REFERENCES author (id)
);

-- Cart Table
CREATE TABLE cart (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_user INT NOT NULL,
    create_date DATE NOT NULL,
    update_date DATE,
    FOREIGN KEY (id_user) REFERENCES [user](id)
);

-- Cart Detail Table (Linking books with cart)
CREATE TABLE cart_detail (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_cart INT NOT NULL,
    id_book INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_cart) REFERENCES cart(id),
    FOREIGN KEY (id_book) REFERENCES book(id)
);

-- Wishlist Table
CREATE TABLE wishlist (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_user INT NOT NULL,
    id_book INT NOT NULL,
    create_date DATE NOT NULL,
    FOREIGN KEY (id_user) REFERENCES [user](id),
    FOREIGN KEY (id_book) REFERENCES book(id)
);

-- Discount Table
CREATE TABLE discount (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    discount_code NVARCHAR(50) NOT NULL,
    description NVARCHAR(255),
    discount_percentage DECIMAL(5,2), -- Discount percentage
    discount_amount DECIMAL(10,2), -- Fixed discount amount
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    status TINYINT NOT NULL
);

-- Bill Discount Table (Linking discounts with bills)
CREATE TABLE bill_discount (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_bill INT NOT NULL,
    id_discount INT NOT NULL,
    FOREIGN KEY (id_bill) REFERENCES bill(id),
    FOREIGN KEY (id_discount) REFERENCES discount(id)
);

-- Payment Table
CREATE TABLE payment (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_bill INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    payment_date DATE NOT NULL,
    status NVARCHAR(50) NOT NULL,
    method VARCHAR(100) NOT NULL, -- e.g., 'Credit Card', 'Cash on Delivery', etc.
    FOREIGN KEY (id_bill) REFERENCES bill(id)
);

-- Indexes
CREATE INDEX idx_book_barcode ON book(barcode);
CREATE INDEX idx_user_user_name ON [user](user_name);
CREATE INDEX idx_user_email ON [user](email);
CREATE INDEX idx_book_name ON book(name);

INSERT INTO book_category (name, code) 
VALUES 
('Tiểu thuyết', 'TC01'),
('Khoa học', 'KH01'),
('Lịch sử', 'LS01'),
('Văn học thiếu nhi', 'VH01'),
('Tâm lý học', 'TL01');

INSERT INTO publisher (name, address, phone, email) 
VALUES 
('NXB Trẻ', '12 Nguyễn Thị Minh Khai, Q1, TP.HCM', '0909123456', 'info@nxbt.vn'),
('NXB Giáo dục', '234 Lý Chính Thắng, Q3, TP.HCM', '0912345678', 'contact@nxbgd.vn');

INSERT INTO author (name, address, bio, phone, email) 
VALUES 
('Nguyễn Nhật Ánh', 'Bình Định', 'Nhà văn nổi tiếng với các tác phẩm cho thiếu nhi', '0918888888', 'nna@book.vn'),
('Trần Đăng Khoa', 'Hà Nội', 'Nhà thơ, nhà văn nổi tiếng với các tác phẩm văn học thiếu nhi', '0922334455', 'tdk@book.vn');

INSERT INTO book (name, barcode, price, quantity, status, description, priority, create_date, create_by, id_publisher, code_category) 
VALUES 
('Cho tôi xin một vé đi tuổi thơ', '893497415', 75000, 200, 1, 'Tác phẩm nổi tiếng của Nguyễn Nhật Ánh', 1, '2024-10-01', 'admin', 1, 'VH01'),
('Đắc Nhân Tâm', '893497416', 95000, 150, 1, 'Cuốn sách về nghệ thuật giao tiếp và thu phục lòng người', 2, '2024-10-01', 'admin', 2, 'TL01');

INSERT INTO role (name, code) 
VALUES 
('Quản trị viên', 'ADMIN'),
('Người dùng', 'USER');

INSERT INTO [user] (full_name, phone, address, email, user_name, password, gender, date_of_birth, status, create_date, create_by, code_role) 
VALUES 
('Nguyễn Văn A', '0912345679', '123 Võ Văn Kiệt, Q1, TP.HCM', 'nva@gmail.com', 'nguyenvana', 'password123', 'Nam', '1990-02-01', 1, '2024-10-01', 'admin', 'USER'),
('Trần Thị B', '0902345678', '456 Điện Biên Phủ, Q3, TP.HCM', 'ttb@gmail.com', 'tranthib', 'password123', 'Nữ', '1995-03-10', 1, '2024-10-01', 'admin', 'USER');

INSERT INTO method (name, description, processing_fee, currency, active, create_date, created_by) 
VALUES 
('Thanh toán qua thẻ tín dụng', 'Thanh toán bằng thẻ Visa, MasterCard', 10000, 'VND', '1', '2024-10-01', 'admin'),
('Thanh toán khi nhận hàng', 'Trả tiền mặt khi giao hàng', 0, 'VND', '1', '2024-10-01', 'admin');

INSERT INTO shipping (method_name, description, cost_ship, delivery_time, carrier, tracking_url, active, create_date, created_by) 
VALUES 
('Giao hàng nhanh', 'Giao hàng trong vòng 2 ngày', 30000, 2, 'Giao hàng nhanh', 'http://trackingurl.com', 1, '2024-10-01', 'admin'),
('Giao hàng tiết kiệm', 'Giao hàng trong vòng 5 ngày', 20000, 5, 'Giao hàng tiết kiệm', 'http://trackingurl.com', 1, '2024-10-01', 'admin');

INSERT INTO bill (total_price, id_method, id_shipping, create_date) 
VALUES 
(200000, 1, 1, '2024-10-05'),
(150000, 2, 2, '2024-10-06');

INSERT INTO cart (id_user, create_date) 
VALUES 
(3, '2024-10-01'),
(2, '2024-10-01');

INSERT INTO wishlist (id_user, id_book, create_date) 
VALUES 
(3, 1, '2024-10-03'),
(2, 2, '2024-10-03');

INSERT INTO discount (discount_code, description, discount_percentage, discount_amount, start_date, end_date, status) 
VALUES 
('DISCOUNT10', 'Giảm giá 10% cho đơn hàng đầu tiên', 10.00, NULL, '2024-10-01', '2024-12-31', 1),
('DISCOUNT50K', 'Giảm 50,000đ cho đơn hàng trên 500,000đ', NULL, 50000, '2024-10-01', '2024-12-31', 1);

INSERT INTO evaluate (rate, comment, create_date, id_customer, id_book) 
VALUES 
('5', 'Sách rất hay và hữu ích', '2024-10-05', 3, 1),
('4', 'Nội dung tốt nhưng trình bày chưa thu hút', '2024-10-06', 2, 2);

INSERT INTO bill_detail (id_bill, id_book, quantity, price, vat) 
VALUES 
(3, 1, 2, 15000, 10.00),
(4, 2, 1, 75000, 5.00);

INSERT INTO cart_detail (id_cart, id_book, quantity, price) 
VALUES 
(3, 1, 1, 75000),
(2, 2, 1, 95000);

INSERT INTO purchase_history (id_user, id_book, purchase_date, quantity, total_price, id_payment_method, id_shipping, status) 
VALUES 
(2, 1, '2024-10-01', 2, 150000, 1, 1, 1),
(3, 2, '2024-10-02', 1, 95000, 2, 2, 1);

INSERT INTO book_join_author (role, id_book, id_author) 
VALUES 
('Tác giả chính', 1, 1),
('Tác giả phụ', 2, 2);

INSERT INTO detail_bill (quantity, price, VAT, id_book) 
VALUES 
(2, 75000, '10%', 1),
(1, 95000, '5%', 2);

INSERT INTO payment (id_bill, amount, payment_date, status, method) 
VALUES 
(3, 150000, '2024-10-01', 'Thành công', 'Thanh toán qua thẻ tín dụng'),
(4, 95000, '2024-10-02', 'Thành công', 'Thanh toán khi nhận hàng');
