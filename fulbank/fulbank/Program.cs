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
}