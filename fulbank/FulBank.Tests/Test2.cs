using Microsoft.VisualStudio.TestTools.UnitTesting;
using fulbank;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace FulBank.Tests
{
    [TestClass]
    public class MenuFinalTransactionDepotTests
    {
        [TestMethod]
        public void BtnValider_Click_ChampVideCompte_AfficheMessageErreur()
        {
            // Arrange
            var form = new MenuFinalTransactionDepot();

            // Récupération des contrôles privés
            var txtCompte = GetPrivateTextBox(form, "txtCompte");
            var txtMontant = GetPrivateTextBox(form, "txtMontant");

            txtCompte.Text = ""; // Champ vide
            txtMontant.Text = "100"; // Montant valide

            // Mock des données statiques nécessaires
            typeof(InfoDab).GetProperty("TauxDeChange").SetValue(null, "1");
            typeof(InfoDab).GetProperty("DabId").SetValue(null, "1");

            // Capture du message d'erreur affiché
            string message = null;
            MessageBoxManager2.Hook((text, caption) =>
            {
                message = text;
            });

            // Act : appel de la méthode BtnValider_Click via réflexion
            MethodInfo method = typeof(MenuFinalTransactionDepot)
                .GetMethod("BtnValider_Click", BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(form, new object[] { null, null });

            // Assert : Vérifie le message attendu
            Assert.AreEqual("Tous les champs sont obligatoires.", message);
        }

        // Récupération des TextBox privées
        private TextBox GetPrivateTextBox(Form form, string name)
        {
            var field = typeof(MenuFinalTransactionDepot)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return (TextBox)field.GetValue(form);
        }
    }

    public static class MessageBoxManager2
    {
        private static Action<string, string> callback;

        public static void Hook(Action<string, string> messageBoxCallback)
        {
            callback = messageBoxCallback;
        }

        public static DialogResult Show(string text, string caption = "")
        {
            callback?.Invoke(text, caption);
            return DialogResult.OK;
        }
    }

    // Mock de InfoDab pour tests unitaires
    public static class InfoDab
    {
        public static string TauxDeChange { get; set; }
        public static string DabId { get; set; }
    }
}
