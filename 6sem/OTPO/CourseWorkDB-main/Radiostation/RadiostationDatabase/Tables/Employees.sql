CREATE TABLE [dbo].[Employees]
(
	
Id int not null identity(1,1) primary key,
AspNetUserId nvarchar(450) not null,
PositionId int not null,
Education nvarchar(450),
WorkTime int,
)
