using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
namespace fulbank
{
    public partial class MenuCompte : Form
    {
        // Déclaration des panels comme champs de la classe
        private RoundedPanel panelCompte;
        private RoundedPanel panelCoursCrypto;
        private RoundedPanel panelTransaction;
        private RoundedPanel panelAutres;
        private RoundedPanel panelCompte1;
        private RoundedPanel panelCompte2;
        private int selectedPanelIndex = 0; // Index to track the currently selected panel

        // AH

        private Utilisateur util = Utilisateur.getInstance();
        private List<int> comptes = Utilisateur.getInstance().getNumComptes();
        private List<float> soldes = Utilisateur.getInstance().getSoldes();
        private List<string> monnaies = Utilisateur.getInstance().getMonnaies();
        private List<string> types = Utilisateur.getInstance().getTypeComptes();
        
        private List<RoundedPanel> panelsComptes = new List<RoundedPanel> { };

        // Liste des panels et un index pour suivre le panel actif
        private RoundedPanel[] panels;
        private int currentPanelIndex = 0;
        public MenuCompte()
        {

            util.cleansComptes();
            util.PullComptes();
            panels = new RoundedPanel[comptes.Count()];

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
            foreach (var panel in panels) { 
                panel.BackColor = Color.FromArgb(34, 67, 153); // Default color
            }

            // Highlight the selected panel
            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Highlight color
        }

        private void Initializeform3()
        {
            // Configuration générale du formulaire
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // entrée du code pour insert dans les panels des compte (on y crois)
            // pour les info interne au compte ; select compteSource,compteDest,DateOperation,montant,monnaie from historique_compte where ...

            for (int i = 0; i < comptes.Count(); i++)
            {
                panelsComptes.Add(new RoundedPanel {BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 });
                InitPanelLabel(panelsComptes[i], types[i] + ": " + soldes[i] + monnaies[i]);
                MessageBox.Show(i + "" +types[i] + ": " + soldes[i] + monnaies[i]);
            }



            //// Initialisation des panneaux avec taille par défaut
            panelCompte1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            // Ajout des panneaux au formulaire
            Control[] controlPannels = new Control[] {};
            foreach (RoundedPanel panel in controlPannels)
                {
                controlPannels.Append(panel);
                }
            this.Controls.AddRange(controlPannels);

            // Initialisation des labels et leur centrage dans les panneaux

            panels = new RoundedPanel[] { panelCompte1, panelCompte2 };

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();

            // Appeler l'ajustement de la disposition des panneaux
            AdjustPanelLayout();
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
                RoundedPanel panel = panelsComptes[i];

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
    }
}