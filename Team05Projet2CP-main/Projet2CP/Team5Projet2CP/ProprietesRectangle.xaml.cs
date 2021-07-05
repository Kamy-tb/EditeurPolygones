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
    /// Logique d'interaction pour ProprietesRectangle.xaml
    /// </summary>
    public partial class ProprietesRectangle : Window
    {
        public bool OK { get; set; }
        public bool ThicknessOnly { get; set; }
        public float width { get; private set; } = 120;
        public float hight { get; private set; } = 80;
        public double X { get; set; } = 450;
        public double Y { get; set; } = 250;
        public String nom { get; set; } = "";
        public SolidColorBrush ColorFill = Brushes.White ;
        public SolidColorBrush ColorOut = Brushes.Black;
      
        public ProprietesRectangle()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OK = false;
            if (ThicknessOnly)
            {
                wTextBox.IsEnabled = false;
                hTextBox.IsEnabled = false;
                CxTextBox.IsEnabled = false;
                CyTextBox.IsEnabled = false;
                NTextBox.IsEnabled = false;
                hTextBox.Focus();
            }
            else
            {
                wTextBox.Focus();
            }
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (wTextBox.IsEnabled)
                    width = float.Parse(wTextBox.Text);
                if (hTextBox.IsEnabled)
                    hight = float.Parse(hTextBox.Text);
                if (CxTextBox.IsEnabled)
                    X = double.Parse(CxTextBox.Text);
                if (CyTextBox.IsEnabled)
                    Y = double.Parse(CyTextBox.Text);
                if (NTextBox.IsEnabled)
                    nom = NTextBox.Text;
                if (width < 3 || hight < 2)
                {
                    throw (new ArgumentException("Invalid"));
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
        private void Mode_ChangeClr(object sender, RoutedEventArgs e)
        {
            ColorPicker cpw = new ColorPicker();                //ouvrir Color picker window
            cpw.Owner = this;
            cpw.SelectedFillBrush = Brushes.White;                //restore last selection
            cpw.SelectedOutBrush = Brushes.Black;
            cpw.ShowDialog();
            if (cpw.OK)
            {
                ColorFill = cpw.SelectedFillBrush;
                ColorOut = cpw.SelectedOutBrush;
            }
        }
        private void Mode_ParDefault(object sender, RoutedEventArgs e)
        {
            ColorFill = Brushes.White;
            ColorOut = Brushes.Black;
        }
    }

}

