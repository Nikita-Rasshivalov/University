ALTER TABLE [dbo].Records
	ADD CONSTRAINT FK_Records_Genres
	FOREIGN KEY (GenreId)
	REFERENCES Genres (Id)
