using MySqlConnector;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace fulbank
{
    public partial class MenuFinalTransactionDepot : Form
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

        public MenuFinalTransactionDepot()
        {
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
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = "Transaction Dépôt";
            this.BackColor = Color.FromArgb(128, 194, 236);

            panelTransaction = new RoundedPanel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                BorderRadius = 50
            };
            this.Controls.Add(panelTransaction);

            lblTitre = new Label
            {
                Text = "Transaction Dépôt",
                Font = new Font("Arial", 50, FontStyle.Bold),
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

            lblCompteDestination = new Label
            {
                Text = "Numéro Compte Destination :",
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panelChamps.Controls.Add(lblCompteDestination);

            txtCompteDestination = new TextBox();
            panelChamps.Controls.Add(txtCompteDestination);

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

            lblCompteDestination.Location = new Point(margin, txtCompteSource.Bottom + margin);
            txtCompteDestination.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtCompteDestination.Location = new Point(margin, lblCompteDestination.Bottom + margin);

            lblMontant.Location = new Point(margin, txtCompteDestination.Bottom + margin);
            txtMontant.Size = new Size(panelChamps.Width - 2 * margin, panelChamps.Height / 12);
            txtMontant.Location = new Point(margin, lblMontant.Bottom + margin);

            panelTransaction.Size = new Size(this.ClientSize.Width * 59 / 100, this.ClientSize.Height / 5);
            panelTransaction.Location = new Point((((this.ClientSize.Width - panelTransaction.Width) / 2) * 4 / 3), margin);

            float baseFontSize = this.ClientSize.Height / 40f;
            lblTitre.Font = new Font("Arial", baseFontSize * 3, FontStyle.Bold);
            lblMontant.Font = lblCompteSource.Font = lblCompteDestination.Font = new Font("Arial", baseFontSize * 2);
            txtMontant.Font = txtCompteSource.Font = txtCompteDestination.Font = new Font("Arial", baseFontSize * 2);

            lblTitre.AutoSize = true;
            lblTitre.Location = new Point((panelTransaction.Width - lblTitre.Width) / 2, margin);
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            // Récupérer et valider les informations des TextBox
            string compte = txtCompteSource.Text.Trim();
            string montantText = txtMontant.Text.Trim();

            if (string.IsNullOrWhiteSpace(compte))
            {
                MessageBox.Show("Veuillez entrer un numéro de compte valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompteSource.Focus();
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
                // Effectuer le dépôt
                EffectuerDepot(compte, montant);

                // Afficher un message de succès
                MessageBox.Show("Le dépôt a été effectué avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Réinitialiser les champs après le dépôt réussi
                txtCompteSource.Clear();
                txtMontant.Clear();

                // Revenir au premier champ
                txtCompteSource.Focus();
            }
            catch (Exception ex)
            {
                // Gérer les exceptions et afficher un message d'erreur
                MessageBox.Show($"Une erreur s'est produite lors du dépôt : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EffectuerDepot(string compte, decimal montant)
        {
            using (var connection = ConnexionBDD.Connexion())
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        // Vérifier si le compte existe
                        string queryCheckSource = @"
                            SELECT solde
                            FROM CompteBanquaire
                            WHERE numeroDeCompte = @Compte";

                        decimal soldeSource;

                        using (var command = new MySqlCommand(queryCheckSource, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Compte", compte);
                            var result = command.ExecuteScalar();

                            if (result == null)
                            {
                                throw new InvalidOperationException("Le compte n'existe pas.");
                            }

                            soldeSource = Convert.ToDecimal(result);

                            if (soldeSource < montant)
                            {
                                throw new InvalidOperationException("Fonds insuffisants sur le compte.");
                            }
                        }

                        // Débiter le montant du compte
                        string queryDebit = @"
                            UPDATE CompteBanquaire
                            SET solde = solde - @Montant
                            WHERE numeroDeCompte = @Compte";

                        using (var command = new MySqlCommand(queryDebit, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Montant", montant);
                            command.Parameters.AddWithValue("@Compte", compte);
                            command.ExecuteNonQuery();
                        }

                        // Créditer le montant au compte (ce serait le même compte dans ce cas pour un dépôt)
                        string queryCredit = @"
                            UPDATE CompteBanquaire
                            SET solde = solde + @Montant
                            WHERE numeroDeCompte = @Compte";

                        using (var command = new MySqlCommand(queryCredit, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Montant", montant);
                            command.Parameters.AddWithValue("@Compte", compte);
                            command.ExecuteNonQuery();
                        }

                        // Insérer l'opération dans la table Opperation
                        string queryInsertOperation = @"
                            INSERT INTO Opperation (dateOperation, montant, suprimee, compte)
                            VALUES (@DateOperation, @Montant, false, @Compte)";

                        using (var command = new MySqlCommand(queryInsertOperation, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@DateOperation", DateTime.Now);
                            command.Parameters.AddWithValue("@Montant", montant);
                            command.Parameters.AddWithValue("@Compte", compte);
                            command.ExecuteNonQuery();
                        }

                        // Commit de la transaction
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du dépôt : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw; // Relever l'exception pour le traitement en amont si nécessaire
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
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