create Database Cellular;
go 
use Cellular;
go

create table dbo.Agents(
ID int identity(1,1) not null,
AgentName Nvarchar(100) not null,
AgentPassword Nvarchar(100) not null,
Sales int not null
constraint PK_Agents_ID Primary key (ID)
);

create table ClientTypes(
ID int identity(1,1) not null,
TypeName nvarchar(50) not null,
MinutePrice int not null,
SmsPrice int not null,

constraint PK_ClientTypes_ID Primary key (ID)
);

create table dbo.Clients(
ID int identity(1,1) not null,
IDNumber int not null,
ClientTypeID int not null,
ClientName nvarchar(100) not null,
LastName nvarchar(100) not null,
ClientAddress nvarchar(100) null,
ContactNumber nvarchar(100) null,
CallsToCenter int not null,

constraint PK_Clients_ID Primary key (ID),
constraint FK_Clients_ClientTypes_ClientTypeID foreign key (ClientTypeID) references dbo.ClientTypes(ID)
);

create table Payments(
ID int identity(1,1) not null,
ClientID int not null,
PaymentMonth date not null,
TotalPayment float not null,

constraint PK_Payment_ID Primary key (ID),
constraint FK_Payments_Clients_ClientID foreign key (ClientID) references dbo.Clients(ID)
);

create table Lines
(
ID int identity(1,1) not null,
ClientID int not null,
Number nvarchar(11) not null,
LineStatus nvarchar(100) not null,

constraint PK_Line_ID Primary key (ID),
constraint FK_Lines_Clients_ClientID foreign key (ClientID) references dbo.Clients(ID)
);

create table FavoriteNums
(
ID int identity(1,1) not null,
FirstNumber nvarchar(100) not null,
SecondNumber nvarchar(100) not null,
ThirdNumber nvarchar(100) not null,

constraint PK_FavoriteNums_ID Primary key (ID)
);

create table Packages
(
ID int identity(1,1) not null,
LineID int not null,
PackageName nvarchar(100) not null,
TotalPrice float not null,
MaxMinute int null,
MinutePrice float null,
Discount float null,
FavoriteNumsID int null,
MostCalledNum bit null,
FamilyDiscount bit null

constraint PK_Package_ID Primary key (ID),
constraint FK_Packages_Lines_LineID foreign key (LineID) references dbo.Lines(ID),
constraint FK_Packages_FavoriteNums_FavoriteNumsID foreign key (FavoriteNumsID) references dbo.FavoriteNums(ID)
);

create table Calls
(
ID int identity(1,1) not null,
LineID int not null,
Duration float not null,
CallMonth date not null,
DestinationNumber nvarchar(100) not null

constraint PK_Call_ID Primary key (ID),
constraint FK_Calls_Lines_LineID foreign key (LineID) references dbo.Lines(ID),
);

create table SMS
(
ID int identity(1,1) not null,
LineID int not null,
CallMonth date not null,
DestinationNumber nvarchar(100) not null

constraint PK_SMS_ID Primary key (ID),
constraint FK_SMS_Lines_LineID foreign key (LineID) references dbo.Lines(ID),
);
