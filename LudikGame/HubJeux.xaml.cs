using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace LudikGame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class HubJeux : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        public HubJeux()
        {
            this.InitializeComponent();
        }

        private void calculs_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(Calculs));
        }

        private void formes_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(Formes));
        }

        private void carte_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(Carte));
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
