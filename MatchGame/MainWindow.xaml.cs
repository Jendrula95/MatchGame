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

namespace MatchGame
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
                int index = random.Next(animalEmoji.Count); // wybiera liczbe od 0 do liczby pozostłuch emoji na liście i nazywa tę wartość "index"
                string nextEmoji = animalEmoji[index];  // używa losowej liczby "index" do pobrania emoji z listy
                textBlock.Text = nextEmoji;   // przypisuje do kontrolki textBlock losowe emoji z listy
                animalEmoji.RemoveAt(index); // usuwa emoji z listy 
            }
        }
    }
}
