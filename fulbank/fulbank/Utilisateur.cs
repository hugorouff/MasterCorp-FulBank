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

        Utilisateur(int idUtil)
        {
            this.id = idUtil;
            this.numComptes = new List<int>();
            this.soldes = new List<float>();
            this.typeComptes = new List<string>();
            this.monnaies = new List<string>();
        }

        public int getId()  { return id; }

        public static void NewInstance(int idUtil) 
        {
            Utilisateur.utilActuel = new Utilisateur(idUtil);
        }

        public static Utilisateur getInstance()
        {
            if (utilActuel == null)
            {
                throw new InvalidOperationException("L'utilisateur n'a pas été initialisé. Appelez Utilisateur.NewInstance() avant d'utiliser cette méthode.");
            }
            return utilActuel;
        }


        public static Utilisateur getUser()
        {
            return Utilisateur.utilActuel;
        }
        public List<int> getNumComptes() { return numComptes; }
        public List<float> getSoldes() { return soldes; }
        public List<string> getTypeComptes() { return typeComptes; }
        public List <string> getMonnaies() { return monnaies; }


        public void cleansComptes()
        {
            this.typeComptes.Clear();
            this.monnaies.Clear();
            this.soldes.Clear();
            this.typeComptes.Clear();
        }

        public void PullComptes()
        { 
            MySqlConnection BDD = ConnexionBDD.Connexion();
            MySqlCommand cmd = new MySqlCommand("select numeroCompte,typeCompte,solde,Monnaie from comptes_utilisateur where titulaire = @id_ or coTitulaire = @id_ ;", BDD);
            cmd.Parameters.Add(new MySqlParameter("id_", this.id));
            BDD.Open();
            MySqlDataReader data = cmd.ExecuteReader();
            
            while (data.Read()) {
                this.numComptes.Add(data.GetInt32(0));
                this.soldes.Add(data.GetFloat(2));
                this.typeComptes.Add(data.GetString(1));
                this.monnaies.Add(data.GetString(3));
                }
            BDD.Close();
        }
        
   
    }
}

