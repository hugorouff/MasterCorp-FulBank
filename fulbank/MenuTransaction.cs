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
    public partial class MenuTransaction : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelDepot;
        private RoundedPanel panelRetrait;
        private RoundedPanel panelEchange;
        private RoundedPanel panelAutres;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuTransaction()
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

        private void MenuTransaction_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
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
            RoundedPanel panelDepot = new RoundedPanel();
            panelDepot.BackColor = Color.FromArgb(34, 67, 153);
            panelDepot.Size = new Size(1090, 225);  // Agrandir le panel 
            panelDepot.Location = new Point(560, -25);  // Centré 
            panelDepot.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelDepot.BorderRadius = 90;
            this.Controls.Add(panelDepot);

            // Label Compte 
            Label lblDepot = new Label();
            lblDepot.Text = "Dépôt";
            lblDepot.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblDepot.ForeColor = Color.FromArgb(128, 194, 236);
            lblDepot.AutoSize = true;
            panelDepot.Controls.Add(lblDepot);
            Methode.CenterControlInParent(lblDepot);

            // Panel contenant les champs Crypto
            RoundedPanel panelRetrait = new RoundedPanel();
            panelRetrait.BackColor = Color.FromArgb(34, 67, 153);
            panelRetrait.Size = new Size(1090, 225);  // Agrandir le panel 
            panelRetrait.Location = new Point(560, (this.ClientSize.Height - 520) / 2 + 5);  // Centré 
            panelRetrait.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelRetrait.BorderRadius = 90;
            this.Controls.Add(panelRetrait);

            // Label Nom Crypto 
            Label lblRetrait = new Label();
            lblRetrait.Text = "Retrait";
            lblRetrait.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblRetrait.ForeColor = Color.FromArgb(128, 194, 236);
            lblRetrait.AutoSize = true;
            panelRetrait.Controls.Add(lblRetrait);
            Methode.CenterControlInParent(lblRetrait);

            // Panel contenant les champs Transaction
            RoundedPanel panelEchange = new RoundedPanel();
            panelEchange.BackColor = Color.FromArgb(34, 67, 153);
            panelEchange.Size = new Size(1090, 225);  // Agrandir le panel 
            panelEchange.Location = new Point(560, (this.ClientSize.Height - 260) / 2 + 155);  // Centré 
            panelEchange.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelEchange.BorderRadius = 90;
            this.Controls.Add(panelEchange);

            // Label Nom Transaction 
            Label lblEchange = new Label();
            lblEchange.Text = "Échange";
            lblEchange.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblEchange.ForeColor = Color.FromArgb(128, 194, 236);
            lblEchange.AutoSize = true;
            panelEchange.Controls.Add(lblEchange);
            Methode.CenterControlInParent(lblEchange);


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

            panels = new RoundedPanel[] { panelDepot, panelRetrait, panelEchange, panelAutres };
        }
    }
}