use fulbank;

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

create view comptes_utilisateur as
select numeroDeCompte, label, solde,M.nom
from CompteBanquaire as CB inner join Monnaie as M on M.id = CB.monaie 
inner join TypeCompte as TC on TC.id = CB.`type`;


drop view test;
create view test as
select * from Opperation;