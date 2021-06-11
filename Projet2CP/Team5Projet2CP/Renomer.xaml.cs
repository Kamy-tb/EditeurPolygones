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
using System.Windows.Shapes;

namespace Team5Projet2CP
{
    /// <summary>
    /// Logique d'interaction pour Renomer.xaml
    /// </summary>
    public partial class Renomer : Window
    {
        public bool OK { get; set; }
        public String nom { get; set; } = "" ;
        public Renomer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OK = false;
          
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NomTextBox.IsEnabled )
                    nom = NomTextBox.Text;
                if ( nom == "" )
                {
                    throw (new ArgumentException("Veillez entrer le nouveau nom d'abord"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            OK = true;
            Close();
        }



    }
}
