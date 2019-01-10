using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace LudikGame
{
    public sealed partial class Scores : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        StorageFolder localCacheFolder = ApplicationData.Current.LocalCacheFolder;
        String jeu = "";
        Score scorejeu = Score.GetInstance();
        public Scores()
        {
            this.InitializeComponent();
            calcul.DataContext = scorejeu;
            forme.DataContext = scorejeu;
            GetData();
        }
        public async void GetData()
        {
            StorageFile sampleFile;
            try
            {
                //on essaye de lire le fichier
               sampleFile = await localCacheFolder.GetFileAsync("dataFile.txt");
            }catch(Exception e)
            {
                //on créer le fichier
                sampleFile = await localCacheFolder.CreateFileAsync("dataFile.txt");
            }
            List<string> sc = new List<string>();
            sc.Add(scorejeu.Score_calcul.ToString());
            sc.Add(scorejeu.Score_forme.ToString());
            await FileIO.WriteLinesAsync(sampleFile, sc);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (Application.Current.Resources.ContainsKey("jeu"))
            {
                jeu = (String)Application.Current.Resources["jeu"];
                if(jeu == "Calcul")
                {
                    prov_jeu.Text = "Vous venez de quitter ou perdre le jeu de Calcul !";
                }
                else
                {
                    prov_jeu.Text = "Vous venez de quitter ou perdre le jeu de Forme !";
                }
            }
            Application.Current.Resources.Remove("jeu");
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
