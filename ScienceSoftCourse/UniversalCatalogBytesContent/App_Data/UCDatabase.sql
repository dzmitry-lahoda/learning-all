SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ItemTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemTypes_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFieldTypesForItemTypeById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetFieldTypesForItemTypeById]
@ItemTypeId int
AS
SELECT * FROM FieldTypes
WHERE ItemTypeId = @ItemTypeId
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllItemTypes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllItemTypes]
AS
SELECT * FROM ItemTypes
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FieldTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FieldTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TypeOfField] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FieldTypes_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Items_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Fields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[FieldTypeId] [int] NOT NULL,
	[Content] [image] NOT NULL,
 CONSTRAINT [PK_Fields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddItemType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddItemType]
@Id int OUTPUT,
@Name varchar(50)
AS
INSERT INTO ItemTypes
(Name)
VALUES (@Name);
SET @Id = @@IDENTITY
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteItemType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DeleteItemType]
@Id int
AS
DELETE FROM ItemTypes WHERE Id = @Id
RETURN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddFieldType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddFieldType]
@Id int OUTPUT,
@ItemTypeId  int,
@Name varchar(50),
@TypeOfField varchar(50)
AS
INSERT INTO FieldTypes
(ItemTypeId, Name, TypeOfField)
VALUES (@ItemTypeId, @Name, @TypeOfField);
SET @Id = @@IDENTITY
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddItem]
@Id int OUTPUT,
@Name nvarchar(50),
@ItemTypeId int
AS
INSERT INTO Items
(Name,ItemTypeId)
VALUES (@Name,@ItemTypeId);
SET @Id = @@IDENTITY
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetItemsOfItemTypeById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetItemsOfItemTypeById]
@ItemTypeId int
AS
SELECT * FROM Items
WHERE ItemTypeId = @ItemTypeId
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFieldsForItemById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetFieldsForItemById]
@ItemId int
AS
SELECT * FROM Fields
WHERE ItemId = @ItemId
RETURN' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddField]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddField]
@Id int OUTPUT,
@ItemId  int,
@FieldTypeId varchar(50),
@Content image
AS
INSERT INTO Fields
(ItemId, FieldTypeId, Content)
VALUES (@ItemId, @FieldTypeId, @Content);
SET @Id = @@IDENTITY
RETURN
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FieldTypes_ItemTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[FieldTypes]'))
ALTER TABLE [dbo].[FieldTypes]  WITH CHECK ADD  CONSTRAINT [FK_FieldTypes_ItemTypes] FOREIGN KEY([ItemTypeId])
REFERENCES [dbo].[ItemTypes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FieldTypes] CHECK CONSTRAINT [FK_FieldTypes_ItemTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Items_ItemTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Items]'))
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_ItemTypes] FOREIGN KEY([ItemTypeId])
REFERENCES [dbo].[ItemTypes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_ItemTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Fields_FieldTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Fields]'))
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD  CONSTRAINT [FK_Fields_FieldTypes] FOREIGN KEY([FieldTypeId])
REFERENCES [dbo].[FieldTypes] ([Id])
GO
ALTER TABLE [dbo].[Fields] CHECK CONSTRAINT [FK_Fields_FieldTypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Fields_Items]') AND parent_object_id = OBJECT_ID(N'[dbo].[Fields]'))
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD  CONSTRAINT [FK_Fields_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Fields] CHECK CONSTRAINT [FK_Fields_Items]
