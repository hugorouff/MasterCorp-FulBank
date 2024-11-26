using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fulbank
{
    public partial class MenuFinalTransactionDepot : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelMontant;
        private RoundedPanel panelCompteChoisi;
        private RoundedPanel panelArgentCompteChoisi;
        private RoundedPanel panelChoixOperation;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel
        private string selectedAction;  // Nouveau champ pour stocker l'action sélectionnée
        private TextBox txtMontant;
        private Label lblChoixOperation;
        private Label lblCompteChoisi;
        private string[] operations = { "Retrait", "Dépôt", "Échange" };
        private int selectedOperationIndex = 0;
        private string selectedAccount = "Compte 1"; // Exemple, tu peux ajouter la sélection dynamique
        private decimal accountBalance = 1000; // Exemple de solde de compte pour les transactions
        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuFinalTransactionDepot()
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
        }

        private void MenuFinalTransaction_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            ExecuteTransaction(); // Exécuter la transaction basée sur l'action sélectionnée
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            Form nextForm = null;

            // Decide which form to show based on the selected panel
            switch (selectedPanelIndex)
            {
                case 0:
                    nextForm = new MenuChoixCompte("Depot");
                    break;
                case 1:
                    this.Close();
                    break;
            }

            if (nextForm != null)
            {
                nextForm.Show();
                this.Hide();
            }
        }

        private void BtnMaison_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }

        /*private void BtnHaut_Click(object sender, EventArgs e)
        {
            // Changer l'index de l'opération vers le haut
            if (selectedOperationIndex > 0)
            {
                selectedOperationIndex--;
                UpdateOperationSelection();
            }
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            // Changer l'index de l'opération vers le bas
            if (selectedOperationIndex < operations.Length - 1)
            {
                selectedOperationIndex++;
                UpdateOperationSelection();
            }
        }

        private void UpdateOperationSelection()
        {
            lblChoixOperation.Text = operations[selectedOperationIndex];
        }*/

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            // Changer l'index du panel sélectionné vers le bas
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

        // Mise à jour de la sélection du panel et de l'action
        private void UpdatePanelSelection()
        {
            // Déselectionner tous les panels
            foreach (var panel in panels)
            {
                panel.Visible = false; // Cache tous les panels
            }

            // Sélectionner le panel actif
            panels[selectedPanelIndex].Visible = true; // Affiche le panel actif

            // Déterminer l'action en fonction du panel sélectionné
            switch (selectedPanelIndex)
            {
                case 0:
                    selectedAction = "Depot";
                    break;
                case 1:
                    selectedAction = "Retrait";
                    break;
                case 2:
                    selectedAction = "Echange";
                    break;
                default:
                    selectedAction = "";
                    break;
            }
        }

        private void ExecuteTransaction()
        {
            if (string.IsNullOrWhiteSpace(txtMontant.Text) || !decimal.TryParse(txtMontant.Text, out decimal transactionAmount))
            {
                MessageBox.Show("Veuillez entrer un montant valide.");
                return;
            }

            switch (operations[selectedOperationIndex])
            {
                case "Dépôt":
                    accountBalance += transactionAmount;  // Ajouter le montant au solde
                    MessageBox.Show($"Dépôt effectué. Nouveau solde: {accountBalance:C}");
                    break;

                case "Retrait":
                    if (accountBalance >= transactionAmount)
                    {
                        accountBalance -= transactionAmount; // Soustraire du solde
                        MessageBox.Show($"Retrait effectué. Nouveau solde: {accountBalance:C}");
                    }
                    else
                    {
                        MessageBox.Show("Fonds insuffisants pour effectuer ce retrait.");
                    }
                    break;

                case "Échange":
                    // Logique de conversion ou d'échange à définir
                    MessageBox.Show("Échange de devises effectué.");
                    break;

                default:
                    MessageBox.Show("Aucune action sélectionnée.");
                    break;
            }
        }
        private void Initializeform3()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des panneaux avec taille par défaut
            panelChoixOperation = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelMontant = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompteChoisi = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelArgentCompteChoisi = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            // Ajout des panneaux au formulaire
            this.Controls.AddRange(new Control[] { panelChoixOperation, panelMontant, panelCompteChoisi, panelArgentCompteChoisi });

            panels = new RoundedPanel[] { panelChoixOperation, panelMontant, panelCompteChoisi, panelArgentCompteChoisi };

            // Appel de la méthode de création des boutons et de l'ajustement
            Methode.CreateDirectionalButtons(this, BtnHaut_Click, BtnBas_Click, BtnGauche_Click, BtnDroite_Click, BtnValider_Click, BtnRetour_Click, BtnMaison_Click, BtnFermer_Click);

            // Initialiser la disposition des panneaux
            AdjustPanelLayout();

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();

            // Label Montant 
            Label lblMontant = new Label();
            lblMontant.Text = "Montant :";
            lblMontant.ForeColor = Color.FromArgb(128, 194, 236);
            lblMontant.Font = new Font("Arial", 60);  // Taille énorme 
            lblMontant.AutoSize = true; // Assurez-vous que le label s'ajuste à son contenu
            lblMontant.Location = new Point(10, 100);
            panelMontant.Controls.Add(lblMontant);

            // TextBox pour le montant
            txtMontant = new TextBox
            {
                Font = new Font("Arial", 60, FontStyle.Regular),
                Size = new Size(690, 100),
                Location = new Point(600, 100) // Assurez-vous que cette position permet au texte d'être visible
            };
            panelMontant.Controls.Add(txtMontant);

            // Label pour le compte choisi
            lblCompteChoisi = new Label
            {
                Text = "Compte choisi: " + selectedAccount,
                Font = new Font("Arial", 60, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                BackColor = Color.FromArgb(34, 67, 153),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true // S'assurer que le label s'ajuste à son contenu
            };
            panelCompteChoisi.Controls.Add(lblCompteChoisi);
            Methode.CenterControlInParent(lblCompteChoisi);

            // Label Choix de l'operation
            lblChoixOperation = new Label
            {
                Text = operations[selectedOperationIndex],
                Font = new Font("Arial", 60, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                BackColor = Color.FromArgb(34, 67, 153),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true // Assurez-vous que le label s'ajuste à son contenu
            };
            panelChoixOperation.Controls.Add(lblChoixOperation);
            Methode.CenterControlInParent(lblChoixOperation);
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
    }
}
