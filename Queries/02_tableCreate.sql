USE SuperheroesDb;

CREATE TABLE Superhero(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	Name varChar(20),
	Alias varChar(30),
	Origin varChar(20)
)

CREATE TABLE Assistant(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	Name varChar(20),
)

CREATE TABLE Power(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	Name varChar(20),
	Description varChar(255),
)