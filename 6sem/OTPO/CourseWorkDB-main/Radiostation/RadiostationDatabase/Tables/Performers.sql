CREATE TABLE [dbo].[Performers]
(
	Id int not null identity(1,1) primary key,
	Name varchar(50) not null,
	Surname varchar(50) not null,
	GroupId int

)
