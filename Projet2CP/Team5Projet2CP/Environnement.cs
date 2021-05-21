using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;
using Path = System.Windows.Shapes.Path;

namespace Team5Projet2CP
{
    class Element
    {
        public MyPolygon p;
        public Path obj;
        public Element() {}
        public Element(MyPolygon p, Path obj)
        {
            this.p = p;
            this.obj = obj;
        }
    }
    class MyStruct
    {
        public Point pnt;
        public Boolean intersct; // a true si le point est un point d'interserction a faux si c un point du polygon 
        public Boolean visite;
        public MyStruct(Point pnt, Boolean intersct, Boolean visite)
        {
            this.pnt = pnt;
            this.intersct = intersct;
            this.visite = visite;
        }
    }


    class Environnement
    {
        public Element ElementCopier = new Element();
        public List<Element> Env = new List<Element>()  ;
        public void AddToEnv (MyPolygon p , Path obj) // Ajouter a la list d'environnement 
        {
            Env.Add(new Element(p, obj)); 
        }
        public MyPolygon GetMyPolygon(int index) // recuperer le champ my polygon [ index ] 
        {
                return Env[index].p;
        }
        public void SetChamp(int index , MyPolygon valeur1 , Path valeur2)
        {
            Env[index].p = valeur1;
            Env[index].obj = valeur2; 
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
        public void Supprimer (Path obj) // Supprimer de l'environnement 
        {
            int index = Recherche(obj); 
            if (index != -1)
            {
                Env.RemoveAt(index); 
            }
        }

        private Point GetIntersectionPoint(Point l1p1, Point l1p2, Point l2p1, Point l2p2) // recupere point d'intersection de deux segments
        {
            double A1 = l1p2.Y - l1p1.Y;
            double B1 = l1p1.X - l1p2.X;
            double C1 = A1 * l1p1.X + B1 * l1p1.Y;

            double A2 = l2p2.Y - l2p1.Y;
            double B2 = l2p1.X - l2p2.X;
            double C2 = A2 * l2p1.X + B2 * l2p1.Y;
            Point p = new Point(-1, -1);

            double det = A1 * B2 - A2 * B1;
            if (det == 0d)
            {
                return p; // les lignes sont parallèles
            }
            else
            {
                double x = (B2 * C1 - B1 * C2) / det;
                double y = (A1 * C2 - A2 * C1) / det;
                bool online1 = ((Math.Min(l1p1.X, l1p2.X) < x || Equals(Math.Min(l1p1.X, l1p2.X), x))
                    && (Math.Max(l1p1.X, l1p2.X) > x || Equals(Math.Max(l1p1.X, l1p2.X), x))
                    && (Math.Min(l1p1.Y, l1p2.Y) < y || Equals(Math.Min(l1p1.Y, l1p2.Y), y))
                    && (Math.Max(l1p1.Y, l1p2.Y) > y || Equals(Math.Max(l1p1.Y, l1p2.Y), y))
                    );
                bool online2 = ((Math.Min(l2p1.X, l2p2.X) < x || Equals(Math.Min(l2p1.X, l2p2.X), x))
                    && (Math.Max(l2p1.X, l2p2.X) > x || Equals(Math.Max(l2p1.X, l2p2.X), x))
                    && (Math.Min(l2p1.Y, l2p2.Y) < y || Equals(Math.Min(l2p1.Y, l2p2.Y), y))
                    && (Math.Max(l2p1.Y, l2p2.Y) > y || Equals(Math.Max(l2p1.Y, l2p2.Y), y))
                    );

                if (online1 && online2)
                    return new Point(x, y);
            }
            return p; //intersection est OUT de au moins un segment
        }
        private List<Point> GetIntersectionPoints(Point l1p1, Point l1p2, MyPolygon poly) // recupere list de points d'un segment avec un polygone
        {
            List<Point> intersectionPoints = new List<Point>();
            List<Point> l1 = poly.GetPoints();
            int i = 0;
            foreach (Point point in l1) //(int i = 0; i < poly.getPnt_list.count ; i++)
            {

                int next = (i + 1 == l1.Count) ? 0 : i + 1;

                Point ip = GetIntersectionPoint(l1p1, l1p2, l1[i], l1[next]);

                if (ip != new Point(-1, -1)) intersectionPoints.Add(ip);
                i++;
            }

            return intersectionPoints;
        }
        public List<Point> GetintersectionPointsOf2POLYGONS(MyPolygon poly1, MyPolygon poly2) // recupere pnts intersections de deux polygones
        {
            List<Point> intersectionPoints = new List<Point>();
            Point point2 = new Point();
            List<Point> l1 = poly1.GetPoints();
            Point point1 = l1[0];
            for (int i = 1; i < l1.Count; i++)                                  // (Point point1 in poly1.SetPoints)
            {
                point2 = l1[i];
                intersectionPoints.AddRange(GetIntersectionPoints(point1, point2, poly2));
                point1 = point2;

            }
            point1 = l1[0];
            intersectionPoints.AddRange(GetIntersectionPoints(point1, point2, poly2));
            intersectionPoints = intersectionPoints.Distinct().ToList(); // enlever les doublons 
            return intersectionPoints;

        }
        private List<Point> OrderClockwise(List<Point> list_glob) // Ordonnée les points de la liste pour former un parcourt cas simple 
        {
            double mX = 0;
            double my = 0;
            foreach (Point p in list_glob)
            {
                mX += p.X;
                my += p.Y;
            }
            mX /= list_glob.Count;
            my /= list_glob.Count;

            return list_glob.OrderBy(v => Math.Atan2(v.Y - my, v.X - mX)).ToList();
        }
        private List<Point> Combiner(List<Point> pnt_list_intr, List<Point> pnt_list_i) // Ordonnée les points de la liste pour former un parcourt
        {
            List<Point> pnt_list_1 = new List<Point>();
            double a, b;
            int index3;
            int index2;
            List<Point> sortet = new List<Point>();
            List<Point> intrt = new List<Point>();
            index3 = 0;
            foreach (Point point in pnt_list_i)
            {
                if (index3 != (pnt_list_i.Count - 1))
                {
                    index2 = index3;
                }
                else
                {
                    index2 = -1;
                }

                if (pnt_list_i[index2 + 1].X == point.X)
                {
                    a = point.X;
                    foreach (Point intrs in pnt_list_intr)
                    {
                        if (intrs.X == a)
                        {
                            intrt.Add(intrs);
                        }

                    }
                    if (intrt != null)
                    {
                        if (point.Y < pnt_list_i[index2 + 1].Y)
                        {
                            sortet = intrt.OrderBy(p => (p.Y)).ToList();
                        }
                        else
                            sortet = intrt.OrderByDescending(p => (p.Y)).ToList(); pnt_list_1.Add(point);

                    }
                    pnt_list_1.AddRange(sortet);
                    intrt.Clear();
                    sortet.Clear();

                }
                else
                {
                    a = (pnt_list_i[index2 + 1].Y - point.Y) / (pnt_list_i[index2 + 1].X - point.X);
                    b = point.Y - (a * point.X);
                    foreach (Point intrs in pnt_list_intr)
                    {
                        if (intrs.Y == a * intrs.X + b)
                        {
                            intrt.Add(intrs);
                        }

                    }
                    if (intrt != null)
                    {
                        if (point.Y != pnt_list_i[index2 + 1].Y)
                        {
                            if (point.Y < pnt_list_i[index2 + 1].Y)
                            {
                                sortet = intrt.OrderBy(p => (p.Y)).ToList();
                            }
                            else
                                sortet = intrt.OrderByDescending(p => (p.Y)).ToList();
                        }
                        else
                        {

                            if (point.X < pnt_list_i[index2 + 1].X)
                                sortet = intrt.OrderBy(p => (p.X)).ToList();
                            else
                                sortet = intrt.OrderByDescending(p => (p.X)).ToList();
                        }

                    }
                    pnt_list_1.Add(point);
                    pnt_list_1.AddRange(intrt);
                    intrt.Clear();
                    sortet.Clear();
                }

                index3++;
            }

            return pnt_list_1;
        }
        private List<MyStruct> RemplirStructure(List<Point> MonPolygon, List<Point> intersection) // Remplir notre liste de structure 
        {
            MyStruct valeur;
            List<Point> ListAvecDouble = new List<Point>();
            List<Point> ListOrdonee = new List<Point>();
            List<MyStruct> Poly = new List<MyStruct>();

            MonPolygon = OrderClockwise(MonPolygon);
            intersection = OrderClockwise(intersection);

            ListAvecDouble = Combiner(intersection, MonPolygon);
            ListOrdonee = ListAvecDouble.Distinct().ToList(); // enlever les doublons

            // Remplir les champs de la structure 
            for (int i = 0; i < ListOrdonee.Count; i++)
            {
                valeur = new MyStruct(ListOrdonee[i], false, false);
                Poly.Add(valeur);

                // Verifie si le point est un point d'intersection le mettre a vrai champ 2 
                if (intersection.Contains(Poly[i].pnt)) { Poly[i].intersct = true; }
            }
            return Poly;
        }
        private int IndexPointDepart(List<MyStruct> A, MyPolygon B) // index point de depart 
        {
            int i = 0; Boolean found = false;
            while ((!found) && (i < A.Count))
            {
                if ((A[i].intersct == false) && (A[i].visite == false)) { found = !B.PointIntPolygon(A[i].pnt); }
                if (!found) { i++; }
            }
            if (!found) { i = -1; }
            return i;
        }
        private MyStruct SuivantMyStruct(List<MyStruct> A, ref int index) //  retourne le suivant de A dans mystruct et son index 
        {
            index = index + 1;
            if (index == A.Count) index = 0;
            if ((index > -1) && (index < A.Count)) return A[index];
            else return null;
        }
        private MyStruct PrecedentMyStruct(List<MyStruct> A, ref int index)  //  retourne le precedent de A dans mystruct et son index 
        {
            index = index - 1;
            if (index == -1) index = A.Count - 1; MessageBox.Show("INDEX = " + index.ToString());
            if ((index > -1) && (index < A.Count)) return A[index];
            else return null;
        }
        private int RechercheMyStruct(List<MyStruct> A, Point x) //Rechercher un point dans la liste de structure
        {
            int index = 0; Boolean found = false;
            while ((!found) && (index < A.Count))
            {
                if (A[index].pnt == x) { found = true; }
                else index++;
            }
            if (!found) { index = -1; }
            return index;
        }
        public List<Point> polygone_int_polygone(MyPolygon pol1, MyPolygon pol2)
        {
            int cpt1 = 0;
            int cpt2 = 0;
            bool trouv = false;
            List<Point> liste1 = pol1.GetPoints();
            List<Point> liste2 = pol2.GetPoints();
            List<Point> liste3 = new List<Point>();
            while (cpt1 < liste1.Count)
            {
                if (pol2.PointIntPolygon(liste1[cpt1]) == true)
                {
                    trouv = true;
                    liste3.Add(liste1[cpt1]);
                }
                cpt1++;
            }
            while (cpt2 < liste2.Count)
            {
                if (pol1.PointIntPolygon(liste2[cpt2]) == true)
                {
                    trouv = true;
                    liste3.Add(liste2[cpt2]);
                }
                cpt2++;
            }
            if (!trouv) return (null);
            else return liste3;

        }
        public List<Point> PathToPoints(Path convert) // Recupere la liste des points a partir d'un path
        {
            List<Point> res = new List<Point>();
            PathGeometry g = convert.Data.GetFlattenedPathGeometry();
            foreach (var f in g.Figures)
            {
                foreach (var s in f.Segments)
                {
                     
                    if (s is PolyLineSegment)
                    {
                        foreach (var pt in ((PolyLineSegment)s).Points) { res.Add(pt);  }
                       
                    }
                }
            }
            return res;
        }
        private List<Path> SeparatePathComplex (Path seperate)  
        {
            List<Point> resList = new List<Point>(); Element elem = new Element() ; 
            List<Path> res = new List<Path>();
            PathGeometry g = seperate.Data.GetFlattenedPathGeometry();
            SolidColorBrush fill = (SolidColorBrush)seperate.Fill ;
            SolidColorBrush stroke = (SolidColorBrush)seperate.Stroke ;  
            foreach (var f in g.Figures)
            {
                resList.Clear();
                foreach (var s in f.Segments)
                {
                    if (s is PolyLineSegment)
                    {
                        foreach (var pt in ((PolyLineSegment)s).Points) { resList.Add(pt); }
                        
                    }   
                }
                elem.p = new MyComplex(resList, fill, stroke);  elem.obj = elem.p.Draw();
                Env.Add(elem); 
                res.Add(elem.obj);
            }
            return res; 
        }
        public Path Union(List<Element> store)
        {
            MyPolygon A = new MyPolygon(); MyPolygon B = new MyPolygon();
            A = store[0].p; B = store[1].p;
            Path myPath = new Path();
            int count = 1;
            SolidColorBrush combinationFill = null;
            SolidColorBrush combinationStroke = null;
            double combinationStrokeThickness = 0;
            CombinedGeometry combination = new CombinedGeometry();
            List<Point> ListIntersection = new List<Point>(); List<Point> Resultat = new List<Point>();
            List<MyStruct> ListA, ListB;
            MyComplex MyRes; Path PathRes = null;
            List<Path> ListPath = new List<Path>();
            ListIntersection = GetintersectionPointsOf2POLYGONS(A, B);
            int NbIntersection = ListIntersection.Count;
            if ( (NbIntersection == 0) && (polygone_int_polygone(A, B) == null)  )
            {
                MessageBox.Show("Les deux polygones ne se croisent pas -Combinaison impossible-");
                return null;    
            }
            else
            {
                foreach (Element t in store)
                {
                    Path s = t.obj;
                    Path path = s as Path;
                    Geometry geometry = path.Data as Geometry;

                    if (count == 1)
                    {
                        combination.Geometry1 = geometry;                       // recuperer le premier element selectionner et l'ajouter a la combination 
                        combinationFill = s.Fill as SolidColorBrush;
                        combinationStroke = s.Stroke as SolidColorBrush;
                        combinationStrokeThickness = s.StrokeThickness;
                    }

                    if (count == 2)
                    {
                        combination.Geometry2 = geometry;                       // recuperer le deuxieme element selectionner et l'ajouter a la combination
                    }
                    count++;
                }

                combination.GeometryCombineMode = GeometryCombineMode.Union;

                double x = Math.Round(combination.Bounds.X); double y = Math.Round(combination.Bounds.Y);
                if (double.IsNegativeInfinity(x) || double.IsPositiveInfinity(x) || double.IsNaN(x) || double.IsNegativeInfinity(y) || double.IsPositiveInfinity(y) || double.IsNaN(y))
                {
                    throw (new ArgumentException("Cette Combination est impossible ! "));
                }
                myPath.Fill = combinationFill;
                myPath.Stroke = combinationStroke;
                myPath.StrokeThickness = combinationStrokeThickness;
                myPath.Data = combination;
                MyComplex res = new MyComplex(PathToPoints(myPath), combinationFill, combinationStroke);
                AddToEnv(res, myPath);
                return myPath;
            }
                
        }
        public List<Path> Soustraction(List<Element> store)
        {
            MyPolygon A = new MyPolygon(); MyPolygon B = new MyPolygon();
            A = store[0].p; B = store[1].p;
            Path myPath = new Path();
            int count = 1;
            SolidColorBrush combinationFill = null;
            SolidColorBrush combinationStroke = null;
            double combinationStrokeThickness = 0;
            CombinedGeometry combination = new CombinedGeometry();
           
            List<Point> ListIntersection = new List<Point>(); List<Point> Resultat = new List<Point>();
            List<MyStruct> ListA, ListB;
            List<Path> ResultatReturn = new List<Path>(); 
            MyComplex MyRes; 
           
            ListIntersection = GetintersectionPointsOf2POLYGONS(A, B);
            int NbIntersection = ListIntersection.Count;
            if ((NbIntersection == 0) && (polygone_int_polygone(A,B) == null) )
            {
               MessageBox.Show("Les deux polygones ne se croisent pas -Combinaison impossible-");
               return null;
            }
            else
            {
                foreach (Element t in store)
                {
                    Path s = t.obj;
                    Path path = s as Path;
                    Geometry geometry = path.Data as Geometry; 

                    if (count == 1)
                    {
                        combination.Geometry1 = geometry;                      
                        combinationFill = s.Fill as SolidColorBrush;
                        combinationStroke = s.Stroke as SolidColorBrush;
                        combinationStrokeThickness = s.StrokeThickness;
                    }

                    if (count == 2)
                    {
                        combination.Geometry2 = geometry;                       
                    }
                    count++;
                }

                combination.GeometryCombineMode = GeometryCombineMode.Exclude;

                double x = Math.Round(combination.Bounds.X);                                        
                double y = Math.Round(combination.Bounds.Y);
                if (double.IsNegativeInfinity(x) || double.IsPositiveInfinity(x) || double.IsNaN(x) || double.IsNegativeInfinity(y) || double.IsPositiveInfinity(y) || double.IsNaN(y))
                {
                    throw (new ArgumentException("Cette combinaison est impossible"));
                }
                myPath = new Path();
                myPath.Fill = combinationFill;
                myPath.Stroke = combinationStroke;
                myPath.StrokeThickness = combinationStrokeThickness;
                myPath.Data = combination;
               
                
                if (NbIntersection == 0) // Polygone a l'interieur d'un autre 
                {
                    MyComplex res = new MyComplex(PathToPoints(myPath), combinationFill, combinationStroke);
                    AddToEnv(res, myPath);
                    ResultatReturn.Add(myPath);
                }
                else
                {
                    List<Path> list = SeparatePathComplex(myPath);
                    foreach (var l in list)
                    {
                        ResultatReturn.Add(l);
                    }
                }
                
                return ResultatReturn;
            }    
        }
        public List<Path> Intersection(List<Element> store)
        {
            MyPolygon A = new MyPolygon(); MyPolygon B = new MyPolygon();
            A = store[0].p; B = store[1].p;
            Path myPath = new Path();
            int count = 1;                                                     
            SolidColorBrush combinationFill = null;
            SolidColorBrush combinationStroke = null;
            double combinationStrokeThickness = 0;
            CombinedGeometry combination = new CombinedGeometry();
            List<Path> ResultatReturn = new List<Path>();
            List<Point> ListIntersection = new List<Point>(); List<Point> Resultat = new List<Point>();
           

            ListIntersection = GetintersectionPointsOf2POLYGONS(A, B);
            int NbIntersection = ListIntersection.Count;
            if ((NbIntersection == 0) && (polygone_int_polygone(A, B) == null) )
            {
                MessageBox.Show("Les deux polygones ne se croisent pas -Combinaison impossible-");
                return null;
            }
            else
            {
                foreach (Element t in store)
                {
                    Path s = t.obj;
                    Path path = s as Path;
                    Geometry geometry = path.Data as Geometry;
                    if (count == 1)
                    {
                        combination.Geometry1 = geometry;
                        combinationFill = s.Fill as SolidColorBrush;
                        combinationStroke = s.Stroke as SolidColorBrush;
                        combinationStrokeThickness = s.StrokeThickness;
                    }

                    if (count == 2)
                    {
                        combination.Geometry2 = geometry;
                    }
                    count++;
                }
                combination.GeometryCombineMode = GeometryCombineMode.Intersect;
                double x = Math.Round(combination.Bounds.X);
                double y = Math.Round(combination.Bounds.Y);
                if (double.IsNegativeInfinity(x) || double.IsPositiveInfinity(x) || double.IsNaN(x) || double.IsNegativeInfinity(y) || double.IsPositiveInfinity(y) || double.IsNaN(y))
                {
                    throw (new ArgumentException(" Cette combinaison n'est pas possible"));
                }
                myPath = new Path();
                myPath.Fill = combinationFill;
                myPath.Stroke = combinationStroke;
                myPath.StrokeThickness = combinationStrokeThickness;
                myPath.Data = combination;
                
                List < Path > listsep = SeparatePathComplex(myPath);
                foreach (var l in listsep)
                {
                    ResultatReturn.Add(l);
                }

                return ResultatReturn ;
            }
        }

            



    }
}
