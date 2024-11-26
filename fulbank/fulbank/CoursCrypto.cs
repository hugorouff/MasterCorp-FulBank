using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace fulbank
{
    public partial class CoursCrypto : Form
    {
        // Déclaration des variables
        private RoundedPanel[] panels;
        private int selectedPanelIndex = 0; // Index du panel sélectionné dans la liste globale
        private int visibleStartIndex = 0; // Premier index visible
        private readonly List<string> cryptos = new List<string>
        {
            "binancecoin", "bitcoin", "cardano", "celestia", "chainlink",
            "cosmos", "dogecoin", "ethereum", "fantom", "litecoin",
            "monero", "neo", "polkadot", "ripple", "shiba-inu", "solana",
            "stellar", "sui", "tron", "uniswap", "usd-coin", "vechain",
            "wrapped-bitcoin"
        };
        private readonly List<string> currencies = new List<string> { "usd", "eur", "gbp" };
        private const int panelsPerPage = 4; // Nombre de panels visibles à la fois

        public CoursCrypto()
        {
            InitializeComponent();
            InitializeForm();
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

        private async void InitializeForm()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(128, 194, 236);
            this.Text = "FulBank";

            Methode.Fulbank(this);

            // Charger les données et initialiser les panneaux
            await LoadCryptoDataAsync();

            // Vérifier l'initialisation des panneaux
            if (panels == null || panels.Length == 0)
            {
                Console.Error.WriteLine("Les panneaux n'ont pas été initialisés.");
                return;
            }

            UpdatePanelSelection();
            AdjustPanelLayout();

            this.Resize += (s, e) => AdjustPanelLayout();
        }

        private async Task LoadCryptoDataAsync()
        {
            string apiUrl = $"https://api.coingecko.com/api/v3/simple/price?ids={string.Join(",", cryptos)}&vs_currencies={string.Join(",", currencies)}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(apiUrl);
                    Console.WriteLine("Données JSON récupérées : " + json);
                    JObject data = JObject.Parse(json);

                    List<RoundedPanel> createdPanels = new List<RoundedPanel>();

                    // Get the current date and time
                    string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    foreach (var crypto in cryptos)
                    {
                        if (data[crypto] != null)
                        {
                            Dictionary<string, string> currencyPrices = new Dictionary<string, string>();

                            foreach (var currency in currencies)
                            {
                                var price = data[crypto]?[currency]?.ToString();
                                if (price != null)
                                {
                                    currencyPrices[currency.ToUpper()] = price;
                                }
                            }

                            if (currencyPrices.Count > 0)
                            {
                                var panel = CreateCryptoPanel(crypto.ToUpper(), currencyPrices, currentTime); // Pass the current time to the panel creation
                                createdPanels.Add(panel);
                            }
                        }
                    }

                    panels = createdPanels.ToArray();
                    this.Controls.AddRange(panels);
                    Console.WriteLine($"{panels.Length} panneaux créés et ajoutés au formulaire.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Erreur lors de la récupération des données de l'API : " + ex.Message);
                }
            }
        }

        private RoundedPanel CreateCryptoPanel(string cryptoName, Dictionary<string, string> currencyPrices, string timestamp)
        {
            RoundedPanel panel = new RoundedPanel
            {
                BackColor = Color.FromArgb(34, 67, 153),
                BorderRadius = 90,
                Anchor = AnchorStyles.None
            };

            Label lblCrypto = new Label
            {
                Text = cryptoName,
                Font = new Font("Arial", 40, FontStyle.Bold),
                ForeColor = Color.FromArgb(128, 194, 236),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 70
            };
            panel.Controls.Add(lblCrypto);

            FlowLayoutPanel pricesContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoScroll = true,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Height = 70
            };

            foreach (var currencyPrice in currencyPrices)
            {
                Label lblCurrency = new Label
                {
                    Text = $"{currencyPrice.Key}:",
                    Font = new Font("Arial", 30, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = true,
                    Margin = new Padding(10, 0, 10, 0)
                };

                Label lblPrice = new Label
                {
                    Text = $"{currencyPrice.Value}",
                    Font = new Font("Arial", 30, FontStyle.Regular),
                    ForeColor = Color.FromArgb(207, 162, 0),
                    AutoSize = true,
                    Margin = new Padding(10, 0, 10, 0)
                };

                pricesContainer.Controls.Add(lblCurrency);
                pricesContainer.Controls.Add(lblPrice);
            }

            panel.Controls.Add(pricesContainer);

            Label lblTimestamp = new Label
            {
                Text = $"Last updated: {timestamp}",
                Font = new Font("Arial", 12, FontStyle.Italic),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 30
            };
            panel.Controls.Add(lblTimestamp);

            return panel;
        }

        private void AdjustPanelLayout()
        {
            int panelWidth = this.ClientSize.Width * 53 / 90;
            int buttonHeight = this.ClientSize.Height / 5;
            int panelHeight = buttonHeight;                 

            int buttonWidth = this.ClientSize.Width / 8;
            int buttonSpacing = this.ClientSize.Height / 20;

            int panelX = this.ClientSize.Width - buttonWidth - panelWidth - buttonSpacing;

            int visiblePanelsCount = Math.Min(panelsPerPage, panels.Length - visibleStartIndex); 
            int totalPanelHeight = visiblePanelsCount * panelHeight + (visiblePanelsCount - 1) * buttonSpacing;
            int topMargin = (this.ClientSize.Height - totalPanelHeight) / 2;

            // Parcourir les panneaux
            for (int i = 0; i < panels.Length; i++)
            {
                RoundedPanel panel = panels[i];

                if (i >= visibleStartIndex && i < visibleStartIndex + panelsPerPage)
                {
                    int visibleIndex = i - visibleStartIndex;
                    panel.Size = new Size(panelWidth, panelHeight);
                    panel.Location = new Point(panelX * 105 / 100, topMargin + visibleIndex * (panelHeight + buttonSpacing));
                    panel.Visible = true;
                }
                else
                {
                    panel.Visible = false;
                }

                if (panel.Controls.Count > 0 && panel.Controls[0] is Label lbl)
                {
                    Methode.CenterControlInParent(lbl);
                }
            }
        }


        private void BtnHaut_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex > 0)
            {
                selectedPanelIndex--;
                if (selectedPanelIndex < visibleStartIndex)
                {
                    visibleStartIndex--;
                }
                UpdatePanelSelection();
                AdjustPanelLayout();
            }
        }

        private void BtnBas_Click(object sender, EventArgs e)
        {
            if (selectedPanelIndex < panels.Length - 1)
            {
                selectedPanelIndex++;
                if (selectedPanelIndex >= visibleStartIndex + panelsPerPage)
                {
                    visibleStartIndex++;
                }
                UpdatePanelSelection();
                AdjustPanelLayout();
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Connexion form2 = new Connexion();
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

        private void BtnGauche_Click(object sender, EventArgs e) { }
        private void BtnDroite_Click(object sender, EventArgs e) { }
        private void BtnValider_Click(object sender, EventArgs e) { }

        private void UpdatePanelSelection()
        {
            foreach (var panel in panels)
            {
                panel.BackColor = Color.FromArgb(34, 67, 153); // Couleur par défaut
            }

            panels[selectedPanelIndex].BackColor = Color.FromArgb(50, 100, 200); // Couleur sélectionnée
        }
    }
}