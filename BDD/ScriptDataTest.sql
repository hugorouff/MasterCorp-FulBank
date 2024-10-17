use fulbank;

delimiter $$
show tables;
select * from  CompteBanquaire ;
select * from  DAB;
select * from  Monnaie;
select * from  Opperation;
select * from  Profiles;
select * from  TypeCompte;
select * from  Utilisateur;
select * from  comptes_utilisateur;
select * from  opperations_utilisateur;
select * from  opperations_utilisateur_nom$$
delimiter ;






insert into DAB(addresse)
values ("1 rue champole"),
("3 rue paul Deschanel"),
("16 rue tauchon"),
("8 route des étilleux"),
("969 avenu des près");

insert into Monnaie(nom)
values ("euro"),
("dollar"),
("Bitcoin"),
("doublon"),
("eternium"),
("Ferendium(inexistant)");

insert into Profiles(labelle)
values ("utilisateur"),
("banquier");

insert into TypeCompte(label)
values ("courant"),
("epargne"),
("crypto");

insert into Utilisateur(motDePasse, nom, prenom, courielle, numerotelephone,typeProfile)
values ("motDePasseSecure","F","Kevin","Kferondon@lyceefulbert.fr","000000000000000",1),
("motdep@stropcool","H","kevin","Khuet@lyceefulbert.fr","001122334455667",1),
("MOTDEPASSESIOSISR","S","KEVIN","soulierK@lyceefulbert.fr","2623151214658",1),
("MOTPASSECAST","alex","CAST","Castagna@lyceefulbert.fr","123456789111315",2),
("asazeazzeaze","L","Leblanc","lLeblanc@lyceefulbert.fr","123456789123456",1);

insert into CompteBanquaire(numeroDeCompte,solde,coTitulaire,`type`,titulaire, monaie)
values	(123456789,19564512.99,null,3,1,6),
		(019876521,33333333.33,null,1,2,1),
        (010204050,100,2,1,1,1),
        (1020,0.99,null,1,3,2),
        (13214566,1000,null,2,5,1);
        
insert into Opperation (dateOperation,montant,tauxDeChange,monaie, DAB,compte,compteCible)
values("1111-11-11 11:11:11",100,1,1,1,010204050,019876521),
("1111-11-11 11:11:11",120,1,5,2,123456789,null),
("1954-05-14 16:39:40",100,1,1,1,null,13214566); 

#*/