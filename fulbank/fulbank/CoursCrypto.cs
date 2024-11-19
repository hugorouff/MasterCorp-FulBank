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
        private RoundedPanel[] panels;
        private int selectedPanelIndex = 0; // Index pour suivre le panel actif

        public CoursCrypto()
        {
            InitializeComponent();
            Initializeform2();
            UpdatePanelSelection();

            // Créer les boutons directionnels
            Methode.CreateDirectionalButtons(
                this,
                BtnHaut_Click,
                BtnBas_Click,
                BtnGauche_Click,
                BtnDroite_Click,
                BtnValider_Click,
                BtnRetour_Click,
                BtnMaison_Click,
                BtnFermer_Click
            );
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

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex < panels.Length - 1)
            {
                selectedPanelIndex++;
            }
            UpdatePanelSelection();
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
            }
            UpdatePanelSelection();
        }

        private void BtnGauche_Click(object sender, EventArgs e) { }
        private void BtnDroite_Click(object sender, EventArgs e) { }
        private void BtnValider_Click(object sender, EventArgs e) { }

        // Mise à jour de la sélection visuelle du panel
        private void UpdatePanelSelection()
        {
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Couleur par défaut
            }

            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Couleur sélectionnée
        }

        private void Initializeform2()
        {
            // Configuration générale du formulaire
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            // Appelle la méthode pour afficher le panel de FulBank
            Methode.Fulbank(this);

            // Initialisation des panneaux
            RoundedPanel panelCrypto = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            RoundedPanel panelCrypto1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            this.Controls.AddRange(new Control[] { panelCrypto, panelCrypto1 });

            // Initialisation des labels dans chaque panel
            InitPanelLabel(panelCrypto, "Bitcoin", "57000$/BTC");
            InitPanelLabel(panelCrypto1, "Ethereum", "4000$/ETH");

            // Assigner les panels à la liste
            panels = new RoundedPanel[] { panelCrypto, panelCrypto1 };

            // Ajustement de la disposition des panneaux
            AdjustPanelLayout();

            // Ajouter un événement pour réajuster les panneaux au redimensionnement
            this.Resize += (s, e) => AdjustPanelLayout();
        }

        // Ajustement de la disposition des panneaux
        private void AdjustPanelLayout()
        {
            int panelWidth = this.ClientSize.Width * 53 / 90;
            int buttonHeight = this.ClientSize.Height / 5;
            int panelHeight = buttonHeight;

            int buttonWidth = this.ClientSize.Width / 8;
            int buttonSpacing = this.ClientSize.Height / 20;

            int panelX = this.ClientSize.Width - buttonWidth - panelWidth - buttonSpacing;

            int topMargin = (this.ClientSize.Height - (panelHeight * panels.Length + buttonSpacing * (panels.Length - 1))) / 2;

            for (int i = 0; i < panels.Length; i++)
            {
                RoundedPanel panel = panels[i];
                panel.Size = new Size(panelWidth, panelHeight);
                panel.Location = new Point(panelX * 105 / 100, topMargin + i * (panelHeight + buttonSpacing));

                if (panel.Controls.Count > 0 && panel.Controls[0] is Label lbl)
                {
                    Methode.CenterControlInParent(lbl);
                }
            }
        }

        // Initialisation des labels dans un panel
        private void InitPanelLabel(RoundedPanel panel, string title, string price)
        {
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Arial", 90, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };

            Label lblPrice = new Label
            {
                Text = price,
                Font = new Font("Arial", 90, FontStyle.Bold),
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = true
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblPrice);

            Methode.CenterControlInParent(lblTitle);
            Methode.CenterControlInParent(lblPrice);
        }
    }
}