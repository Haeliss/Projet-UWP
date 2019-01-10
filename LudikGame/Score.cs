using System;
using System.ComponentModel;

namespace LudikGame
{
    class Score : INotifyPropertyChanged 
    {
        private int score_calcul;
        private int score_forme;
        private static Score uniqueInstance;
        private String scoreString;
        private String calculString;
        private String formeString;
        private int scoreActu;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


            public Score()
        {
            this.score_calcul = 0;
            this.score_forme = 0;
            this.scoreActu = 0;
        }

        public int Score_calcul
        {
            get => score_calcul;
            set {
                score_calcul = value;
                CalculString = "Calculs : " + value;
                this.RaisePropertyChanged("Score_calcul");
            }
        }
        public int Score_forme
        {
            get => score_forme;
            set
            {
                score_forme = value;
                FormeString = "Formes : " + value;
                this.RaisePropertyChanged("Score_forme");

            }
        }
        public string ScoreString
        {
            get => "Score : " + scoreActu;
            set
            {
                scoreString = value;
                this.RaisePropertyChanged("ScoreString");
            }
        }
        public string CalculString {
            get => "Calculs : "+score_calcul;
            set
            {
                calculString = value;
                this.RaisePropertyChanged("CalculString");
            }
        }
        public string FormeString {
            get => "Formes : "+score_forme;
            set
            {
                formeString = value;
                this.RaisePropertyChanged("FormeString");
            }
        }

        public int ScoreActu
        {
            get => scoreActu;
            set
            {
                scoreActu = value;
                ScoreString = "Score : " + value;
                this.RaisePropertyChanged("ScoreActu");
            }
        }

        public static Score GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Score();

            }
            return uniqueInstance;
        }
    }
}
