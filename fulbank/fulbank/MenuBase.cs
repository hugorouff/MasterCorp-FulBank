using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace fulbank
{
    public partial class MenuBase : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelCompte;
        private RoundedPanel panelCoursCrypto;
        private RoundedPanel panelTransaction;
        private RoundedPanel panelAutres;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuBase()
        {
            InitializeComponent();
            Initializeform2();
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
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            Form nextForm = null;

            // Decide which form to show based on the selected panel
            switch (selectedPanelIndex)
            {
                case 0:
                    nextForm = new MenuCompte();
                    break;
                case 1:
                    nextForm = new CoursCrypto(); // Assuming you have this form
                    break;
                case 2:
                    nextForm = new MenuTransaction(); // Assuming you have this form
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

        private void BtnRetour_Click(object sender, EventArgs e) { }

        private void BtnMaison_Click(object sender, EventArgs e) { }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

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

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form1 = new Connexion();
            form1.Show();
            this.Close();
        }

        private void Initializeform2()
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

            // Panel contenant les champs Compte
            RoundedPanel panelCompte = new RoundedPanel();
            panelCompte.BackColor = Color.FromArgb(34, 67, 153);
            panelCompte.Size = new Size(1090, 225);  // Agrandir le panel 
            panelCompte.Location = new Point(560, -25);  // Centré 
            panelCompte.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCompte.BorderRadius = 90;
            this.Controls.Add(panelCompte);

            // Label Compte 
            Label lblCompte = new Label();
            lblCompte.Text = "Comptes";
            lblCompte.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblCompte.ForeColor = Color.FromArgb(128, 194, 236);
            lblCompte.AutoSize = true;
            panelCompte.Controls.Add(lblCompte);
            Methode.CenterControlInParent(lblCompte);

            // Panel contenant les champs Crypto
            RoundedPanel panelCoursCrypto = new RoundedPanel();
            panelCoursCrypto.BackColor = Color.FromArgb(34, 67, 153);
            panelCoursCrypto.Size = new Size(1090, 225);  // Agrandir le panel 
            panelCoursCrypto.Location = new Point(560, (this.ClientSize.Height - 520) / 2 + 5);  // Centré 
            panelCoursCrypto.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCoursCrypto.BorderRadius = 90;
            this.Controls.Add(panelCoursCrypto);

            // Label Nom Crypto 
            Label lblCrypto = new Label();
            lblCrypto.Text = "Cours Crypto";
            lblCrypto.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblCrypto.ForeColor = Color.FromArgb(128, 194, 236);
            lblCrypto.AutoSize = true;
            panelCoursCrypto.Controls.Add(lblCrypto);
            Methode.CenterControlInParent(lblCrypto);

            // Panel contenant les champs Transaction
            RoundedPanel panelTransaction = new RoundedPanel();
            panelTransaction.BackColor = Color.FromArgb(34, 67, 153);
            panelTransaction.Size = new Size(1090, 225);  // Agrandir le panel 
            panelTransaction.Location = new Point(560, (this.ClientSize.Height - 260) / 2 + 155);  // Centré 
            panelTransaction.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelTransaction.BorderRadius = 90;
            this.Controls.Add(panelTransaction);

            // Label Nom Transaction 
            Label lblTransaction = new Label();
            lblTransaction.Text = "Transaction";
            lblTransaction.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblTransaction.ForeColor = Color.FromArgb(128, 194, 236);
            lblTransaction.AutoSize = true;
            panelTransaction.Controls.Add(lblTransaction);
            Methode.CenterControlInParent(lblTransaction);

            // Panel contenant les champs Autres
            RoundedPanel panelAutres = new RoundedPanel();
            panelAutres.BackColor = Color.FromArgb(34, 67, 153);
            panelAutres.Size = new Size(1090, 225);  // Agrandir le panel 
            panelAutres.Location = new Point(560, this.ClientSize.Height - 203);  // Centré 
            panelAutres.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelAutres.BorderRadius = 90;
            this.Controls.Add(panelAutres);

            // Label Nom Autres 
            Label lblAutres = new Label();
            lblAutres.Text = "Autres";
            lblAutres.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblAutres.ForeColor = Color.FromArgb(128, 194, 236);
            lblAutres.AutoSize = true;
            panelAutres.Controls.Add(lblAutres);
            Methode.CenterControlInParent(lblAutres);

            panels = new RoundedPanel[] { panelCompte, panelCoursCrypto, panelTransaction, panelAutres };

        }
    }
}