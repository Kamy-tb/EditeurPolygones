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
        private float rayon;
        private int nbrcote;
        protected SolidColorBrush CouleurFill;
        protected SolidColorBrush CouleurStroke;
        protected Point centre;
        protected List<Point> pnt_list = new List<Point>();
        protected Path myPath;

       

        public MyPolygon()   // Constructeur 
        {
        }

        public MyPolygon(float rayon, int nbrcote, Point centre, SolidColorBrush CouleurFill,SolidColorBrush CouleurStroke)   // Constructeur 
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

        public void CreerPolygon()
        {
            float x, y;

            /* L'idée est de rechercher les coordonées des points en faisant des rotation de (360/nbcote degrée) */
            double Theta = (360 / nbrcote) * (Math.PI / 180);    // Rechercher l'angle de rotation  
            double cosTheta = Math.Cos(Theta);
            double sinTheta = Math.Sin(Theta);

            Point precedent = new Point(centre.X + rayon, centre.Y + rayon);
            pnt_list.Add(precedent);

            /*Trouver les points du polygon en faisant la rotation de point de  (360/nbcotee) degré */
            for (int i = 1; i <= nbrcote; i++)
            {
                x = (float)(cosTheta * (precedent.X - centre.X) - sinTheta * (precedent.Y - centre.Y) + centre.X);
                y = (float)(sinTheta * (precedent.X - centre.X) + cosTheta * (precedent.Y - centre.Y) + centre.Y);
                precedent.X = x; precedent.Y = y; // Garder le point dans precedent pour la prochaine rotation 
                pnt_list.Add(new Point(x, y));
            }

        }
     

        public String SetName ()
        {
            return name;
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
            this.pnt_list = pnt2;
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
                x = (double)(Cosangle * (pi.X - centre.X) - (Sinangle*(pi.Y-centre.Y)) +centre.X);
                y = (double)(Sinangle * (pi.X - centre.X) + (Cosangle *(pi.Y - centre.Y)) + centre.Y);
                pnt2.Add(new Point(x, y));

            }
            pnt_list.Clear();
            this.pnt_list = pnt2 ;
        }

    }
}




