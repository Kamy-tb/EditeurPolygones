using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Team5Projet2CP
{
    class MyRectangle : MyPolygon
    {
        private double height;
        private double width;


        public MyRectangle(float height, float width, Point centre) : base()
        {
            this.height = height;
            this.width = width;
            base.centre = centre;
            name = "RECTANGLE_" + nbPolygon.ToString();
            CreerRectangle(); // Pour initialiser pnt_list et pnt_collection pour deplacement et rotation        
        }

        public void CreerRectangle() // Determine les points du rectangle ( le deplacement , rotation ) se font avec les points
        {
            pnt_collection.Add(new Point(centre.X - width / 2, centre.Y - height / 2));
            pnt_collection.Add(new Point(centre.X + width / 2, centre.Y - height / 2));
            pnt_collection.Add(new Point(centre.X + width / 2, centre.Y - height / 2));
            pnt_collection.Add(new Point(centre.X - width / 2, centre.Y + height / 2));
            pnt_list = pnt_collection.ToList();
        }

        public Rectangle DrawRectangle()
        {
            Rectangle r = new Rectangle();
            r.Name = name;
            r.Width = width;
            r.Height = height;
            r.Fill = CouleurFill;
            r.Stroke = CouleurStroke;
            Canvas.SetLeft(r, centre.X);
            Canvas.SetTop(r, centre.Y);
            return r;
        }

    }
}
