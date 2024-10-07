use fulbankBis;
CREATE TABLE DAB(
   identifiant INT,
   addresse VARCHAR(50),
   PRIMARY KEY(identifiant)
);

CREATE TABLE Monaies(
   id INT,
   nom VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE Profiles(
   id INT,
   labelle VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE TypeCompte(
   id INT,
   label VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE etat_operation(
   id INT,
   labelle VARCHAR(50) NOT NULL,
   PRIMARY KEY(id)
);

CREATE TABLE Utilisateur(
   id INT,
   motDePasse VARCHAR(255) NOT NULL,
   nom VARCHAR(50),
   prenom VARCHAR(50),
   courielle VARCHAR(50),
   numeroTelephone VARCHAR(50),
   typeProfile INT,
   PRIMARY KEY(id),
   FOREIGN KEY(typeProfile) REFERENCES Profiles(id)
);

CREATE TABLE CompteBanquaire(
   numeroDeCompte INT,
   solde DECIMAL(12,2),
   coTitulaire INT,
   type INT NOT NULL,
   titulaire INT NOT NULL,
   monaie INT NOT NULL,
   PRIMARY KEY(numeroDeCompte),
   FOREIGN KEY(coTitulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(type) REFERENCES TypeCompte(id),
   FOREIGN KEY(titulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(monaie) REFERENCES Monaies(id)
);

CREATE TABLE Opperation(
   compteCible INT,
   compte INT,
   DAB INT,
   etat INT,
   monaie INT,
   dateOperation DATETIME NOT NULL,
   montant INT,
   tauxDeChange INT,
   PRIMARY KEY(compteCible, compte, DAB, etat, monaie),
   UNIQUE(dateOperation),
   FOREIGN KEY(compteCible) REFERENCES CompteBanquaire(numeroDeCompte),
   FOREIGN KEY(compte) REFERENCES CompteBanquaire(numeroDeCompte),
   FOREIGN KEY(DAB) REFERENCES DAB(identifiant),
   FOREIGN KEY(etat) REFERENCES etat_operation(id),
   FOREIGN KEY(monaie) REFERENCES Monaies(id)
);
