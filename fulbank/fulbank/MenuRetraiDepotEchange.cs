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

        private void BtnValider1_Click(object sender, EventArgs e)
        {
            MenuFinalTransaction form2 = new MenuFinalTransaction();
            form2.Show();
            this.Hide();
        }

        private void BtnFermer3_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour3_Click(object sender, EventArgs e)
        {
            MenuTransaction form2 = new MenuTransaction();
            form2.Show();
            this.Close();
        }

        private void BtnMaison3_Click(object sender, EventArgs e)
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
            this.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure du formulaire

            // Configuration générale du formulaire 
            this.Text = "FulBank";
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Size = new Size(1200, 800);  // Taille énorme 

            // Panel contenant les champs FulBank
            Panel panelFul = new Panel();
            panelFul.BackColor = Color.FromArgb(34, 67, 153);
            panelFul.Size = new Size(350, 2000);  // Agrandir le panel 
            panelFul.Location = new Point(425, 0);  // Centré 
            this.Controls.Add(panelFul);

            // Label FulBank 
            Label lblFulBank = new Label();
            lblFulBank.Text = "FulBank";
            lblFulBank.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblFulBank.ForeColor = Color.FromArgb(207, 162, 0);
            lblFulBank.Size = new Size(250, 2200);
            lblFulBank.Paint += new PaintEventHandler(lblFulBank_Paint);
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre 
            Label lblSousTitre = new Label();
            lblSousTitre.Text = "Bank et Crypto";
            lblSousTitre.Font = new Font("Arial", 50, FontStyle.Italic);  // Texte énorme 
            lblSousTitre.ForeColor = Color.FromArgb(207, 162, 0);
            lblSousTitre.Size = new Size(500, 1500);
            lblSousTitre.Paint += new PaintEventHandler(lblSousTitre_Paint);
            panelFul.Controls.Add(lblSousTitre);

            CreatePanels();

            // Création des boutons (haut, bas, gauche, droite) 
            CreateDirectionalButtons();

            // Bouton Valider ✔ 
            RoundedButton btnValider = new RoundedButton();
            btnValider.Text = "✔";
            btnValider.ForeColor = Color.FromArgb(128, 194, 236);
            btnValider.Size = new Size(300, 300);  // Taille énorme 
            btnValider.Location = new Point(2200, 10);  // Position ajustée pour être à droite
            btnValider.BackColor = Color.FromArgb(34, 67, 153);
            btnValider.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnValider.BorderRadius = 90;  // Rayon des coins
            btnValider.Click+= BtnValider1_Click;
            this.Controls.Add(btnValider);

            // Bouton Retour ↩ 
            RoundedButton btnRetour = new RoundedButton();
            btnRetour.Text = "↩";
            btnRetour.ForeColor = Color.FromArgb(128, 194, 236);
            btnRetour.Size = new Size(300, 300);  // Taille énorme 
            btnRetour.Location = new Point(2200, 360);  // Position ajustée 
            btnRetour.BackColor = Color.FromArgb(34, 67, 153);
            btnRetour.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme
            btnRetour.BorderRadius = 90;
            btnRetour.Click += BtnRetour3_Click;
            this.Controls.Add(btnRetour);

            // Bouton Maison 🏠 
            RoundedButton btnMaison = new RoundedButton();
            btnMaison.Text = "🏠";
            btnMaison.ForeColor = Color.FromArgb(128, 194, 236);
            btnMaison.Size = new Size(300, 300);  // Taille énorme 
            btnMaison.Location = new Point(2200, 710);  // Position ajustée 
            btnMaison.BackColor = Color.FromArgb(34, 67, 153);
            btnMaison.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnMaison.Click += BtnMaison3_Click;
            btnMaison.BorderRadius = 90;
            this.Controls.Add(btnMaison);

            // Bouton Fermer X 
            RoundedButton btnFermer = new RoundedButton();
            btnFermer.Text = "X";
            btnFermer.ForeColor = Color.FromArgb(128, 194, 236);
            btnFermer.Size = new Size(300, 300);  // Taille énorme 
            btnFermer.Location = new Point(2200, 1060); // Position ajustée 
            btnFermer.BackColor = Color.FromArgb(34, 67, 153);
            btnFermer.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnFermer.BorderRadius = 90;
            btnFermer.Click += BtnFermer3_Click;
            this.Controls.Add(btnFermer);
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
                Size = new Size(1360, 300),
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
            CenterControlInParent(label);
            this.Controls.Add(panel);

            return panel;
        }

        // Création des boutons directionnels 
        private void CreateDirectionalButtons()
        {
            // Bouton Flèche Haut ↑ 
            RoundedButton btnHaut = new RoundedButton();
            btnHaut.Text = "↑";
            btnHaut.ForeColor = Color.FromArgb(128, 194, 236);
            btnHaut.Size = new Size(300, 300);  // Taille énorme 
            btnHaut.Location = new Point(60, 10);
            btnHaut.BackColor = Color.FromArgb(34, 67, 153);
            btnHaut.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnHaut.BorderRadius = 90;
            btnHaut.Click += BtnHaut_Click;
            this.Controls.Add(btnHaut);

            // Bouton Flèche Bas ↓ 
            RoundedButton btnBas = new RoundedButton();
            btnBas.Text = "↓";
            btnBas.ForeColor = Color.FromArgb(128, 194, 236);
            btnBas.Size = new Size(300, 300);  // Taille énorme 
            btnBas.Location = new Point(60, 1060);
            btnBas.BackColor = Color.FromArgb(34, 67, 153);
            btnBas.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnBas.BorderRadius = 90;
            btnBas.Click += BtnBas_Click;
            this.Controls.Add(btnBas);

            // Bouton Flèche Gauche ← 
            RoundedButton btnGauche = new RoundedButton();
            btnGauche.Text = "←";
            btnGauche.ForeColor = Color.FromArgb(128, 194, 236);
            btnGauche.Size = new Size(300, 300);  // Taille énorme 
            btnGauche.Location = new Point(60, 360);
            btnGauche.BackColor = Color.FromArgb(99, 99, 101);
            btnGauche.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnGauche.BorderRadius = 90;
            this.Controls.Add(btnGauche);

            // Bouton Flèche Droite → 
            RoundedButton btnDroite = new RoundedButton();
            btnDroite.Text = "→";
            btnDroite.ForeColor = Color.FromArgb(128, 194, 236);
            btnDroite.Size = new Size(300, 300);  // Taille énorme 
            btnDroite.Location = new Point(60, 710);
            btnDroite.BackColor = Color.FromArgb(99, 99, 101);
            btnDroite.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme
            btnDroite.BorderRadius = 90;
            this.Controls.Add(btnDroite);
        }
        // Méthode Paint pour le label "FulBank"
        private void lblFulBank_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            e.Graphics.Clear(lbl.BackColor);

            // Appliquer la rotation de 90 degrés
            e.Graphics.TranslateTransform(lbl.Width / 2, lbl.Height / 2); // Centre le texte
            e.Graphics.RotateTransform(-90); // Rotation horaire de 90 degrés

            // Calculer le texte à centrer correctement
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString(lbl.Text, lbl.Font, new SolidBrush(lbl.ForeColor), 0, 0, stringFormat);
        }

        // Méthode Paint pour le label "Bank et Crypto"
        private void lblSousTitre_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            e.Graphics.Clear(lbl.BackColor);

            // Appliquer la rotation de 90 degrés
            e.Graphics.TranslateTransform(lbl.Width / 2, lbl.Height / 2); // Centre le texte
            e.Graphics.RotateTransform(-90); // Rotation horaire de 90 degrés

            // Calculer le texte à centrer correctement
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString(lbl.Text, lbl.Font, new SolidBrush(lbl.ForeColor), 0, 0, stringFormat);
        }

        private void CenterControlInParent(Control control)
        {
            // Vérifier que le parent n'est pas null
            if (control.Parent != null)
            {
                // Calculer la position centrée
                int x = (control.Parent.Width - control.Width) / 2;
                int y = (control.Parent.Height - control.Height) / 2;

                // Mettre à jour la position du contrôle
                control.Location = new Point(x, y);
            }
        }
    }
}
