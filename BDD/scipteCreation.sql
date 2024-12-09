use FulBank;

CREATE TABLE if not exists DAB(
   id INT auto_increment,
   addresse VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE if not exists Monnaie(
   id INT auto_increment,
   nom VARCHAR(50),
   sigle varchar(3),
   PRIMARY KEY(id)
);

CREATE TABLE if not exists `Profiles`(
   id INT auto_increment,
   labelle VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE if not exists TypeCompte(
   id INT auto_increment,
   label VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE if not exists  Utilisateur(
   id INT auto_increment,
   motDePasse VARCHAR(255) NOT NULL,
   nom VARCHAR(50),
   prenom VARCHAR(50),
   courielle VARCHAR(50),
   numeroTelephone VARCHAR(15),
   typeProfile INT,
   PRIMARY KEY(id),
   FOREIGN KEY(typeProfile) REFERENCES `Profiles`(id)
);

CREATE TABLE if not exists  CompteBanquaire(
   numeroDeCompte INT,
   solde float,
   coTitulaire INT,
   titulaire INT NOT NULL,
   `type` INT NOT NULL,
   monaie INT NOT NULL,
   PRIMARY KEY(numeroDeCompte),
   FOREIGN KEY(coTitulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(`type`) REFERENCES TypeCompte(id),
   FOREIGN KEY(titulaire) REFERENCES Utilisateur(id),
   FOREIGN KEY(monaie) REFERENCES Monnaie(id)
);

CREATE TABLE if not exists Opperation(
   id INT auto_increment,
   dateOperation DATETIME NOT NULL,
   montant float not null default 0,
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
create view if not exists  opperations_utilisateur as 
	select id,compte , compteCible, montant, dateOperation
	from Opperation where suprimee is false;

create view if not exists opperations_utilisateur_nom as 
	select O.id, O.compte ,UP.nom as 'UtilisateurSource' , O.compteCible, US.nom as 'UtilisateurCible', O.montant, O.dateOperation
	from Opperation as O left join CompteBanquaire as CBP on O.compte = CBP.numeroDeCompte
	left join CompteBanquaire as CBS on O.compteCible = CBS.numeroDeCompte
	left join Utilisateur as UP on CBP.titulaire = UP.id
	left join Utilisateur as US on CBS.titulaire = US.id
	where suprimee is false;

create view if not exists  comptes_utilisateur as
	select numeroDeCompte, label, solde,M.nom
	from CompteBanquaire as CB inner join Monnaie as M on M.id = CB.monaie 
	inner join TypeCompte as TC on TC.id = CB.`type`;

CREATE VIEW if not exists historique_compte AS
    SELECT 
        CBSO.numeroDeCompte AS compteSource,
        CBDE.numeroDeCompte AS compteDest,
        dateOperation AS dateOperation,
        OPP.montant,
        MON.sigle AS monnaie
    FROM
        Opperation AS OPP
            INNER JOIN
        Monnaie AS MON ON OPP.monaie = MON.id
            LEFT JOIN
        CompteBanquaire AS CBSO ON CBSO.numeroDeCompte = OPP.compte
            LEFT JOIN
        CompteBanquaire AS CBDE ON CBDE.numeroDeCompte = OPP.compteCible
    WHERE
        suprimee = FALSE
    ORDER BY dateOperation;

# triggers
delimiter $$
create trigger if not exists default_user_role before insert
	on Utilisateur for each row
    begin 
		if (new.typeProfile is null) then
        set new.typeProfile = 1;
        end if;
	end$$
    

create trigger if not exists  block_supress_opperation before delete
on Opperation for each row
 begin
    signal sqlstate '45000'
		set message_text = 'impossible de suprimer, passer le boolen de la variable suprimee à true';
 end$$


#procedures

create procedure if not exists soft_delete_operation(in idOpp int)
begin
update Opperation
set suprimee = true
where id = idOpp;
end$$

create procedure if not exists opperation_banquaire(in montant_transaction float, in taux_change int, in _DAB int, in compte_source int, in compte_dest int)
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
    end $$
    
create function if not exists checkConnexion(mdp varchar(255), usr int) returns bool
begin
	declare mdp_check varchar(255) ;
	select binary motDePasse into mdp_check from Utilisateur where id = usr;

	if mdp_check = binary mdp 
	then
		return True;
	else
		return false;
	end if;
end $$

create procedure if not exists retrait(in montantR float, in compteSource int, in tauxDeChange int, in DAB int)
begin 					#montant requis 

declare montantPoseder int;
declare monaie_source int;



SELECT solde, monaie INTO montantPoseder, monaie_source 
FROM CompteBanquaire
WHERE numeroDeCompte = compteSource;

if (montantPoseder > montantR)
then

start transaction;


insert into Opperation(dateOperation, montant,compte,tauxDeChange,monaie,DAB)
values(now(),montantR,compteSource,tauxDeChange,monaie_source ,DAB);

update CompteBanquaire 
set solde = solde - montantR
where numeroDeCompte = compteSource;
end if;

commit;

end$$

create procedure if not exists depos(in montantEntrant float, in compteDest int, in tauxDeChange int, in DAB int)
begin

declare monaie int;

select monaie into monaie
from CompteBanquaire 
where numeroDeCompte = compteDest;

start transaction;
insert into Opperation(dateOperation, montant, compteCible,tauxDeChange,monaie,DAB)
values(now(),montantEntrant, compteDest, tauxDeChange, monaie,DAB);

update CompteBanquaire
set solde = solde + montantEntrant
where numeroDeCompte = compteDest;

commit;

end$$

create procedure if not exists virement(in montant_transaction float, in compteSource int, in compteDest int, in tauxDeChange int, in DAB int)
begin 

declare montant_source int;
declare monaie_source int;

select monaie, solde into monaie_source, montant_source
from CompteBanquaire
where numeroDeCompte = compteSource ;

if (montant_source > montant_transaction)
then
start transaction;
insert into Opperation(dateOperation, montant,compte, compteCible,tauxDeChange,monaie,DAB)
values(now(),montant_transaction,compteSource, compteDest, tauxDeChange, monaie_source,DAB);


update CompteBanquaire
set solde = solde + montant_transaction
where numeroDeCompte = compteDest;


update CompteBanquaire 
set solde = solde - montant_transaction
where numeroDeCompte = compteSource;

commit;

end if;

end$$

