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

        /** initialisation des objets de nos classes **/
        Environnement MyEnv = new Environnement();  // Objet de notre environnement
        MyPolygon p, SelectedMyPolygon = null;
        Path obj = new Path();
        Path SelectedPolygon;
        Point MousePosition; 
        private ProprietesPolygon dw;
        private double depx = 0, depy = 0; // Pour le deplacement 

        SolidColorBrush F, S; // Pour les couleurs

        /****************** Variable de rotation par souris *****************************/
        RotateTransform TestRotate;
        double x, y; double theta = 0;
        Boolean _Rotate = false; Boolean clean = false;
        /*****************************  Variables de selection ***************************/
        DoubleCollection dbl;
        int Thik;
        int index;
        /*********************************************************************************/


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
            if (dw.Nbcote > 50) { dw.Nbcote = 50; }
            p = new MyPolygon(dw.R, dw.Nbcote, new Point(dw.X, dw.Y), dw.ColorFill, dw.ColorOut);

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
            MyEnv.SetEnv(p, obj);                   //Ajouter a l'environnement
        }
        
        private void Selection(object sender, MouseButtonEventArgs e)
        {

            if (SelectedPolygon != null)
            {
                dbl = null;
                Thik = 1;
                SelectedPolygon.StrokeThickness = Thik;
                SelectedPolygon.StrokeDashArray = dbl;
            }
            if (e.OriginalSource is Path)
            {
                dbl = new DoubleCollection() { 2 };
                SelectedPolygon = (Path)e.OriginalSource;
                index = MyEnv.Recherche(SelectedPolygon);
                SelectedMyPolygon = MyEnv.GetMyPolygon(index);
                Thik = 3;
                SelectedPolygon.StrokeThickness = Thik;
                SelectedPolygon.StrokeDashArray = dbl;
                SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;


            }
            else
            {
                SelectedPolygon = null;
            }
        }

        /****************************************************   Pour choisir Deplacement ou rotation   *************************************************/
        Boolean mov;
        private void obj_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;

            MenuItem Depl = new MenuItem() { Header = "Deplacement" };
            MenuItem Rotation = new MenuItem() { Header = "rotation" };
            Depl.Click += mov_butt;
            Rotation.Click += rotate_butt;
            cm.Items.Add(Depl);
            cm.Items.Add(Rotation);

            cm.IsOpen = true;
        }
        private void mov_butt(object sender, RoutedEventArgs e)
        {
            mov = true;
            _Rotate = false;
        }
        private void rotate_butt(object sender, RoutedEventArgs e)
        {
            _Rotate = true;
            mov = false;
        }
        /***********************************************************************************************************************************************/



        /******************************************* Le deplacement et rotation (par souris et numerique ) *****************************************/
        Boolean drag = false;
        private void obj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            drag = true;
            x = e.GetPosition(SelectedPolygon).X;                                           //get click coordinates within shape (relative to top left corner)
            y = e.GetPosition(SelectedPolygon).Y;
            MousePosition = e.GetPosition(MyCanvas); 

        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                if (SelectedPolygon == null) { return; }

                if (mov)
                {
                    Mouse.SetCursor(Cursors.Hand);
                    Canvas.SetLeft(SelectedPolygon, e.GetPosition(MyCanvas).X - x);                             //when placing Shape on Canvas, mouse position within Shape must be kept (x, y)
                    Canvas.SetTop(SelectedPolygon, e.GetPosition(MyCanvas).Y - y);
                    Path path = SelectedPolygon;
                    Geometry geometry = path.Data;
                    clean = true;
                }
                if (_Rotate)
                {
                    Mouse.SetCursor(Cursors.ScrollAll);
                    if (index != -1)
                    {
                        SelectedMyPolygon = MyEnv.GetMyPolygon(index);
                        x = e.GetPosition(MyCanvas).X - SelectedMyPolygon.GetCentre().X;
                        y = e.GetPosition(MyCanvas).Y - SelectedMyPolygon.GetCentre().Y;
                        theta = Math.Atan2(y, x) * 180 / Math.PI;
                        TestRotate = new RotateTransform(theta, SelectedMyPolygon.GetCentre().X, SelectedMyPolygon.GetCentre().Y);
                        SelectedPolygon.RenderTransform = TestRotate;
                        clean = true;
                    }
                    else { MessageBox.Show(" ERREUR "); }

                }

            }




        }

        private void MyCanvas_MouseLeftButtonUp (object sender, MouseButtonEventArgs e)
        {

            if (drag)
            {
                if (mov)
                {
                    if (clean)
                    {
                        RecalculateGeometryBounds();
                        SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;
                    }

                }
                if (_Rotate)
                {
                    if (clean)
                    {
                        MyCanvas.Children.Remove(SelectedPolygon);
                        SelectedMyPolygon.Rotation(theta);
                        SelectedPolygon = SelectedMyPolygon.Draw();
                        MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                        MyCanvas.Children.Add(SelectedPolygon);
                        SelectedPolygon = null;
                        SelectedMyPolygon = null;
                    }
                    
                }
                clean = false;
                drag = false;
            }

        }

        public void RecalculateGeometryBounds()                  
        {
            Geometry geometry = null;

            List<Point> pnt_list3 = new List<Point>();

            Path path = SelectedPolygon;
            geometry = path.Data; //as Geometry;

            int index = MyEnv.Recherche(path);
            if (index != -1)
            {

                if (geometry is PathGeometry)
                {
                    double deltaX = Canvas.GetLeft(SelectedPolygon);
                    double deltaY = Canvas.GetTop(SelectedPolygon);
                    SelectedMyPolygon.Deplacer(deltaX, deltaY);
                    pnt_list3 = SelectedMyPolygon.SetPoints();
                    PathGeometry pathGeometry = geometry as PathGeometry;
                    PathFigure figure = pathGeometry.Figures[0];
                    PathFigure newFigure = new PathFigure();
                    int index2 = 0;
                    foreach (Point point in pnt_list3)
                    {
                        if (index2 == 0)                                 //define first point of polyline as startpoint
                            newFigure.StartPoint = point;
                        else
                            newFigure.Segments.Add(new LineSegment((point), true));
                        index2++;
                    }
                    newFigure.Segments.Add(new LineSegment((pnt_list3[0]), true));
                    pathGeometry.Figures.Clear();
                    pathGeometry.Figures.Add(newFigure);
                    path.Data = pathGeometry;
                    MyEnv.SetChamp(index, SelectedMyPolygon, path);
                    Canvas.SetLeft(SelectedPolygon, 0);
                    Canvas.SetTop(SelectedPolygon, 0);


                }
            }
        }

        private void Deplacer_click(object sender, RoutedEventArgs e)
        {
            if (index != -1)
            {
                depx = double.Parse(PositionX.Text);
                depy = double.Parse(PositionY.Text);
                SelectedMyPolygon.Deplacer(depx, depy);
                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer le path precedent 
                SelectedPolygon = SelectedMyPolygon.Draw();
                SelectedPolygon.StrokeThickness = Thik;
                SelectedPolygon.StrokeDashArray = dbl;
                MyCanvas.Children.Add(SelectedPolygon); // ajouter le nouveau 
                SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;
                PositionY.Text = "0"; PositionX.Text = "0";

                // MyPolgon[index] et Path[index] ont été modifié faut mettre a jour dans notre environnement :
                MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
                return;
            }

        }

        private void Rotation_Click(object sender, RoutedEventArgs e)
        {
            if (index != -1)
            {
                SelectedMyPolygon.Rotation(double.Parse(Rotation.Text));
                MyCanvas.Children.Remove(SelectedPolygon);
                SelectedPolygon = SelectedMyPolygon.Draw();
                SelectedPolygon.StrokeThickness = Thik;
                SelectedPolygon.StrokeDashArray = dbl;
                MyCanvas.Children.Add(SelectedPolygon);
                SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;
                Rotation.Text = "0";

                // MyPolgon[index] et Path[index] ont été modifié faut les mettre a jour dans notre environnement :
                MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
                return;
            }
        }
        /*********************************************************************************************************************************************/

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolygon != null)
            {
                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                MyEnv.Supprimer(SelectedPolygon);  // Supprimer de l'environnement 
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }

        private void Copier_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolygon != null)
            {
                MyEnv.ElementCopier.obj = SelectedPolygon;
                MyEnv.ElementCopier.p = MyEnv.GetMyPolygon(MyEnv.Recherche(SelectedPolygon));
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }

        private void Coller_Click(object sender, RoutedEventArgs e)
        {
        /*    if (MyEnv.ElementCopier.obj != null)
            {
                //   MyPolygon MyPolygone = MyEnv.ElementCopier.p;
                //    MyPolygone.CreerPolygon();
                //    obj = MyPolygone.Draw();
              
                //    MyCanvas.Children.Add(MyEnv.ElementCopier.obj);
              //  MyEnv.SetEnv(MyEnv.ElementCopier.p ,obj); 
            } */
       //     else
        //    { 
                MessageBox.Show("Rien a coller");
    //        }
        }
        
    }



}