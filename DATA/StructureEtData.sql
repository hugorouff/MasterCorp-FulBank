use FulBank;

CREATE TABLE if not exists DAB(
   id INT auto_increment,
   addresse VARCHAR(50),
   PRIMARY KEY(id)
);

CREATE TABLE if not exists Monnaie(
   id INT auto_increment,
   nom VARCHAR(50),
   sigle varchar(5),
	labelApi varchar(20),
    isCrypto boolean,
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
  CONSTRAINT fk_profiles FOREIGN KEY(typeProfile) REFERENCES `Profiles`(id)
);

CREATE TABLE if not exists  CompteBanquaire(
   numeroDeCompte INT,
   solde float,
   coTitulaire INT null,
   titulaire INT NOT NULL,
   `type` INT NOT NULL,
   monaie INT NOT NULL,
   PRIMARY KEY(numeroDeCompte),
   CONSTRAINT fk_co_titulaire FOREIGN KEY(coTitulaire) REFERENCES Utilisateur(id),
   CONSTRAINT fk_type FOREIGN KEY(`type`) REFERENCES TypeCompte(id),
   CONSTRAINT fk_titulaire FOREIGN KEY(titulaire) REFERENCES Utilisateur(id),
   CONSTRAINT fk_monaie FOREIGN KEY(monaie) REFERENCES Monnaie(id)
);

CREATE TABLE if not exists Opperation(
   id INT auto_increment,
   dateOperation DATETIME NOT NULL,
   montant float not null default 0, #maybe one for entering money and one for outgoing money 
   tauxDeChange float,
   monaie INT, # utile ?
   DAB INT NOT NULL,
   compte INT,
   compteCible INT,
   suprimee bool NOT NULL default false,
   PRIMARY KEY(id),
   CONSTRAINT fk_monai_op FOREIGN KEY(monaie) REFERENCES Monnaie(id),
   CONSTRAINT fk_DAB FOREIGN KEY(DAB) REFERENCES DAB(id),
   CONSTRAINT fk_compte_source FOREIGN KEY(compte) REFERENCES CompteBanquaire(numeroDeCompte),
   CONSTRAINT fk_compte_dest FOREIGN KEY(compteCible) REFERENCES CompteBanquaire(numeroDeCompte)
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

create view if not exists comptes_utilisateur as
    select `CB`.`numeroDeCompte` AS `numeroCompte`,
       `CB`.`titulaire`      AS `titulaire`,
       `CB`.`coTitulaire`    AS `coTitulaire`,
       `TC`.`label`          AS `typeCompte`,
       `CB`.`solde`          AS `solde`,
       `M`.`sigle`           AS `monnaie`
from ((((`FulBank`.`CompteBanquaire` `CB` join `FulBank`.`Monnaie` `M`
         on (`M`.`id` = `CB`.`monaie`)) join `FulBank`.`TypeCompte` `TC`
        on (`TC`.`id` = `CB`.`type`)) left join `FulBank`.`Utilisateur` `UT`
       on (`UT`.`id` = `CB`.`titulaire`)) left join `FulBank`.`Utilisateur` `UCT` on (`UCT`.`id` = `CB`.`coTitulaire`));



# create view if not exists  comptes_utilisateur as
# 	select numeroDeCompte as "numeroCompte", TC.label as , solde,M.nom
# 	from CompteBanquaire as CB inner join Monnaie as M on M.id = CB.monaie
# 	inner join TypeCompte as TC on TC.id = CB.`type`;

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

create view if not exists label_api as
	select 
		CB.numeroDeCompte as numeroDeCompte, 
        M.labelApi as labelApi,
        M.isCrypto as isCrypto
	from CompteBanquaire as CB
	inner join Monnaie as M on CB.monaie = M.id;

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


# procedures

create procedure if not exists soft_delete_operation(in idOpp int)
begin
update Opperation
set suprimee = true
where id = idOpp;
end$$



create procedure if not exists opperation_banquaire(in montant_transaction float, in taux_change float, in _DAB int, in compte_source int, in compte_dest int)
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

#drop procedure if exists changementMonaie$$
create procedure if not exists changementMonaie (in montantR float, in montantA float, in tauxDeChange float, in DAB int, in compteSource int, in compteDest int)
begin 
		declare montantPosederSource float;
        declare montantPosederDest float;
        declare monaieSource int;
        
        if (compteSource is null or compteDest is null) then
			signal sqlstate '45110'
				set message_text = 'compte source ou destination null';
        end if;
        
		if (montantR is null or montantA is null or tauxDeChange is null) then
			signal sqlstate '45100'
				set message_text = 'un montant ou le taux de change est null';
        end if;
        
        select solde, monaie into montantPosederSource, monaieSource
        from CompteBanquaire 
        where numeroDeCompte = compteSource;
        
        select solde into montantPosederDest
        from CompteBanquaire
        where numeroDeCompte = compteDest;
        
        if (montantPosederSource >= montantR)
        then 
            start transaction; 
            insert into Opperation (dateOperation,montant, tauxDeChange, monaie,DAB, compte, compteCible)
			values(now(), montantR ,tauxDeChange, monaieSource, DAB, compteSource, compteDest);
            
			update CompteBanquaire
			set solde = montantPosederSource - montantR
			where numeroDeCompte = compteSource;
			
			update CompteBanquaire
			set solde = montantPosederDest + montantA
			where numeroDeCompte = compteDest;
			
		commit;
        end if;
        
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

DROP procedure retrait;
create procedure if not exists retrait(in montantR float, in compteSource int, in tauxDeChange float, in DAB int)
begin 					#montant requis

declare montantPoseder float;
declare monaie_source int;



SELECT solde, monaie INTO montantPoseder, monaie_source
FROM CompteBanquaire
WHERE numeroDeCompte = compteSource;

if (montantPoseder >= montantR)
then

start transaction;
insert into Opperation
	  (dateOperation, 	montant,	compte,			tauxDeChange,	monaie,			DAB)
values(now(),			montantR,	compteSource,	tauxDeChange,	monaie_source ,	DAB);

update CompteBanquaire
set solde = solde - montantR
where numeroDeCompte = compteSource;
end if;

commit;

end$$

create procedure if not exists depos(in montantEntrant float, in compteDest int, in tauxDeChange float, in DAB int)
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

create procedure if not exists virement(in montant_transaction float, in compteSource int, in compteDest int, in tauxDeChange float, in DAB int)
begin

declare montant_source float;
declare monaie_source int;

select monaie, solde into monaie_source, montant_source
from CompteBanquaire
where numeroDeCompte = compteSource ;

if (montant_source >= montant_transaction)
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

# put grnt role to admin creazting db !!!
# create Role Role_App;

grant select on FulBank.opperations_utilisateur to Role_App;
grant select on FulBank.comptes_utilisateur to Role_App;
grant select on FulBank.opperations_utilisateur_nom to Role_App;
grant select on FulBank.historique_compte to Role_App;
grant select on FulBank.label_api  to Role_App;


grant execute on procedure FulBank.soft_delete_operation to Role_App;
grant execute on procedure FulBank.opperation_banquaire to Role_App;
grant execute on procedure FulBank.changementMonaie to Role_App;
grant execute on procedure FulBank.retrait to Role_App;
grant execute on procedure FulBank.depos to Role_App;
grant execute on procedure FulBank.virement to Role_App;


insert into FulBank.DAB(DAB.id,DAB.addresse)
values (1,"5 rue du test"),(100,"27 rue paul deschanel"),(200,"10 rue des sablier"),(300,"96 rue saint cheron");

insert into FulBank.Monnaie(Monnaie.nom,Monnaie.sigle, Monnaie.labelApi, Monnaie.isCrypto)
values ("binancecoin","BNB","binancecoin",true),
       ("bitcoin", "BTC", "bitcoin", true),
       ("cardano", "ADA", "cardano",true),
       ("celestia", "TIA","celestia",true),
       ("chainlink","LINK","chainlink",true),
       ("cosmos","ATOM","cosmos",true),
       ("dogecoin","DOGE","dogecoin",true),
       ("ethereum","ETH","ethereum",true),
       ("fantom","FTM","fantom",true),
       ("litecoin","LTC","litecoin",true),
       ("monero", "XMR", "monero",true),
       ("neo","NEO","neo",true),
       ("polkadot","DOT","polkadot",true),
       ("ripple","XRP","ripple",true),
       ("shiba-inu","SHIB","shiba-inu",true),
       ("solana","SOL","solana",true),
       ("stellar","XLM","stellar",true),
       ("sui","SUI","sui",true),
       ("tron","TRX","tron",true),
       ("uniswap","UNI","uniswap",true),
       ("usd-coin","USDC","usd-coin",true),
       ("vechain","VET","vechain",true),
       ("wrapped-bitcoin","WBTC","wrapped-bitcoin",true),
       ("dollard", "USD", "usd",false),
       ("euro","EUR","eur",false),
       ("livre sterling", "GBP","gbp",false);

insert into FulBank.Profiles(labelle)
value ("user");

insert into FulBank.TypeCompte(label)
values ("epargne"),("courant"),("crypto");

insert into FulBank.Utilisateur(id, motDePasse, nom, prenom, courielle, numeroTelephone, typeProfile)
values	(1,1,"joncar","morgan","joncart@morgan.com","0784411041",1),
		(100,1234,"castagna","alexandre","castagna@alexandre.com","0784411041",1),
		(200,4321,"roof","hugo","roof@hugo.com","0784411041",1);

insert into CompteBanquaire(numeroDeCompte, solde, coTitulaire, titulaire, type, monaie)
values (100,1000,null,1,1,25),
       (110,1100,null,1,3,1),
       (120,1200,null,1,2,25),
       (101,1010,null,100,1,25),
       (111,1110,1,100,3,1),
       (121,1210,null,100,2,26),
       (200,2000,null,200,1,25),
       (210,2100,null,200,3,3),
       (220,2200,null,200,2,25);

