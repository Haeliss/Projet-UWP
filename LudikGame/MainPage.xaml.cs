using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LudikGame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        StorageFolder localCacheFolder = ApplicationData.Current.LocalCacheFolder;
        Score scorejeu = Score.GetInstance();
        public MainPage()
        {
            this.InitializeComponent();
            GetData();
        }
        public async void GetData()
        {
            try
            {
                //on essaye de lire le fichier
                StorageFile sampleFile = await localCacheFolder.GetFileAsync("dataFile.txt");
                if (sampleFile != null)
                {
                    var readFile = await FileIO.ReadLinesAsync(sampleFile);

                    scorejeu.Score_calcul = int.Parse(readFile[0]);
                    scorejeu.Score_forme = int.Parse(readFile[1]);
                }
            }
            catch (Exception e)
            {
            }
        }
        private void jouer_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(HubJeux));
        }

        private void scores_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(Scores));
        }
    }
}
