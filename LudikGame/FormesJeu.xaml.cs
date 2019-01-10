using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace LudikGame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FormesJeu : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        Score scorejeu = Score.GetInstance();
        //dictionnaire qui contient la colonne+1 de chaque forme
        Dictionary<String,int> dict = new Dictionary<String, int>();
        List<int> colonnes = new List<int>();
        Polygon polygon1 = new Polygon();
        Rectangle myRectangle = new Rectangle();
        Ellipse myCircle = new Ellipse();
        String quest = "Rectangle";
        Stopwatch watch = new Stopwatch();
        public FormesJeu()
        {
            this.InitializeComponent();
            Application.Current.Resources["jeu"] = "Forme";
            score.DataContext = scorejeu;
            initJeu();
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            Perdu();
        }

        private void rep3_Checked(object sender, RoutedEventArgs e)
        {
            Chrono();
            int column = dict[quest];
            rep3.IsChecked = false;
            if (column == 3)
            {
                scorejeu.ScoreActu += 1;
                choixAnimation();
                initJeu();
            }
            else
            {
                Perdu();
            }
        }

        private void rep2_Checked(object sender, RoutedEventArgs e)
        {
            Chrono();
            int column = dict[quest];
            rep2.IsChecked = false;
            if (column == 2)
            {
                scorejeu.ScoreActu += 1;
                choixAnimation();
                initJeu();
            }
            else
            {
                Perdu();
            }
        }

        private void rep1_Checked(object sender, RoutedEventArgs e)
        {
            Chrono();
            int column = dict[quest];
            rep1.IsChecked = false;
            if (column == 1)
            {
                scorejeu.ScoreActu += 1;
                choixAnimation();
                initJeu();
            }
            else
            {
                Perdu();
            }
        }
        public void create_Rectangle(int column)
        {
            myRectangle.Width = 100;
            myRectangle.Height = 50;
            myRectangle.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            Grid.SetColumn(myRectangle, column);
            Grid.SetRow(myRectangle, 2);
            LayoutRoot.Children.Add(myRectangle);
            dict.Add("Rectangle", column+1);
        }
        public void create_Circle(int column)
        {
            myCircle.Width = 100;
            myCircle.Height = 100;
            myCircle.Fill = new SolidColorBrush(Windows.UI.Colors.LightGray);
            Grid.SetColumn(myCircle, column);
            Grid.SetRow(myCircle, 2);
            LayoutRoot.Children.Add(myCircle);
            dict.Add("Cercle", column+1);
        }
        public void create_Triangle(int column)
        {
            polygon1.Fill = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            var points = new PointCollection();
            points.Add(new Windows.Foundation.Point(130, 0));
            points.Add(new Windows.Foundation.Point(100, 150));
            points.Add(new Windows.Foundation.Point(160, 150));
            polygon1.Points = points;
            Grid.SetColumn(polygon1, column);
            Grid.SetRow(polygon1, 2);
            LayoutRoot.Children.Add(polygon1);
            dict.Add("Triangle", column+1);
        }
        public void initJeu()
        {
            //en fonction du score on change la forme à trouver
            if (scorejeu.ScoreActu < 10)
            {
                quest = "Rectangle";
            }else if(scorejeu.ScoreActu >=10 && scorejeu.ScoreActu < 20)
            {
                quest = "Cercle";
            }else if (scorejeu.ScoreActu >= 20 && scorejeu.ScoreActu < 30)
            {
                quest = "Triangle";
            }
            else
            {
                if (scorejeu.ScoreActu % 2 == 0)
                {
                    quest = "Rectangle";
                }else if (scorejeu.ScoreActu % 5 == 0)
                {
                    quest = "Cercle";
                }else
                {
                    quest = "Triangle";
                }
            }
            //on écrit la question
            question.Text = "Quel est le " + quest + " ?";
            //on enlève les précédentes formes
            LayoutRoot.Children.Remove(myRectangle);
            LayoutRoot.Children.Remove(myCircle);
            LayoutRoot.Children.Remove(polygon1);
            //on remet le dictionnaire à 0
            dict = new Dictionary<string, int>();
            //on créer 3 colonnes qu'on mélange et on attribue aux formes
            colonnes.Add(0);
            colonnes.Add(1);
            colonnes.Add(2);
            colonnes = Shuffle(colonnes);
            create_Rectangle(colonnes[0]);
            create_Circle(colonnes[1]);
            create_Triangle(colonnes[2]);
            //on remet à 0 la liste des colonnes
            colonnes = new List<int>();
            watch.Reset();
            watch.Start();
        }

        public static List<int> Shuffle(List<int> list)
        {
            Random r = new Random();
            List<int> randomInts = new List<int>();

            while (list.Count > 0)
            {
                int i = r.Next(0,list.Count);
                randomInts.Add(list[i]);
                list.RemoveAt(i);
            }
            return randomInts;
        }
        public void Perdu()
        {
            if (scorejeu.Score_forme <= scorejeu.ScoreActu)
            {
                scorejeu.Score_forme = scorejeu.ScoreActu;
            }
            scorejeu.ScoreActu = 0;
            rootFrame.Navigate(typeof(Scores));
        }
        public void Chrono()
        {
            watch.Stop();
            if (watch.ElapsedMilliseconds > 2000)
            {
                Perdu();
            }
        }
        public void animationRectangle()
        {
            if (myRectangle.Opacity > 0.5)
            {
                myRectangle.Opacity -= 0.1;
            }
            else myRectangle.Opacity = 1.0;
        }
        public void animationTriangle()
        {
            if (polygon1.Opacity > 0.5)
            {
                polygon1.Opacity -= 0.1;
            }
            else polygon1.Opacity = 1.0;
        }
        public void animationCircle()
        {
            if (myCircle.Opacity > 0.5)
            {
                myCircle.Opacity -= 0.1;
            }
            else myCircle.Opacity = 1.0;
        }
        public void choixAnimation()
        {
            if(quest == "Rectangle")
            {
                animationRectangle();
            }else if (quest == "Cercle")
            {
                animationCircle();
            }else if(quest == "Triangle")
            {
                animationTriangle();
            }
        }

    }
}
