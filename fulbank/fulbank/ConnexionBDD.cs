using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector; 

namespace fulbank
{
    internal class ConnexionBDD
    {
        static public ConnexionBDD connexion ;

        private MySqlConnection connexionDB;



        private ConnexionBDD()
        {
            try
            {
                this.connexionDB = new MySqlConnection(" Server = 172.16.119.51 ; User ID = hugorouff ; Password = hugorouff ; Database=FulBank");
                connexionDB.Open();
                connexionDB.Close();
            } 
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}

