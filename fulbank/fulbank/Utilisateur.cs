using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulbank
{
    internal class Utilisateur
    {
        private static Utilisateur utilActuel;


        private int id;

        Utilisateur(int idUtil ) 
        { 
            this.id = idUtil;
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
    }
}