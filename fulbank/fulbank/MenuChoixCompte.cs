using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySqlConnector;

namespace fulbank
{
    public partial class MenuChoixCompte : Form
    {
        private RoundedPanel panelChoixCompte; // Panel pour "Choix de Compte"
        private List<Account> accounts; // Liste des comptes
        private int selectedAccountIndex = 0; // Index du compte sélectionné
        private Label accountDetailsLabel; // Label pour afficher les détails du compte
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;

        private RoundedPanel panelCompte1;
        private RoundedPanel panelCompte2;
        private RoundedPanel panelCompte3;
        private RoundedPanel panelCompte4;
        private int currentPageIndex = 0; // Page actuelle
        private int totalPages = 0; // Nombre total de pages

        private List<string> compteInfos; // Stocke les données des comptes

        public MenuChoixCompte(string choix)
        {
            this.Icon = new Icon("Resources/logo-fulbank.ico");
            InitializeComponent();

            try
            {
                var utilisateur = Utilisateur.getInstance(); // Vérification de l'utilisateur
                Initializeform3();
                LoadAccountsFromDatabase(); // Charger les comptes depuis la base de données
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Retourner à l'écran de connexion
                Connexion formConnexion = new Connexion();
                formConnexion.Show();
                this.Close();
            }
        }

        private void LoadAccountsFromDatabase()
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

            // Ajouter un label de titre "Choix de Compte" en haut, uniquement une fois
            InitPanelLabelChoix(panelCompte1, "Choix de Compte");

            // Déterminer les données à afficher pour la page actuelle
            int startIndex = currentPageIndex * 4;
            int endIndex = Math.Min(startIndex + 4, compteInfos.Count);

            // Ajouter les labels des comptes dans les panels
            for (int i = startIndex; i < endIndex; i++)
            {
                int panelIndex = i+1 % 4; // Position dans les panels (0-3)
                var panel = panels[panelIndex];
                string compteInfo = compteInfos[i];

                // Ajouter un label avec les données du compte
                InitPanelLabel(panel, compteInfo);
            }

            AdjustPanelLayout();
        }

        private void Initializeform3()
        {
            this.Size = new Size(1580, 1024);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des 4 panels fixes pour les comptes
            panelCompte1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte3 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte4 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            this.Controls.AddRange(new Control[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 });

            panels = new RoundedPanel[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 };

            // Création des boutons de navigation et d'actions
            Methode.CreateDirectionalButtons(this, BtnHaut_Click, BtnBas_Click, BtnGauche_Click, BtnDroite_Click, BtnValider_Click, BtnRetour_Click, BtnMaison_Click, BtnFermer_Click);
            Methode.Fulbank(this);

            this.Resize += (s, e) => AdjustPanelLayout();
            AdjustPanelLayout();
        }

        // Ajuster la disposition des panneaux
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

        // Méthode pour initialiser un label dans un panel avec les données du compte
        private void InitPanelLabel(RoundedPanel panel, string text)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", 45, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
        }

        // Méthode pour initialiser le label "Choix de Compte"
        private void InitPanelLabelChoix(RoundedPanel panel, string text)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", 80, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
        }

        private void UpdatePanel()
        {
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Couleur par défaut
            }

            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Couleur sélectionnée
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex < 3)
            {
                selectedPanelIndex++;
                UpdatePanel();
            }
            else if (currentPageIndex < totalPages - 1)
            {
                currentPageIndex++;
                selectedPanelIndex = 0;
                UpdatePage();
            }
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
                UpdatePanel();
            }
            else if (currentPageIndex > 0)
            {
                currentPageIndex--;
                selectedPanelIndex = 3;
                UpdatePage();

            }
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (accounts != null && accounts.Count > 0)
            {
                // Récupération du compte sélectionné
                var selectedAccount = accounts[selectedAccountIndex];
                string account = selectedAccount.ToString();
                if (account != null) 
                {
                    MenuFinalTransactionDepot finalTransactionMenu = new MenuFinalTransactionDepot();
                    finalTransactionMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Action non définie, impossible de valider.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Aucun compte sélectionné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

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

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }
    }
}