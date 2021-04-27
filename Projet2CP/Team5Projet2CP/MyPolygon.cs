using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.IO;

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
        private PointCollection pnt = new PointCollection();
        protected List<Point> pnts = new List<Point>();




        public MyPolygon()   // Constructeur 
        {
            nbPolygon++;
            name = "POLYGON_" + nbPolygon.ToString();
            CouleurFill = Brushes.White;
            CouleurStroke = Brushes.Black;
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
            pnt.Add(precedent);

            /*Trouver les points du polygon en faisant la rotation de point de  (360/nbcotee) degré */
            for (int i = 1; i <= nbrcote; i++)
            {
                x = (float)(cosTheta * (precedent.X - centre.X) - sinTheta * (precedent.Y - centre.Y) + centre.X);
                y = (float)(sinTheta * (precedent.X - centre.X) + cosTheta * (precedent.Y - centre.Y) + centre.Y);
                precedent.X = x; precedent.Y = y; // Garder le point dans precedent pour la prochaine rotation 
                pnt.Add(new Point(x, y));
            }

            pnts = pnt.ToList();
        }
        public PointCollection SetPoints()
        {
            return pnt;
        }
        public String SetName ()
        {
            return name;
        }

        public Polygon Draw()
        {
            Polygon p = new Polygon();
            p.Name = name;
            p.Points = pnt;
            p.Fill = CouleurFill;
            p.Stroke = CouleurStroke;
            return p;
        }
        public void Deplacer(double t, double s)
        {
            PointCollection pnt2 = new PointCollection();
            foreach (Point pi in pnts)
            {

                pnt2.Add(new Point(pi.X + t, pi.Y + s));

            }
            pnts.Clear();
            this.pnt = pnt2;
            centre = new Point(centre.X + t, centre.Y + s);
            pnts = pnt.ToList();
        }
        public void Rotation(double angle)
        {
            PointCollection pnt3 = new PointCollection();
            double xx, yy;
            double alpha = angle * (Math.PI / 180);
            double Cosangle = Math.Cos(alpha);
            double Sinangle = Math.Sin(alpha);

            foreach (Point pi in pnts)
            {
                xx = (double)(Cosangle * (pi.X - centre.X) - (Sinangle*(pi.Y-centre.Y)) +centre.X);
                yy = (double)(Sinangle * (pi.X - centre.X) + (Cosangle * (pi.Y - centre.Y)) + centre.Y);
                pnt3.Add(new Point(xx, yy));

            }
            pnts.Clear();
            this.pnt = pnt3;
            pnts = pnt.ToList();
        }

    }
}




