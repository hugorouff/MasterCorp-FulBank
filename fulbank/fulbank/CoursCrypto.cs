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
        }

        private void CoursCrypto_Load(object sender, EventArgs e)
        {

        }

        private void BtnFermer3_Click(object sender, EventArgs e)
        {
            Connexion form2 = new();
            form2.Show();
            this.Close();
        }

        private void BtnRetour3_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
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

            // Panel contenant les champs Crypto
            RoundedPanel panelCrypto = new RoundedPanel();
            panelCrypto.BackColor = Color.FromArgb(34, 67, 153);
            panelCrypto.Size = new Size(1360, 300);  // Agrandir le panel 
            panelCrypto.Location = new Point(100, -310);  // Centré 
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
            panelCrypto1.Location = new Point(100, 40);  // Centré 
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

            // Création des boutons (haut, bas, gauche, droite) 
            CreateDirectionalButtons();

            // Bouton Valider ✔ 
            RoundedButton btnValider = new RoundedButton();
            btnValider.Text = "✔";
            btnValider.ForeColor = Color.FromArgb(128, 194, 236);
            btnValider.Size = new Size(300, 300);  // Taille énorme 
            btnValider.Location = new Point(2200, 10);  // Position ajustée pour être à droite
            btnValider.BackColor = Color.FromArgb(99, 99, 101);
            btnValider.Font = new Font("Arial", 100, FontStyle.Bold);  // Texte énorme 
            btnValider.BorderRadius = 90;  // Rayon des coins 
            btnValider.Enabled = false;
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
    }
}
