ALTER TABLE GroupDetails
	ADD CONSTRAINT FK_GroupDetail_Groups
	FOREIGN KEY (GroupId)
	REFERENCES Groups (Id)
