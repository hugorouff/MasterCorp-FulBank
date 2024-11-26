using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fulbank;


namespace fulbank
{
    public partial class MenuHistoriqueCompts : Form
    {
        private RoundedPanel panelhisto1;
        private RoundedPanel panelhisto2;
        private RoundedPanel panelhisto3;
        private RoundedPanel panelhisto4;

        private RoundedPanel[] panels; // Panneaux actifs dans une page
        private int currentPageIndex = 0; // Page actuelle
        private int selectedPanelIndex = 0;
        private int totalPages = 0; // Nombre total de pages

        //variable de stoquage requete db
        
        private List<int?> comptesSources = new List<int?>();
        private List<int?> comptesDests = new List<int?>();
        private List<string> dateOperations = new List<string>();
        private List<float> montants = new List<float>();
        private List<string> monaies = new List<string>();
        
        public MenuHistoriqueCompts()
        {
            InitializeComponent();
            Initializeform2();
            UpdatePanelSelection();

            // Créer les boutons directionnels
            Methode.CreateDirectionalButtons(
                this,
                BtnHaut_Click,
                BtnBas_Click,
                BtnGauche_Click,
                BtnDroite_Click,
                BtnValider_Click,
                BtnRetour_Click,
                BtnMaison_Click,
                BtnFermer_Click
            );

            LoadAcountData();
        }

        private void LoadAcountData()
        {
            Utilisateur util = Utilisateur.getInstance();
            int numCompt = util.getCompteChoisi();

            MySqlConnection bdd = ConnexionBDD.Connexion();
            MySqlCommand cmd = new MySqlCommand("select compteSource, compteDest, dateOperation, montant, monnaie " +
                "from historique_compte " +
                "where compteSource = @compteSource_ or  compteDest = @CompteDest_", bdd);

            cmd.Parameters.Add(new MySqlParameter("compteSource_", numCompt));
            cmd.Parameters.Add(new MySqlParameter("compteDest_", numCompt));

            bdd.Open();
            MySqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                try { comptesSources.Add(data.GetInt32(0)); }
                catch (Exception e) 
                {
                    comptesSources.Add(null);
                }
                try { comptesDests.Add(data.GetInt32(1)); }
                catch (Exception e) { comptesDests.Add(null); }
                dateOperations.Add(data.GetDateTime(2)+"");
                montants.Add(data.GetFloat(3));
                monaies.Add(data.GetString(4));
            }
            bdd.Close();

            UpdatePage();



        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            MenuCompte form2 = new MenuCompte();
            form2.Show();
            this.Close();
        }

        private void BtnMaison_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex < panels.Length - 1)
            {
                selectedPanelIndex++;
            }
            else if (currentPageIndex < totalPages - 1)
            {
                currentPageIndex++;
                selectedPanelIndex = 0;
            }
            UpdatePanelSelection();
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
            }
            else if (currentPageIndex < 0)
            {
                currentPageIndex++;
                selectedPanelIndex = 0;
            }
            UpdatePanelSelection();
        }

        private void BtnGauche_Click(object sender, EventArgs e) { }
        private void BtnDroite_Click(object sender, EventArgs e) { }
        private void BtnValider_Click(object sender, EventArgs e) { }


        // Mise à jour de la sélection visuelle du panel
        private void UpdatePanelSelection()
        {
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Couleur par défaut
            }

            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Couleur sélectionnée
        }
        private void UpdatePage()
        {
            // Effacer les contenus existants des panels
            foreach (var panel in panels)
            {
                panel.Controls.Clear();
            }

            // Déterminer les données à afficher pour la page actuelle
            int startIndex = currentPageIndex * 4;
            int endIndex = Math.Min(startIndex + 4, comptesSources.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                int panelIndex = i % 4; // Position dans les panels (0-3)
                var panel = panels[panelIndex];

                // Ajouter un label avec les données du compte
                InitPanelLabel(panel, comptesSources[i], comptesDests[i], dateOperations[i], montants[i], monaies[i]);

            }

            AdjustPanelLayout();
            UpdatePanelSelection();
        }

        private void Initializeform2()
        {
            // Configuration générale du formulaire
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            // Appelle la méthode pour afficher le panel de FulBank
            Methode.Fulbank(this);

            // Initialisation des panneaux
            panelhisto1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelhisto2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelhisto3 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelhisto4 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            this.Controls.AddRange(new Control[] { });

            // Initialisation des labels dans chaque panel

            this.Controls.AddRange(new Control[] {panelhisto1, panelhisto2,panelhisto3,panelhisto4});

            // Assigner les panels à la liste
            panels = new RoundedPanel[] { panelhisto1, panelhisto2, panelhisto3, panelhisto4 };

            // Ajouter un événement pour réajuster les panneaux au redimensionnement
            this.Resize += (s, e) => AdjustPanelLayout();
            AdjustPanelLayout();
        }


        // Ajustement de la disposition des panneaux
        private void AdjustPanelLayout()
        {
            int panelWidth = this.ClientSize.Width * 53 / 90;
            int buttonHeight = this.ClientSize.Height / 5;
            int panelHeight = buttonHeight;

            int buttonWidth = this.ClientSize.Width / 8;
            int buttonSpacing = this.ClientSize.Height / 20;

            int panelX = this.ClientSize.Width - buttonWidth - panelWidth - buttonSpacing;

            int topMargin = (this.ClientSize.Height - (panelHeight * panels.Length + buttonSpacing * (panels.Length - 1))) / 2;

            for (int i = 0; i < panels.Length; i++)
            {
                RoundedPanel panel = panels[i];
                panel.Size = new Size(panelWidth, panelHeight);
                panel.Location = new Point(panelX * 105 / 100, topMargin + i * (panelHeight + buttonSpacing));

                if (panel.Controls.Count > 0 && panel.Controls[0] is Label lbl)
                {
                    Methode.CenterControlInParent(lbl);
                }
            }
        }

        // Initialisation des labels dans un panel
        private void InitPanelLabel(RoundedPanel panel, int? compteSource, int? compteDest, string dateOperation, float montant, string monaie )
        {
            string texte = "";
            if (compteSource is not null && compteDest is not null)
            {
                texte += "virement du compte" + compteSource + "\nvers le compte" + compteDest;
            }
            else if (compteSource is null)
            {
                texte += "dépos dans le compte" + compteDest;
            }
            else 
            {
                texte += "retrait depuis le compte " + compteSource;
            } 
            texte += "\nle " + dateOperation + "de " + montant + " " + monaie;

            Label text = new Label
            {
                
                Text = texte,
                Font = new Font("Arial", 50, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(text);
            Methode.CenterControlInParent(text);
        }
    }
}