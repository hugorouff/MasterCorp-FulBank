"# MasterCorp-Fulbank" 

# MasterCorp FulBank
## Description
MasterCorp FulBank est une application de gestion bancaire développée en C#. Elle permet aux utilisateurs de gérer leurs comptes bancaires, d'effectuer des transactions et de consulter leurs historiques d'opérations. L'application intègre également la gestion des cryptomonnaies, offrant la possibilité d'acheter, vendre et suivre différentes cryptomonnaies directement depuis l'interface.

## Prérequis
- .NET 8 SDK
- MariaDB (version 10.5 ou supérieure)
- Visual Studio 2022 (recommandé)

## Installation
### Option 1 : Utiliser l'installateur
1. Téléchargez l'installateur depuis la [page des releases](https://github.com/hugorouff/MasterCorp-FulBank/releases)
2. Exécutez le fichier d'installation et suivez les instructions à l'écran
3. Après l'installation, vous devrez :
   - Configurer le fichier `DatabaseConfig.ini` avec vos informations de connexion à la base de données
   - Créer la base de données et un utilisateur comme indiqué à l'étape suivante
4. Installez l'application en exécutant le programme d'installation

### Option 2 : Installation manuelle
#### Étape 1 : Cloner le dépôt
```bash
git clone https://github.com/hugorouff/MasterCorp-FulBank.git
cd MasterCorp-FulBank
```

#### Étape 2 : Configurer la base de données MariaDB
1. Connectez-vous à votre serveur MariaDB
2. Créez la base et l'utilisateur :
```sql
CREATE DATABASE FulBank;
CREATE USER 'fulbank_user'@'192.168.56.%' IDENTIFIED BY 'mot_de_passe_securise';
CREATE ROLE Role_App;
SET DEFAULT ROLE Role_App FOR AppFuBank@'192.168.56.%';
```
> **Note :** l'utilisateur executant le script de creation doit avoir toute les permission sur la base et pouvoir donner des permission sur au moins les procedure et fonction ainsi que sur les select <br>
pour autoriser un utilisateur a donner des privilége au moment de lui donner des privilège il faut rajouter apres l'utilisateur "WITH GRANT OPTION;" tels que:<br>
> **note :**
> ``GRANT ALL PRIVILEGES ON DATABASE_NAME.* TO 'USERNAME'@'HOSTNAME' WITH GRANT OPTION;``
3. Exécutez le script SQL fourni dans le dossier `sql/` :
```bash
mysql -u fulbank_user -p FulBank < chemin/vers/fulbank_schema_and_data.sql
```
> **Note :** Ce script crée les tables, vues, triggers, procédures stockées, et insère des données de test.  
> **Les données de test peuvent être modifiées, sauf les enregistrements de la table `Monnaie`, indispensables au fonctionnement de l'application.**

#### Étape 3 : Configurer le fichier de connexion
Mettez à jour `DatabaseConfig.ini` :
```ini
[DatabaseSettings]
Server=192.168.56.220 (adresse du serveur)
Database=FulBank
User=fulbank_user
Password=mot_de_passe_securise

[InfoDab]
DabID=1
```

#### Étape 4 : Installer l'application
Exécutez le programme d'installation inclus dans le dépôt pour installer l'application sur votre système.

#### Étape 5 : Lancer l'application
1. Ouvrez le projet dans Visual Studio 2022
2. Compilez et exécutez le projet
3. Créez un compte utilisateur depuis l'application et commencez à l'utiliser

## Fonctionnalités
### Gestion bancaire
- Création et gestion de comptes bancaires
- Transactions : dépôts, retraits, virements
- Consultation de l'historique des transactions

### Gestion des cryptomonnaies
- Achat, vente et suivi de cryptomonnaies
- Gestion des portefeuilles numériques

### Interface utilisateur
- Interface intuitive et ergonomique
- Tableau de bord personnalisable

## Scripts de base de données
Un script complet est disponible dans `sql/fulbank_schema_and_data.sql`. Il contient :
- **Structure** : création des tables, vues, triggers, procédures stockées, fonctions
- **Données de test** : utilisateurs, comptes bancaires, types de comptes, profils
- **Monnaies supportées** : cryptomonnaies et devises classiques pré-enregistrées (à ne pas modifier)

> ⚠️ En cas de modification du script, assurez-vous de **ne pas altérer la table `Monnaie`**, essentielle pour le bon fonctionnement de la gestion de portefeuille crypto.

## Licence
Aucune licence spécifiée.

## Remarques importantes
- Vérifiez que MariaDB est actif avant de lancer l'application
- Même en utilisant l'installateur, la configuration de la base reste nécessaire
- En cas d'erreur de connexion, vérifiez les paramètres de `DatabaseConfig.ini` ainsi que les privilèges de l'utilisateur MariaDB
- Toutes les procédures, triggers, vues et données de tests sont disponibles dans un unique script SQL pour simplifier l'intégration

## Contribution
Pour toute suggestion ou contribution, ouvrez une issue ou une pull request sur [le dépôt GitHub](https://github.com/hugorouff/MasterCorp-FulBank).
