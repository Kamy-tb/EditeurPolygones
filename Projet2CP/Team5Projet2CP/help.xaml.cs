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
using System.Configuration;
using System.Net.Mail;
using System.Net;


namespace Team5Projet2CP
{
    /// <summary>
    /// Logique d'interaction pour help.xaml
    /// </summary>
    public partial class help : Window
    {
        public help()
        {
            InitializeComponent();
        }

        private void OperationBooléennes_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new OpérationsBooléennes());

        }

        private void Creation_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new PrésetationLog());
        }


        private void JeuDeContruction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnregistrementEtExportation_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Crayon_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new Crayon());
        }

        private void creationAndEdition_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new CreationEtEdition());
        }

        private void JEnregistrementEtExportation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContactUs_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private void Accueil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void apropos_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new aPropos());
        }
    }
}
