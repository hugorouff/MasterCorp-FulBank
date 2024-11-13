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
    public partial class MenuCompte : Form
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
            Connexion form2 = new ();
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

            // Label Nom Crypto 
            Label lblNomCompte = new Label();
            lblNomCompte.Text = "Courant: ";
            lblNomCompte.Font = new Font("Arial", 100, FontStyle.Bold);  // Texte énorme 
            lblNomCompte.ForeColor = Color.FromArgb(128, 194, 236);
            lblNomCompte.Location = new Point(0, 75);  // Centré 
            lblNomCompte.AutoSize = true;
            panelCompte.Controls.Add(lblNomCompte);

            // Label Prix Crypto 
            Label lblSoldeCompte = new Label();
            lblSoldeCompte.Text = "1500$";
            lblSoldeCompte.Font = new Font("Arial", 100, FontStyle.Bold);  // Texte énorme 
            lblSoldeCompte.ForeColor = Color.FromArgb(207, 162, 0);
            lblSoldeCompte.Location = new Point(900, 75);  // Centré 
            lblSoldeCompte.AutoSize = true;
            panelCompte.Controls.Add(lblSoldeCompte);

            RoundedPanel panelCompte1 = new RoundedPanel();
            panelCompte1.BackColor = Color.FromArgb(34, 67, 153);
            panelCompte1.Size = new Size(1090, 225);  // Agrandir le panel 
            panelCompte1.Location = new Point(560, (this.ClientSize.Height - 520) / 2 + 5);  // Centré 
            panelCompte1.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCompte1.BorderRadius = 90;
            this.Controls.Add(panelCompte1);

            // Label Nom Crypto 
            Label lblNomCompte1 = new Label();
            lblNomCompte1.Text = "Courant: ";
            lblNomCompte1.Font = new Font("Arial", 100, FontStyle.Bold);  // Texte énorme 
            lblNomCompte1.ForeColor = Color.FromArgb(128, 194, 236);
            lblNomCompte1.Location = new Point(0, 75);  // Centré 
            lblNomCompte1.AutoSize = true;
            panelCompte1.Controls.Add(lblNomCompte1);

            // Label Prix Crypto 
            Label lblSoldeCompte1 = new Label();
            lblSoldeCompte1.Text = "1500$";
            lblSoldeCompte1.Font = new Font("Arial", 100, FontStyle.Bold);  // Texte énorme 
            lblSoldeCompte1.ForeColor = Color.FromArgb(207, 162, 0);
            lblSoldeCompte1.Location = new Point(900, 75);  // Centré 
            lblSoldeCompte1.AutoSize = true;
            panelCompte1.Controls.Add(lblSoldeCompte1);

            panels = new RoundedPanel[] { panelCompte, panelCompte1 };
        }
    }
}