ALTER TABLE [dbo].[Performers]
	ADD CONSTRAINT [FK_Performers_Groups]
	FOREIGN KEY (GroupId)
	REFERENCES [Groups] (Id)
