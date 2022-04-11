ALTER TABLE [dbo].Employees
	ADD CONSTRAINT FK_Employees_Positions
	FOREIGN KEY (PositionId)
	REFERENCES Positions (Id)
