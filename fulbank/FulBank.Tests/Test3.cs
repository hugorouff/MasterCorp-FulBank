using Microsoft.VisualStudio.TestTools.UnitTesting;
using fulbank;
using System.Reflection;
using System.Windows.Forms;

namespace FulBank.Tests
{
    [TestClass]
    public class MenuFinalTransactionRetraitTests
    {
        [TestMethod]
        public void BtnValider_Click_ChampVideCompteSource_AfficheMessageErreur()
        {
            // Arrange
            var form = new MenuFinalTransactionRetrait();

            var txtCompteSource = GetPrivateTextBox(form, "txtCompteSource");
            var txtMontant = GetPrivateTextBox(form, "txtMontant");

            txtCompteSource.Text = ""; // Champ vide
            txtMontant.Text = "100";   // Montant valide

            // Simuler données statiques requises
            typeof(InfoDab).GetProperty("TauxDeChange").SetValue(null, "1");
            typeof(InfoDab).GetProperty("DabId").SetValue(null, "123");

            string message = null;
            MessageBoxManager3.Hook((text, caption) => { message = text; });

            // Act
            var method = typeof(MenuFinalTransactionRetrait).GetMethod("BtnValider_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(form, new object[] { null, null });

            // Assert
            Assert.AreEqual("Tous les champs sont obligatoires.", message);
        }

        private TextBox GetPrivateTextBox(MenuFinalTransactionRetrait form, string name)
        {
            var field = typeof(MenuFinalTransactionRetrait)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return (TextBox)field.GetValue(form);
        }
    }

    public static class MessageBoxManager3
    {
        private static System.Action<string, string> callback;

        public static void Hook(System.Action<string, string> messageBoxCallback)
        {
            callback = messageBoxCallback;
        }

        public static DialogResult Show(string text, string caption = "")
        {
            callback?.Invoke(text, caption);
            return DialogResult.OK;
        }
    }
}