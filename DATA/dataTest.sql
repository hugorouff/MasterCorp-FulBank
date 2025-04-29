use FulBank;

insert into FulBank.DAB(DAB.addresse)
values ("5 rue du test"),("jean paul des chanel");

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
values(1,1,"joncar","morgan","joncart@morgan.com","0784411041",1);

insert into CompteBanquaire(numeroDeCompte, solde, coTitulaire, titulaire, type, monaie)
values (100,1000,null,1,1,25),
       (110,1000,null,1,3,1),
       (120,1000,null,1,2,25);

