﻿using System;
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
                this.connexionDB = new MySqlConnection(" Server = 192.168.56.220 ; User ID = cast ; Password = C@st ; Database=fulbank");
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

