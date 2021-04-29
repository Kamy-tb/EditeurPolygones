using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = System.Windows.Shapes.Path;

namespace Team5Projet2CP
{
    struct Element
    {
        public MyPolygon p;
        public Path obj;
        public Element(MyPolygon p, Path obj)
        {
            this.p = p;
            this.obj = obj;
        } 
    }
    class Environnement
    {
  
        public List<Element> Env = new List<Element>()  ;
     //   Element polygonReserve = new Element(new MyPolygon(), new Path());

        public void SetEnv (MyPolygon p , Path obj) // Ajouter a la list d'environnement 
        {
            Env.Add(new Element(p, obj)); 
        }

        public MyPolygon GetMyPolygon(int index)
        {
           return Env[index].p; 
        }

        public int Recherche (Path obj ) //Nous recherch obj dans la liste pour recuperer l'index (le but est de recuperer MyPolygone qui correspend a le path pbj)
        {
            Boolean found = false ; 
            int i = 0;
            if (obj != null)
            {
                while ( (found != true) && (i< Env.Count) ) 
                {
                    if (Env[i].obj == obj)
                        found = true;
                    else
                        i++;
                }
            }
            if ( (found == false) || (obj ==null) )  // Si pas trouver ou l'objet est null 
                    i = -1;
            return i;
        }
        





    }
}
