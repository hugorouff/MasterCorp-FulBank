﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace fulbank
{
    public partial class MenuBase : Form
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
        public MenuBase()
        {
            InitializeComponent();
            Initializeform2();
            UpdatePanelSelection();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider2_Click(object sender, EventArgs e)
        {
            Form nextForm = null;

            // Decide which form to show based on the selected panel
            switch (selectedPanelIndex)
            {
                case 0:
                    nextForm = new MenuCompte();
                    break;
                case 1:
                    nextForm = new CoursCrypto(); // Assuming you have this form
                    break;
                case 2:
                    nextForm = new MenuTransaction(); // Assuming you have this form
                    break;
                case 3:
                    this.Close();
                    break;
            }

            if (nextForm != null)
            {
                nextForm.Show();
                this.Hide();
            }
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

        private void BtnFermer2_Click(object sender, EventArgs e)
        {
            Connexion form1 = new Connexion();
            form1.Show();
            this.Close();
        }

        /*private void BtnRetour2_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }

        private void BtnMaison2_Click(object sender, EventArgs e)
        {
            MenuBase form2 = new MenuBase();
            form2.Show();
            this.Close();
        }*/

        private void Initializeform2()
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

            // Panel contenant les champs Compte
            RoundedPanel panelCompte = new RoundedPanel();
            panelCompte.BackColor = Color.FromArgb(34, 67, 153);
            panelCompte.Size = new Size(1360, 300);  // Agrandir le panel 
            panelCompte.Location = new Point(100, -310);  // Centré 
            panelCompte.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCompte.BorderRadius = 90;
            this.Controls.Add(panelCompte);

            // Label Compte 
            Label lblCompte = new Label();
            lblCompte.Text = "Comptes";
            lblCompte.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblCompte.ForeColor = Color.FromArgb(128, 194, 236);
            lblCompte.AutoSize = true;
            panelCompte.Controls.Add(lblCompte);
            CenterControlInParent(lblCompte);

            // Panel contenant les champs Crypto
            RoundedPanel panelCoursCrypto = new RoundedPanel();
            panelCoursCrypto.BackColor = Color.FromArgb(34, 67, 153);
            panelCoursCrypto.Size = new Size(1360, 300);  // Agrandir le panel 
            panelCoursCrypto.Location = new Point(100, 40);  // Centré 
            panelCoursCrypto.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelCoursCrypto.BorderRadius = 90;
            this.Controls.Add(panelCoursCrypto);

            // Label Nom Crypto 
            Label lblCrypto = new Label();
            lblCrypto.Text = "Cours Crypto";
            lblCrypto.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblCrypto.ForeColor = Color.FromArgb(128, 194, 236);
            lblCrypto.AutoSize = true;
            panelCoursCrypto.Controls.Add(lblCrypto);
            CenterControlInParent(lblCrypto);

            // Panel contenant les champs Transaction
            RoundedPanel panelTransaction = new RoundedPanel();
            panelTransaction.BackColor = Color.FromArgb(34, 67, 153);
            panelTransaction.Size = new Size(1360, 300);  // Agrandir le panel 
            panelTransaction.Location = new Point(100, 390);  // Centré 
            panelTransaction.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelTransaction.BorderRadius = 90;
            this.Controls.Add(panelTransaction);

            // Label Nom Transaction 
            Label lblTransaction = new Label();
            lblTransaction.Text = "Transaction";
            lblTransaction.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblTransaction.ForeColor = Color.FromArgb(128, 194, 236);
            lblTransaction.AutoSize = true;
            panelTransaction.Controls.Add(lblTransaction);
            CenterControlInParent(lblTransaction);

            // Panel contenant les champs Autres
            RoundedPanel panelAutres = new RoundedPanel();
            panelAutres.BackColor = Color.FromArgb(34, 67, 153);
            panelAutres.Size = new Size(1360, 300);  // Agrandir le panel 
            panelAutres.Location = new Point(100, 740);  // Centré 
            panelAutres.Anchor = AnchorStyles.None;  // Garder le panel centré 
            panelAutres.BorderRadius = 90;
            this.Controls.Add(panelAutres);

            // Label Nom Autres 
            Label lblAutres = new Label();
            lblAutres.Text = "Autres";
            lblAutres.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme 
            lblAutres.ForeColor = Color.FromArgb(128, 194, 236);
            lblAutres.AutoSize = true;
            panelAutres.Controls.Add(lblAutres);
            CenterControlInParent(lblAutres);

            panels = new RoundedPanel[] { panelCompte, panelCoursCrypto, panelTransaction, panelAutres };

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
            btnValider.Click += BtnValider2_Click;
            this.Controls.Add(btnValider);

            // Bouton Retour ↩ 
            RoundedButton btnRetour = new RoundedButton();
            btnRetour.Text = "↩";
            btnRetour.ForeColor = Color.FromArgb(128, 194, 236);
            btnRetour.Size = new Size(300, 300);  // Taille énorme 
            btnRetour.Location = new Point(2200, 360);  // Position ajustée 
            btnRetour.BackColor = Color.FromArgb(99, 99, 101);
            btnRetour.Font = new Font("Arial", 120, FontStyle.Bold);  // Texte énorme
            btnRetour.BorderRadius = 90;
            //btnRetour.Click += BtnRetour2_Click;
            btnRetour.Enabled = false;
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
            //btnMaison.Click += BtnMaison2_Click;
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
            btnFermer.Click += BtnFermer2_Click;
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
            btnBas.Click += BtnBas_Click; // Hook up the event handler
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
            btnGauche.Enabled = false;
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
            btnDroite.Enabled = false;
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