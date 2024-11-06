using System;
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BtnValider_Click(object sender, EventArgs e)
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

        private void BtnRetour_Click(object sender, EventArgs e) { }

        private void BtnMaison_Click(object sender, EventArgs e) { }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

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

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form1 = new Connexion();
            form1.Show();
            this.Close();
        }

        private void Initializeform2()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des panneaux avec taille par défaut
            panelCompte = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCoursCrypto = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelTransaction = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelAutres = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            // Ajout des panneaux au formulaire
            this.Controls.AddRange(new Control[] { panelCompte, panelCoursCrypto, panelTransaction, panelAutres });

            // Initialisation des labels et leur centrage dans les panneaux
            InitPanelLabel(panelCompte, "Comptes");
            InitPanelLabel(panelCoursCrypto, "Cours Crypto");
            InitPanelLabel(panelTransaction, "Transaction");
            InitPanelLabel(panelAutres, "Autres");

            panels = new RoundedPanel[] { panelCompte, panelCoursCrypto, panelTransaction, panelAutres };

            // Appel de la méthode de création des boutons et de l'ajustement
            Methode.CreateDirectionalButtons(this, BtnHaut_Click, BtnBas_Click, BtnGauche_Click, BtnDroite_Click, BtnValider_Click, BtnRetour_Click, BtnMaison_Click, BtnFermer_Click);

            // Initialiser la disposition des panneaux
            AdjustPanelLayout();

            // Ajouter un événement pour redimensionner les panneaux automatiquement
            this.Resize += (s, e) => AdjustPanelLayout();
        }

        // Méthode pour ajuster la disposition des panneaux
        private void AdjustPanelLayout()
        {
            int panelWidth = (int)(this.ClientSize.Width * 0.55); // Largeur du panel = 55% de la largeur de l'écran
            int panelHeight = (int)(this.ClientSize.Height * 0.15); // Hauteur du panel = 15% de la hauteur de l'écran
            int panelSpacing = (int)(this.ClientSize.Height * 0.02); // Espace entre les panels

            // Ajustement des dimensions des panneaux
            panelCompte.Size = new Size(panelWidth, panelHeight);
            panelCoursCrypto.Size = new Size(panelWidth, panelHeight);
            panelTransaction.Size = new Size(panelWidth, panelHeight);
            panelAutres.Size = new Size(panelWidth, panelHeight);

            // Centrer les panneaux verticalement
            panelCompte.Location = new Point((this.ClientSize.Width - panelWidth) / 2, panelSpacing);
            panelCoursCrypto.Location = new Point((this.ClientSize.Width - panelWidth) / 2, panelCompte.Bottom + panelSpacing);
            panelTransaction.Location = new Point((this.ClientSize.Width - panelWidth) / 2, panelCoursCrypto.Bottom + panelSpacing);
            panelAutres.Location = new Point((this.ClientSize.Width - panelWidth) / 2, panelTransaction.Bottom + panelSpacing);

            // Réajuster les labels à l'intérieur de chaque panel
            foreach (var panel in panels)
            {
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

/*
// Panel contenant les champs FulBank
Panel panelFul = new Panel();
panelFul.BackColor = Color.FromArgb(34, 67, 153);

// Ajustement initial de la taille et de la position
AjusterElements(form, panelFul);

// Ajouter le panel au formulaire
form.Controls.Add(panelFul);

// Label FulBank
Label lblFulBank = new Label();
lblFulBank.Text = "FulBank";
lblFulBank.Font = new Font("Arial", 90, FontStyle.Bold);
lblFulBank.ForeColor = Color.FromArgb(207, 162, 0);
panelFul.Controls.Add(lblFulBank);

// Label Sous-titre
Label lblSousTitre = new Label();
lblSousTitre.Text = "Bank et Crypto";
lblSousTitre.Font = new Font("Arial", 35, FontStyle.Italic);
lblSousTitre.ForeColor = Color.FromArgb(207, 162, 0);
panelFul.Controls.Add(lblSousTitre);

// Ajuster la mise en page pour la première fois
//AdjustLayout();

lblFulBank.Paint += new PaintEventHandler(lblFulBank_Paint);
lblSousTitre.Paint += new PaintEventHandler(lblSousTitre_Paint);

// Écouter l'événement de redimensionnement pour ajuster dynamiquement les éléments
form.Resize += (sender, args) => AjusterElements(form, panelFul);


// Méthode pour ajuster la taille et la position des éléments
private static void AjusterElements(Form form, Panel panelFul)
{
    // Calcule la taille et la position des boutons directionnels pour connaître leur emplacement final
    int buttonWidth = form.ClientSize.Width / 8;
    int buttonHeight = form.ClientSize.Height / 5;
    int buttonsRightEdge = buttonWidth + 20; // Position finale des boutons directionnels avec une marge

    // Ajustement de la taille et de la position de `panelFul`
    panelFul.Size = new Size((int)(form.ClientSize.Width - buttonsRightEdge - 20), form.ClientSize.Height);
    panelFul.Location = new Point(buttonsRightEdge + 10, 0); // Ajoute une marge de 20 pixels à droite des boutons

    // Ajustement des tailles des labels en fonction de la hauteur du panel
    foreach (Control control in panelFul.Controls)
    {
        if (control is Label lbl)
        {
            lbl.Size = new Size(panelFul.Width, panelFul.Height / panelFul.Controls.Count);
        }
    }
}*/