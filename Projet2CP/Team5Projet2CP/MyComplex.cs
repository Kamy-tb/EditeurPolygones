using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5Projet2CP
{
    class MyComplex : MyPolygon
    {
        public MyComplex() : base() { }
        public MyComplex(List<Point> pnt_list, SolidColorBrush CouleurFill, SolidColorBrush CouleurStroke) : base(pnt_list, CouleurFill, CouleurStroke)
        {
            base.centre = Centre(pnt_list);
            base.nbrcote = pnt_list.Count; 
        }


        public Point Centre(List<Point> list) 
        {
            double MaxX = list.Max(Point => Point.X);
            double MaxY = list.Max(Point => Point.Y);
            double MinX = list.Min(Point => Point.X);
            double MinY = list.Min(Point => Point.Y);
            return new Point((MinX + MaxX) / 2, (MinY + MaxY) / 2);
        }
    }
}
