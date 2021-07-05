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
using System.IO;
using System.Drawing;
using Path = System.Windows.Shapes.Path;
using Microsoft.Win32;



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
            positionY.Text = "0"; positionX.Text = "0";
            Rotate.Text = "0";
            verticalRuler.Visibility = Visibility.Hidden;
            horizontalRuler.Visibility = Visibility.Hidden;
            KeyDown += MyCanvas_KeyDown;


            string filePath = "bib_8.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            if (lines != null) add_rem = true;
            int ind = 0;
            while ((ind < lines.Count))
            {
                string[] tokens = lines[ind].Split('-');
                string name = tokens[0];
                ListBoxItem itm = new ListBoxItem();
                itm.Content = name;
                test.Items.Add(itm);
                ind++; cpt++;
            }

        }
        Boolean colle = false;
  

        // initialisation des objets de nos classes 
        Environnement MyEnv = new Environnement();  // Objet de notre environnement
        MyPolygon p, SelectedMyPolygon = null;
        Path obj = new Path();
        Path SelectedPolygon;
        Point MousePosition; 
        private ProprietesPolygon dw;
        private double depx = 0, depy = 0; // Pour le deplacement 

        SolidColorBrush F, S; // Pour les couleurs
        double colx;
        double coly;
        //********************************* Variable de rotation par souris ************************************
        RotateTransform TestRotate;
        double x, y; double theta = 0;
        Boolean _Rotate = false; Boolean clean = false;
        //********************************* Variable de zoom par souris ************************************
        ScaleTransform TestScale;
        double zoomFactor;
        Boolean _zoom = false; Boolean _zoomX = false;
        //****************************************  Variables de selection *************************************
        DoubleCollection dbl;
        int Thik;
        int index;
        //************************** SELECTIONNER DEUX POLYGONES POUR LES OPERATIONS*****************************
        List<Element> store = new List<Element>(); int cpt = 1;
        //****************************************  Variables de selection pour les operations*************************************
        int Thik2;
        DoubleCollection dbl2;
        Boolean FinOper = true;
        Boolean Stor2 = true;
        //*************************************** Crayon ***********************************************
        Polyline newPol = null;
        Path pat;
        List<Point> rr = new List<Point>();
        bool cr = false;
        bool finalCtrlPoint = false;
        //*******************************************************************************************************
        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
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
                                // MyEnv.Apres.Push(MyEnv.Env);
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
                            try
                            {
                                MyEnv.After();
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
                    default: break;
                }
            }
        }

        bool add_rem;
        private void test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem itm = new ListBoxItem();
            itm = (ListBoxItem)test.SelectedItem;
            if (add_rem)
            {
                string nom_polygone = itm.Content.ToString();

                string filePath = "bib_8.txt";
                List<MyPolygon> pol = new List<MyPolygon>();
                List<string> lines = File.ReadAllLines(filePath).ToList();
                foreach (var line in lines)
                {
                    string[] tokens = line.Split('-');

                    string name = tokens[0];
                    List<Point> points = MyEnv.strpntTolist(tokens[1]);
                    Color cf = (Color)ColorConverter.ConvertFromString(tokens[2]);
                    SolidColorBrush fill = new SolidColorBrush(cf);
                    Color cs = (Color)ColorConverter.ConvertFromString(tokens[3]);
                    SolidColorBrush stroke = new SolidColorBrush(cs);
                    p = new MyPolygon(points, fill, stroke);
                    p.SetName(name);
                    pol.Add(p);
                }

                bool trouv = false;
                int ind = 0;
                while ((ind < pol.Count) && (!trouv))
                {
                    if (pol[ind].GetName().Equals(nom_polygone) == true)
                    {
                        MyComplex comp = new MyComplex(pol[ind].GetPoints() , pol[ind].GetFill() , pol[ind].GetStroke()); 
                        comp.SetName(pol[ind].GetName()); 
                        obj = comp.Draw();
                        MyEnv.AddToEnv( comp , obj);
                        MyCanvas.Children.Add(obj);
                        trouv = true;
                    }
                    ind++;
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            add_rem = true;
            ListBoxItem itm = new ListBoxItem();
            itm.Content = SelectedMyPolygon.GetName();
            test.Items.Add(itm);
            StreamReader sr = File.OpenText("bib_8.txt");
            List<string> list_str = new List<string>();
            String Str;
            int cpth = 1;
            while (cpth < cpt)
            {
                list_str.Add(sr.ReadLine());
                cpth++;
            };
            sr.Close();

            String nomstr = SelectedMyPolygon.GetName();
            String fillstr = SelectedMyPolygon.GetFill().ToString();
            String strokelstr = SelectedMyPolygon.GetStroke().ToString();
            String pointstr = MyEnv.pntlistTOString_2(SelectedMyPolygon.GetPoints());
            List<String> elementString = new List<string>() { nomstr, pointstr, fillstr, strokelstr };

            Str = String.Join("-", elementString.ToArray());
            list_str.Add(Str);
            StreamWriter sw = File.CreateText("bib_8.txt");
            foreach (var s in list_str)
            {
                sw.WriteLine(s);
            }
            sw.Close(); cpt++;
        }

        private void remove(object sender, RoutedEventArgs e)
        {
            add_rem = false;
            ListBoxItem itm = new ListBoxItem();
            itm = (ListBoxItem)test.SelectedItem;
            test.Items.Remove(itm);
            string nom_polygone = itm.Content.ToString();
            string filePath = "bib_8.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

            bool trouv = false;
            int ind = 0;
            while ((ind < lines.Count) && (!trouv))
            {
                string[] tokens = lines[ind].Split('-');

                string name = tokens[0];
                if (name.Equals(nom_polygone))
                {
                    trouv = true;
                    lines.RemoveAt(ind);
                }
                ind++;
            }

            StreamWriter sw = File.CreateText("bib_8.txt");
            foreach (var l in lines)
            {
                sw.WriteLine(l);
            }
            sw.Close(); cpt--;
        }

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
      
        private void Selection(object sender, MouseButtonEventArgs e)
        {
            colx = e.GetPosition(MyCanvas).X;
            coly = e.GetPosition(MyCanvas).Y;

                if (SelectedPolygon != null && FinOper)
                {
                    dbl = null;
                    Thik = 1;
                    SelectedPolygon.StrokeThickness = Thik;
                    SelectedPolygon.StrokeDashArray = dbl;
                }
                if (e.OriginalSource is Path  )
                {
                   if(FinOper)
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
                   }
                   else
                   {
                    SelectedPolygon = (Path)e.OriginalSource;
                    index = MyEnv.Recherche(SelectedPolygon);
                    if (index != -1) { SelectedMyPolygon = MyEnv.GetMyPolygon(index); }
                   }


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
            MenuItem Zoom = new MenuItem() { Header = "Redimension" };
            MenuItem ZoomX = new MenuItem() { Header = "Redimension horizontale" };
            MenuItem nom = new MenuItem() { Header = "Renomer" };
            Depl.Click += mov_butt;
            Rotation.Click += rotate_butt;
            Zoom.Click += Zoom_Click;
            ZoomX.Click += ZoomX_Click;
            nom.Click += Nom_Click;
            cm.Items.Add(Depl);
            cm.Items.Add(Rotation);
            cm.Items.Add(Zoom);
            cm.Items.Add(ZoomX);
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
                if (nw.OK)
                {
                    Forpile f = new Forpile(index, SelectedMyPolygon.GetName());
                    MyEnv.change(f);
                    SelectedMyPolygon.SetName(nw.nom);
                }
                ID.Text = SelectedMyPolygon.GetName();
            }
                
        }

        private void mov_butt(object sender, RoutedEventArgs e)
        {
            mov = true;
            _Rotate = false;
            _zoom = false;
            _zoomX = false; 
        }
        private void rotate_butt(object sender, RoutedEventArgs e)
        {
            _Rotate = true;
            mov = false;
            _zoom = false; _zoomX = false;
        }
        private void ZoomX_Click(object sender, RoutedEventArgs e)
        {
            _zoomX = true ;
            _zoom = false ;
            mov = false;
            _Rotate = false;
        }
        private void Zoom_Click(object sender, RoutedEventArgs e)
        {
            _zoom = true;
            mov = false;
            _Rotate = false; _zoomX = false;
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
                if (_zoomX)
                {
                    if (index != -1)
                    {
                        SelectedMyPolygon = MyEnv.GetMyPolygon(index);
                        if (SelectedMyPolygon.rayon == 0) zoomFactor = (e.GetPosition(MyCanvas).X - SelectedMyPolygon.GetCentre().X) / (SelectedMyPolygon.GetPoints()[0].X - SelectedMyPolygon.GetCentre().X);
                        else zoomFactor = (e.GetPosition(MyCanvas).X - SelectedMyPolygon.GetCentre().X) / SelectedMyPolygon.rayon;

                        TestScale = new ScaleTransform(zoomFactor, 1, SelectedMyPolygon.GetCentre().X, SelectedMyPolygon.GetCentre().Y);
                        SelectedPolygon.RenderTransform = TestScale;
                        clean = true;
                    }
                    else { MessageBox.Show("Un probleme a été rencontré"); return; }
                }
            }
            horizontalRuler.RaiseHorizontalRulerMoveEvent(e);
            verticalRuler.RaiseVerticalRulerMoveEvent(e);
            if(cr)
            {
                if (newPol == null) return;
                newPol.Points[newPol.Points.Count - 1] = e.GetPosition(MyCanvas);
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
                        Forpile f = new Forpile(index, theta);
                        MyEnv.change(f);
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
                        Forpile f = new Forpile(index, SelectedMyPolygon.rayon, SelectedMyPolygon.GetPoints());
                        MyEnv.change(f);
                        SelectedMyPolygon.SetPoints(newCoord);
                        SelectedMyPolygon.rayon = Math.Abs(zoomFactor) * SelectedMyPolygon.rayon;
                        SelectedPolygon = SelectedMyPolygon.Draw();
                        MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                        MyCanvas.Children.Add(SelectedPolygon);
                        SelectedPolygon = null;
                        SelectedMyPolygon = null;
                        _zoom = false; mov = true; 
                    }
                }
                if (_zoomX)
                {
                    if (clean)
                    {

                        MyCanvas.Children.Remove(SelectedPolygon);

                        List<Point> newCoord = new List<Point>();
                        foreach (Point p in SelectedMyPolygon.GetPoints())
                        {
                            x = zoomFactor * (p.X - SelectedMyPolygon.GetCentre().X) + SelectedMyPolygon.GetCentre().X;
                            newCoord.Add(new Point(x, p.Y ));
                        }
                        Forpile f = new Forpile(index, SelectedMyPolygon.rayon, SelectedMyPolygon.GetPoints());
                        MyEnv.change(f);
                        SelectedMyPolygon.SetPoints(newCoord);
                        SelectedMyPolygon.rayon = 0; 
                        SelectedPolygon = SelectedMyPolygon.Draw();
                        MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
                        MyCanvas.Children.Add(SelectedPolygon);
                        SelectedPolygon = null;
                        SelectedMyPolygon = null;
                        _zoomX = false; mov = true; 
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
                        Forpile f = new Forpile(index, deltaX, deltaY);
                        MyEnv.change(f);
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
                   

                    Forpile f = new Forpile(index, depx, depy);
                    MyEnv.change(f);

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
                Forpile f = new Forpile(index, rot);
                MyEnv.change(f);
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
                FinOper = false;
                if (store.Count == 2)
                {
                    MessageBoxResult result = MessageBox.Show("Deux element sont deja selectionés\nVoulez vous enlever vider la liste", "Information", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) { store.Clear(); }
                    Stor2 = false;
                }
                else
                {
                    store.Add(new Element(SelectedMyPolygon, SelectedPolygon));
                    dbl2 = new DoubleCollection() { 2 };
                    Thik2 = 3;
                    SelectedPolygon.StrokeThickness = Thik2;
                    SelectedPolygon.StrokeDashArray = dbl2;
                    MessageBox.Show("Element ajouté a la liste"); // je veux le rendre s'enleve automatiquement sans avoir a clické ok
                    Stor2 = true;
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
                    FinOper = true;
                    res = MyEnv.Intersection(store);
                    if (res != null)
                    {
                        foreach (var s in store)
                        {
                            MyEnv.Supprimer(s.obj); MyEnv.Supprimer(s.obj);
                            MyCanvas.Children.Remove(s.obj); MyCanvas.Children.Remove(s.obj);
                        }
                        Forpile f = new Forpile(res.Count, MyEnv.Env.Count);
                        MyEnv.change(f);
                        foreach (var r in res)
                        {
                            MyCanvas.Children.Add(r);
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
                FinOper = true;
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
                        Forpile f = new Forpile(1, MyEnv.Env.Count);
                        MyEnv.change(f);
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
                    FinOper = true;
                    res = MyEnv.Soustraction(store);
                    if (res != null)
                    {
                        Forpile f = new Forpile(res.Count,MyEnv.Env.Count);
                        MyEnv.change(f);
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
                    Forpile f = new Forpile(index,SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                    MyEnv.change(f);
                    SelectedPolygon.Fill = cpw.SelectedBrush;
                    SelectedMyPolygon.SetFill(cpw.SelectedBrush);
                    colorfill.Text = SelectedPolygon.Fill.ToString();
                    RecFond.Fill = SelectedPolygon.Fill;
                }
                MyCanvas.Children.Remove(SelectedPolygon); // Supprimer du canvas
                MyCanvas.Children.Add(SelectedPolygon);
                MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);

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
                Forpile f = new Forpile(index, SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                MyEnv.change(f);
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
                Forpile f = new Forpile(index, SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                MyEnv.change(f);
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
                    Forpile f = new Forpile(index, SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                    MyEnv.change(f);
                    SelectedPolygon.Stroke = cpw.SelectedBrush;
                    SelectedMyPolygon.SetStroke(cpw.SelectedBrush);
                    colorborder.Text = SelectedPolygon.Stroke.ToString();
                    RecContour.Fill = SelectedPolygon.Stroke;
                }
                MyCanvas.Children.Remove(SelectedPolygon);
                MyCanvas.Children.Add(SelectedPolygon); // Supprimer du canvas
                MyEnv.SetChamp(index, SelectedMyPolygon, SelectedPolygon);
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
                Forpile f = new Forpile(index, SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                MyEnv.change(f);
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
                Forpile f = new Forpile(index, SelectedMyPolygon.GetFill(), SelectedMyPolygon.GetStroke());
                MyEnv.change(f);
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


        /******************************************** cryaon ********************************************************************/
        private void crayon_click(object sender, RoutedEventArgs e)
        {
            cr = true;
            newPol = null;
        }
        private void Mouse_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            finalCtrlPoint = true;
            newPol.Points.RemoveAt(newPol.Points.Count - 1);
            newPol.Points.Add(newPol.Points[0]);
            pat = ConvertPolyLineToPath(newPol, finalCtrlPoint);
            if (cr == true)
            {
                MyCanvas.Children.Remove(newPol);
                MyCanvas.Children.Add(pat);
                List<Point> mylist = MyEnv.PathToPoints(pat);
                MyComplex newComp = new MyComplex(mylist, Brushes.White , Brushes.Black);
                MyEnv.AddToEnv(newComp, pat);
            }
            cr = false;
        }
        private void newPol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool inter = false;
            if (e.ChangedButton == MouseButton.Left)
            {
                Point lastPoint = e.GetPosition(MyCanvas);
                rr.Clear();
                for (int j = 0; j < newPol.Points.Count - 2; j++)
                {
                    rr.Add(newPol.Points[j]);
                }


                if (newPol.Points.Count > 2)
                {
                    Line l = new Line();
                    l.X1 = newPol.Points[newPol.Points.Count - 2].X;
                    l.Y1 = newPol.Points[newPol.Points.Count - 2].Y;
                    l.X2 = lastPoint.X;
                    l.Y2 = lastPoint.Y;

                    inter = GetIntersction2(newPol, l);


                    if (inter == true)
                    {

                        MessageBox.Show("erreur");



                    }

                }
                if (inter == false)
                {
                    newPol.Points.Add(lastPoint);


                }
                MouseDoubleClick += Mouse_DoubleClick;

            }
        }
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (newPol == null)
            {
                newPol = new Polyline();
                newPol.Stroke = Brushes.Black;
                newPol.StrokeThickness = 2;
                newPol.Points.Add(e.GetPosition(MyCanvas));
                newPol.Points.Add(e.GetPosition(MyCanvas));
                MyCanvas.Children.Add(newPol);
                newPol.MouseLeftButtonDown += newPol_MouseLeftButtonDown;

            }
            else
            {
                MouseDoubleClick += Mouse_DoubleClick;
            }

        }
        public bool GetIntersction2(Polyline p, Line line)
        {
            Line myLine = new Line();
            IntersectionStruct inter;
            bool inters = false;

            for (int i = 0; i < p.Points.Count - 1; i++)
            {
                myLine.X1 = p.Points[i].X; myLine.Y1 = p.Points[i].Y;
                myLine.X2 = p.Points[i + 1].X; myLine.Y2 = p.Points[i + 1].Y;
                inter = intersectLines(myLine, line);

                if (inter.intersect == true && ((Math.Max(myLine.X1, myLine.X2) > inter.point.X) && (inter.point.X > Math.Min(myLine.X1, myLine.X2))
                && (Math.Max(myLine.Y1, myLine.Y2) > inter.point.Y) && (inter.point.Y > Math.Min(myLine.Y1, myLine.Y2))
                && (Math.Max(line.X1, line.X2) > inter.point.X) && (inter.point.X > Math.Min(line.X1, line.X2))
                && (Math.Max(line.Y1, line.Y2) > inter.point.Y) && (inter.point.Y > Math.Min(line.Y1, line.Y2))))
                {
                    inters = inter.intersect;
                }


            }
            return inters;

        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            help hlp = new help();
            hlp.Owner = Application.Current.MainWindow;
            hlp.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Filename = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            ofd.Filter = "Text Document (.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                Filename = ofd.FileName;
            }
            List<String> EnvrStr = MyEnv.saveEnvirnment(MyEnv.Env);
            File.WriteAllLines(Filename, EnvrStr);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string Filename = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            ofd.Filter = "Text Document (.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                Filename = ofd.FileName;
            }

            MyEnv.restorEnvirnment(Filename);
            foreach (var elem in MyEnv.Env)
            {
                obj = elem.p.Draw();
                MyCanvas.Children.Add(obj);
            }
        }

        private void HelpMenu_Click(object sender, RoutedEventArgs e)
        {
            help hlp = new help();
            hlp.Owner = Application.Current.MainWindow;
            hlp.ShowDialog();
        }

        private void exporter_Click(object sender, RoutedEventArgs e)
        {
            string Filename = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            ofd.Filter = "Text Document (.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                Filename = ofd.FileName;
            }
            List<String> EnvrStr = MyEnv.saveEnvirnment(MyEnv.Env);
            File.WriteAllLines(Filename, EnvrStr);
        }

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public struct IntersectionStruct
        {
            public Point point;
            public bool intersect;
            public IntersectionStruct(Point p, bool b)
            {
                this.point = p;
                this.intersect = b;
            }
        }
        public IntersectionStruct intersectLines(Line line1, Line line2)
        {

            double A1;
            double A2;
            double B1;
            double B2;
            Boolean intersect = new Boolean();
            IntersectionStruct intt;
            Point interscetionPoint = new Point();
            Point a1 = new Point(line1.X1, line1.Y1);
            Point b1 = new Point(line1.X2, line1.Y2);
            Point a2 = new Point(line2.X1, line2.Y1);
            Point b2 = new Point(line2.X2, line2.Y2);

            A1 = (a1.Y - b1.Y) / (a1.X - b1.X);
            B1 = -(A1) * (a1.X) + a1.Y;
            A2 = (a2.Y - b2.Y) / (a2.X - b2.X);
            B2 = -(A2) * (a2.X) + a2.Y;
            // equation2 = GetSegmentEquation(a2, b2);

            if (A1 == A2)
            {
                intersect = false;
            }
            else
            {

                interscetionPoint.X = -(B1 - B2) / (A1 - A2);
                interscetionPoint.Y = (A2 * interscetionPoint.X) + B2;

                if ((Math.Max(line1.X1, line1.X2) >= interscetionPoint.X) && (interscetionPoint.X >= Math.Min(line1.X1, line1.X2))
                && (Math.Max(line1.Y1, line1.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line1.Y1, line1.Y2))
                && (Math.Max(line2.X1, line2.X2) >= interscetionPoint.X) && (interscetionPoint.X >= Math.Min(line2.X1, line2.X2))
                && (Math.Max(line2.Y1, line2.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line2.Y1, line2.Y2)))
                {
                    intersect = true;
                }

            }
            intt = new IntersectionStruct(interscetionPoint, intersect);
            return intt;

        }
        public Path ConvertPolyLineToPath(Polyline polyLine, bool closed) //Convert PolyLine to polygon, if shaped was closed during creation
        {
            Path myPath = null;
            PathGeometry pathGeom = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.IsClosed = closed;
            if (closed)                                             //for polygon
            {
                int index = 0;
                foreach (Point point in polyLine.Points)
                {
                    if (index == 0)                                 //define first point of polyline as startpoint
                        figure.StartPoint = point;
                    else if (index == polyLine.Points.Count - 1)    //exclude last point, identical with startpoint
                    {
                        break;
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment((point), true));    //other points are ok
                    }
                    index++;
                }
                pathGeom.Figures.Add(figure);

                myPath = new Path();
                myPath.Data = pathGeom;
                myPath.Stroke = Brushes.Black;
                myPath.StrokeThickness = 1;
                myPath.Fill = Brushes.White;
            }
            return myPath;

        }




        private void Nouveau_Click(object sender, RoutedEventArgs e)
        {
            MyEnv.Env.Clear();
            MyCanvas.Children.Clear();
            MyPolygon.RazNbPolygon(); 
        }
    }



}