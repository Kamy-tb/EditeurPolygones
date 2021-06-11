using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.IO;
using Path = System.Windows.Shapes.Path;

namespace Team5Projet2CP
{
    class MyPolygon
    {
        protected static int nbPolygon = 0; // un cnt pour pouvoir mettre le nom par defaut 
        protected String name;
        public double rayon;
        protected int nbrcote;
        protected SolidColorBrush CouleurFill;
        protected SolidColorBrush CouleurStroke;
        protected Point centre;
        protected List<Point> pnt_list = new List<Point>();
        protected Path myPath;


        
        public MyPolygon()   // Constructeur pour Myrectangle
        {
        }

        public MyPolygon(float rayon, int nbrcote, Point centre, SolidColorBrush CouleurFill, SolidColorBrush CouleurStroke)   // Constructeur pour Mypolygon
        {
            this.rayon = rayon;
            this.nbrcote = nbrcote;
            this.centre = centre;
            this.CouleurFill = CouleurFill;
            this.CouleurStroke = CouleurStroke;
            nbPolygon++;
            /* Par default */
            name = "POLYGON_" + nbPolygon.ToString();
        }
       
        public MyPolygon(List<Point> list, SolidColorBrush CouleurFill, SolidColorBrush CouleurStroke) // constructeur pour Mycomplex
        {
            this.pnt_list = new List<Point>(list);
            this.CouleurFill = CouleurFill;
            this.CouleurStroke = CouleurStroke;
            nbPolygon++;
            name = "POLYGON_" + nbPolygon.ToString();
        }
        public List<Point> GetPoints() { return pnt_list; }
        public void SetPoints(List<Point> value) { pnt_list = value; }
        public Point GetCentre() { return centre; }
        public void SetCentre(Point valeur) { centre = valeur;  }
        public String GetName() { return name; }
        public void SetName(String n) { name = n;  }
        public SolidColorBrush GetFill() { return CouleurFill; }
        public SolidColorBrush GetStroke () { return CouleurStroke;  }
        public void SetFill(SolidColorBrush clr) { CouleurFill = clr; }
        public void SetStroke(SolidColorBrush clr) { CouleurStroke = clr; }
        public void CreerPolygon()
        {
            float x, y;
            pnt_list.Clear(); // S'assurer que la liste est vide 
            /* L'idée est de rechercher les coordonées des points en faisant des rotation de (360/nbcote degrée) */
            double Theta = (360 / nbrcote) * (Math.PI / 180);    // Rechercher l'angle de rotation  
            double cosTheta = Math.Cos(Theta);
            double sinTheta = Math.Sin(Theta);

            Point precedent = new Point(centre.X - rayon, centre.Y);
            if (nbrcote == 4)
            {
                precedent.X = centre.X - rayon / Math.Sqrt(2);
                precedent.Y = centre.Y - rayon / Math.Sqrt(2);
            }
            pnt_list.Add(precedent);

            /*Trouver les points du polygon en faisant la rotation de point de  (360/nbcotee) degré */
            for (int i = 1; i < nbrcote; i++)
            {
                x = (float)(cosTheta * (precedent.X - centre.X) - sinTheta * (precedent.Y - centre.Y) + centre.X);
                y = (float)(sinTheta * (precedent.X - centre.X) + cosTheta * (precedent.Y - centre.Y) + centre.Y);
                precedent.X = x; precedent.Y = y; // Garder le point dans precedent pour la prochaine rotation 
                pnt_list.Add(new Point(x, y));
            }

        }
        public Path Draw()
        {
            PathGeometry pathGeom = new PathGeometry();
            PathFigure figure = new PathFigure();
            int index = 0;
            foreach (Point point in pnt_list)
            {
                if (index == 0)                                 //define first point of polyline as startpoint
                    figure.StartPoint = point;
                else
                    figure.Segments.Add(new LineSegment((point), true));
                index++;
            }
            figure.Segments.Add(new LineSegment((pnt_list[0]), true));
            pathGeom.Figures.Add(figure);
            myPath = new Path();
            myPath.Data = pathGeom;
            myPath.Stroke = CouleurStroke;
            myPath.StrokeThickness = 1;
            myPath.Fill = CouleurFill;
            return myPath;
        }
        public void Deplacer(double t, double s)
        {
            List<Point> pnt2 = new List<Point>();
            foreach (Point pi in pnt_list)
            {

                pnt2.Add(new Point(pi.X + t, pi.Y + s));
            }

            pnt_list.Clear();
            pnt_list = pnt2;
            centre = new Point(centre.X + t, centre.Y + s);
        }
        public void Rotation(double angle)
        {
            List<Point> pnt2 = new List<Point>();
            double x, y;
            double alpha = angle * (Math.PI / 180);
            double Cosangle = Math.Cos(alpha);
            double Sinangle = Math.Sin(alpha);

            foreach (Point pi in pnt_list)
            {
                x = (double)(Cosangle * (pi.X - centre.X) - (Sinangle * (pi.Y - centre.Y)) + centre.X);
                y = (double)(Sinangle * (pi.X - centre.X) + (Cosangle * (pi.Y - centre.Y)) + centre.Y);
                pnt2.Add(new Point(x, y));

            }
            pnt_list.Clear();
            this.pnt_list = pnt2;
        }

        /*Vérifier si un point p est à l'intérieur d'un polygone*/
        public bool PointIntPolygon(Point p)
        {
            int i;
            int j;
            bool resultat = false;

            for (i = 0, j = nbrcote - 1; i < nbrcote; j = i++)
            {
                if ((pnt_list[i].Y > p.Y) != (pnt_list[j].Y > p.Y) &&
                    (p.X < (pnt_list[j].X - pnt_list[i].X) * (p.Y - pnt_list[i].Y) / (pnt_list[j].Y - pnt_list[i].Y) + pnt_list[i].X))
                {
                    resultat = !resultat;
                }
            }
            return resultat;
        }
   
    
    }
}




