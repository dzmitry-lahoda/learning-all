use Store
go

CREATE TABLE articles (
  Id int NOT NULL default '0',
  Athour nvarchar(50) default '',
  Title nvarchar(50) default '',
  Body text default '',
  Tags text default '',
  LastUpdate datetime NOT NULL,
  Version timestamp NOT NULL,
  PRIMARY KEY  (Id)
)
go