using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySqlConnector;

namespace fulbank
{
    public partial class MenuRetraiDepotEchange : Form
    {
        private RoundedPanel panelChoixCompte;
        private List<Account> accounts; // Liste des comptes
        private int selectedAccountIndex = 0; // Index du compte sélectionné
        private Label accountDetailsLabel; // Label pour afficher les détails du compte
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;

        public MenuRetraiDepotEchange(string action)
        {
            InitializeComponent();

            try
            {
                var utilisateur = Utilisateur.getInstance(); // Vérification de l'utilisateur
                Initializeform3();
                LoadAccountsFromDatabase(); // Charger les comptes depuis la base de données
                DisplayAccountDetails(); // Afficher les détails du compte sélectionné
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

        private void Initializeform3()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation du panneau de choix de compte
            panelChoixCompte = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            this.Controls.AddRange(new Control[] { panelChoixCompte });

            // Initialisation des labels et leur centrage dans les panneaux
            InitPanelLabel(panelChoixCompte, "Choix de Compte");

            panels = new RoundedPanel[] { panelChoixCompte };

            // Appel de la méthode de création des boutons et de l'ajustement
            Methode.CreateDirectionalButtons(this, BtnHaut_Click, BtnBas_Click, BtnGauche_Click, BtnDroite_Click, BtnValider_Click, BtnRetour_Click, BtnMaison_Click, BtnFermer_Click);

            // Initialiser la disposition des panneaux
            AdjustPanelLayout();

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();

            // Ajout d'un label pour les détails du compte
            accountDetailsLabel = new Label
            {
                Font = new Font("Arial", 30, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panelChoixCompte.Controls.Add(accountDetailsLabel);
            Methode.CenterControlInParent(accountDetailsLabel);
            this.Controls.Add(panelChoixCompte);
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

        private void LoadAccountsFromDatabase()
        {
            accounts = new List<Account>();
            try
            {
                var utilisateur = Utilisateur.getInstance(); // Récupérer l'utilisateur actuel

                using (var connection = ConnexionBDD.Connexion())
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    CompteBancaire.numeroDeCompte AS id,
                    CompteBancaire.solde AS balance,
                    Monnaie.nom AS currencyName,
                    TypeCompte.label AS accountType
                FROM 
                    CompteBancaire
                INNER JOIN Monnaie ON CompteBancaire.monnaie = Monnaie.id
                INNER JOIN TypeCompte ON CompteBancaire.type = TypeCompte.id
                WHERE 
                    CompteBancaire.titulaire = @userId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", utilisateur.getId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(new Account
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader.GetString("accountType"),
                                    Balance = reader.GetDecimal("balance"),
                                    Currency = reader.GetString("currencyName")
                                });
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du chargement des comptes : {ex.Message}");
            }
        }

        // Afficher les détails du compte sélectionné
        private void DisplayAccountDetails()
        {
            if (accounts != null && accounts.Count > 0)
            {
                var account = accounts[selectedAccountIndex];
                accountDetailsLabel.Text = $"Compte : {account.Name}\nSolde : {account.Balance:C}";
                Methode.CenterControlInParent(accountDetailsLabel);
            }
            else
            {
                accountDetailsLabel.Text = "Aucun compte disponible";
                Methode.CenterControlInParent(accountDetailsLabel);
            }
        }

        private void DisplayAccountsInPanel()
        {
            // Nettoyer le contenu existant
            panelChoixCompte.Controls.Clear();

            // Vérifier s'il y a des comptes à afficher
            if (accounts != null && accounts.Count > 0)
            {
                int buttonHeight = 80; // Hauteur des boutons
                int spacing = 10; // Espacement entre les boutons
                int yOffset = 20; // Décalage initial

                // Ajouter un bouton pour chaque compte
                for (int i = 0; i < accounts.Count; i++)
                {
                    var account = accounts[i];

                    Button accountButton = new Button
                    {
                        Text = $"{account.Name}\nSolde : {account.Balance:C} {account.Currency}",
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(34, 67, 153),
                        Size = new Size(panelChoixCompte.Width - 40, buttonHeight),
                        Location = new Point(20, yOffset),
                        Tag = i, // Stocker l'index pour l'identification
                        FlatStyle = FlatStyle.Flat
                    };

                    accountButton.FlatAppearance.BorderSize = 0;

                    // Ajouter un événement pour sélectionner un compte lors du clic
                    accountButton.Click += (s, e) =>
                    {
                        selectedAccountIndex = (int)((Button)s).Tag; // Récupérer l'index à partir du Tag
                        DisplayAccountDetails(); // Mettre à jour les détails
                    };

                    panelChoixCompte.Controls.Add(accountButton);
                    yOffset += buttonHeight + spacing; // Ajuster la position pour le bouton suivant
                }
            }
            else
            {
                Label noAccountsLabel = new Label
                {
                    Text = "Aucun compte disponible",
                    Font = new Font("Arial", 20, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true
                };
                panelChoixCompte.Controls.Add(noAccountsLabel);
                Methode.CenterControlInParent(noAccountsLabel);
            }
        }

        // Navigation vers le compte précédent
        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (accounts != null && accounts.Count > 0)
            {
                selectedAccountIndex = (selectedAccountIndex - 1 + accounts.Count) % accounts.Count;
                DisplayAccountDetails();
            }
        }

        // Navigation vers le compte suivant
        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (accounts != null && accounts.Count > 0)
            {
                selectedAccountIndex = (selectedAccountIndex + 1) % accounts.Count;
                DisplayAccountDetails();
            }
        }

        // Valider la sélection du compte
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (accounts != null && accounts.Count > 0)
            {
                var selectedAccount = accounts[selectedAccountIndex];
                MessageBox.Show($"Compte sélectionné : {selectedAccount.Name}", "Validation");
                // Passez à l'étape suivante avec le compte sélectionné
            }
        }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            MenuTransaction form2 = new MenuTransaction();
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

    // Classe pour représenter un compte bancaire
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } // Type de compte (e.g., "Courant", "Épargne")
        public decimal Balance { get; set; }
        public string Currency { get; set; } // Nom de la monnaie
    }
}