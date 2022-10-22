using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text = TimeTextBlock.Text + " - Jeszcze raz?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐇","🐇",
                "🐄", "🐄",
                "🐈","🐈",
                "🐩","🐩",
                "🦔","🦔",
                "🐢","🐢",
                "🐳","🐳",
                "🦙","🦙",
            }; // tworzy liste ośmiu par emoji 
            Random random = new Random(); // tworzy generator liczb losowych
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) // znajduje wszystkie textBlock z głównej siatki i wykonuje dla każdej 
                //z nich podane instrukcje 
            {
                if (textBlock.Name != "TimeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count); // wybiera liczbe od 0 do liczby pozostłuch emoji na liście i nazywa tę wartość "index"
                    string nextEmoji = animalEmoji[index];  // używa losowej liczby "index" do pobrania emoji z listy
                    textBlock.Text = nextEmoji;   // przypisuje do kontrolki textBlock losowe emoji z listy
                    animalEmoji.RemoveAt(index); // usuwa emoji z listy 

                }
              
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false; // rejestruje czy kliknięto z emoji
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)                  // jeśli gracz kliknął pierwszą emoji to obrazek zostaje ukryty
            {
                textBlock.Visibility = Visibility.Hidden; // ukrywa 
                lastTextBlockClicked = textBlock;   // zapisuje obrazek na wypadek gdyby musiał ponownie się pojawić 
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)  // jeśli gracz dopasował obrazek to ukrywa obydwa i resetuje findingMatch by można było grać dalej
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
           
            else // jeśłi gracz nie dopasował obrazka to pierwszy kliknięty obrazek znowu się pojawia a findingMatch zostaje zresetowane
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)  // tu resetujemy gre gdy gracz odnajdzie wszystkie pary, inaczej gra szła by dalej
            {
                SetUpGame();
                
            }
        }
    }
}
