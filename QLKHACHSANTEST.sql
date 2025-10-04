/* =========================
   RESET (tuỳ chọn)
   ========================= */
IF DB_ID('QLKHACHSANTEST') IS NOT NULL
BEGIN
    ALTER DATABASE QLKHACHSANTEST SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLKHACHSANTEST;
END
GO

/* =========================
   CREATE DATABASE
   ========================= */
CREATE DATABASE QLKHACHSANTEST;
GO
USE QLKHACHSANTEST;
GO

/* =========================
   ENUM TABLES
   ========================= */
CREATE TABLE RoomStatusEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO RoomStatusEnum (Value) VALUES ('Available'),('Booked'),('Occupied'),('Maintenance');

CREATE TABLE BookingStatusEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO BookingStatusEnum (Value) VALUES ('Booked'),('CheckedIn'),('CheckedOut'),('Cancelled');

CREATE TABLE UserStatusEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO UserStatusEnum (Value) VALUES ('Active'),('Inactive'),('Blocked');

CREATE TABLE PaymentMethodEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO PaymentMethodEnum (Value) VALUES ('Cash'),('Card'),('Transfer');

CREATE TABLE PaymentStatusEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO PaymentStatusEnum (Value) VALUES ('Paid'),('Unpaid'),('PartiallyPaid');

CREATE TABLE PricingTypeEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO PricingTypeEnum (Value) VALUES ('Hourly'),('Nightly'),('Daily'),('Weekly');

CREATE TABLE InvoiceStatusEnum ( Value VARCHAR(20) PRIMARY KEY );
INSERT INTO InvoiceStatusEnum (Value) VALUES ('Cancelled'),('Overdue'),('Paid'),('Unpaid'),('PartiallyPaid');

CREATE TABLE GenderEnum ( Value NVARCHAR(10) PRIMARY KEY );
INSERT INTO GenderEnum (Value) VALUES (N'Nam'),(N'Nữ'),(N'Khác');
GO

/* =========================
   ROLES & USERS
   ========================= */
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(MAX) NULL
);
CREATE UNIQUE INDEX UX_Roles_RoleName ON Roles(RoleName);

INSERT INTO Roles (RoleName, Description) VALUES
(N'admin', N'Quyền quản trị'),
(N'receptionist', N'Lễ tân');

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleID INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleID),
    Phone VARCHAR(15),
    Email VARCHAR(100),
    Status VARCHAR(20) NOT NULL DEFAULT 'Active' FOREIGN KEY REFERENCES UserStatusEnum(Value),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE UNIQUE INDEX UX_Users_Username ON Users(Username);

/* Mật khẩu: Matkhau1234 (bcrypt) */
INSERT INTO Users (FullName, Username, PasswordHash, RoleID, Phone, Email, Status, CreatedAt) VALUES
(N'Quản trị viên', 'admin',      '$2b$10$4SAIj3s6O3iNF/1g33mQmeVLhxa/MbLLqWJS5Z0EvzG5kkUrNLjiC', 1, '0900000001', 'admin@qlks.local',      'Active', GETDATE()),
(N'Lễ tân 01',     'reception1', '$2b$10$4SAIj3s6O3iNF/1g33mQmeVLhxa/MbLLqWJS5Z0EvzG5kkUrNLjiC', 2, '0900000002', 'reception1@qlks.local', 'Active', GETDATE()),
(N'Lễ tân 02',     'reception2', '$2b$10$4SAIj3s6O3iNF/1g33mQmeVLhxa/MbLLqWJS5Z0EvzG5kkUrNLjiC', 2, '0900000003', 'reception2@qlks.local', 'Active', GETDATE());
GO

/* =========================
   CUSTOMERS
   ========================= */
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NULL,
    Phone VARCHAR(15),
    Email VARCHAR(100),
    Address NVARCHAR(200),
    IDNumber VARCHAR(20),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Customers_Gender FOREIGN KEY (Gender) REFERENCES dbo.GenderEnum(Value)
);
CREATE INDEX ix_customers_phone ON Customers(Phone);
CREATE INDEX ix_customers_idnumber ON Customers(IDNumber);

INSERT INTO Customers (FullName, Gender, Phone, Email, Address, IDNumber, CreatedAt) VALUES
(N'Nguyễn Văn A', N'Nam', '0911111111', 'a@example.com', N'12 Nguyễn Trãi, HN', '012345678', GETDATE()),
(N'Trần Thị B',   N'Nữ',  '0922222222', 'b@example.com', N'34 Lê Lợi, HCM',      '987654321', GETDATE()),
(N'Lê Văn C',     N'Nam', '0933333333', 'c@example.com', N'56 Hai Bà Trưng, ĐN','111222333', GETDATE()),
(N'Phạm Thị D',   N'Nữ',  '0944444444', 'd@example.com', N'78 Trần Phú, HP',    '444555666', GETDATE()),
(N'Đỗ Văn E',     N'Nam', '0955555555', 'e@example.com', N'90 Nguyễn Huệ, HN',  '777888999', GETDATE()),
(N'Vũ Thị F',     N'Nữ',  '0966666666', 'f@example.com', N'11 Pasteur, HCM',    '222333444', GETDATE()),
(N'Phan Quốc G',  N'Nam', '0977777777', 'g@example.com', N'23 Lý Thường Kiệt, HN','01234567001', GETDATE()),
(N'Bùi Minh H',   N'Nam', '0988888888', 'h@example.com', N'45 Trần Hưng Đạo, HCM','01234560002', GETDATE()),
(N'Hoàng Anh I',  N'Nữ',  '0999999990', 'i@example.com', N'67 Quang Trung, ĐN', '01234560003', GETDATE()),
(N'Đặng Thị J',   N'Nữ',  '0901234567', 'j@example.com', N'89 Lạch Tray, HP',   '01234567004', GETDATE()),
(N'Lý Văn K',     N'Nam', '0812345678', 'k@example.com', N'12 Phan Chu Trinh, HN','01234567005', GETDATE()),
(N'Trương Mỹ L',  N'Nữ',  '0822345678', 'l@example.com', N'34 Nguyễn Huệ, HCM', '01234567006', GETDATE()),
(N'Mai Quốc M',   N'Nam', '0832345678', 'm@example.com', N'56 Điện Biên Phủ, ĐN','01234567007', GETDATE()),
(N'Huỳnh Thị N',  N'Nữ',  '0842345678', 'n@example.com', N'78 Đường 3/2, Cần Thơ','01234567008', GETDATE()),
(N'Tạ Đức O',     N'Khác','0852345678', 'o@example.com', N'90 Hùng Vương, Huế', '01234567009', GETDATE()),
(N'Lương Gia P',  N'Nam', '0862345678', 'p@example.com', N'11 Nguyễn Văn Cừ, Q.5, HCM','01234567010', GETDATE());
GO

/* =========================
   ROOM TYPES & PRICING
   ========================= */
CREATE TABLE RoomTypes (
    RoomTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(MAX) NULL
);

INSERT INTO RoomTypes (TypeName, Description) VALUES
(N'Standard', N'Phòng tiêu chuẩn 1-2 khách'),
(N'Deluxe',   N'Phòng rộng, view đẹp'),
(N'Suite',    N'Phòng cao cấp, có phòng khách');

CREATE TABLE RoomPricing (
    PricingID INT IDENTITY(1,1) PRIMARY KEY,
    RoomTypeID INT NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomTypeID),
    PricingType VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES PricingTypeEnum(Value),
    Price DECIMAL(10,2) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT uq_roompricing_type UNIQUE (RoomTypeID, PricingType)
);

/* Standard */
INSERT INTO RoomPricing (RoomTypeID, PricingType, Price, IsActive) VALUES
(1,'Hourly',  80000, 1),
(1,'Nightly', 350000,1),
(1,'Daily',   500000,1),
(1,'Weekly',  3000000,1);
/* Deluxe */
INSERT INTO RoomPricing (RoomTypeID, PricingType, Price, IsActive) VALUES
(2,'Hourly',  120000,1),
(2,'Nightly', 550000,1),
(2,'Daily',   750000,1),
(2,'Weekly',  4900000,1);
/* Suite */
INSERT INTO RoomPricing (RoomTypeID, PricingType, Price, IsActive) VALUES
(3,'Hourly',  180000,1),
(3,'Nightly', 950000,1),
(3,'Daily',   1300000,1),
(3,'Weekly',  8500000,1);
GO

/* =========================
   ROOMS
   ========================= */
CREATE TABLE Rooms (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomNumber VARCHAR(10) NOT NULL UNIQUE,
    RoomTypeID INT NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomTypeID),
    Status VARCHAR(20) NOT NULL DEFAULT 'Available' FOREIGN KEY REFERENCES RoomStatusEnum(Value),
    Note NVARCHAR(200)
);
CREATE INDEX ix_rooms_type_status ON Rooms(RoomTypeID, Status);

INSERT INTO Rooms (RoomNumber, RoomTypeID, Status, Note) VALUES
('101', 1, 'Available',   N'Gần thang máy'),
('102', 1, 'Available',   N'Gần lễ tân'),
('103', 1, 'Maintenance', N'Đang bảo trì'),
('104', 1, 'Available',   N'Sạch sẽ'),
('105', 1, 'Booked',      N'Khách sắp đến'),
('201', 2, 'Available',   N'View phố'),
('202', 2, 'Available',   N'Ban công'),
('203', 2, 'Occupied',    N'Đang sử dụng'),
('301', 3, 'Available',   N'Phòng góc'),
('302', 3, 'Available',   N'Tầng cao');
GO

/* =========================
   BOOKING (Header) + BOOKING ROOMS (Lines)
   ========================= */
CREATE TABLE Bookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
    CreatedBy INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status VARCHAR(20) NOT NULL DEFAULT 'Booked' FOREIGN KEY REFERENCES BookingStatusEnum(Value),
    Notes NVARCHAR(MAX) NULL
);

CREATE TABLE BookingRooms (
    BookingRoomID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    RoomID INT NOT NULL,
    PricingID INT NOT NULL,
    CheckInDate DATETIME NULL,
    CheckOutDate DATETIME NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Booked',
    Note NVARCHAR(500) NULL,
    CONSTRAINT FK_BookingRooms_Bookings FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID) ON DELETE CASCADE,
    CONSTRAINT FK_BookingRooms_Rooms FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    CONSTRAINT FK_BookingRooms_Pricing FOREIGN KEY (PricingID) REFERENCES RoomPricing(PricingID),
    CONSTRAINT FK_BookingRooms_Status FOREIGN KEY (Status) REFERENCES BookingStatusEnum(Value)
);
CREATE INDEX ix_bookingrooms_room_status ON BookingRooms(RoomID, Status);
CREATE INDEX ix_bookingrooms_checkin ON BookingRooms(CheckInDate);
CREATE INDEX ix_bookingrooms_checkout ON BookingRooms(CheckOutDate);

-- Trigger chặn trùng lịch (Booked/CheckedIn) cho cùng RoomID
GO
CREATE TRIGGER TR_BookingRooms_NoOverlap
ON BookingRooms
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN BookingRooms br
          ON br.RoomID = i.RoomID
         AND br.BookingRoomID <> i.BookingRoomID
         AND br.Status IN ('Booked','CheckedIn')
         AND i.Status  IN ('Booked','CheckedIn')
         AND (br.CheckInDate  < ISNULL(i.CheckOutDate, '9999-12-31'))
         AND (ISNULL(i.CheckInDate, '0001-01-01') < br.CheckOutDate)
    )
    BEGIN
        RAISERROR (N'Room is already booked/occupied in the given time range.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

/* =========================
   SERVICES & BOOKING ROOM SERVICES (dịch vụ theo từng phòng)
   ========================= */
CREATE TABLE Services (
    ServiceID INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL UNIQUE,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL DEFAULT(0),
    IsActive BIT NOT NULL DEFAULT 1
);

INSERT INTO Services (ServiceName, Price, Quantity, IsActive) VALUES
(N'Giặt ủi',           50000, 100, 1),
(N'Minibar',           80000, 100, 1),
(N'Ăn sáng',           70000, 100, 1),
(N'Đưa đón sân bay',  200000, 100, 1),
(N'Spa',              300000, 100, 1),
(N'Bữa tối',          150000, 100, 1),
(N'Trái cây',          60000, 100, 1),
(N'Nước suối',         20000, 100, 1);

CREATE TABLE BookingRoomServices (
    BookingRoomServiceID INT IDENTITY(1,1) PRIMARY KEY,
    BookingRoomID INT NOT NULL,
    ServiceID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    CONSTRAINT uq_bookingroom_service UNIQUE (BookingRoomID, ServiceID),
    CONSTRAINT FK_BRS_BookingRooms FOREIGN KEY (BookingRoomID) REFERENCES BookingRooms(BookingRoomID) ON DELETE CASCADE,
    CONSTRAINT FK_BRS_Services FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);
GO

/* =========================
   INVOICES & PAYMENTS (tổng theo booking)
   ========================= */
CREATE TABLE Invoices (
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL UNIQUE,
    RoomCharge DECIMAL(10,2) NOT NULL,
    ServiceCharge DECIMAL(10,2) NOT NULL,
    Surcharge DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    Discount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    TotalAmount DECIMAL(10,2) NOT NULL,
    IssuedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IssuedBy INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Status VARCHAR(20) NOT NULL DEFAULT 'Paid' FOREIGN KEY REFERENCES InvoiceStatusEnum(Value),
    Note NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Invoices_Bookings FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID) ON DELETE CASCADE
);

CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    Method VARCHAR(20) NOT NULL DEFAULT 'Cash' FOREIGN KEY REFERENCES PaymentMethodEnum(Value),
    Status VARCHAR(20) NOT NULL DEFAULT 'Paid' FOREIGN KEY REFERENCES PaymentStatusEnum(Value),
    CONSTRAINT FK_Payments_Invoices FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID) ON DELETE CASCADE
);
GO

/* =========================
   SAMPLE DATA: MULTI-ROOM BOOKING + SERVICES THEO PHÒNG
   ========================= */

-- Booking header
INSERT INTO Bookings (CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES (6, 1, '2025-09-23 08:40:00', 'Booked', N'Booking nhiều phòng mẫu'); -- BookingID = 1

-- Dòng phòng 101 (Standard) – Nightly
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
SELECT 1,
       r.RoomID,
       (SELECT PricingID FROM RoomPricing WHERE RoomTypeID = r.RoomTypeID AND PricingType = 'Nightly'),
       '2025-09-23 14:00:00', '2025-09-24 12:00:00',
       'Booked', N'Phòng cho khách A'
FROM Rooms r WHERE r.RoomNumber = '101';

-- Dòng phòng 201 (Deluxe) – Hourly, khung giờ khác
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
SELECT 1,
       r.RoomID,
       (SELECT PricingID FROM RoomPricing WHERE RoomTypeID = r.RoomTypeID AND PricingType = 'Hourly'),
       '2025-09-23 21:00:00', '2025-09-24 03:00:00',
       'Booked', N'Phòng cho khách B'
FROM Rooms r WHERE r.RoomNumber = '201';

-- Dịch vụ gắn THEO TỪNG PHÒNG
-- Phòng 101 dùng "Ăn sáng" x2
INSERT INTO BookingRoomServices (BookingRoomID, ServiceID, Quantity)
SELECT br.BookingRoomID, s.ServiceID, 2
FROM BookingRooms br
JOIN Rooms r ON r.RoomID = br.RoomID AND r.RoomNumber = '101'
JOIN Services s ON s.ServiceName = N'Ăn sáng'
WHERE br.BookingID = 1;

-- Phòng 201 dùng "Minibar" x1 và "Nước suối" x3
INSERT INTO BookingRoomServices (BookingRoomID, ServiceID, Quantity)
SELECT br.BookingRoomID, s.ServiceID, 1
FROM BookingRooms br
JOIN Rooms r ON r.RoomID = br.RoomID AND r.RoomNumber = '201'
JOIN Services s ON s.ServiceName = N'Minibar'
WHERE br.BookingID = 1;

INSERT INTO BookingRoomServices (BookingRoomID, ServiceID, Quantity)
SELECT br.BookingRoomID, s.ServiceID, 3
FROM BookingRooms br
JOIN Rooms r ON r.RoomID = br.RoomID AND r.RoomNumber = '201'
JOIN Services s ON s.ServiceName = N'Nước suối'
WHERE br.BookingID = 1;

-- (Demo) Invoice & Payment tổng theo booking
INSERT INTO Invoices (BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note)
VALUES (1, 350000 + (6 * 120000), (2*70000) + (1*80000) + (3*20000), 0, 0,
        (350000 + 720000) + (140000 + 80000 + 60000), GETDATE(), 1, 'Paid', N'Demo tính nhanh');
-- Thanh toán đầy đủ
INSERT INTO Payments (InvoiceID, Amount, PaymentDate, Method, Status)
SELECT InvoiceID, TotalAmount, GETDATE(), 'Card', 'Paid' FROM Invoices WHERE BookingID = 1;
GO

/* =========================
   HỖ TRỢ TRUY VẤN NHANH (VIEW)
   ========================= */
-- View phẳng hoá Booking-Phòng (dùng cho browse)
CREATE VIEW v_BookingRoomsFlat AS
SELECT 
    b.BookingID, b.CustomerID, b.CreatedBy, b.BookingDate, b.Status AS BookingStatus,
    br.BookingRoomID, br.RoomID, r.RoomNumber, r.RoomTypeID, rt.TypeName AS RoomTypeName,
    br.PricingID, rp.PricingType, rp.Price AS UnitPrice,
    br.CheckInDate, br.CheckOutDate, br.Status AS RoomStatus, br.Note
FROM BookingRooms br
JOIN Bookings b      ON b.BookingID  = br.BookingID
JOIN Rooms r         ON r.RoomID     = br.RoomID
JOIN RoomTypes rt    ON rt.RoomTypeID= r.RoomTypeID
JOIN RoomPricing rp  ON rp.PricingID = br.PricingID;
GO

PRINT 'QLKHACHSANTEST (multi-room booking + per-room services) ready.';
