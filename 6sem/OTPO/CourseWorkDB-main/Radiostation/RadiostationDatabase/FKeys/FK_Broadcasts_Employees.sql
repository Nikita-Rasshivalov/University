ALTER TABLE [dbo].Broadcasts
	ADD CONSTRAINT FK_Broadcasts_Employees
	FOREIGN KEY (EmployeeId)
	REFERENCES [Employees] (Id)
