CREATE TABLE [dbo].[Records]
(
Id int not null identity(1,1) primary key,
СompositionName varchar(450) not null,
PerformerId int not null,
GenreId int not null,
Album varchar(450) not null,
RecordDate date not null,
Lasting int not null,
Rating decimal(2,1) not null
)
