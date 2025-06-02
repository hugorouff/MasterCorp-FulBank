using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using IniParser;
using IniParser.Model;
using System.Resources;

namespace fulbank
{
    internal class ConnexionBDD : IDisposable
    {
        static public ConnexionBDD connexion;
        private MySqlConnection connexionDB;
        private bool disposed = false; // Indique si l'objet a déjà été libéré
        private string connectionString;

        private ConnexionBDD()
        {
            try
            {
                // Chemin du fichier de configuration
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DatabaseConfig.ini");

                // Vérifier si le fichier existe
                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"Fichier de configuration non trouvé : {configPath}");
                }

                // Lire le fichier de configuration
                var parser = new FileIniDataParser();
                IniData configData = parser.ReadFile(configPath);

                // Récupérer les paramètres de connexion
                string server = configData["DatabaseSettings"]["Server"];
                string userId = configData["DatabaseSettings"]["UserID"];
                string password = configData["DatabaseSettings"]["Password"];
                string database = configData["DatabaseSettings"]["Database"];
                string dabId = configData["InfoDab"]["DabID"];

                // Set id au dab
                InfoDab.DabId = dabId;

                //MessageBox.Show(InfoDab.DabId);
                //MessageBox.Show($"le ini {dabId}");
                //MessageBox.Show($"la static { InfoDab.DabId}");

                // Construire la chaîne de connexion
                connectionString = $"Server={server};User ID={userId};Password={password};Database={database}";

                // Créer et tester la connexion
                this.connexionDB = new MySqlConnection(connectionString);
                connexionDB.Open();
                //MySqlCommand cmd = new MySqlCommand("set role Role_App;");
                //cmd.ExecuteNonQuery();
                connexionDB.Close();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Erreur de fichier : {ex.Message}");
            }
            catch (IniParser.Exceptions.ParsingException ex)
            {
                MessageBox.Show($"Erreur de parsing du fichier INI : {ex.Message}");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de connexion MySQL : {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue : {ex.Message}");
            }
        }

        public static MySqlConnection Connexion()
        {
            if (ConnexionBDD.connexion is null)
            {
                ConnexionBDD.connexion = new ConnexionBDD();
                return ConnexionBDD.connexion.connexionDB;
            }
            else
            {
                return ConnexionBDD.connexion.connexionDB;
            }
        }

        // Implémentation du pattern Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Empêche le ramasse-miettes d'appeler le finaliseur
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Libère les ressources managées
                    if (connexionDB != null)
                    {
                        connexionDB.Dispose();
                        connexionDB = null;
                    }
                }
                // Libère les ressources non managées ici si nécessaire
                disposed = true;
            }
        }

        // Finaliseur (au cas où Dispose n'est pas appelé explicitement)
        ~ConnexionBDD()
        {
            Dispose(false);
        }

        public void MyOpen()
        {
            if (connexionDB.State != ConnectionState.Open)
            {
                connexionDB.Open();
            }
        }

        public void MyClose()
        {
            if (connexionDB != null && connexionDB.State == ConnectionState.Open)
            {
                connexionDB.Close();
            }
        }
    }
}