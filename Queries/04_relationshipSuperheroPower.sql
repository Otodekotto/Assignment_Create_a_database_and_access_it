CREATE TABLE SuperheroPower(
	SuperheroId int NOT NULL,
	PowerId int NOT NULL,
	CONSTRAINT PK_SuperheroPower PRIMARY KEY (SuperheroId, PowerId),
	CONSTRAINT FK_SuperheroPower_Superhero FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
	CONSTRAINT FK_SuperheroPower_Power FOREIGN KEY (PowerId) REFERENCES Power(Id)
)

