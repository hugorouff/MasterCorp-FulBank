using MySqlConnector;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

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
            // Largeur des boutons directionnels (Supposons qu'il y en ait à gauche)
            int buttonWidth = form.ClientSize.Width / 8;
            int spacing = 15;
            int margin = 20;

            // Largeur du panneau latéral
            int panelWidth = form.ClientSize.Width / 10;

            // Panel contenant le bandeau FulBank (placé après les boutons directionnels)
            Panel panelFul = new Panel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                Size = new Size(panelWidth, form.ClientSize.Height),
                Location = new Point(buttonWidth + spacing * 2, 0) // Après les boutons directionnels
            };
            form.Controls.Add(panelFul);

            // Taille dynamique des polices
            float titleFontSize = form.ClientSize.Height / 18f;
            float subtitleFontSize = form.ClientSize.Height / 28f;

            // Label FulBank (Texte vertical)
            Label lblFulBank = new Label
            {
                Text = "FulBank",
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = false,
                Font = new Font("Arial", titleFontSize, FontStyle.Bold),
                Size = new Size(panelFul.Width, panelFul.Height / 3),
                Location = new Point(0, panelFul.Height / 4)
            };
            lblFulBank.Paint += new PaintEventHandler(lblFulBank_Paint);
            panelFul.Controls.Add(lblFulBank);

            // Label Sous-titre
            Label lblSousTitre = new Label
            {
                Text = "Bank et Crypto",
                ForeColor = Color.FromArgb(207, 162, 0),
                AutoSize = false,
                Font = new Font("Arial", subtitleFontSize, FontStyle.Italic),
                Size = new Size(panelFul.Width, panelFul.Height / 4),
                Location = new Point(0, lblFulBank.Bottom + margin)
            };
            lblSousTitre.Paint += new PaintEventHandler(lblSousTitre_Paint);
            panelFul.Controls.Add(lblSousTitre);

            // Ajustement dynamique en cas de redimensionnement
            form.Resize += (s, e) => AdjustLayout(form, panelFul, lblFulBank, lblSousTitre);
        }

        public static void AdjustLayout(Form form, Panel panelFul, Label lblFulBank, Label lblSousTitre)
        {
            // Largeur des boutons de direction
            int buttonWidth = form.ClientSize.Width / 8;
            int spacing = 15;
            int margin = 20;

            // Largeur du panneau latéral
            int panelWidth = form.ClientSize.Width / 10;

            panelFul.Size = new Size(panelWidth, form.ClientSize.Height);
            panelFul.Location = new Point(buttonWidth + spacing * 2, 0); // Ajusté à droite des boutons

            // Ajustement des tailles de police
            float titleFontSize = form.ClientSize.Height / 18f;
            float subtitleFontSize = form.ClientSize.Height / 28f;

            lblFulBank.Font = new Font("Arial", titleFontSize, FontStyle.Bold);
            lblFulBank.Size = new Size(panelFul.Width, panelFul.Height / 2);
            lblFulBank.Location = new Point(0, panelFul.Height / 2);

            lblSousTitre.Font = new Font("Arial", subtitleFontSize, FontStyle.Italic);
            lblSousTitre.Size = new Size(panelFul.Width, panelFul.Height / 2);
            lblSousTitre.Location = new Point(0, panelFul.Height / 4 - margin * 12);
        }

        // Rotation du texte "FulBank"
        public static void lblFulBank_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            e.Graphics.Clear(lbl.BackColor);

            e.Graphics.TranslateTransform(lbl.Width / 2, lbl.Height / 2);
            e.Graphics.RotateTransform(-90);

            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.DrawString(lbl.Text, lbl.Font, new SolidBrush(lbl.ForeColor), 0, 0, stringFormat);
        }

        // Rotation du texte "Bank et Crypto"
        public static void lblSousTitre_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            e.Graphics.Clear(lbl.BackColor);

            e.Graphics.TranslateTransform(lbl.Width / 2, lbl.Height / 2);
            e.Graphics.RotateTransform(-90);

            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

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
        public string Name { get; set; } // Type de compte (ex : "Courant", "Épargne")
        public decimal Balance { get; set; }
        public string Currency { get; set; } // Nom de la monnaie
    }

    // Les info pour le dab
    public class InfoDab
    {
        public static string DabId { get; set; } = "1";
        public static string TauxDeChange { get; set; } = "1";


        // récup taux de change
        public static async Task<decimal> GetTauxDeChange(string crypto, string currency)
        {
            string apiUrl = $"https://api.coingecko.com/api/v3/simple/price?ids={crypto}&vs_currencies={currency}";

            using HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync(apiUrl);
                var data = JObject.Parse(response);

                if (data[crypto]?[currency] != null &&
                    decimal.TryParse(data[crypto]![currency]!.ToString(), out decimal rate))
                {
                    return rate;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de taux : {ex.Message}");
                return 0; 
            }
        }

        // Label pour l'api
        public static string GetLabelApi(int numeroDeCompte)
        {
            string labelApi = null;
            var BDD = ConnexionBDD.Connexion();

            if (BDD.State != ConnectionState.Open)
                BDD.Open();

            string query = @"
            SELECT M.labelApi
            FROM CompteBanquaire AS CB
            INNER JOIN Monnaie AS M ON M.id = CB.monaie
            WHERE CB.numeroDeCompte = @numero;";

            using (MySqlCommand cmd = new MySqlCommand(query, BDD))
            {
                cmd.Parameters.AddWithValue("@numero", numeroDeCompte);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labelApi = reader.GetString(0);
                    }
                }
            }

            return labelApi;
        }


        // Crypto ? 
        public static bool IsCrypto(int numeroCompte)
        {
            var conn = ConnexionBDD.Connexion();

            if (conn.State != ConnectionState.Open)
                conn.Open();

            string query = @"
            SELECT M.isCrypto 
            FROM CompteBanquaire CB
            JOIN Monnaie M ON M.id = CB.monaie
            WHERE CB.numeroDeCompte = @num;";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@num", numeroCompte);

                object result = cmd.ExecuteScalar();
                if (result != null && bool.TryParse(result.ToString(), out bool isCrypto))
                {
                    return isCrypto;
                }
            }

            return false;
        }



        // Crypto -> Crypto
        public static async Task<decimal> ConvertCryptoToCrypto(decimal amount, string sourceCrypto, string targetCrypto)
        {
            decimal sourceToEur = await GetTauxDeChange(sourceCrypto, "eur");
            decimal targetToEur = await GetTauxDeChange(targetCrypto, "eur");

            if (sourceToEur == 0 || targetToEur == 0)
                return 0;

            // Convertir en EUR puis en crypto cible
            decimal eurAmount = amount * sourceToEur;
            return eurAmount / targetToEur;
        }

        // Crypto -> Monaie
        public static async Task<decimal> ConvertCryptoToFiat(decimal amount, string crypto, string fiat)
        {
            decimal rate = await GetTauxDeChange(crypto, fiat.ToLower());

            if (rate == 0)
                return 0;

            return amount * rate;
        }

        // Monaie -> Crypto
        public static async Task<decimal> ConvertFiatToCrypto(decimal amount, string fiat, string crypto)
        {
            decimal rate = await GetTauxDeChange(crypto, fiat.ToLower());

            if (rate == 0)
                return 0;

            return amount / rate;
        }



    }

}