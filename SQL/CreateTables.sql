Create Table Cars(
	Id int identity(1,1) primary key,
	BrandId int not null,
	ColorId int not null,
	ModelYear smallint not null,
	DailyPrice float not null,
	[Description] varchar(100) null
)

Create Table Brands(
	Id int identity(1,1) primary key,
	[Name] varchar(50) not null
)

Create Table Colors(
	Id int identity(1,1) primary key,
	[Name] varchar(50) not null
)

Alter Table Cars Add Constraint FK_BrandId_Brands_Id Foreign Key (BrandId) References Brands(Id)
Alter Table Cars Add Constraint FK_ColorId_Colors_Id Foreign Key (ColorId) References Colors(Id)

CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (100)    NOT NULL,
    [PasswordHash] VARBINARY (500) NOT NULL,
    [PasswordSalt] VARBINARY (500) NOT NULL,
    [Status]       BIT             NOT NULL
);

Create Table Customers(
	Id int identity(1,1) primary key,
	UserId int not null,
	CompanyName varchar (100) null
)

Alter Table Customers Add Constraint FK_Customers_UserId_Users_Id Foreign Key (UserID) References Users(Id)

Create Table Rentals(
	Id int identity(1,1) primary key,
	CarId int not null,
	CustomerId int not null,
	RentDate datetime not null,
	ReturnDate datetime
)

Alter Table Rentals Add Constraint FK_Rentals_CustomerId_Users_Id Foreign Key (CustomerId) References Users(Id)
Alter Table Rentals Add Constraint FK_Rentals_CarId_Cars_Id Foreign Key (CarId) References Cars(Id)

Create Table CarImages(
	Id int identity(1,1) not null primary key,
	CarId int not null,
	ImagePath varchar(100),
	[Date] datetime not null
)

Alter Table CarImages Add Constraint FK_CarImages_CarId_Cars_Id Foreign Key (CarId) References Cars(Id)

CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [PasswordHash] VARBINARY (500) NOT NULL,
    [PasswordSalt] VARBINARY (500) NOT NULL,
    [Status]       BIT             NOT NULL
);

CREATE TABLE [dbo].[UserOperationClaims] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [UserId]           INT NOT NULL,
    [OperationClaimId] INT NOT NULL
);

CREATE TABLE [dbo].[OperationClaims] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NOT NULL
);
