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
    /// Logique d'interaction pour ProprietesPolygon.xaml
    /// </summary>
    public partial class ProprietesPolygon : Window
    {
        public bool OK { get; set; }
        public bool ThicknessOnly { get; set; }
        public bool RadiusEnable { get; set; }
        public int Nbcote { get;  set; } = 4;
        public float R { get; private set; } = 50;
        public double S { get; private set; } = 0.5;
        public double X { get; set; } = 100;
        public double Y { get; set; } = 100;
        public String nom { get; set; } = " ";
        public SolidColorBrush ColorFill;
        public SolidColorBrush ColorOut;
        int i ;
        public ProprietesPolygon()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OK = false;
            
 
            if (ThicknessOnly)
            {
                NBCTextBox.IsEnabled = false;
                RTextBox.IsEnabled = false;
                CxTextBox.IsEnabled = false;
                CyTextBox.IsEnabled = false;
                NTextBox.IsEnabled=false;
                sTextBox.Focus();
            }
            else
            {
                NBCTextBox.Focus();
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NBCTextBox.IsEnabled)
                    Nbcote = int.Parse(NBCTextBox.Text);
                if (RTextBox.IsEnabled)
                    R = float.Parse(RTextBox.Text);
                if (sTextBox.IsEnabled)
                    S = double.Parse(sTextBox.Text);
                if (CxTextBox.IsEnabled)
                    X = double.Parse(CxTextBox.Text);
                if (CxTextBox.IsEnabled)
                    nom = NTextBox.Text;
                    if (CyTextBox.IsEnabled)
                    Y = double.Parse(CyTextBox.Text);
                if (Nbcote < 3 || R < 2 || S < 0)
                {
                    throw (new ArgumentException("Invalid input."));
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

        private void Mode_Plain(object sender, RoutedEventArgs e)
        {
            ColorPicker cpw = new ColorPicker();                //open Color picker window
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

        private void Mode_NonPlain(object sender, RoutedEventArgs e)
        {
            ColorFill = null ;
            ColorOut = Brushes.Black;
        }
    }
}
