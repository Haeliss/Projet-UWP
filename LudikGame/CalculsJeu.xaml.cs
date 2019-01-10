using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace LudikGame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CalculsJeu : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        int c1, c2;
        String operation;
        Score scorejeu = Score.GetInstance();
        public CalculsJeu()
        {
            this.InitializeComponent();
            CreationCalcul();
            resultat.Focus(FocusState.Pointer);
            score.DataContext = scorejeu;
            Application.Current.Resources["jeu"] = "Calcul";
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            Perdu();
        }

        private void valider_Click(object sender, RoutedEventArgs e)
        {
            int res = 0;
            bool gagner = false;
            try
            {
                res = int.Parse(resultat.Text);
            }
            catch (FormatException ex) {
                Perdu();
            }catch(OverflowException oe)
            {
                Perdu();
            }
            switch (operation)
            {
                case "+":
                    if ((c1 + c2) == res) { gagner = true; }
                        break;
                case "-":
                    if ((c1 - c2) == res) { gagner = true; }
                    break;
                case "*":
                    if ((c1 * c2) == res) { gagner = true; }
                    break;
                case "/":
                    if ((c1 / c2) == res) { gagner = true; }
                    break;
            }
            
            if (gagner)
            {
                scorejeu.ScoreActu += 1;
                resultat.Text = "";
                CreationCalcul();
                resultat.Focus(FocusState.Pointer);
            }
            else
            {
                Perdu();
            }
        }
        public void CreationCalcul()
        {
            Random aleatoire = new Random();
            //on créer les deux nombres du calcul de manière aléatoire
            c1 = aleatoire.Next(1, 20);
            c2 = aleatoire.Next(1, 20);
            //on défini le calcul en fonction de score : de plus en plus difficile
            if (scorejeu.ScoreActu < 20)
            {
                operation = "+";
            }
            else if (scorejeu.ScoreActu > 20 && scorejeu.ScoreActu < 40)
            {
                operation = "-";
            }
            else if (scorejeu.ScoreActu > 40 && scorejeu.ScoreActu < 60)
            {
                operation = "*";
            }
            else {
                if(scorejeu.ScoreActu % 2 == 0)
                {
                    operation = "+";
                }
                else if(scorejeu.ScoreActu % 5 == 0)
                {
                    operation = "*";
                }
                else
                {
                    operation = "-";
                }
            }
            //on écrit la question
            question.Text = "Quel est le résultat à ce calcul : " + c1 + operation + c2;
        }
        public void Perdu()
        {
            //on vérifie si le score actuel est un meilleur score, si oui on l'ajoute aux scores.
            if (scorejeu.Score_calcul <= scorejeu.ScoreActu)
            {
                scorejeu.Score_calcul = scorejeu.ScoreActu;
            }
            scorejeu.ScoreActu = 0;
            rootFrame.Navigate(typeof(Scores));
        }
    }
}
