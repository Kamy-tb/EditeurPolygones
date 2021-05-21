using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media ;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Team5Projet2CP
{
    class MyRectangle : MyPolygon
    {
        private double height;
        private double width;


        public MyRectangle(float height, float width, Point centre , SolidColorBrush CouleurFill, SolidColorBrush CouleurStroke) : base()
        {
            this.height = height;
            this.width = width;
            base.centre = centre;
            base.nbrcote = 4; 
            base.CouleurFill = CouleurFill;
            base.CouleurStroke = CouleurStroke; 
            name = "POLYGON_" + nbPolygon.ToString();
            CreerRectangle(); // Pour initialiser pnt_list et pnt_collection pour deplacement et rotation        
        }

        public void CreerRectangle() // Determine les points du rectangle ( le deplacement , rotation ) se font avec les points
        {
            pnt_list.Add(new Point(centre.X - width / 2, centre.Y - height / 2));
            pnt_list.Add(new Point(centre.X + width / 2, centre.Y - height / 2));
            pnt_list.Add(new Point(centre.X + width / 2, centre.Y - height / 2));
            pnt_list.Add(new Point(centre.X - width / 2, centre.Y + height / 2));
        }

        

    }
}
