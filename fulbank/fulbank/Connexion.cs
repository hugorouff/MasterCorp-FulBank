using System.Drawing.Drawing2D;
using System.Media;

namespace fulbank
{
    public partial class Connexion : Form
    {
        private TextBox txtIdentifiant;  // Déclarer au niveau de la classe pour un accès global
        private TextBox txtMotDePasse;   // Déclarer au niveau de la classe pour un accès global

        public Connexion()
        {
            InitializeComponent();
            Initializeform1();
        }

        // Action du bouton Valider 
        private void BtnValider1_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Hide();
        }

        // Action du bouton Fermer 
        private void BtnFermer1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Initializeform1()
        {
            // Définir le formulaire en plein écran
            this.WindowState = FormWindowState.Maximized; // Maximise le formulaire
            this.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure du formulaire

            // Configuration générale du formulaire 
            this.Text = "FulBank";
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Size = new Size(1024, 768);  // Taille énorme 

            // Panel contenant les champs Identifiant et Mot de passe 
            Panel panelChamps = new Panel();
            panelChamps.BackColor = Color.FromArgb(34, 67, 153);
            panelChamps.Size = new Size(1710, 750);  // Agrandir le panel 
            panelChamps.Location = new Point((this.ClientSize.Width - panelChamps.Width) / 2, -325);  // Centré 
            panelChamps.Anchor = AnchorStyles.None;  // Garder le panel centré 
            this.Controls.Add(panelChamps);

            // Label Identifiant 
            Label lblIdentifiant = new Label();
            lblIdentifiant.Text = "Identifiant :";
            lblIdentifiant.ForeColor = Color.FromArgb(128, 194, 236);
            lblIdentifiant.Location = new Point(10, 150);
            lblIdentifiant.Font = new Font("Arial", 55);  // Taille énorme 
            lblIdentifiant.AutoSize = true;
            panelChamps.Controls.Add(lblIdentifiant);

            // TextBox Identifiant 
            txtIdentifiant = new TextBox();
            txtIdentifiant.Size = new Size(1000, 400);  // Taille énorme 
            txtIdentifiant.Location = new Point(600, 150);
            txtIdentifiant.Font = new Font("Arial", 55);  // Texte énorme 
            panelChamps.Controls.Add(txtIdentifiant);

            // Label Mot de passe 
            Label lblMotDePasse = new Label();
            lblMotDePasse.Text = "Mot de passe :";
            lblMotDePasse.ForeColor = Color.FromArgb(128, 194, 236);
            lblMotDePasse.Location = new Point(10, 500);
            lblMotDePasse.Font = new Font("Arial", 55);  // Taille énorme 
            lblMotDePasse.AutoSize = true;
            panelChamps.Controls.Add(lblMotDePasse);

            // TextBox Mot de passe 
            txtMotDePasse = new TextBox();
            txtMotDePasse.Size = new Size(1000, 400);  // Taille énorme 
            txtMotDePasse.Location = new Point(600, 500);
            txtMotDePasse.Font = new Font("Arial", 55);  // Texte énorme 
            txtMotDePasse.UseSystemPasswordChar = true;
            panelChamps.Controls.Add(txtMotDePasse);

            // Panel contenant les champs FulBank
            RoundedPanel panelFul = new RoundedPanel();
            panelFul.BackColor = Color.FromArgb(34, 67, 153);
            panelFul.Size = new Size(1000, 350);  // Agrandir le panel 
            panelFul.Location = new Point((this.ClientSize.Width - panelFul.Width) / 2, 605);  // Centré 
            panelFul.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelFul.BorderRadius = 90;
            this.Controls.Add(panelFul);

            // Label FulBank 
            Label lblFulBank = new Label();
            lblFulBank.Text = "FulBank";
            lblFulBank.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblFulBank.ForeColor = Color.FromArgb(207, 162, 0);
            lblFulBank.Location = new Point(150, 75);  // Centré 
            lblFulBank.AutoSize = true;
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre 
            Label lblSousTitre = new Label();
            lblSousTitre.Text = "Bank et Crypto";
            lblSousTitre.Font = new Font("Arial", 50, FontStyle.Italic);  // Texte énorme 
            lblSousTitre.ForeColor = Color.FromArgb(207, 162, 0);
            lblSousTitre.Location = new Point(500, 250);  // Centré 
            lblSousTitre.AutoSize = true;
            panelFul.Controls.Add(lblSousTitre);

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
            btnValider.Click += BtnValider1_Click;
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
            this.Controls.Add(btnRetour);

            // Bouton Maison 🏠 
            RoundedButton btnMaison = new RoundedButton();
            btnMaison.Text = "🏠";
            btnMaison.ForeColor = Color.FromArgb(128, 194, 236);
            btnMaison.Size = new Size(300, 300);  // Taille énorme 
            btnMaison.Location = new Point(2200, 710);  // Position ajustée 
            btnMaison.BackColor = Color.FromArgb(99, 99, 101);
            btnMaison.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnMaison.BorderRadius = 90;
            btnMaison.Enabled = false;
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
            btnFermer.Click += BtnFermer1_Click;
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
            btnGauche.BackColor = Color.FromArgb(34, 67, 153);
            btnGauche.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            btnGauche.BorderRadius = 90;
            btnGauche.Click += BtnGauche_Click;
            this.Controls.Add(btnGauche);

            // Bouton Flèche Droite → 
            RoundedButton btnDroite = new RoundedButton();
            btnDroite.Text = "→";
            btnDroite.ForeColor = Color.FromArgb(128, 194, 236);
            btnDroite.Size = new Size(300, 300);  // Taille énorme 
            btnDroite.Location = new Point(60, 710);
            btnDroite.BackColor = Color.FromArgb(34, 67, 153);
            btnDroite.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme
            btnDroite.BorderRadius = 90;
            btnDroite.Click += BtnDroite_Click;
            this.Controls.Add(btnDroite);
        }

        private void Connexion_Load(object sender, EventArgs e)
        {

        }

        private void BtnGauche_Click(object sender, EventArgs e)
        {
            if (txtIdentifiant.Text.Length > 0 && txtIdentifiant.SelectionStart > 0)
            {
                txtIdentifiant.SelectionStart--; // Déplacement à gauche dans txtIdentifiant
                txtIdentifiant.Focus();
            }
            else if (txtMotDePasse.Text.Length > 0 && txtMotDePasse.SelectionStart > 0)
            {
                txtMotDePasse.SelectionStart--; // Déplacement à gauche dans txtMotDePasse
                txtMotDePasse.Focus();
            }
        }

        private void BtnDroite_Click(object sender, EventArgs e)
        {
            if (txtIdentifiant.Text.Length > 0 && txtIdentifiant.SelectionStart > 0)
            {
                txtIdentifiant.SelectionStart++; // Déplacement à droite dans txtIdentifiant
                txtIdentifiant.Focus();
            }
            else if (txtMotDePasse.Text.Length > 0 && txtMotDePasse.SelectionStart > 0)
            {
                txtMotDePasse.SelectionStart++; // Déplacement à droite dans txtMotDePasse
                txtMotDePasse.Focus();
            }
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (txtMotDePasse.Focused)
            {
                txtIdentifiant.Focus(); // Change le focus vers le champ Identifiant
            }
            else
            {
                txtIdentifiant.Focus(); // Reste concentré sur le champ Identifiant
            }
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (txtIdentifiant.Focused)
            {
                txtMotDePasse.Focus(); // Change le focus vers le champ Mot de passe
            }
            else
            {
                txtMotDePasse.Focus(); // Reste concentré sur le champ Mot de passe
            }
        }
    }
}