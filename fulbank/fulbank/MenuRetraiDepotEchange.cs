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
    public partial class MenuRetraiDepotEchange : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelDepot;
        private RoundedPanel panelRetrait;
        private RoundedPanel panelEchange;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel
        private string selectedAction;  // Nouveau champ pour stocker l'action sélectionnée
        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuRetraiDepotEchange(string action)
        {
            InitializeComponent();
            Initializeform3();
            selectedAction = action;  // Stocke l'action sélectionnée
            UpdatePanelSelection();
            UpdatePanelVisibility();  // Appelle la méthode pour afficher la section correcte
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

        private void UpdatePanelVisibility()
        {
            panelDepot.Visible = selectedAction == "Depot";
            panelRetrait.Visible = selectedAction == "Retrait";
            panelEchange.Visible = selectedAction == "Echange";
            UpdatePanelSelection();
        }

        private void MenuRetraiDepotEchange_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            MenuFinalTransaction form2 = new MenuFinalTransaction();
            form2.Show();
            this.Hide();
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

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

            CreatePanels();
        }

        private void CreatePanels()
        {
            panelDepot = CreateRoundedPanel("Choix du Compte", new Point(100, -310));
            panelRetrait = CreateRoundedPanel("Choix du Compte", new Point(100, -310));
            panelEchange = CreateRoundedPanel("Choix du Compte", new Point(100, -310));

            panels = new RoundedPanel[] { panelDepot, panelRetrait, panelEchange };
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
                Font = new Font("Arial", 90, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
            this.Controls.Add(panel);

            return panel;
        }
    }
}
