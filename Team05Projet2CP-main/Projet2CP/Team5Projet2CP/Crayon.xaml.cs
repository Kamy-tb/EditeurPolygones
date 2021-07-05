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

namespace Team5Projet2CP
{
    /// <summary>
    /// Logique d'interaction pour Crayon.xaml
    /// </summary>
    public partial class Crayon : Page
    {
        public Crayon()
        {
            InitializeComponent();
        }


        private void Accueil_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(null);




        }

        private void apropos_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame5.Navigate(new aPropos());
        }

        private void _mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }


}
