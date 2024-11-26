use fulbank;
/*
show tables;

explain Opperation;

drop view opperations_utilisateur;
create view opperations_utilisateur as 
select id,compte , compteCible, montant, dateOperation
from Opperation where suprimee is false;


drop view opperations_utilisateur_nom;
create view opperations_utilisateur_nom as 
select O.id, O.compte ,UP.nom as 'UtilisateurSource' , O.compteCible, US.nom as 'UtilisateurCible', O.montant, O.dateOperation
from Opperation as O left join CompteBanquaire as CBP on O.compte = CBP.numeroDeCompte
left join CompteBanquaire as CBS on O.compteCible = CBS.numeroDeCompte
left join Utilisateur as UP on CBP.titulaire = UP.id
left join Utilisateur as US on CBS.titulaire = US.id
where suprimee is false;
#*/
drop view comptes_utilisateur;
create view comptes_utilisateur as
select numeroDeCompte as numeroCompte , titulaire as titulaire, coTitulaire as coTitulaire, label as typeCompte, solde as solde,M.nom as monnaie
from CompteBanquaire as CB inner join Monnaie as M on M.id = CB.monaie 
inner join TypeCompte as TC on TC.id = CB.`type`
left join Utilisateur as UT on UT.id = CB.titulaire
left join Utilisateur as UCT on UCT.id = CB.coTitulaire;
#*/

delimiter $$
show tables;
select * from  CompteBanquaire ;
select * from  Utilisateur;
select * from  comptes_utilisateur;

explain Utilisateur;
explain CompteBanquaire;
$$
delimiter ;



create view compte_nom_utilisateur as
select T.nom as "nom_titulaire", T.prenom as "prenom titulaire",CT.nom as "nom_co_titulaire", CT.prenom as "prenom_co_titulaire" , CB.numeroDeCompte, TC.label, CB.solde, M.nom as "nom_monnaie"
from CompteBanquaire as CB inner join Utilisateur as T on CB.titulaire = T.id;


select * from comptes_utilisateur