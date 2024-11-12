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
    public partial class MenuFinalTransaction : Form
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
        public MenuFinalTransaction()
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
                    nextForm = new MenuRetraiDepotEchange("Depot");
                    break;
                case 1:
                    nextForm = new MenuRetraiDepotEchange("Retrait"); // Assuming you have this form
                    break;
                case 2:
                    nextForm = new MenuRetraiDepotEchange("Echange"); // Assuming you have this form
                    break;
                case 3:
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

        private void Initializeform3()
        {
            // Définir le formulaire en plein écran
            this.WindowState = FormWindowState.Maximized; // Maximise le formulaire
            this.Size = new Size(1920, 1080);  // Taille ajustée pour un écran 1920x1080
            this.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure du formulaire
            // Configuration générale du formulaire 
            this.Text = "FulBank";
            this.BackColor = Color.FromArgb(128, 194, 236);
            // Appelle la méthode pour afficher le panel de fulbank
            Methode.Fulbank(this);

            // Panel contenant les champs Compte Choisi
            panelChoixOperation = new RoundedPanel();
            panelChoixOperation.BackColor = Color.FromArgb(34, 67, 153);
            panelChoixOperation.Size = new Size(1090, 225);  // Agrandir le panel 
            panelChoixOperation.Location = new Point(560, -25);  // Centré 
            this.Controls.Add(panelChoixOperation);

            // Panel contenant les champs Montant
            panelMontant = new RoundedPanel();
            panelMontant.BackColor = Color.FromArgb(34, 67, 153);
            panelMontant.Size = new Size(1090, 225);  // Agrandir le panel 
            panelMontant.Location = new Point(560, (this.ClientSize.Height - 520) / 2 + 5);  // Centré 
            this.Controls.Add(panelMontant);
            
            // Panel contenant les champs Compte Choisi
            panelCompteChoisi = new RoundedPanel();
            panelCompteChoisi.BackColor = Color.FromArgb(34, 67, 153);
            panelCompteChoisi.Size = new Size(1090, 225);  // Agrandir le panel 
            panelCompteChoisi.Location = new Point(560, (this.ClientSize.Height - 260) / 2 + 155);  // Centré 
            this.Controls.Add(panelCompteChoisi);

            // Panel contenant les champs Montant Compte Choisi
            panelArgentCompteChoisi = new RoundedPanel();
            panelArgentCompteChoisi.BackColor = Color.FromArgb(34, 67, 153);
            panelArgentCompteChoisi.Size = new Size(1090, 225);  // Agrandir le panel 
            panelArgentCompteChoisi.Location = new Point(560, this.ClientSize.Height - 203);  // Centré 
            this.Controls.Add(panelArgentCompteChoisi);

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

            panels = new RoundedPanel[] { panelChoixOperation, panelMontant, panelCompteChoisi, panelArgentCompteChoisi };
        }

        private RoundedPanel CreateRoundedPanel(string labelText, Point location)
        {
            var panel = new RoundedPanel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                Size = new Size(1090, 225),
                Location = location,
                Anchor = AnchorStyles.None,
                BorderRadius = 90,
                Visible = false // Initially not visible
            };
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Arial", 120, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
            this.Controls.Add(panel);

            return panel;
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
    }
}
