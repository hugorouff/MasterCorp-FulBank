﻿using System.Drawing.Drawing2D;

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
            Panel panelFul = new Panel();
            panelFul.BackColor = Color.FromArgb(34, 67, 153);
            panelFul.Size = new Size(300, 1080);  // Agrandir le panel 
            panelFul.Location = new Point(260, 0);  // Centré 
            form.Controls.Add(panelFul);

            // Label FulBank 
            Label lblFulBank = new Label();
            lblFulBank.Text = "FulBank";
            lblFulBank.Font = new Font("Arial", 90, FontStyle.Bold);  // Texte énorme 
            lblFulBank.ForeColor = Color.FromArgb(207, 162, 0);
            lblFulBank.Size = new Size(225, 1500);
            lblFulBank.Paint += new PaintEventHandler(lblFulBank_Paint);
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre 
            Label lblSousTitre = new Label();
            lblSousTitre.Text = "Bank et Crypto";
            lblSousTitre.Font = new Font("Arial", 35, FontStyle.Italic);  // Texte énorme 
            lblSousTitre.ForeColor = Color.FromArgb(207, 162, 0);
            lblSousTitre.Size = new Size(430, 1000);
            lblSousTitre.Paint += new PaintEventHandler(lblSousTitre_Paint);
            panelFul.Controls.Add(lblSousTitre);
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
                    Size = new Size(form.ClientSize.Width / 6, form.ClientSize.Height / 8), // Taille augmentée
                    Font = new Font(SystemFonts.DefaultFont.FontFamily, Math.Max(18, form.ClientSize.Height / 25)) // Taille de police augmentée
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
                int buttonHeight = form.ClientSize.Height / 5; // Hauteur augmentée pour les boutons directionnels
                int buttonWidth = form.ClientSize.Width / 8; // Largeur augmentée pour les boutons directionnels
                int leftMargin = 0;
                int topMargin = 40;

                // Positionner les boutons directionnels
                btnHaut.Size = new Size(buttonWidth, buttonHeight);
                btnHaut.Location = new Point(leftMargin, topMargin);

                btnGauche.Size = new Size(buttonWidth, buttonHeight);
                btnGauche.Location = new Point(leftMargin, btnHaut.Bottom + topMargin);

                btnBas.Size = new Size(buttonWidth, buttonHeight);
                btnBas.Location = new Point(leftMargin, btnGauche.Bottom + topMargin);

                btnDroite.Size = new Size(buttonWidth, buttonHeight);
                btnDroite.Location = new Point(leftMargin, btnBas.Bottom + topMargin);

                // Ajuster les boutons de contrôle
                int controlButtonWidth = form.ClientSize.Width / 8; // largeur des boutons de contrôle augmentée
                int controlButtonHeight = form.ClientSize.Height / 5; // hauteur des boutons de contrôle augmentée

                btnValider.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnValider.Location = new Point(form.ClientSize.Width - controlButtonWidth - leftMargin, topMargin);

                btnRetour.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnRetour.Location = new Point(form.ClientSize.Width - controlButtonWidth - leftMargin, btnValider.Bottom + topMargin);

                btnMaison.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnMaison.Location = new Point(form.ClientSize.Width - controlButtonWidth - leftMargin, btnRetour.Bottom + topMargin);

                btnFermer.Size = new Size(controlButtonWidth, controlButtonHeight);
                btnFermer.Location = new Point(form.ClientSize.Width - controlButtonWidth - leftMargin, btnMaison.Bottom + topMargin);
            }

            // Initialiser la disposition des boutons
            AdjustButtonLayout();

            // Événement de redimensionnement du formulaire
            form.Resize += (s, e) => AdjustButtonLayout();
        }
    }
}