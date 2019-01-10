using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace LudikGame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Carte : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        Library library = new Library();
        public Carte()
        {
            this.InitializeComponent();
            Localisation();

        }
        private async void Localisation()
        {
            Geopoint point = null;
            try
            {
                point = await library.Position();
            }catch(UnauthorizedAccessException e)
            {

            }
            if (point != null)
            {
                erreur.Text = "";
                DependencyObject marker = library.Marker();
                Display.Children.Add(marker);
                Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(marker, point);
                Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(marker, new Point(0.5, 0.5));
                Display.ZoomLevel = 12;
                Display.Center = point;
            }
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(HubJeux));
        }
    }
}
