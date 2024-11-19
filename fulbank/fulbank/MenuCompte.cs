using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*namespace fulbank
{
    public partial class MenuCompte : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelCompte;
        private RoundedPanel panelCoursCrypto;
        private RoundedPanel panelTransaction;
        private RoundedPanel panelAutres;
        private RoundedPanel panelCompte1;
        private RoundedPanel panelCompte2;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuCompte()
        {
            InitializeComponent();
            Initializeform3();
            Methode.CreateDirectionalButtons(
            this,
            BtnHaut_Click,    // Gestionnaire d'événement pour le bouton Haut
            BtnBas_Click,     // Gestionnaire d'événement pour le bouton Bas
            BtnGauche_Click,  // Gestionnaire d'événement pour le bouton Gauche
            BtnDroite_Click,  // Gestionnaire d'événement pour le bouton Droite
            BtnValider_Click, // Gestionnaire d'événement pour le bouton Valider
            BtnRetour_Click,  // Gestionnaire d'événement pour le bouton Retour
            BtnMaison_Click,  // Gestionnaire d'événement pour le bouton Maison
            BtnFermer_Click   // Gestionnaire d'événement pour le bouton Fermer
            );
            UpdatePanelSelection();
            LoadAccountData();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
        {

        }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
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
            // Change the selected panel index downwards
            if (selectedPanelIndex < panels.Length - 1)
            {
                selectedPanelIndex++;
            }
            UpdatePanelSelection();
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            // Empêcher selectedPanelIndex de descendre en dessous de 0
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
            }
            UpdatePanelSelection();
        }


        // Method to update the visual state of the panels based on selection
        private void UpdatePanelSelection()
        {
            // Deselect all panels
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Default color
            }

            // Highlight the selected panel
            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Highlight color
        }

        private void Initializeform3()
        {
            // Configuration générale du formulaire
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des panneaux avec taille par défaut
            panelCompte1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            // Ajout des panneaux au formulaire
            this.Controls.AddRange(new Control[] { panelCompte1, panelCompte2 });

            // Initialisation des labels et leur centrage dans les panneaux
            InitPanelLabel(panelCompte1, "Courant: 1500$");
            InitPanelLabel(panelCompte2, "Épargne: 3000$");

            panels = new RoundedPanel[] { panelCompte1, panelCompte2 };

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();

            // Appeler l'ajustement de la disposition des panneaux
            AdjustPanelLayout();
        }

        // Méthode pour ajuster la disposition des panneaux
        private void AdjustPanelLayout()
        {
            // Largeur et hauteur des panneaux basées sur les dimensions des boutons
            int panelWidth = this.ClientSize.Width * 53 / 90;
            int buttonHeight = this.ClientSize.Height / 5;
            int panelHeight = buttonHeight; // Même hauteur que les boutons de contrôle

            // Largeur des boutons de contrôle (Valider, Retour, Maison, Fermer)
            int buttonWidth = this.ClientSize.Width / 8;
            int buttonSpacing = this.ClientSize.Height / 20; // Même espacement que pour les boutons

            // Position horizontale des panneaux juste à côté des boutons de contrôle
            int panelX = this.ClientSize.Width - buttonWidth - panelWidth - buttonSpacing;

            // Calculer la marge supérieure pour centrer les panneaux verticalement
            int topMargin = (this.ClientSize.Height - (panelHeight * panels.Length + buttonSpacing * (panels.Length - 1))) / 2;

            // Ajustement des panneaux sur l'écran
            for (int i = 0; i < panels.Length; i++)
            {
                RoundedPanel panel = panels[i];

                // Définir la taille et la position des panneaux
                panel.Size = new Size(panelWidth, panelHeight);
                panel.Location = new Point(panelX * 105 / 100, topMargin + i * (panelHeight + buttonSpacing));

                // Centrer le contenu (label) dans le panneau
                if (panel.Controls.Count > 0 && panel.Controls[0] is Label lbl)
                {
                    Methode.CenterControlInParent(lbl);
                }
            }
        }

        // Méthode pour initialiser un label dans un panel
        private void InitPanelLabel(RoundedPanel panel, string text)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", 90, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
        }
    }
}*/

namespace fulbank
    {
        public partial class MenuCompte : Form
        {
            private RoundedPanel panelCompte1;
            private RoundedPanel panelCompte2;
            private RoundedPanel panelCompte3;
            private RoundedPanel panelCompte4;

            private RoundedPanel[] panels; // Panneaux actifs dans une page
            private int currentPageIndex = 0; // Page actuelle
            private int totalPages = 0; // Nombre total de pages

            private List<string> compteInfos; // Stocke les données des comptes

            public MenuCompte()
            {
                InitializeComponent();
                Initializeform3();
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

                // Charger les données des comptes
                LoadAccountData();
            }

            private void LoadAccountData()
            {
                // Récupérer l'utilisateur connecté
                Utilisateur utilisateur = Utilisateur.getInstance();
                if (utilisateur == null)
                {
                    MessageBox.Show("Aucun utilisateur connecté.");
                    return;
                }

                // Charger les données depuis la base de données
                utilisateur.PullComptes();

                // Construire les données sous forme de texte à afficher dans les panels
                compteInfos = new List<string>();
                var numComptes = utilisateur.GetNumComptes();
                var soldes = utilisateur.GetSoldes();
                var types = utilisateur.GetTypeComptes();
                var monnaies = utilisateur.GetMonnaies();

                for (int i = 0; i < numComptes.Count; i++)
                {
                    string info = $"{types[i]}: {soldes[i]} {monnaies[i]} (N° {numComptes[i]})";
                    compteInfos.Add(info);
                }

                // Calculer le nombre total de pages
                totalPages = (int)Math.Ceiling((double)compteInfos.Count / 4);

                // Afficher la première page
                UpdatePage();
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
                int endIndex = Math.Min(startIndex + 4, compteInfos.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    int panelIndex = i % 4; // Position dans les panels (0-3)
                    var panel = panels[panelIndex];
                    string compteInfo = compteInfos[i];

                    // Ajouter un label avec les données du compte
                    InitPanelLabel(panel, compteInfo);
                }

                AdjustPanelLayout();
            }

            private void BtnBas_Click(object sender, EventArgs e)
            {
                if (currentPageIndex < totalPages - 1)
                {
                    currentPageIndex++;
                    UpdatePage();
                }
            }

            private void BtnHaut_Click(object sender, EventArgs e)
            {
                if (currentPageIndex > 0)
                {
                    currentPageIndex--;
                    UpdatePage();
                }
            }

            private void Initializeform3()
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.FromArgb(128, 194, 236);
                this.Text = "FulBank";

                Methode.Fulbank(this);

                // Initialisation des 4 panels fixes
                panelCompte1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
                panelCompte2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
                panelCompte3 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
                panelCompte4 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

                this.Controls.AddRange(new Control[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 });

                panels = new RoundedPanel[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 };

                this.Resize += (s, e) => AdjustPanelLayout();
                AdjustPanelLayout();
            }

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

            private void InitPanelLabel(RoundedPanel panel, string text)
            {
                Label label = new Label
                {
                    Text = text,
                    Font = new Font("Arial", 50, FontStyle.Bold),
                    ForeColor = Color.FromArgb(128, 194, 236),
                    AutoSize = true
                };
                panel.Controls.Add(label);
                Methode.CenterControlInParent(label);
            }
        private void BtnValider_Click(object sender, EventArgs e)
        {

        }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }

        private void BtnMaison_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }
    }
}