using MySqlConnector;
using System.Drawing.Drawing2D;
using System.Media;

namespace fulbank
{
    
    public partial class Connexion : Form
    {
        //private Methode methode = new Methode();
        private TextBox txtIdentifiant;  // Déclarer au niveau de la classe pour un accès global
        private TextBox txtMotDePasse;   // Déclarer au niveau de la classe pour un accès global
        private Label lblSousTitre;
        private Label lblFulBank;
        private RoundedPanel panelFul;
        private Panel panelChamps;
        private Label lblMotDePasse;
        private Label lblIdentifiant;
        // Boutons
        public RoundedButton btnHaut;
        public RoundedButton btnBas;
        public RoundedButton btnGauche;
        public RoundedButton btnDroite;
        public RoundedButton btnMaison;
        public RoundedButton btnRetour;
        public RoundedButton btnValider;
        public RoundedButton btnFermer;

        private MySqlConnection BDD = ConnexionBDD.Connexion();

        public Connexion()
        {
            InitializeComponent();
            Initializeform1();
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

            // Ajouter l'événement Resize pour ajuster les contrôles lors du redimensionnement de la fenêtre
            this.Resize += new EventHandler(Connexion_Resize);
        }

        // Événement appelé lors du redimensionnement de la fenêtre
        private void Connexion_Resize(object sender, EventArgs e)
        {
            AdjustLayout(); // Appelle une méthode pour ajuster les éléments
        }

        // Action du bouton Valider 
        private void BtnValider_Click(object sender, EventArgs e)
        {
            string id = this.txtIdentifiant.Text;
            string mdp = this.txtMotDePasse.Text;
            MySqlCommand cmd = new MySqlCommand("Select checkConnexion(@mdp_ , @id_ );", BDD);
            
            cmd.Parameters.Add(new MySqlParameter("id_", id));
            cmd.Parameters.Add(new MySqlParameter("mdp_", mdp));

            BDD.Open();
            MySqlDataReader data = cmd.ExecuteReader();

            data.Read();
            if (data.GetBoolean(0))
            {
                Utilisateur.NewInstance(int.Parse(id));
                MenuBase form2 = new MenuBase();
                form2.Show();
                this.Hide();
            }
            BDD.Close();
        }

        // Action du bouton Fermer 
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {

        }

        private void BtnMaison_Click(object sender, EventArgs e)
        {

        }

        private void Connexion_Load(object sender, EventArgs e) { }

        private void Initializeform1()
        {
            this.WindowState = FormWindowState.Maximized; // Maximise le formulaire
            this.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure du formulaire
            this.Text = "FulBank";
            this.BackColor = Color.FromArgb(128, 194, 236);

            // Panel contenant les champs Identifiant et Mot de passe
            panelChamps = new Panel();
            panelChamps.BackColor = Color.FromArgb(34, 67, 153);
            this.Controls.Add(panelChamps);

            // Label Identifiant
            lblIdentifiant = new Label();
            lblIdentifiant.Text = "Identifiant :";
            lblIdentifiant.ForeColor = Color.FromArgb(128, 194, 236);
            lblIdentifiant.AutoSize = true;
            panelChamps.Controls.Add(lblIdentifiant);

            // TextBox Identifiant
            txtIdentifiant = new TextBox();
            txtIdentifiant.UseSystemPasswordChar = false; // Pas pour mot de passe
            panelChamps.Controls.Add(txtIdentifiant);

            // Label Mot de passe
            lblMotDePasse = new Label();
            lblMotDePasse.Text = "Mot de passe :";
            lblMotDePasse.ForeColor = Color.FromArgb(128, 194, 236);
            lblMotDePasse.AutoSize = true;
            panelChamps.Controls.Add(lblMotDePasse);

            // TextBox Mot de passe
            txtMotDePasse = new TextBox();
            txtMotDePasse.UseSystemPasswordChar = true;
            panelChamps.Controls.Add(txtMotDePasse);

            // Panel FulBank
            panelFul = new RoundedPanel();
            panelFul.BackColor = Color.FromArgb(34, 67, 153);
            panelFul.BorderRadius = 50;
            this.Controls.Add(panelFul);

            // Label FulBank
            lblFulBank = new Label();
            lblFulBank.Text = "FulBank";
            lblFulBank.Font = new Font("Arial", 90, FontStyle.Bold);
            lblFulBank.ForeColor = Color.FromArgb(207, 162, 0);
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre
            lblSousTitre = new Label();
            lblSousTitre.Text = "Bank et Crypto";
            lblSousTitre.Font = new Font("Arial", 35, FontStyle.Italic);
            lblSousTitre.ForeColor = Color.FromArgb(207, 162, 0);
            panelFul.Controls.Add(lblSousTitre);

            // Ajuster la mise en page pour la première fois
            AdjustLayout();
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
            if (txtIdentifiant.Text.Length > 0 && txtIdentifiant.SelectionStart < txtIdentifiant.Text.Length)
            {
                txtIdentifiant.SelectionStart++; // Déplacement à droite dans txtIdentifiant
                txtIdentifiant.Focus();
            }
            else if (txtMotDePasse.Text.Length > 0 && txtMotDePasse.SelectionStart < txtMotDePasse.Text.Length)
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

        private void AdjustLayout()
        {
            // Taille et position de panelChamps
            panelChamps.Size = new Size(this.ClientSize.Width * 3 / 4, this.ClientSize.Height / 2);
            panelChamps.Location = new Point((this.ClientSize.Width - panelChamps.Width) / 2);

            int margin = this.ClientSize.Height / 50;

            // Position des labels et des textboxes dans panelChamps
            lblIdentifiant.Location = new Point(margin, margin);
            txtIdentifiant.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 4);
            txtIdentifiant.Location = new Point(margin, lblIdentifiant.Bottom + margin);

            lblMotDePasse.Location = new Point(margin, txtIdentifiant.Bottom + margin * 4);
            txtMotDePasse.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 4);
            txtMotDePasse.Location = new Point(margin, lblMotDePasse.Bottom + margin);

            // Taille et position de panelFul
            panelFul.Size = new Size(this.ClientSize.Width * 2 / 4, this.ClientSize.Height / 4);
            panelFul.Location = new Point((this.ClientSize.Width - panelFul.Width) / 2, panelChamps.Bottom + margin * 4);

            // Ajustement dynamique de la taille de police
            float baseFontSize = this.ClientSize.Height / 40f;
            lblFulBank.Font = new Font("Arial", baseFontSize * 4, FontStyle.Bold);
            lblSousTitre.Font = new Font("Arial", baseFontSize * 2, FontStyle.Italic);
            lblIdentifiant.Font = new Font("Arial", baseFontSize * 2);
            lblMotDePasse.Font = new Font("Arial", baseFontSize * 2);
            txtIdentifiant.Font = new Font("Arial", baseFontSize * 2);
            txtMotDePasse.Font = new Font("Arial", baseFontSize * 2);

            // Centrage dynamique de lblFulBank dans panelFul
            lblFulBank.AutoSize = true; // Activer AutoSize pour obtenir la largeur réelle
            lblFulBank.Location = new Point((panelFul.Width - lblFulBank.Width) / 2, margin / 20);

            // Aligner lblSousTitre à gauche avec un décalage fixe
            lblSousTitre.AutoSize = true; // Activer AutoSize pour obtenir la largeur réelle
            lblSousTitre.Location = new Point(margin*20, lblFulBank.Bottom + margin / 20);
        }
    }
}