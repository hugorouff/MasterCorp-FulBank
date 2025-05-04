using Microsoft.VisualStudio.TestTools.UnitTesting;
using fulbank;
using System.Reflection;
using System.Windows.Forms;
using System;
using MethodInvoker = System.Windows.Forms.MethodInvoker;
using WinFormsApp = System.Windows.Forms.Application;

namespace FulBank.Tests
{
    [TestClass]
    public class MenuFinalTransactionChangeMonnaieTests
    {
        [TestMethod]
        public void BtnValider_Click_ChampVideCompteSource_AfficheMessageErreur()
        {
            // Arrange
            var form = new MenuFinalTransactionChangeMonnaie();

            // Récupération des contrôles privés du formulaire
            var txtCompteSource = GetPrivateTextBox(form, "txtCompteSource");
            var txtCompteDestination = GetPrivateTextBox(form, "txtCompteDestination");
            var txtMontant = GetPrivateTextBox(form, "txtMontant");

            // Simulation de l'état initial des champs
            txtCompteSource.Text = ""; // Champ vide
            txtCompteDestination.Text = "12345"; // Compte valide
            txtMontant.Text = "100"; // Montant valide

            // Capture du message de la boîte de message
            string message = null;
            MessageBoxManager.Hook((text, caption) =>
            {
                message = text; // Capture du message
            });

            // Act: Appel de la méthode BtnValider_Click via réflexion
            MethodInfo method = typeof(MenuFinalTransactionChangeMonnaie)
                .GetMethod("BtnValider_Click", BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(form, new object[] { null, null });

            // Assert: Vérification que le message d'erreur correct est affiché
            Assert.AreEqual("Veuillez entrer un numéro de compte source valide.", message);
        }

        // Méthode pour accéder aux contrôles privés (TextBox dans ce cas) du formulaire
        private TextBox GetPrivateTextBox(MenuFinalTransactionChangeMonnaie form, string name)
        {
            var field = typeof(MenuFinalTransactionChangeMonnaie)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return (TextBox)field.GetValue(form);
        }
    }

    // Classe pour intercepter les MessageBox affichés et les rediriger vers un gestionnaire
    public static class MessageBoxManager
    {
        private static Action<string, string> callback;

        // Méthode pour installer un gestionnaire pour intercepter MessageBox
        public static void Hook(Action<string, string> messageBoxCallback)
        {
            callback = messageBoxCallback;
        }

        // Méthode personnalisée Show pour capturer et manipuler les MessageBox
        public static DialogResult Show(string text, string caption = "")
        {
            callback?.Invoke(text, caption); // Appel du callback avec le texte et le titre
            return DialogResult.OK; // Retourne OK pour simuler un clic sur le bouton OK
        }
    }
}