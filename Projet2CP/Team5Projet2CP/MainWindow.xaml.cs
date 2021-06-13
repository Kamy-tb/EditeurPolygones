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
        Boolean RuleVisible;
        public MainWindow()
        {
            InitializeComponent();
            RuleVisible = false;
            verticalRuler.Visibility = Visibility.Hidden;
            horizontalRuler.Visibility = Visibility.Hidden;
            KeyDown += MyCanvas_KeyDown;
        }
        Boolean colle = false;
        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Back)
            {
                if (SelectedPolygon != null)
                {
                    MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                    MyEnv.Supprimer(SelectedPolygon);  // Supprimer de l'environnement 
                    SelectedPolygon = null;
                    SelectedMyPolygon = null;
                    ID.Text = "";
                    Rayon.Text = "";
                    nbcot.Text = "";
                    centreX.Text = "";
                    centreY.Text = "";
                }
                else
                {
                    MessageBox.Show("Selectionnée d'abord un element ");
                }
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl)
            {
                switch (e.Key)
                {
                    case Key.X:
                        {
                            int i = 0;
                            if (SelectedPolygon != null)
                            {
                                // copier
                                MyEnv.ElementCopier.obj = SelectedPolygon;
                                i = MyEnv.Recherche(SelectedPolygon);
                                if (i != -1) { MyEnv.ElementCopier.p = MyEnv.GetMyPolygon(i); }
                                else MessageBox.Show("Selectionnée d'abord un element ");
                                // supprimer
                                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                                MyEnv.Supprimer(SelectedPolygon);  // Supprimer de l'environnement 
                                
                            }
                            else
                            {
                                MessageBox.Show("Selectionnée d'abord un element ");
                            }

                        }
                        break;
                    case Key.C:
                        {
                            int i = 0;
                            if (SelectedPolygon != null)
                            {
                                MyEnv.ElementCopier.obj = SelectedPolygon;
                                i = MyEnv.Recherche(SelectedPolygon);
                                if (i != -1) { MyEnv.ElementCopier.p = MyEnv.GetMyPolygon(i); }
                                else MessageBox.Show("Selectionnée d'abord un element ");
                            }
                            else
                            {
                                MessageBox.Show("Selectionnée d'abord un element ");
                            }
                        }
                        break;
                    case Key.V:
                        {
                            
                            MyPolygon c;
                            if (MyEnv.ElementCopier.obj != null)
                            {
                                MyPolygon a = MyEnv.ElementCopier.p;
                                c = new MyPolygon(a.GetPoints(), a.GetFill(), a.GetStroke());
                                c.SetCentre(a.GetCentre()); c.rayon = a.rayon;
                                c.Deplacer(colx - a.GetCentre().X, coly - a.GetCentre().Y);
                                obj = c.Draw();
                                MyCanvas.Children.Add(obj);
                                MyEnv.AddToEnv(c, obj);
                            }
                            else
                            {
                                MessageBox.Show("Rien a coller");
                            }
                            colle = false;



                        }
                        break;
                    case Key.Z:
                        {
                            try
                            {
                                MyEnv.Back();
                                MyCanvas.Children.RemoveRange(0, MyCanvas.Children.Count);
                                foreach (var item in MyEnv.Env)
                                {
                                    MyCanvas.Children.Add(item.obj);
                                }
                            }

                            catch (System.ArgumentException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                    case Key.Y:
                        {
                            MyEnv.After();
                            MyCanvas.Children.RemoveRange(0, MyCanvas.Children.Count);
                            foreach (var item in MyEnv.Env)
                            {
                                MyCanvas.Children.Add(item.obj);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        private void test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        int cpt = 1;
        private void Add(object sender, RoutedEventArgs e)
        {
            ListBoxItem itm = new ListBoxItem();
            itm.Content = "new" + cpt.ToString(); cpt++;
            test.Items.Add(itm);
        }

        private void remove(object sender, RoutedEventArgs e)
        {
            ListBoxItem itm = new ListBoxItem();
            test.Items.Remove(test.SelectedItem);
        }


        // initialisation des objets de nos classes 
        Environnement MyEnv = new Environnement();  // Objet de notre environnement
        MyPolygon p, SelectedMyPolygon = null;
        Path obj = new Path();
        Path SelectedPolygon;
        Point MousePosition; 
        private ProprietesPolygon dw;
        private double depx = 0, depy = 0; // Pour le deplacement 

        SolidColorBrush F, S; // Pour les couleurs

        //********************************* Variable de rotation par souris ************************************
        RotateTransform TestRotate;
        double x, y; double theta = 0;
        Boolean _Rotate = false; Boolean clean = false;
        //********************************* Variable de zoom par souris ************************************
        ScaleTransform TestScale;
        double zoomFactor;
        Boolean _zoom = false;
        //****************************************  Variables de selection *************************************
        DoubleCollection dbl;
        int Thik;
        int index;
        //************************** SELECTIONNER DEUX POLYGONES POUR LES OPERATIONS*****************************
        List<Element> store = new List<Element>();
        //*******************************************************************************************************
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            dw = new ProprietesPolygon();
            dw.Owner = Application.Current.MainWindow;      //DimensionWindow is Parent centered
            dw.ShowDialog();

            if (!dw.OK)                                     //if values not confirmed
            {
                return;
            }
            if (dw.Nbcote > 50) { dw.Nbcote = 50; }
            p = new MyPolygon(dw.R, dw.Nbcote, new Point(dw.X, dw.Y), dw.ColorFill, dw.ColorOut);

            p.CreerPolygon();
            if (dw.nom != "")
            {
                p.SetName(dw.nom); 
            }
            F = dw.ColorFill;
            S = dw.ColorOut;
            obj = p.Draw();
            MyCanvas.Children.Add(obj); 
            MyEnv.AddToEnv(p, obj);                   //Ajouter a l'environnement
        }
        private void DrawRect_Click(object sender, RoutedEventArgs e)
        {
            ProprietesRectangle dw = new ProprietesRectangle();
            dw.Owner = Application.Current.MainWindow;      //DimensionWindow is Parent centered
            dw.ShowDialog();

            if (!dw.OK)                                     //if values not confirmed
            {
                return;
            }
            MyRectangle Rec = new MyRectangle(dw.hight, dw.width, new Point(dw.X, dw.Y), dw.ColorFill, dw.ColorOut);
            Rec.CreerRectangle();
            if (dw.nom != "")
            {
                Rec.SetName(dw.nom);
            }
            F = dw.ColorFill;
            S = dw.ColorOut;
            obj = Rec.Draw();
            MyCanvas.Children.Add(obj);
            MyEnv.AddToEnv(Rec, obj);                   //Ajouter a l'environnement
        }
        private void AfficherPropriete(MyPolygon p)
        {
            ID.Text = p.GetName();
            if (p.rayon != 0) { Rayon.Text = p.rayon.ToString(); }
            nbcot.Text = p.GetPoints().Count.ToString();
            centreX.Text = p.GetCentre().X.ToString();
            centreY.Text = p.GetCentre().Y.ToString();

            colorfill.Text = SelectedPolygon.Fill.ToString();
            colorborder.Text = SelectedPolygon.Stroke.ToString();
            RecFond.Fill = SelectedPolygon.Fill;
            RecContour.Fill = SelectedPolygon.Stroke;
        }
        double colx;
        double coly;
        private void Selection(object sender, MouseButtonEventArgs e)
        {
            colx = e.GetPosition(MyCanvas).X;
            coly = e.GetPosition(MyCanvas).Y;

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
                    if (index != -1) { SelectedMyPolygon = MyEnv.GetMyPolygon(index); }
                    Thik = 3;
                    SelectedPolygon.StrokeThickness = Thik;
                    SelectedPolygon.StrokeDashArray = dbl;
                    // Afficher dans propriete a droite : 
                    AfficherPropriete(SelectedMyPolygon);

                    SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                    SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;


                }
                else
                {
                    ID.Text = "";
                    Rayon.Text = "";
                    nbcot.Text = "";
                    centreX.Text = "";
                    centreY.Text = "";
                    SelectedPolygon = null;
                }
            
        }

        //****************************************************   Pour choisir Deplacement ou rotation   *************************************************
        Boolean mov = true ;
        private void obj_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;

            MenuItem Depl = new MenuItem() { Header = "Deplacement" };
            MenuItem Rotation = new MenuItem() { Header = "rotation" };
            MenuItem Zoom = new MenuItem() { Header = "zoom" };
            MenuItem nom = new MenuItem() { Header = "Renomer" };
            Depl.Click += mov_butt;
            Rotation.Click += rotate_butt;
            Zoom.Click += Zoom_Click;
            nom.Click += Nom_Click;
            cm.Items.Add(Depl);
            cm.Items.Add(Rotation);
            cm.Items.Add(Zoom); 
            cm.Items.Add(nom);
            cm.IsOpen = true;
        }

        private void Nom_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMyPolygon != null)
            {
                Renomer nw = new Renomer();                //ouvrir renomer window
                nw.Owner = this;
                nw.ShowDialog();
                if (nw.OK) SelectedMyPolygon.SetName(nw.nom);
                ID.Text = SelectedMyPolygon.GetName();
            }
                
        }

        private void mov_butt(object sender, RoutedEventArgs e)
        {
            mov = true;
            _Rotate = false;
            _zoom = false;
        }
        private void rotate_butt(object sender, RoutedEventArgs e)
        {
            _Rotate = true;
            mov = false;
            _zoom = false;
        }
        private void Zoom_Click(object sender, RoutedEventArgs e)
        {
            _zoom = true;
            mov = false;
            _Rotate = false;
        }

        //***********************************************************************************************************************************************



        //******************************************* Le deplacement et rotation (par souris et numerique ) *****************************************
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
                Mouse.SetCursor(Cursors.Hand);
                if (mov)
                {
                    Canvas.SetLeft(SelectedPolygon, e.GetPosition(MyCanvas).X - x);                             //when placing Shape on Canvas, mouse position within Shape must be kept (x, y)
                    Canvas.SetTop(SelectedPolygon, e.GetPosition(MyCanvas).Y - y);
                    Path path = SelectedPolygon;
                    Geometry geometry = path.Data;
                    clean = true;
                }
                if (_Rotate)
                {
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
                    else { MessageBox.Show("Un probleme a été rencontré"); return;  }
                }
                if (_zoom)
                {
                    if (index != -1)
                    {
                        SelectedMyPolygon = MyEnv.GetMyPolygon(index);
                        if (SelectedMyPolygon.rayon == 0) zoomFactor = (e.GetPosition(MyCanvas).X - SelectedMyPolygon.GetCentre().X) / (SelectedMyPolygon.GetPoints()[0].X - SelectedMyPolygon.GetCentre().X);
                        else zoomFactor = (e.GetPosition(MyCanvas).X - SelectedMyPolygon.GetCentre().X) / SelectedMyPolygon.rayon;

                        TestScale = new ScaleTransform(zoomFactor, zoomFactor, SelectedMyPolygon.GetCentre().X, SelectedMyPolygon.GetCentre().Y);
                        SelectedPolygon.RenderTransform = TestScale;
                        clean = true;
                    }
                    else { MessageBox.Show("Un probleme a été rencontré"); return; }
                }
            }
            horizontalRuler.RaiseHorizontalRulerMoveEvent(e);
            verticalRuler.RaiseVerticalRulerMoveEvent(e);
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
                if (_zoom)
                {
                    if (clean)
                    {
                        MyCanvas.Children.Remove(SelectedPolygon);
                        List<Point> newCoord = new List<Point>();
                        foreach (Point p in SelectedMyPolygon.GetPoints())
                        {
                            x = zoomFactor * (p.X - SelectedMyPolygon.GetCentre().X) + SelectedMyPolygon.GetCentre().X;
                            y = zoomFactor * (p.Y - SelectedMyPolygon.GetCentre().Y) + SelectedMyPolygon.GetCentre().Y;
                            newCoord.Add(new Point(x, y));
                        }
                        SelectedMyPolygon.SetPoints(newCoord);
                        SelectedMyPolygon.rayon = Math.Abs(zoomFactor) * SelectedMyPolygon.rayon;
                        SelectedPolygon = SelectedMyPolygon.Draw();
                        MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                        MyCanvas.Children.Add(SelectedPolygon);
                        SelectedPolygon = null;
                        SelectedMyPolygon = null;
                        _zoom = false;
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
                if (path != null)
                {
                    geometry = path.Data;

                    int index = MyEnv.Recherche(path);
                    if (index != -1)
                    {

                        if (geometry is PathGeometry)
                        {
                            double deltaX = Canvas.GetLeft(SelectedPolygon);
                            double deltaY = Canvas.GetTop(SelectedPolygon);
                            SelectedMyPolygon.Deplacer(deltaX, deltaY);
                            pnt_list3 = SelectedMyPolygon.GetPoints();
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
                
                
          
        }
        private void Deplacer_click(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                try
                {
                    depx = double.Parse(positionX.Text);
                    depy = double.Parse(positionY.Text);
                    if ((depx + (SelectedMyPolygon.GetPoints()).Max(Point => Point.X) > MyCanvas.ActualWidth) || (depy + (SelectedMyPolygon.GetPoints()).Max(Point => Point.Y) > MyCanvas.ActualHeight))
                    {
                        MessageBox.Show("Deplassement impossible  \nsort des limites du canvas");
                        return;
                    }
                    if ((depx + (SelectedMyPolygon.GetPoints()).Min(Point => Point.X) < 0) || (depy + (SelectedMyPolygon.GetPoints()).Min(Point => Point.Y) < 0))
                    {
                        MessageBox.Show("Deplassement impossible  \nsort des limites du canvas");
                        return;
                    }
                    SelectedMyPolygon.Deplacer(depx, depy);
                    MyCanvas.Children.Remove(SelectedPolygon); // Supprimer le path precedent 
                    SelectedPolygon = SelectedMyPolygon.Draw();
                    SelectedPolygon.StrokeThickness = Thik;
                    SelectedPolygon.StrokeDashArray = dbl;
                    MyCanvas.Children.Add(SelectedPolygon); // ajouter le nouveau 
                    SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                    SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;
                    positionY.Text = "0"; positionX.Text = "0";

                    // MyPolgon[index] et Path[index] ont été modifié faut mettre a jour dans notre environnement :
                    MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                }
                 catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
                return;
            }

        }    
        private void Rotation_Click(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                try
                {
                double rot = double.Parse(Rotate.Text);
                SelectedMyPolygon.Rotation(rot);
                MyCanvas.Children.Remove(SelectedPolygon);
                SelectedPolygon = SelectedMyPolygon.Draw();
                SelectedPolygon.StrokeThickness = Thik;
                SelectedPolygon.StrokeDashArray = dbl;
                MyCanvas.Children.Add(SelectedPolygon);
                SelectedPolygon.MouseRightButtonUp += obj_MouseRightButtonUp;
                SelectedPolygon.MouseLeftButtonDown += obj_MouseLeftButtonDown;
                Rotate.Text = "0";

                // MyPolgon[index] et Path[index] ont été modifié faut les mettre a jour dans notre environnement :
                MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
                return;
            }
        }
        private void SelectForOperation_Click(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                if (store.Count == 2)
                {
                    MessageBoxResult result = MessageBox.Show("Deux element sont deja selectionés\nVoulez vous enlever vider la liste", "Information", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) { store.Clear(); }
                }
                else
                {
                    store.Add(new Element(SelectedMyPolygon, SelectedPolygon));
                    MessageBox.Show("Element ajouté a la liste"); // je veux le rendre s'enleve automatiquement sans avoir a clické ok
                }
            }
            else
            {
                MessageBox.Show("Selectionné d'abord l'element a ajouter ");
            }
        }
        private void Intersection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Path> res;
                if (store.Count == 2)
                {
                    res = MyEnv.Intersection(store);
                    if (res != null)
                    {
                        foreach (var r in res)
                        {
                            MyCanvas.Children.Add(r);
                        }

                        foreach (var s in store)
                        {
                            MyEnv.Supprimer(s.obj); MyEnv.Supprimer(s.obj);
                            MyCanvas.Children.Remove(s.obj); MyCanvas.Children.Remove(s.obj);
                        }
                    }

                }
                else MessageBox.Show("SELECTINNER DEUX ELEMENTS");
                store.Clear();

                

            }
            catch
            {
                MessageBox.Show(" - Combinaison impossible - ");
                store.Clear();
            }

        }
        private void Union_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (store.Count != 2)
                {
                    MessageBox.Show("Selectionner d'abord deux elements");
                }
                else
                {
                    Path res = MyEnv.Union(store);
                    if (res != null)
                    {
                        foreach (var s in store) // supprimer les 2 elements 
                        {
                            MyEnv.Supprimer(s.obj); MyEnv.Supprimer(s.obj);
                            MyCanvas.Children.Remove(s.obj); MyCanvas.Children.Remove(s.obj);
                        }
                        MyCanvas.Children.Add(res);
                    }
                }
                store.Clear();
            }
            catch
            {
                MessageBox.Show("Cette combinaison est impossible.");
                store.Clear();
            }
        }
        private void Soustraction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Path> res ;
                if (store.Count == 2)
                {
                    res = MyEnv.Soustraction(store);
                    if (res != null)
                    {
                        foreach (var r in res)
                        {
                            MyCanvas.Children.Add(r);
                        }
                        
                        foreach (var s in store)
                        {
                            MyEnv.Supprimer(s.obj); MyEnv.Supprimer(s.obj);
                            MyCanvas.Children.Remove(s.obj); MyCanvas.Children.Remove(s.obj);
                        }
                    }
                }
                else MessageBox.Show("SELECTINNER DEUX ELEMENTS");
                store.Clear();
            }
            catch
            {
                MessageBox.Show("Cette combinaison est impossible");
                store.Clear();
            }


        }
        //*********************************************************************************************************************************************
        private void BackTo(object sender, RoutedEventArgs e)
        {
            try
            {
                MyEnv.Back();
                MyCanvas.Children.RemoveRange(0, MyCanvas.Children.Count);
                foreach (var item in MyEnv.Env)
                {
                    MyCanvas.Children.Add(item.obj);
                }
            }

            catch (System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AvantTo(object sender, RoutedEventArgs e)
        {
            MyEnv.After();
            MyCanvas.Children.RemoveRange(0, MyCanvas.Children.Count);
            foreach (var item in MyEnv.Env)
            {
                MyCanvas.Children.Add(item.obj);
            }
        }
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolygon != null)
            {
                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                MyEnv.Supprimer(SelectedPolygon);  // Supprimer de l'environnement 
                SelectedPolygon = null;
                SelectedMyPolygon = null;
                ID.Text = "";
                Rayon.Text = "";
                nbcot.Text = "";
                centreX.Text = "";
                centreY.Text = "";
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void Copier_Click(object sender, RoutedEventArgs e)
        {
            int i = 0; 
            if (SelectedPolygon != null)
            {
                MyEnv.ElementCopier.obj = SelectedPolygon;
                i = MyEnv.Recherche(SelectedPolygon);
                if (i != -1) { MyEnv.ElementCopier.p = MyEnv.GetMyPolygon(i); }
                else MessageBox.Show("Selectionnée d'abord un element "); 
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void Coller_Click(object sender, RoutedEventArgs e)
        {
            MyPolygon c; 
            if (MyEnv.ElementCopier.obj != null)
            {
                MyPolygon a = MyEnv.ElementCopier.p; 
                c = new MyPolygon(a.GetPoints() , a.GetFill() , a.GetStroke() );
                c.SetCentre(a.GetCentre()); c.rayon = a.rayon; 
                c.Deplacer(colx- a.GetCentre().X, coly- a.GetCentre().Y);
                obj = c.Draw();
                MyCanvas.Children.Add(obj);
                MyEnv.AddToEnv(c, obj);
            }
            else
            {
                MessageBox.Show("Rien a coller");
            }

        }
        private void Couper_Click(object sender, RoutedEventArgs e)
        {
            int i = 0; 
            if (SelectedPolygon != null)
            {
                // copier
                MyEnv.ElementCopier.obj = SelectedPolygon;
                i = MyEnv.Recherche(SelectedPolygon); 
                if (i != -1) { MyEnv.ElementCopier.p = MyEnv.GetMyPolygon(i); }
                else MessageBox.Show("Selectionnée d'abord un element ");
                // supprimer
                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                MyEnv.Supprimer(SelectedPolygon);  // Supprimer de l'environnement 
            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void ChangerColorFond(object sender, RoutedEventArgs e)
        {

            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                ChangeColor cpw = new ChangeColor();                //ouvrir Color picker window
                cpw.Owner = this;
                cpw.ShowDialog();
                if (cpw.OK)
                {
                    SelectedPolygon.Fill = cpw.SelectedBrush;
                    SelectedMyPolygon.SetFill(cpw.SelectedBrush);
                    colorfill.Text = SelectedPolygon.Fill.ToString();
                    RecFond.Fill = SelectedPolygon.Fill;
                }
                MyCanvas.Children.Remove(SelectedPolygon);
                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void BlackFond(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
               
                MyCanvas.Children.Remove(SelectedPolygon);
                SelectedPolygon.Fill = Brushes.Black ;
                SelectedMyPolygon.SetFill(Brushes.Black);
                colorfill.Text = SelectedPolygon.Fill.ToString();
                RecFond.Fill = SelectedPolygon.Fill;
                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }

        private void MasqueDemasque(object sender, RoutedEventArgs e)
        {
            if (RuleVisible)
            {
                verticalRuler.Visibility = Visibility.Hidden;
                horizontalRuler.Visibility = Visibility.Hidden;
                RuleVisible = false;
            }
            else
            {
                verticalRuler.Visibility = Visibility.Visible;
                horizontalRuler.Visibility = Visibility.Visible;
                RuleVisible = true;
            }
        
        }

        private void WhiteFond(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {

                MyCanvas.Children.Remove(SelectedPolygon);

                SelectedPolygon.Fill = Brushes.White;
                SelectedMyPolygon.SetFill(Brushes.White);
                colorfill.Text = SelectedPolygon.Fill.ToString();
                RecFond.Fill = SelectedPolygon.Fill;
                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void ChangerColorContour(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                ChangeColor cpw = new ChangeColor();                //ouvrir Color picker window
                cpw.Owner = this;
                cpw.ShowDialog();
                if (cpw.OK)
                {
                    SelectedPolygon.Stroke = cpw.SelectedBrush;
                    SelectedMyPolygon.SetStroke(cpw.SelectedBrush);
                    colorborder.Text = SelectedPolygon.Stroke.ToString();
                    RecContour.Fill = SelectedPolygon.Stroke;
                }
                MyCanvas.Children.Remove(SelectedPolygon);
                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void BlackContour(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {
                
                MyCanvas.Children.Remove(SelectedPolygon);

                SelectedPolygon.Stroke = Brushes.Black;
                SelectedMyPolygon.SetStroke(Brushes.Black);
                colorborder.Text = SelectedPolygon.Stroke.ToString();
                RecContour.Fill = SelectedPolygon.Stroke;

                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }
        private void WhiteContour(object sender, RoutedEventArgs e)
        {
            if ((SelectedPolygon != null) && (SelectedMyPolygon != null))
            {

                MyCanvas.Children.Remove(SelectedPolygon);

                SelectedPolygon.Stroke = Brushes.White;
                SelectedMyPolygon.SetStroke(Brushes.White);
                colorborder.Text = SelectedPolygon.Stroke.ToString();
                RecContour.Fill = SelectedPolygon.Stroke;

                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas

            }
            else
            {
                MessageBox.Show("Selectionnée d'abord un element ");
            }
        }

        
    }



}