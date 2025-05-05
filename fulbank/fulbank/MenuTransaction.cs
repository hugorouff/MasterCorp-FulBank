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
            this.Icon = new Icon("Resources/logo-fulbank.ico");
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
                    nextForm = new MenuFinalTransactionDepot();
                    break;
                case 1:
                    nextForm = new MenuFinalTransactionRetrait();
                    break;
                case 2:
                    nextForm = new MenuFinalTransactionVirement();
                    break;
                case 3:
                    nextForm = new MenuFinalTransactionChangeMonnaie();
                    break;
                case 4:
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
            this.Size = new Size(1580, 1024);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des panneaux avec taille par défaut
            panelDepot = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelRetrait = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelEchange = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelAutres = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            // Ajout des panneaux au formulaire
            this.Controls.AddRange(new Control[] { panelDepot, panelRetrait, panelEchange, panelAutres });

            // Initialisation des labels et leur centrage dans les panneaux
            InitPanelLabel(panelDepot, "Dépôt");
            InitPanelLabel(panelRetrait, "Retrait");
            InitPanelLabel(panelEchange, "Virement");
            InitPanelLabel(panelAutres, "Change Monnaie");

            panels = new RoundedPanel[] { panelDepot, panelRetrait, panelEchange, panelAutres };

            // Appel de la méthode de création des boutons et de l'ajustement
            Methode.CreateDirectionalButtons(this, BtnHaut_Click, BtnBas_Click, BtnGauche_Click, BtnDroite_Click, BtnValider_Click, BtnRetour_Click, BtnMaison_Click, BtnFermer_Click);

            // Initialiser la disposition des panneaux
            AdjustPanelLayout();

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();
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
                Font = new Font("Arial", 60, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
        }
    }
}