using MySqlConnector;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace fulbank
{
    public partial class MenuFinalTransactionRetrait : Form
    {
        private TextBox txtMontant;  // TextBox pour le montant
        private TextBox txtCompteSource;   // TextBox pour le numéro de 
        private Label lblMontant;
        private Label lblCompteSource;        
        private Label lblTitre;
        private RoundedPanel panelTransaction;
        private Panel panelChamps;
        private Button btnValider;

        public MenuFinalTransactionRetrait()
        {
            this.Icon = new Icon("Resources/logo-fulbank.ico");
            InitializeComponent();
            InitializeLayout();
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
            Methode.Fulbank(this);

            // Ajouter l'événement Resize pour ajuster les contrôles lors du redimensionnement de la fenêtre
            this.Resize += new EventHandler(Connexion_Resize);
        }

        // Événement appelé lors du redimensionnement de la fenêtre
        private void Connexion_Resize(object sender, EventArgs e)
        {
            AdjustLayout(); // Appelle une méthode pour ajuster les éléments
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1580, 1024);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Transaction Retrait";
            this.BackColor = Color.FromArgb(128, 194, 236);

            panelTransaction = new RoundedPanel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                BorderRadius = 50
            };
            this.Controls.Add(panelTransaction);

            lblTitre = new Label
            {
                Text = "Transaction Retrait",
                Font = new Font("Arial", 35, FontStyle.Bold),
                ForeColor = Color.FromArgb(207, 162, 0)
            };
            panelTransaction.Controls.Add(lblTitre);

            panelChamps = new Panel
            {
                BackColor = Color.FromArgb(34, 67, 153)
            };
            this.Controls.Add(panelChamps);

            lblCompteSource = new Label
            {
                Text = "Numéro Compte Source :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panelChamps.Controls.Add(lblCompteSource);

            txtCompteSource = new TextBox();
            panelChamps.Controls.Add(txtCompteSource);

            lblMontant = new Label
            {
                Text = "Montant :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panelChamps.Controls.Add(lblMontant);

            txtMontant = new TextBox();
            panelChamps.Controls.Add(txtMontant);

            AdjustLayout();
            Methode.Fulbank(this);
        }

        private void AdjustLayout()
        {
            int margin = this.ClientSize.Height / 40;

            panelChamps.Size = new Size(this.ClientSize.Width * 59 / 100, this.ClientSize.Height * 2 / 2);
            panelChamps.Location = new Point((((this.ClientSize.Width - panelChamps.Width) / 2) * 4 / 3), panelTransaction.Bottom + margin * 4);

            lblCompteSource.Location = new Point(margin, margin);
            txtCompteSource.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtCompteSource.Location = new Point(margin, lblCompteSource.Bottom + margin);

            //lblCompteDestination.Location = new Point(margin, txtCompte.Bottom + margin);
            //txtCompteDestination.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            //txtCompteDestination.Location = new Point(margin, lblCompteDestination.Bottom + margin);

            lblMontant.Location = new Point(margin, txtCompteSource.Bottom + margin);
            txtMontant.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtMontant.Location = new Point(margin, lblMontant.Bottom + margin);

            panelTransaction.Size = new Size(this.ClientSize.Width * 59 / 100, this.ClientSize.Height / 5);
            panelTransaction.Location = new Point((((this.ClientSize.Width - panelTransaction.Width) / 2) * 4 / 3), margin);

            float baseFontSize = this.ClientSize.Height / 40f;
            lblTitre.Font = new Font("Arial", baseFontSize * 2.2f, FontStyle.Bold);
            lblMontant.Font = lblCompteSource.Font = new Font("Arial", baseFontSize * 1.5f);
            txtMontant.Font = txtCompteSource.Font = new Font("Arial", baseFontSize * 1.5f);

            lblTitre.AutoSize = true;
            lblTitre.Location = new Point((panelTransaction.Width - lblTitre.Width) / 2, margin);
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            string compte = txtCompteSource.Text.Trim();
            string montantText = txtMontant.Text.Trim();
            string tauxDeChange = InfoDab.TauxDeChange;
            string dabId = InfoDab.DabId;

            // Vérification des champs obligatoires
            if (string.IsNullOrEmpty(compte) || string.IsNullOrEmpty(montantText) ||
                string.IsNullOrEmpty(tauxDeChange) || string.IsNullOrEmpty(dabId))
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Conversion et validation des entrées utilisateur
            if (!decimal.TryParse(montantText, out decimal montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMontant.Focus();
                return;
            }

            //if (!int.TryParse(tauxDeChangeText, out int tauxDeChange) || tauxDeChange <= 0)
            //{
            //    MessageBox.Show("Veuillez entrer un taux de change valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtTauxDeChange.Focus();
            //    return;
            //}

            //if (!int.TryParse(dabText, out int dabId) || dabId <= 0)
            //{
            //    MessageBox.Show("Veuillez entrer un identifiant de DAB valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtDAB.Focus();
            //    return;
            //}

            // Appel à la méthode EffectuerDepot
            try
            {
                EffectuerRetrait(montant, compte, int.Parse(tauxDeChange), int.Parse(dabId));
                txtMontant.Clear();
                txtCompteSource.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur inattendue s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============== Fonction Retrait =============== \\
        private void EffectuerRetrait(decimal montant, string compte, int tauxDeChange, int dabId)
        {
            try
            {
                // Réutiliser la connexion singleton existante
                var connection = ConnexionBDD.Connexion();

                // Vérifier si la connexion est fermée 
                if (connection.State == ConnectionState.Closed)
                {
                    // Rouvrir la connexion si nécessaire
                    connection.Open();
                }

                using (var command = new MySqlCommand("retrait", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@montantR", montant);
                    command.Parameters.AddWithValue("@compteSource", Convert.ToInt32(compte));
                    command.Parameters.AddWithValue("@tauxDeChange", tauxDeChange);
                    command.Parameters.AddWithValue("@DAB", dabId);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Le retrait a été effectué avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du retrait : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Fermer la connexion sans la disposer
                if (ConnexionBDD.connexion != null)
                {
                    ConnexionBDD.connexion.MyClose();
                }
            }
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            var previousMenu = new MenuTransaction();
            previousMenu.Show();
            this.Close();
        }

        private void BtnMaison_Click(object sender, EventArgs e)
        {
            var mainMenu = new MenuBase();
            mainMenu.Show();
            this.Close();
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnGauche_Click(object sender, EventArgs e)
        {
            if (txtCompteSource.Text.Length > 0 && txtCompteSource.SelectionStart > 0)
            {
                txtCompteSource.SelectionStart--;
                txtCompteSource.Focus();
            }
            else if (txtMontant.Text.Length > 0 && txtMontant.SelectionStart > 0)
            {
                txtMontant.SelectionStart--;
                txtMontant.Focus();
            }
            /*else if (txtMontant.Text.Length > 0 && txtMontant.SelectionStart > 0)
            {
                txtMontant.SelectionStart--;
                txtMontant.Focus();
            }*/
        }

        private void BtnDroite_Click(object sender, EventArgs e)
        {
            if (txtCompteSource.Text.Length > 0 && txtCompteSource.SelectionStart < txtCompteSource.Text.Length)
            {
                txtCompteSource.SelectionStart++;
                txtCompteSource.Focus();
            }
            else if (txtMontant.Text.Length > 0 && txtMontant.SelectionStart < txtMontant.Text.Length)
            {
                txtMontant.SelectionStart++;
                txtMontant.Focus();
            }
            /*else if (txtMontant.Text.Length > 0 && txtMontant.SelectionStart < txtMontant.Text.Length)
            {
                txtMontant.SelectionStart++;
                txtMontant.Focus();
            }*/
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (txtMontant.Focused)
            {
                txtMontant.Focus();
            }
            else
            {
                txtMontant.Focus();
            }
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (txtCompteSource.Focused)
            {
                txtMontant.Focus();
            }
            else
            {
                txtMontant.Focus();
            }
        }
    }
}