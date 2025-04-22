using MySqlConnector;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace fulbank
{
    public partial class MenuFinalTransactionChangeMonnaie : Form
    {
        private TextBox txtMontant;  // TextBox pour le montant
        private TextBox txtCompteSource;   // TextBox pour le numéro de compte
        private TextBox txtCompteDestination;
        private Label lblMontant;
        private Label lblCompteSource;
        private Label lblCompteDestination;
        private Label lblTitre;
        private RoundedPanel panelTransaction;
        private Panel panelChamps;
        private Button btnValider;

        public MenuFinalTransactionChangeMonnaie()
        {
            //InitializeComponent();
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
            this.Text = "Changement de monnaie";
            this.BackColor = Color.FromArgb(128, 194, 236);

            panelTransaction = new RoundedPanel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                BorderRadius = 50
            };
            this.Controls.Add(panelTransaction);

            lblTitre = new Label
            {
                Text = "Changement de monnaie",
                Font = new Font("Arial", 24, FontStyle.Bold), // Réduction de la taille de la police
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = true
            };
            panelTransaction.Controls.Add(lblTitre);

            panelChamps = new Panel
            {
                BackColor = Color.FromArgb(34, 67, 153)
            };
            this.Controls.Add(panelChamps);

            lblCompteSource = new Label
            {
                Text = "Compte Source :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true,
                Font = new Font("Arial", 14) // Réduction de la taille de la police
            };
            panelChamps.Controls.Add(lblCompteSource);

            txtCompteSource = new TextBox { Font = new Font("Arial", 14) };
            panelChamps.Controls.Add(txtCompteSource);

            lblCompteDestination = new Label
            {
                Text = "Compte Destination :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true,
                Font = new Font("Arial", 14)
            };
            panelChamps.Controls.Add(lblCompteDestination);

            txtCompteDestination = new TextBox { Font = new Font("Arial", 14) };
            panelChamps.Controls.Add(txtCompteDestination);

            lblMontant = new Label
            {
                Text = "Montant :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true,
                Font = new Font("Arial", 14)
            };
            panelChamps.Controls.Add(lblMontant);

            txtMontant = new TextBox { Font = new Font("Arial", 14) };
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

            lblCompteDestination.Location = new Point(margin, txtCompteSource.Bottom + margin);
            txtCompteDestination.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtCompteDestination.Location = new Point(margin, lblCompteDestination.Bottom + margin);

            lblMontant.Location = new Point(margin, txtCompteDestination.Bottom + margin);
            txtMontant.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtMontant.Location = new Point(margin, lblMontant.Bottom + margin);

            panelTransaction.Size = new Size(this.ClientSize.Width * 59 / 100, this.ClientSize.Height / 5);
            panelTransaction.Location = new Point((((this.ClientSize.Width - panelTransaction.Width) / 2) * 4 / 3), margin);

            float baseFontSize = this.ClientSize.Height / 40f;
            lblTitre.Font = new Font("Arial", baseFontSize * 2.2f, FontStyle.Bold);
            lblMontant.Font = lblCompteSource.Font = lblCompteDestination.Font = new Font("Arial", baseFontSize * 1.5f);
            txtMontant.Font = txtCompteSource.Font = txtCompteDestination.Font = new Font("Arial", baseFontSize * 1.5f);

            lblTitre.AutoSize = true;
            lblTitre.Location = new Point((panelTransaction.Width - lblTitre.Width) / 2, margin);
        }

        private async void BtnValider_Click(object sender, EventArgs e)
        {
            string compteS = txtCompteSource.Text.Trim();
            string compteD = txtCompteDestination.Text.Trim();
            string montantText = txtMontant.Text.Trim();
            string tauxDeChange = InfoDab.TauxDeChange;
            string dabId = InfoDab.DabId;

            if (string.IsNullOrWhiteSpace(compteS))
            {
                MessageBox.Show("Veuillez entrer un numéro de compte source valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompteSource.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(compteD))
            {
                MessageBox.Show("Veuillez entrer un numéro de compte de destination valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompteDestination.Focus();
                return;
            }

            if (!decimal.TryParse(montantText, out decimal montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide et supérieur à 0.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMontant.Focus();
                return;
            }

            try
            {
                await EffectuerChangementMonaieAsync(montant, int.Parse(compteS), int.Parse(compteD), int.Parse(dabId));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur inattendue s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task EffectuerChangementMonaieAsync(decimal montant, int compteS, int compteD, int dabId)
        {
            bool isCryptoS = InfoDab.IsCrypto(compteS);
            bool isCryptoD = InfoDab.IsCrypto(compteD);

            string labelS = InfoDab.GetLabelApi(compteS);
            string labelD = InfoDab.GetLabelApi(compteD);

            decimal tauxDecimal = await InfoDab.GetTauxDeChange(labelS, labelD);
            int tauxDeChange = (int)Math.Round(tauxDecimal, MidpointRounding.AwayFromZero);

            var connection = ConnexionBDD.Connexion();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (isCryptoS && isCryptoD)
                {
                    AppelerProcedure("virement", montant, compteS, compteD, tauxDeChange, dabId);
                }
                else if (isCryptoS && !isCryptoD)
                {
                    // 1. Retrait du compte crypto
                    AppelerProcedure("retrait", montant, compteS, compteD, tauxDeChange, dabId);

                    // 2. Conversion et dépôt dans le compte fiat
                    decimal montantConverti = montant * tauxDecimal;
                    AppelerProcedure("depos", montantConverti, compteD, compteS, 1, dabId); // taux = 1 ici
                }
                else if (!isCryptoS && isCryptoD)
                {
                    // Fiat vers crypto
                    decimal montantConverti = montant / tauxDecimal;

                    // 1. Retirer du compte fiat
                    AppelerProcedure("retrait", montant, compteS, compteD, tauxDeChange, dabId);

                    // 2. Dépôt du montant converti en crypto
                    AppelerProcedure("depos", montantConverti, compteD, compteS, 1, dabId);
                }
                else
                {
                    // Fiat → fiat classique
                    AppelerProcedure("virement", montant, compteS, compteD, tauxDeChange, dabId);
                }

                MessageBox.Show("L'opération a été effectuée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'opération : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ConnexionBDD.connexion != null)
                {
                    ConnexionBDD.connexion.MyClose();
                }
            }
        }

        private void AppelerProcedure(string nomProc, decimal montant, int source, int dest, int taux, int dab)
        {
            var connection = ConnexionBDD.Connexion();
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (var command = new MySqlCommand(nomProc, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                switch (nomProc)
                {
                    case "virement":
                        command.Parameters.AddWithValue("@montant_transaction", montant);
                        command.Parameters.AddWithValue("@compteSource", source);
                        command.Parameters.AddWithValue("@compteDest", dest);
                        break;

                    case "retrait":
                        command.Parameters.AddWithValue("@montantR", montant);
                        command.Parameters.AddWithValue("@compteSource", source);
                        break;

                    case "depos":
                        command.Parameters.AddWithValue("@montantEntrant", montant);
                        command.Parameters.AddWithValue("@compteDest", source); // ici source = le compte destinataire
                        break;
                }

                command.Parameters.AddWithValue("@tauxDeChange", taux);
                command.Parameters.AddWithValue("@DAB", dab);
                command.ExecuteNonQuery();
            }

            ConnexionBDD.connexion.MyClose();
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
            else if (txtCompteDestination.Text.Length > 0 && txtCompteDestination.SelectionStart > 0)
            {
                txtCompteDestination.SelectionStart--;
                txtCompteDestination.Focus();
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
            else if (txtCompteDestination.Text.Length > 0 && txtCompteDestination.SelectionStart < txtCompteDestination.Text.Length)
            {
                txtCompteDestination.SelectionStart++;
                txtCompteDestination.Focus();
            }
            /*else if (txtMontant.Text.Length > 0 && txtMontant.SelectionStart < txtMontant.Text.Length)
            {
                txtMontant.SelectionStart++;
                txtMontant.Focus();
            }*/
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (txtCompteDestination.Focused)
            {
                txtCompteSource.Focus();
            }
            else
            {
                txtCompteSource.Focus();
            }
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (txtCompteSource.Focused)
            {
                txtCompteDestination.Focus();
            }
            else
            {
                txtCompteDestination.Focus();
            }
        }
    }
}