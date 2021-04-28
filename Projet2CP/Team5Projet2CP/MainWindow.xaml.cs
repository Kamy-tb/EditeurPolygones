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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ProprietesPolygon dw;
        private double depx=0;
        private double depy=0;
        MyPolygon p;
        Path obj = new Path();
        SolidColorBrush F;
        SolidColorBrush S;
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            dw = new ProprietesPolygon();
            dw.Owner = Application.Current.MainWindow;      //DimensionWindow is Parent centered
            dw.ShowDialog();

            if (!dw.OK)                                     //if values not confirmed
            {
                return;
            }

            Rayon.Text = dw.R.ToString();
            button.Text = dw.Nbcote.ToString();
            if (dw.Nbcote > 50) { dw.Nbcote = 50;  }
            p = new MyPolygon(dw.R, dw.Nbcote, new Point(dw.X, dw.Y), dw.ColorFill,dw.ColorOut);
           
            p.CreerPolygon();
            if (dw.nom != " ")
            {
                ID.Text = dw.nom;

            }
            else
            ID.Text = p.SetName();
            F = dw.ColorFill;
            S = dw.ColorOut;
            obj = p.Draw(); 
            MyCanvas.Children.Add(obj);
            
        }



        private void Deplacer_click(object sender, RoutedEventArgs e)
        {
            depx = double.Parse(PositionX.Text);
            depy = double.Parse(PositionY.Text);
            p.Deplacer(depx, depy);
            MyCanvas.Children.Remove(obj);
            obj = p.Draw();
            MyCanvas.Children.Add(obj);
            PositionY.Text = "0";
            PositionX.Text = "0";
        }

        private void Rotation_Click(object sender, RoutedEventArgs e)
        {
            p.Rotation(double.Parse(Rotation.Text));
            MyCanvas.Children.Remove(obj);
            obj = p.Draw();
            MyCanvas.Children.Add(obj);
            Rotation.Text = "0";
        }
    }
}
