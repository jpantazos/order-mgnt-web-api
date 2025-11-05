IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Sku] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [CreatedAtUtc] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE TABLE [OrderItems] (
        [Id] int NOT NULL IDENTITY,
        [OrderId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Quantity] int NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] ON;
    EXEC(N'INSERT INTO [Customers] ([Id], [Email], [Name])
    VALUES (1, N''john.smith@email.com'', N''John Smith''),
    (2, N''sarah.johnson@email.com'', N''Sarah Johnson''),
    (3, N''michael.brown@email.com'', N''Michael Brown''),
    (4, N''emily.davis@email.com'', N''Emily Davis''),
    (5, N''david.wilson@email.com'', N''David Wilson''),
    (6, N''lisa.anderson@email.com'', N''Lisa Anderson''),
    (7, N''james.taylor@email.com'', N''James Taylor''),
    (8, N''maria.garcia@email.com'', N''Maria Garcia''),
    (9, N''robert.martinez@email.com'', N''Robert Martinez''),
    (10, N''jennifer.lee@email.com'', N''Jennifer Lee'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Sku', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] ON;
    EXEC(N'INSERT INTO [Products] ([Id], [Name], [Sku], [UnitPrice])
    VALUES (1, N''Wireless Mouse'', N''PRD-001'', 29.99),
    (2, N''Mechanical Keyboard'', N''PRD-002'', 89.99),
    (3, N''USB-C Cable'', N''PRD-003'', 12.99),
    (4, N''Monitor Stand'', N''PRD-004'', 45.99),
    (5, N''Webcam HD'', N''PRD-005'', 59.99),
    (6, N''Headset'', N''PRD-006'', 79.99),
    (7, N''Laptop Sleeve'', N''PRD-007'', 24.99),
    (8, N''Desk Lamp'', N''PRD-008'', 34.99),
    (9, N''External SSD 1TB'', N''PRD-009'', 129.99),
    (10, N''Ergonomic Chair Cushion'', N''PRD-010'', 39.99)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Sku', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAtUtc', N'CustomerId', N'Notes', N'Status') AND [object_id] = OBJECT_ID(N'[Orders]'))
        SET IDENTITY_INSERT [Orders] ON;
    EXEC(N'INSERT INTO [Orders] ([Id], [CreatedAtUtc], [CustomerId], [Notes], [Status])
    VALUES (1, ''2025-08-31T10:10:01.4352989Z'', 1, N''Test order in Pending status'', 0),
    (2, ''2025-10-17T10:10:01.4353156Z'', 2, N''Test order in Approved status'', 1),
    (3, ''2025-10-18T10:10:01.4353162Z'', 3, N''Test order in Shipped status'', 2),
    (4, ''2025-09-13T10:10:01.4353166Z'', 4, N''Test order in Cancelled status'', 3),
    (5, ''2025-10-15T10:10:01.4353195Z'', 5, N''Order for customer 5'', 1),
    (6, ''2025-08-26T10:10:01.4353215Z'', 6, N''Order for customer 6'', 2),
    (7, ''2025-10-14T10:10:01.4353221Z'', 7, N''Order for customer 7'', 3),
    (8, ''2025-10-09T10:10:01.4353226Z'', 8, N''Order for customer 8'', 1),
    (9, ''2025-09-15T10:10:01.4353232Z'', 9, N''Order for customer 9'', 1),
    (10, ''2025-09-26T10:10:01.4353239Z'', 10, N''Order for customer 10'', 1),
    (11, ''2025-10-26T10:10:01.4353248Z'', 6, NULL, 3),
    (12, ''2025-10-16T10:10:01.4353251Z'', 4, NULL, 0),
    (13, ''2025-09-11T10:10:01.4353252Z'', 9, NULL, 0),
    (14, ''2025-08-10T10:10:01.4353254Z'', 2, NULL, 2),
    (15, ''2025-10-26T10:10:01.4353255Z'', 2, NULL, 1),
    (16, ''2025-10-29T10:10:01.4353257Z'', 9, NULL, 2),
    (17, ''2025-09-25T10:10:01.4353258Z'', 8, N''Order #17'', 0),
    (18, ''2025-08-21T10:10:01.4353264Z'', 1, N''Order #18'', 2),
    (19, ''2025-08-22T10:10:01.4353267Z'', 3, NULL, 1),
    (20, ''2025-10-26T10:10:01.4353269Z'', 2, NULL, 2),
    (21, ''2025-09-22T10:10:01.4353270Z'', 1, NULL, 2),
    (22, ''2025-10-28T10:10:01.4353272Z'', 3, N''Order #22'', 1),
    (23, ''2025-08-27T10:10:01.4353275Z'', 1, N''Order #23'', 0),
    (24, ''2025-08-19T10:10:01.4353277Z'', 1, NULL, 1),
    (25, ''2025-10-29T10:10:01.4353279Z'', 5, N''Order #25'', 3),
    (26, ''2025-10-24T10:10:01.4353282Z'', 1, N''Order #26'', 2),
    (27, ''2025-08-08T10:10:01.4353285Z'', 10, N''Order #27'', 1),
    (28, ''2025-10-26T10:10:01.4353287Z'', 7, N''Order #28'', 1),
    (29, ''2025-08-20T10:10:01.4353290Z'', 5, N''Order #29'', 2),
    (30, ''2025-08-10T10:10:01.4353293Z'', 3, NULL, 2),
    (31, ''2025-08-12T10:10:01.4353294Z'', 1, N''Order #31'', 3),
    (32, ''2025-10-15T10:10:01.4353297Z'', 8, N''Order #32'', 0),
    (33, ''2025-08-21T10:10:01.4353300Z'', 4, N''Order #33'', 0),
    (34, ''2025-09-07T10:10:01.4353303Z'', 3, NULL, 2),
    (35, ''2025-08-31T10:10:01.4353305Z'', 8, N''Order #35'', 2),
    (36, ''2025-08-02T10:10:01.4353308Z'', 6, N''Order #36'', 2),
    (37, ''2025-08-08T10:10:01.4353311Z'', 4, N''Order #37'', 0),
    (38, ''2025-10-26T10:10:01.4353313Z'', 3, NULL, 0),
    (39, ''2025-10-21T10:10:01.4353315Z'', 5, N''Order #39'', 3),
    (40, ''2025-10-01T10:10:01.4353318Z'', 2, NULL, 1),
    (41, ''2025-10-29T10:10:01.4353319Z'', 9, NULL, 0),
    (42, ''2025-10-08T10:10:01.4353321Z'', 9, NULL, 3);
    INSERT INTO [Orders] ([Id], [CreatedAtUtc], [CustomerId], [Notes], [Status])
    VALUES (43, ''2025-09-11T10:10:01.4353323Z'', 3, NULL, 2),
    (44, ''2025-10-03T10:10:01.4353364Z'', 2, N''Order #44'', 0),
    (45, ''2025-08-07T10:10:01.4353368Z'', 3, NULL, 3),
    (46, ''2025-08-12T10:10:01.4353369Z'', 2, N''Order #46'', 0),
    (47, ''2025-09-10T10:10:01.4353372Z'', 6, N''Order #47'', 2),
    (48, ''2025-09-25T10:10:01.4353375Z'', 6, N''Order #48'', 3),
    (49, ''2025-08-05T10:10:01.4353378Z'', 4, NULL, 1),
    (50, ''2025-08-18T10:10:01.4353379Z'', 10, N''Order #50'', 0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAtUtc', N'CustomerId', N'Notes', N'Status') AND [object_id] = OBJECT_ID(N'[Orders]'))
        SET IDENTITY_INSERT [Orders] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'OrderId', N'ProductId', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderItems]'))
        SET IDENTITY_INSERT [OrderItems] ON;
    EXEC(N'INSERT INTO [OrderItems] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice])
    VALUES (1, 1, 2, 5, 89.99),
    (2, 2, 5, 3, 59.99),
    (3, 2, 2, 5, 89.99),
    (4, 2, 10, 5, 39.99),
    (5, 3, 4, 3, 45.99),
    (6, 4, 10, 5, 39.99),
    (7, 4, 8, 2, 34.99),
    (8, 4, 4, 3, 45.99),
    (9, 5, 3, 5, 12.99),
    (10, 5, 7, 1, 24.99),
    (11, 5, 9, 4, 129.99),
    (12, 5, 1, 3, 29.99),
    (13, 6, 9, 4, 129.99),
    (14, 6, 10, 5, 39.99),
    (15, 6, 5, 5, 59.99),
    (16, 6, 2, 3, 89.99),
    (17, 7, 7, 3, 24.99),
    (18, 8, 2, 5, 89.99),
    (19, 8, 4, 3, 45.99),
    (20, 8, 8, 2, 34.99),
    (21, 9, 8, 3, 34.99),
    (22, 9, 2, 5, 89.99),
    (23, 9, 1, 1, 29.99),
    (24, 10, 4, 1, 45.99),
    (25, 11, 3, 5, 12.99),
    (26, 11, 5, 2, 59.99),
    (27, 12, 8, 4, 34.99),
    (28, 12, 10, 2, 39.99),
    (29, 13, 4, 1, 45.99),
    (30, 13, 10, 3, 39.99),
    (31, 13, 7, 2, 24.99),
    (32, 13, 1, 3, 29.99),
    (33, 14, 4, 5, 45.99),
    (34, 14, 9, 5, 129.99),
    (35, 14, 1, 2, 29.99),
    (36, 14, 6, 1, 79.99),
    (37, 15, 5, 4, 59.99),
    (38, 15, 7, 5, 24.99),
    (39, 15, 3, 5, 12.99),
    (40, 15, 8, 1, 34.99),
    (41, 16, 7, 1, 24.99),
    (42, 16, 6, 2, 79.99);
    INSERT INTO [OrderItems] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice])
    VALUES (43, 17, 3, 4, 12.99),
    (44, 17, 7, 4, 24.99),
    (45, 18, 7, 1, 24.99),
    (46, 18, 2, 5, 89.99),
    (47, 19, 10, 2, 39.99),
    (48, 19, 1, 3, 29.99),
    (49, 20, 2, 1, 89.99),
    (50, 20, 7, 1, 24.99),
    (51, 21, 2, 4, 89.99),
    (52, 21, 7, 4, 24.99),
    (53, 21, 9, 2, 129.99),
    (54, 21, 5, 3, 59.99),
    (55, 22, 8, 3, 34.99),
    (56, 23, 7, 3, 24.99),
    (57, 23, 10, 4, 39.99),
    (58, 23, 3, 2, 12.99),
    (59, 24, 9, 4, 129.99),
    (60, 24, 4, 3, 45.99),
    (61, 24, 8, 3, 34.99),
    (62, 25, 6, 5, 79.99),
    (63, 25, 1, 3, 29.99),
    (64, 25, 5, 1, 59.99),
    (65, 25, 10, 3, 39.99),
    (66, 26, 5, 4, 59.99),
    (67, 26, 1, 1, 29.99),
    (68, 27, 5, 5, 59.99),
    (69, 27, 10, 3, 39.99),
    (70, 27, 3, 5, 12.99),
    (71, 27, 9, 4, 129.99),
    (72, 28, 9, 3, 129.99),
    (73, 28, 4, 1, 45.99),
    (74, 28, 2, 5, 89.99),
    (75, 29, 10, 3, 39.99),
    (76, 29, 2, 2, 89.99),
    (77, 29, 4, 1, 45.99),
    (78, 30, 7, 1, 24.99),
    (79, 31, 10, 5, 39.99),
    (80, 32, 4, 3, 45.99),
    (81, 32, 2, 4, 89.99),
    (82, 32, 3, 3, 12.99),
    (83, 32, 1, 1, 29.99),
    (84, 33, 1, 3, 29.99);
    INSERT INTO [OrderItems] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice])
    VALUES (85, 33, 8, 3, 34.99),
    (86, 33, 10, 4, 39.99),
    (87, 34, 4, 3, 45.99),
    (88, 34, 9, 4, 129.99),
    (89, 34, 10, 5, 39.99),
    (90, 34, 7, 1, 24.99),
    (91, 35, 10, 5, 39.99),
    (92, 35, 2, 4, 89.99),
    (93, 35, 9, 5, 129.99),
    (94, 35, 5, 5, 59.99),
    (95, 36, 3, 5, 12.99),
    (96, 37, 6, 5, 79.99),
    (97, 37, 10, 1, 39.99),
    (98, 38, 4, 5, 45.99),
    (99, 38, 2, 5, 89.99),
    (100, 38, 9, 3, 129.99),
    (101, 38, 10, 1, 39.99),
    (102, 39, 7, 2, 24.99),
    (103, 39, 2, 1, 89.99),
    (104, 39, 8, 1, 34.99),
    (105, 40, 5, 2, 59.99),
    (106, 41, 10, 4, 39.99),
    (107, 41, 3, 5, 12.99),
    (108, 41, 5, 4, 59.99),
    (109, 42, 1, 2, 29.99),
    (110, 42, 9, 5, 129.99),
    (111, 42, 10, 2, 39.99),
    (112, 43, 5, 5, 59.99),
    (113, 44, 1, 5, 29.99),
    (114, 44, 5, 5, 59.99),
    (115, 44, 8, 5, 34.99),
    (116, 44, 3, 5, 12.99),
    (117, 45, 7, 4, 24.99),
    (118, 46, 8, 3, 34.99),
    (119, 46, 7, 2, 24.99),
    (120, 47, 2, 1, 89.99),
    (121, 47, 5, 3, 59.99),
    (122, 48, 5, 4, 59.99),
    (123, 48, 2, 4, 89.99),
    (124, 49, 4, 5, 45.99),
    (125, 49, 10, 2, 39.99),
    (126, 49, 3, 2, 12.99);
    INSERT INTO [OrderItems] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice])
    VALUES (127, 49, 5, 3, 59.99),
    (128, 50, 2, 5, 89.99)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'OrderId', N'ProductId', N'Quantity', N'UnitPrice') AND [object_id] = OBJECT_ID(N'[OrderItems]'))
        SET IDENTITY_INSERT [OrderItems] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251030101002_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251030101002_InitialCreate', N'8.0.14');
END;
GO

COMMIT;
GO

