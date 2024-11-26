using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
namespace fulbank
{
    internal class Utilisateur
    {
        private static Utilisateur utilActuel;


        private int id;
        private List<int> numComptes;
        private List<float> soldes;
        private List<string> typeComptes;
        private List<string> monnaies;
        private int numCompteChoisi;

        // Constructeur privé
        private Utilisateur(int idUtil)
        {
            this.id = idUtil;
            this.numComptes = new List<int>();
            this.soldes = new List<float>();
            this.typeComptes = new List<string>();
            this.monnaies = new List<string>();
        }
        public void setCompteChoisi(int compteChoisi) { this.numCompteChoisi = compteChoisi; }

        public int getId() { return id; }
        public static Utilisateur getInstance() { return utilActuel; }
        public static void NewInstance(int idUtil) { utilActuel = new Utilisateur(idUtil); }
        public int getCompteChoisi() { return numCompteChoisi; }

        public void PullComptes()
        {
            MySqlConnection BDD = ConnexionBDD.Connexion();
            MySqlCommand cmd = new MySqlCommand(
                "select numeroCompte, typeCompte, solde, Monnaie " +
                "from comptes_utilisateur where titulaire = @id_ or coTitulaire = @id_;", BDD);
            cmd.Parameters.Add(new MySqlParameter("id_", this.id));

            BDD.Open();
            MySqlDataReader data = cmd.ExecuteReader();

            // Réinitialiser les données pour éviter les doublons
            numComptes.Clear();
            soldes.Clear();
            typeComptes.Clear();
            monnaies.Clear();

            while (data.Read())
            {
                numComptes.Add(data.GetInt32(0));
                typeComptes.Add(data.GetString(1));
                soldes.Add(data.GetFloat(2));
                monnaies.Add(data.GetString(3));
            }

            BDD.Close();
        }



        public List<int> GetNumComptes() => new List<int>(numComptes);
        public List<float> GetSoldes() => new List<float>(soldes);
        public List<string> GetTypeComptes() => new List<string>(typeComptes);
        public List<string> GetMonnaies() => new List<string>(monnaies);
    }
}