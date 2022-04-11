ALTER TABLE [dbo].GroupDetails
	ADD CONSTRAINT [FK_GroupDetail_Performers]
	FOREIGN KEY (PerformerId)
	REFERENCES Performers (Id)
