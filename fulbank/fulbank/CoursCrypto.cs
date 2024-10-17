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
    public partial class CoursCrypto : Form
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
        public CoursCrypto()
        {
            InitializeComponent();
            Initializeform3();
            UpdatePanelSelection();
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

        private void CoursCrypto_Load(object sender, EventArgs e)
        {

        }

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

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }
        private void BtnValider_Click(object sender, EventArgs e) { }

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
            // Définir le formulaire en plein écran
            this.WindowState = FormWindowState.Maximized; // Maximise le formulaire
            this.Size = new Size(1920, 1080);  // Taille ajustée pour un écran 1920x1080
            this.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure du formulaire
            // Configuration générale du formulaire 
            this.Text = "FulBank";
            this.BackColor = Color.FromArgb(128, 194, 236);
            // Appelle la méthode pour afficher le panel de fulbank
            Methode.Fulbank(this);

            // Panel contenant les champs Crypto
            RoundedPanel panelCrypto = new RoundedPanel();
            panelCrypto.BackColor = Color.FromArgb(34, 67, 153);
            panelCrypto.Size = new Size(1090, 225);  // Agrandir le panel 
            panelCrypto.Location = new Point(560, -25);  // Centré 
            panelCrypto.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCrypto.BorderRadius = 90;
            this.Controls.Add(panelCrypto);

            // Label Nom Crypto 
            Label lblNomCrypto = new Label();
            lblNomCrypto.Text = "Bitcoins";
            lblNomCrypto.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblNomCrypto.ForeColor = Color.FromArgb(128, 194, 236);
            lblNomCrypto.Location = new Point(0, 75);  // Centré 
            lblNomCrypto.AutoSize = true;
            panelCrypto.Controls.Add(lblNomCrypto);

            // Label Prix Crypto 
            Label lblPrixCrypto = new Label();
            lblPrixCrypto.Text = "57000$/BTC";
            lblPrixCrypto.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblPrixCrypto.ForeColor = Color.FromArgb(207, 162, 0);
            lblPrixCrypto.Location = new Point(900, 75);  // Centré 
            lblPrixCrypto.AutoSize = true;
            panelCrypto.Controls.Add(lblPrixCrypto);
            // Panel contenant les champs Crypto
            RoundedPanel panelCrypto1 = new RoundedPanel();
            panelCrypto1.BackColor = Color.FromArgb(34, 67, 153);
            panelCrypto1.Size = new Size(1360, 300);  // Agrandir le panel 
            panelCrypto1.Location = new Point(560, (this.ClientSize.Height - 520) / 2 + 5);  // Centré 
            panelCrypto1.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCrypto1.BorderRadius = 90;
            this.Controls.Add(panelCrypto1);

            // Label Nom Crypto 
            Label lblNomCrypto1 = new Label();
            lblNomCrypto1.Text = "Bitcoins";
            lblNomCrypto1.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblNomCrypto1.ForeColor = Color.FromArgb(128, 194, 236);
            lblNomCrypto1.Location = new Point(0, 75);  // Centré 
            lblNomCrypto1.AutoSize = true;
            panelCrypto1.Controls.Add(lblNomCrypto1);

            // Label Prix Crypto 
            Label lblPrixCrypto1 = new Label();
            lblPrixCrypto1.Text = "57000$/BTC";
            lblPrixCrypto1.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblPrixCrypto1.ForeColor = Color.FromArgb(207, 162, 0);
            lblPrixCrypto1.Location = new Point(900, 75);  // Centré 
            lblPrixCrypto1.AutoSize = true;
            panelCrypto1.Controls.Add(lblPrixCrypto1);

            panels = new RoundedPanel[] { panelCrypto, panelCrypto1 };
        }
    }
}
