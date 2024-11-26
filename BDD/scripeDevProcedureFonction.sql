use fulbank;

delimiter $$ 
create procedure soft_delete_operation(in idOpp int)
begin
update Opperation
set suprimee = true
where id = idOpp;
end$$
# taux de change à mêtre vers la fin
drop procedure opperation_banquaire$$
/*
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
#*/
drop function checkConnexion$$
create function checkConnexion(mdp varchar(255), usr int) returns bool
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



create procedure virement(in montant_transaction float, in compteSource int, in compteDest int, in tauxDeChange int, in DAB int)
begin 

declare montant_source int;
declare monaie int;

select monaie, solde into monaie, montant_source
from CompteBanquaire
where numeroDeCompte = compteSource ;

if (montant_source > montant_transaction)
then
start transaction;
insert into Opperation(dateOperation, montant,compte, compteCible,tauxDeChange,monaie,DAB)
values(now(),montantEntrant,compteSource, compteDest, tauxDeChange, monaie,DAB);


update CompteBanquaire
set solde = solde + montant_transaction
where numeroDeCompte = compteDest;


update CompteBanquaire 
set solde = solde - montant_transaction
where numeroDeCompte = compteSource;

commit;

end if;

end$$

drop procedure depos;
create procedure depos(in montantEntrant float, in compteDest int, in tauxDeChange int, in DAB int)
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

drop procedure retrait;
create procedure retrait(in montantR float, in compteSource int, in tauxDeChange int, in DAB int)
begin

declare montantPoseder int;
declare monaie int;



SELECT solde, solde INTO montantPoseder, monaie
FROM CompteBanquaire
WHERE numeroDeCompte = compteSource;

if (montantPoseder > montantR)
then

start transaction;


insert into Opperation(dateOperation, montant,compte,tauxDeChange,monaie,DAB)
values(now(),montantR,compteSource,tauxDeChange,monaie,DAB);

update CompteBanquaire 
set solde = solde - montantR
where numeroDeCompte = compteSource;
end if;

commit;

end$$








select * from comptes_utilisateur;
call opperation_banquaire(300, 1,1, 102012,102013);
select * from comptes_utilisateur;
call opperation_banquaire(300, 1,1, 102013, null);
select * from comptes_utilisateur;
call opperation_banquaire(300, 1,1, null, 102013);
select * from comptes_utilisateur;

select * from Opperation;$$