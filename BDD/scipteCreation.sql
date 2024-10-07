use FulBank;

CREATE TABLE DAB(
   id INT auto_increment,
   addresse VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE Monnaie(
   id INT auto_increment,
   nom VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE `Profiles`(
   id INT auto_increment,
   labelle VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE TypeCompte(
   id INT auto_increment,
   label VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE Utilisateur(
   id INT auto_increment,
   motDePasse VARCHAR(255) NOT NULL,
   nom VARCHAR(50),
   prenom VARCHAR(50),
   courielle VARCHAR(50),
   numeroTelephone VARCHAR(50),
   typeProfile INT,
   PRIMARY KEY(id),
   FOREIGN KEY(typeProfile) REFERENCES `Profiles`(id)
);

CREATE TABLE CompteBanquaire(
   numeroDeCompte INT,
   solde DECIMAL(12,2),
   coTitulaire INT,
   `type` INT NOT NULL,
   titulaire INT NOT NULL,
   monaie INT NOT NULL,
   PRIMARY KEY(numeroDeCompte),
   FOREIGN KEY(coTitulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(`type`) REFERENCES TypeCompte(id),
   FOREIGN KEY(titulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(monaie) REFERENCES Monnaie(id)
);

CREATE TABLE Opperation(
   id INT auto_increment,
   dateOperation DATETIME NOT NULL,
   montant INT not null default 0,
   tauxDeChange INT,
   suprimee bool NOT NULL default false,
   monaie INT,
   DAB INT NOT NULL,
   compte INT,
   compteCible INT,
   PRIMARY KEY(id),
   FOREIGN KEY(monaie) REFERENCES Monnaie(id),
   FOREIGN KEY(DAB) REFERENCES DAB(id),
   FOREIGN KEY(compte) REFERENCES CompteBanquaire(numeroDeCompte),
   FOREIGN KEY(compteCible) REFERENCES CompteBanquaire(numeroDeCompte)
);


# views
create view opperations_utilisateur as 
	select id,compte , compteCible, montant, dateOperation
	from Opperation where suprimee is false;

create view opperations_utilisateur_nom as 
	select O.id, O.compte ,UP.nom as 'UtilisateurSource' , O.compteCible, US.nom as 'UtilisateurCible', O.montant, O.dateOperation
	from Opperation as O left join CompteBanquaire as CBP on O.compte = CBP.numeroDeCompte
	left join CompteBanquaire as CBS on O.compteCible = CBS.numeroDeCompte
	left join Utilisateur as UP on CBP.titulaire = UP.id
	left join Utilisateur as US on CBS.titulaire = US.id
	where suprimee is false;

create view comptes_utilisateur as
	select numeroDeCompte, label, solde,M.nom
	from CompteBanquaire as CB inner join Monnaie as M on M.id = CB.monaie 
	inner join TypeCompte as TC on TC.id = CB.`type`;


# triggers
delimiter $$
create trigger default_user_role before insert
	on Utilisateur for each row
    begin 
		if (new.typeProfile is null) then
        set new.typeProfile = 1;
        end if;
	end$$
    

create trigger block_supress_opperation before delete
on Opperation for each row
 begin
    signal sqlstate '45000'
		set message_text = 'impossible de suprimer, passer le boolen de la variable suprimee à true';
 end$$


#procedures

create procedure soft_delete_operation(in idOpp int)
begin
update Opperation
set suprimee = true
where id = idOpp;
end$$

create procedure opperation_banquaire(in montant_transaction float, in taux_change int, in _DAB int, in compte_source int, in compte_dest int)
begin
	declare montant_compte_source float default 0;
	declare montant_compte_dest float default 0;
	declare monaie_utilise int default null;


	if (compte_source is not null) then
		select solde into montant_compte_source
		from CompteBanquaire
		where numeroDeCompte = compte_source;
	end if;
	if (compte_dest is not null)then
		select solde into montant_compte_dest
		from CompteBanquaire 
		where numeroDeCompte = compte_dest;
	end if;

	update CompteBanquaire
    set solde = montant_compte_source - montant_transaction 
    where numeroDeCompte = 1;
	start transaction;
		if (compte_source is not null and montant_compte_source-montant_transaction >= 0.0) or (compte_source is null and compte_dest is not null) then
			insert into Opperation (dateOperation,montant, tauxDeChange, monaie,DAB, compte, compteCible)
			values(now(), montant_transaction,taux_change, monaie_utilise, _DAB, compte_source, compte_dest);

			if (compte_source is not null )then
			update CompteBanquaire
			set solde = montant_compte_source - montant_transaction
			where numeroDeCompte = compte_source;
			end if;

			if (compte_dest is not null )then
			update CompteBanquaire
			set solde = montant_compte_dest + montant_transaction
			where numeroDeCompte = compte_dest;
			end if;
		end if;
	commit; 
    end$$