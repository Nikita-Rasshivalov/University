ALTER TABLE [dbo].Broadcasts
	ADD CONSTRAINT FK_Broadcasts_Records
	FOREIGN KEY (RecordId)
	REFERENCES Records (Id)
