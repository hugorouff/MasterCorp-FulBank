using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace fulbank
{
    public partial class MenuCompte : Form
    {
        private RoundedPanel panelCompte1;
        private RoundedPanel panelCompte2;
        private RoundedPanel panelCompte3;
        private RoundedPanel panelCompte4;

        private RoundedPanel[] panels; // Panneaux actifs dans une page
        private int currentPageIndex = 0; // Page actuelle
        private int selectedPanelIndex = 0;//panel actuelle
        private int totalPages = 0; // Nombre total de pages

        private List<string> compteInfos; // Stocke les données des comptes

        public MenuCompte()
        {
            this.Icon = new Icon("Resources/logo-fulbank.ico");
            InitializeComponent();
            Initializeform3();
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

            // Charger les données des comptes
            LoadAccountData();
        }

        private void LoadAccountData()
        {
            // Récupérer l'utilisateur connecté
            Utilisateur utilisateur = Utilisateur.getInstance();
            if (utilisateur == null)
            {
                MessageBox.Show("Aucun utilisateur connecté.");
                return;
            }

            // Charger les données depuis la base de données
            utilisateur.PullComptes();

            // Construire les données sous forme de texte à afficher dans les panels
            compteInfos = new List<string>();
            var numComptes = utilisateur.GetNumComptes();
            var soldes = utilisateur.GetSoldes();
            var types = utilisateur.GetTypeComptes();
            var monnaies = utilisateur.GetMonnaies();

            for (int i = 0; i < numComptes.Count; i++)
            {
                string info = $"{types[i]}: {soldes[i]} {monnaies[i]} (N° {numComptes[i]})";
                compteInfos.Add(info);
            }

            // Calculer le nombre total de pages
            totalPages = (int)Math.Ceiling((double)compteInfos.Count / 4);

            // Afficher la première page
            UpdatePage();
        }
        private void UpdatePanel()
        {
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Couleur par défaut
            }

            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Couleur sélectionnée
        }

        private void UpdatePage()
        {
            // Effacer les contenus existants des panels
            foreach (var panel in panels)
            {
                panel.Controls.Clear();
            }

            // Déterminer les données à afficher pour la page actuelle
            int startIndex = currentPageIndex * 4;
            int endIndex = Math.Min(startIndex + 4, compteInfos.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                int panelIndex = i % 4; // Position dans les panels (0-3)
                var panel = panels[panelIndex];
                string compteInfo = compteInfos[i];

                // Ajouter un label avec les données du compte
                InitPanelLabel(panel, compteInfo);

            }

            AdjustPanelLayout();
            UpdatePanel();
        }
        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex < 3)
            {
                selectedPanelIndex++;
                UpdatePanel();
            }
            else if (currentPageIndex < totalPages - 1)
            {
                currentPageIndex++;
                selectedPanelIndex = 0;
                UpdatePage();
            }
        }

        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
                UpdatePanel();
            }
            else if (currentPageIndex > 0)
            {
                currentPageIndex--;
                selectedPanelIndex = 3;
                UpdatePage();

            }
        }

        private void Initializeform3()
        {
            this.Size = new Size(1580, 1024);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Initialisation des 4 panels fixes
            panelCompte1 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte2 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte3 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };
            panelCompte4 = new RoundedPanel { BackColor = Color.FromArgb(34, 67, 153), BorderRadius = 90 };

            this.Controls.AddRange(new Control[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 });

            panels = new RoundedPanel[] { panelCompte1, panelCompte2, panelCompte3, panelCompte4 };

            this.Resize += (s, e) => AdjustPanelLayout();
            AdjustPanelLayout();
        }

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

        private void InitPanelLabel(RoundedPanel panel, string text)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", 35, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                AutoSize = true
            };
            panel.Controls.Add(label);
            Methode.CenterControlInParent(label);
        }
        private void BtnValider_Click(object sender, EventArgs e)
        {

            Utilisateur.getInstance().setCompteChoisi(Utilisateur.getInstance().GetNumComptes()[selectedPanelIndex + 4 * currentPageIndex]);
            MenuHistoriqueCompts form = new MenuHistoriqueCompts();
            form.Show();
            this.Close();
        }

        private void BtnGauche_Click(object sender, EventArgs e) { }

        private void BtnDroite_Click(object sender, EventArgs e) { }

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
    }
}