using System.Drawing.Drawing2D;

namespace fulbank
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Connexion());
        }
    }


    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 40; // Rayon par défaut pour les coins arrondis

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // Créer un chemin graphique avec des coins arrondis
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, BorderRadius, BorderRadius), 180, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, 0, BorderRadius, BorderRadius), 270, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, this.Height - BorderRadius, BorderRadius, BorderRadius), 0, 90);
            path.AddArc(new Rectangle(0, this.Height - BorderRadius, BorderRadius, BorderRadius), 90, 90);
            path.CloseAllFigures();

            // Appliquer le chemin arrondi au bouton
            this.Region = new Region(path);

            // Dessiner le fond et les bordures
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.FillPath(new SolidBrush(this.BackColor), path);

            // Centrer le texte dans le bouton
            DrawCenteredText(pevent);
        }

        private void DrawCenteredText(PaintEventArgs pevent)
        {
            // Calculer les dimensions du texte
            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);

            // Calculer les positions pour centrer le texte
            int textX = ((this.Width - textSize.Width) / 2) +5;
            int textY = ((this.Height - textSize.Height) / 2) - 5;

            // Dessiner le texte
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, new Point(textX, textY), this.ForeColor);
        }
    }
    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 40;  // Rayon par défaut pour les coins arrondis 
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            // Créer un chemin graphique avec des coins arrondis 
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, BorderRadius, BorderRadius), 180, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, 0, BorderRadius, BorderRadius), 270, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, this.Height - BorderRadius, BorderRadius, BorderRadius), 0, 90);
            path.AddArc(new Rectangle(0, this.Height - BorderRadius, BorderRadius, BorderRadius), 90, 90);
            path.CloseAllFigures();
            // Appliquer le chemin arrondi au bouton 
            this.Region = new Region(path);
            // Dessiner le fond et les bordures 
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.FillPath(new SolidBrush(this.BackColor), path);
            // Dessiner le texte du bouton 
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    public static class Methode
    {
        public static void CenterControlInParent(Control control)
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

        public static void Fulbank(Form form)
        {
            // Panel contenant les champs FulBank
            Panel panelFul = new Panel
            {
                BackColor = Color.FromArgb(34, 67, 153)
            };
            form.Controls.Add(panelFul);

            // Label FulBank
            Label lblFulBank = new Label
            {
                Text = "FulBank",
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = false
            };
            lblFulBank.Paint += new PaintEventHandler(lblFulBank_Paint);
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre
            Label lblSousTitre = new Label
            {
                Text = "Bank et Crypto",
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = false
            };
            lblSousTitre.Paint += new PaintEventHandler(lblSousTitre_Paint);
            panelFul.Controls.Add(lblSousTitre);

            // Redimensionnement dynamique
            form.Resize += (s, e) => AdjustLayout(form, panelFul, lblFulBank, lblSousTitre);
        }

        public static void AdjustLayout(Form form, Panel panelFul, Label lblFulBank, Label lblSousTitre)
        {
            // Calculer la largeur des boutons directionnels
            int buttonWidth = form.ClientSize.Width / 8;
            int spacing = 15;

            // Positionner le panel juste à droite des boutons directionnels
            panelFul.Size = new Size(form.ClientSize.Width / 8, form.ClientSize.Height);
            panelFul.Location = new Point(buttonWidth + spacing * 2, 0);

            int margin = form.ClientSize.Height / 20;

            // Calculer la taille de la police en fonction de la hauteur de la fenêtre
            float baseFontSize = form.ClientSize.Height / 40f;

            // Taille et police du label FulBank
            lblFulBank.Font = new Font("Arial", baseFontSize * 5 / 2, FontStyle.Bold); // Taille dynamique
            lblFulBank.Size = new Size(panelFul.Width - margin * 2, panelFul.Height / 3);
            lblFulBank.Location = new Point((panelFul.Width - lblFulBank.Width) / 3, margin * 13);

            // Taille et police du label SousTitre
            lblSousTitre.Font = new Font("Arial", baseFontSize, FontStyle.Italic); // Taille dynamique
            lblSousTitre.Size = new Size(panelFul.Width - margin, panelFul.Height / 3);
            lblSousTitre.Location = new Point((panelFul.Width - lblSousTitre.Width) * 6 / 4, lblFulBank.Bottom / 2 + margin);
        }


        // Méthode Paint pour le label "FulBank"
        public static void lblFulBank_Paint(object sender, PaintEventArgs e)
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
        public static void lblSousTitre_Paint(object sender, PaintEventArgs e)
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

        public static void CreateDirectionalButtons(
            Form form,
            EventHandler btnHautClick,
            EventHandler btnBasClick,
            EventHandler btnGaucheClick,
            EventHandler btnDroiteClick,
            EventHandler btnValiderClick,
            EventHandler btnRetourClick,
            EventHandler btnMaisonClick,
            EventHandler btnFermerClick)
        {
            // Fonction pour créer un bouton avec des propriétés de base
            RoundedButton CreateButton(string text, EventHandler clickEvent, Color backgroundColor, Color foreColor)
            {
                // Augmenter la taille des boutons et de la police
                RoundedButton button = new RoundedButton
                {
                    Text = text,
                    ForeColor = foreColor,
                    BackColor = backgroundColor,
                    BorderRadius = 90,
                };
                button.Click += clickEvent;
                return button;
            }

            // Créer des boutons directionnels
            RoundedButton btnHaut = CreateButton("↑", btnHautClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));
            RoundedButton btnBas = CreateButton("↓", btnBasClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));
            RoundedButton btnGauche = CreateButton("←", btnGaucheClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));
            RoundedButton btnDroite = CreateButton("→", btnDroiteClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));

            // Créer des boutons de contrôle
            RoundedButton btnValider = CreateButton("✔", btnValiderClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));
            RoundedButton btnRetour = CreateButton("↩", btnRetourClick, Color.FromArgb(99, 99, 101), Color.FromArgb(128, 194, 236));
            RoundedButton btnMaison = CreateButton("🏠", btnMaisonClick, Color.FromArgb(99, 99, 101), Color.FromArgb(128, 194, 236));
            RoundedButton btnFermer = CreateButton("X", btnFermerClick, Color.FromArgb(34, 67, 153), Color.FromArgb(128, 194, 236));

            // Ajouter les boutons au formulaire
            form.Controls.Add(btnHaut);
            form.Controls.Add(btnBas);
            form.Controls.Add(btnGauche);
            form.Controls.Add(btnDroite);
            form.Controls.Add(btnValider);
            form.Controls.Add(btnRetour);
            form.Controls.Add(btnMaison);
            form.Controls.Add(btnFermer);

            // Méthode pour ajuster la disposition des boutons
            void AdjustButtonLayout()
            {
                // Dimensions des boutons
                int buttonHeight = form.ClientSize.Height / 5;
                int buttonWidth = form.ClientSize.Width / 8;

                // Espacement vertical basé sur celui des panneaux
                int buttonSpacing = form.ClientSize.Height / 20;

                // Ajustement de la taille de la police
                float fontSize = Math.Max(14, buttonHeight / 4);

                // Position et dimensions des boutons directionnels
                btnHaut.Size = new Size(buttonWidth, buttonHeight);
                btnHaut.Location = new Point(0, buttonSpacing / 2);
                btnHaut.Font = new Font(btnHaut.Font.FontFamily, fontSize);

                btnGauche.Size = new Size(buttonWidth, buttonHeight);
                btnGauche.Location = new Point(0, btnHaut.Bottom + buttonSpacing);
                btnGauche.Font = new Font(btnGauche.Font.FontFamily, fontSize);

                btnDroite.Size = new Size(buttonWidth, buttonHeight);
                btnDroite.Location = new Point(0, btnGauche.Bottom + buttonSpacing);
                btnDroite.Font = new Font(btnDroite.Font.FontFamily, fontSize);

                btnBas.Size = new Size(buttonWidth, buttonHeight);
                btnBas.Location = new Point(0, btnDroite.Bottom + buttonSpacing);
                btnBas.Font = new Font(btnBas.Font.FontFamily, fontSize);

                // Position et dimensions des boutons de contrôle
                int controlButtonWidth = buttonWidth;
                int controlButtonHeight = buttonHeight;

                btnValider.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnValider.Location = new Point(form.ClientSize.Width - controlButtonWidth, buttonSpacing / 2);
                btnValider.Font = new Font(btnValider.Font.FontFamily, fontSize);

                btnRetour.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnRetour.Location = new Point(form.ClientSize.Width - controlButtonWidth, btnValider.Bottom + buttonSpacing);
                btnRetour.Font = new Font(btnRetour.Font.FontFamily, fontSize);

                btnMaison.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnMaison.Location = new Point(form.ClientSize.Width - controlButtonWidth, btnRetour.Bottom + buttonSpacing);
                btnMaison.Font = new Font(btnMaison.Font.FontFamily, fontSize);

                btnFermer.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnFermer.Location = new Point(form.ClientSize.Width - controlButtonWidth, btnMaison.Bottom + buttonSpacing);
                btnFermer.Font = new Font(btnFermer.Font.FontFamily, fontSize);
            }

            // Initialiser la disposition des boutons
            AdjustButtonLayout();

            // Événement de redimensionnement du formulaire
            form.Resize += (s, e) => AdjustButtonLayout();
        }
    }

    // Classe pour représenter un compte bancaire
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } // Type de compte (e.g., "Courant", "Épargne")
        public decimal Balance { get; set; }
        public string Currency { get; set; } // Nom de la monnaie
    }
}