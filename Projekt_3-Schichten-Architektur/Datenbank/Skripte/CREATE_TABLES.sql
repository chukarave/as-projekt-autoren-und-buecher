-- Tabellen erstellen

CREATE TABLE T_Autoren
(
	Name			varchar(40),
	Autoren_id		integer PRIMARY KEY
);

CREATE TABLE T_Buecher
(
	Titel			varchar(80),
	ISBN			varchar(20) PRIMARY KEY,
	F_Autoren_id	integer
);

ALTER TABLE T_Buecher ADD FOREIGN KEY (F_Autoren_id) REFERENCES T_Autoren (Autoren_id) 
	ON DELETE CASCADE ON UPDATE CASCADE;
