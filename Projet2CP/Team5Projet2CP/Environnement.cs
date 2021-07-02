using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;
using Path = System.Windows.Shapes.Path;
using System.IO;


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
    class Forpile
    {
       public int i=0;
       public double x=0;
       public double y=0;
       public double angle=0;
       public double rayon=0;
       public String name=null;
       public SolidColorBrush CouleurFill=null;
       public SolidColorBrush CouleurStroke=null;
       public List<Point> points=null;
       public int comb = 0;
       public Forpile() { }
       public Forpile(int i,double rayon, List<Point> points)
       {
            this.i = i;
            this.rayon = rayon;
            this.points = points;
       }
       public Forpile(int comb,int i)
       {
            this.i = i;
            this.comb =comb;
            
       }
       public Forpile(int i, double x, double y)
        {
            this.i = i;
            this.x = x;
            this.y = y;            
        }
        public Forpile(int i, double angle)
        {
            this.i = i;
            this.angle = angle; 
        }
        public Forpile(int i, String name)
        {
            this.i = i;
            this.name = name;
        }
        public Forpile(int i, SolidColorBrush CouleurFill, SolidColorBrush CouleurStroke)
        {
            this.i = i;
            this.CouleurFill = CouleurFill;
            this.CouleurStroke = CouleurStroke;
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
        public List<Element> Env = new List<Element>();
        public Stack<List<Element>> Arriere = new Stack<List<Element>>();
        public Stack<List<Element>> Apres = new Stack<List<Element>>();
        public Stack<Forpile> ArrierePile = new Stack<Forpile>();
        public Stack<Forpile> ApresPile = new Stack<Forpile>();
        public List<String> BilblioListe = new List<string>();

        public void AddToEnv(MyPolygon p, Path obj) // Ajouter a la list d'environnement 
        {
            List<Element> e = new List<Element>();
            foreach (var item in Env)
            {
                e.Add(item);
            }
            Arriere.Push(e);
            Forpile f = new Forpile();
            ArrierePile.Push(f);;
            Env.Add(new Element(p, obj));
        }
        public void change(Forpile f)
        {
            ArrierePile.Push(f);
        }

        public void change2(Forpile f)
        {
            ApresPile.Push(f);
        }
        public void Back()
        {
            Forpile f;
            if (Arriere.Count != 0)
            {
                List<Element> e = new List<Element>();
                foreach (var item in Env)
                {
                    e.Add(item);
                }
                f= ArrierePile.Pop();
                if(f.comb==0)
                {
                    if (f.x == 0 && f.y == 0 && f.angle == 0 && f.rayon == 0 && f.CouleurFill==null && f.CouleurStroke==null && f.name==null)
                    {
                        Apres.Push(Env);
                        Env = Arriere.Pop();
                        Forpile fp = new Forpile();
                        change2(fp);
                    }
                    else
                    {

                        int indx = f.i;
                        MyPolygon pi = GetMyPolygon(indx);
                        if (f.angle == 0)
                        {
                            Forpile fp = new Forpile(indx, f.x, f.y);
                            change2(fp);
                            pi.Deplacer(-f.x, -f.y);
                        }
                            
                        else
                        {
                            if (f.angle != 0)
                            {
                                Forpile fp = new Forpile(indx, f.angle);
                                change2(fp);
                                pi.Rotation(-f.angle);
                            }

                        }
                        if (f.rayon != 0)
                        {
                            Forpile fp = new Forpile(indx, pi.rayon, pi.GetPoints());
                            change2(fp);
                            pi.SetPoints(f.points);
                            pi.rayon = f.rayon;

                        }
                        if (f.CouleurFill!=null)
                        {
                            Forpile fp = new Forpile(indx, pi.GetFill(), pi.GetStroke());
                            change2(fp);
                            pi.SetFill(f.CouleurFill);
                            pi.SetStroke(f.CouleurStroke);
                        }
                        if(f.name!=null)
                        {
                            Forpile fp = new Forpile(indx, pi.GetName());
                            change2(fp);
                            pi.SetName(f.name);

                        }                
                        Path obj = pi.Draw();
                        SetChamp(indx, pi, obj);
                    }
 
                }
                else
                {
                    Forpile fp = new Forpile( f.comb, Env.Count);
                    change2(fp);
                   
                    if(f.comb==1)
                    {
                        
                        
                        Apres.Push(Env);
                        Env = Arriere.Pop();
                        Apres.Push(Env);
                        Env = Arriere.Pop();
                        Apres.Push(Env);
                        Env = Arriere.Pop();

                    }
                    else
                    {

                        Apres.Push(Env);
                        Env = Arriere.Pop();
                        Apres.Push(Env);
                        Env = Arriere.Pop();
                        for (int i=0;i<f.comb;i++)
                        {
                            Apres.Push(Env);
                            Env = Arriere.Pop();
                        }
                    }

                }
                
                
            }
        }    
         public void After()
         {
            Forpile f;
            if(ApresPile.Count!=0)
            {
                f = ApresPile.Pop();
                if (f.comb == 0)
                {
                    if (f.x == 0 && f.y == 0 && f.angle == 0 && f.rayon == 0 && f.CouleurFill == null && f.CouleurStroke == null && f.name == null)
                    {
                        Arriere.Push(Env);
                        Env = Apres.Pop();
                        Forpile fp = new Forpile();
                        change(fp);
                    }
                    else
                    {

                        int indx = f.i;
                        MyPolygon pi = GetMyPolygon(indx);
                        if (f.angle == 0)
                        {
                            Forpile fp = new Forpile(indx, f.x, f.y);
                            change(fp);
                            pi.Deplacer(f.x, f.y);
                        }

                        else
                        {
                            if (f.angle != 0)
                            {
                                Forpile fp = new Forpile(indx, f.angle);
                                change(fp);
                                pi.Rotation(f.angle);
                            }
                        }
                        if (f.rayon != 0)
                        {
                            Forpile fp = new Forpile(indx, pi.rayon, pi.GetPoints());
                            change(fp);
                            pi.SetPoints(f.points);
                            pi.rayon = f.rayon;
                        }
                        if (f.CouleurFill != null)
                        {
                            Forpile fp = new Forpile(indx, pi.GetFill(), pi.GetStroke());
                            change(fp);
                            pi.SetFill(f.CouleurFill);
                            pi.SetStroke(f.CouleurStroke);
                        }
                        if (f.name != null)
                        {
                            Forpile fp = new Forpile(indx, pi.GetName());
                            change(fp);
                            pi.SetName(f.name);

                        }


                        Path obj = pi.Draw();
                        SetChamp(indx, pi, obj);
                    }

                }
                else
                {
                    Forpile fp = new Forpile(f.comb, Env.Count);
                    change(fp);
                    if (f.comb == 1)
                    {


                        Arriere.Push(Env);
                        Env = Apres.Pop();
                        Arriere.Push(Env);
                        Env = Apres.Pop();
                        Arriere.Push(Env);
                        Env = Apres.Pop();

                    }
                    else
                    {

                        Arriere.Push(Env);
                        Env = Apres.Pop();
                        Arriere.Push(Env);
                        Env = Apres.Pop();
                        for (int i = 0; i < f.comb; i++)
                        {
                            Arriere.Push(Env);
                            Env = Apres.Pop();
                        }

                    }

                }
            }
 
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
                List<Element> e = new List<Element>();
                foreach (var item in Env)
                {
                    e.Add(item);
                }
                Arriere.Push(e);;
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
            return -1;
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
                AddToEnv(elem.p, elem.obj);
                //Env.Add(elem); 
                res.Add(elem.obj);
            }
            return res; 
        }
        public Path Union(List<Element> store)
        {
            MyPolygon A = new MyPolygon(); MyPolygon B = new MyPolygon();
            A = store[0].p; B = store[1].p;
            Path myPath = new Path();
            int count = 1 , indexa , indexb ; bool CanFind = false; 
            Point PDepart; MyStruct c;
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
                
                if (CanFind)
                {
                    // Remplir les list de structures 
                    ListA = RemplirStructure(A.GetPoints(), ListIntersection);
                    ListB = RemplirStructure(B.GetPoints(), ListIntersection);

                    indexa = IndexPointDepart(ListA, B);
                    if (indexa != -1)
                    {
                        PDepart = ListA[indexa].pnt; ListA[indexa].visite = true;
                        Resultat.Clear();
                        Resultat.Add(PDepart);
                        c = SuivantMyStruct(ListA, ref indexa);
                        do
                        {
                            Resultat.Add(c.pnt); ListA[indexa].visite = true;
                            if (c.intersct)
                            {
                                indexb = RechercheMyStruct(ListB, c.pnt);
                                do
                                {
                                    c = SuivantMyStruct(ListB, ref indexb);
                                    if (c.intersct != true) { Resultat.Add(c.pnt); }
                                } while (c.intersct != true);
                                indexa = RechercheMyStruct(ListA, c.pnt);
                                Resultat.Add(c.pnt);
                                c = SuivantMyStruct(ListA, ref indexa);
                            }
                            else //c.pnt n'est pas un point d'intersection 
                            {
                                c = SuivantMyStruct(ListA, ref indexa);
                                ListA[indexa].visite = true;
                            }
                        } while (c.pnt != PDepart);
                        // enregistrer notre list de point obtenu 
                        MyRes = new MyComplex(Resultat, A.GetFill(), A.GetStroke()); PathRes = MyRes.Draw();

                        Env.Add(new Element(MyRes, PathRes));
                    }

                }
                else
                {
                    SolidColorBrush combinationFill = null;
                    SolidColorBrush combinationStroke = null;
                    double combinationStrokeThickness = 0;
                    CombinedGeometry combination = new CombinedGeometry();
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
                            combinationStrokeThickness =1;
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
                   
                }
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
                    throw (new ArgumentException("-Combinaison impossible-"));
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

        public String pntlistTOString(List<Point> points, string Filename)
        {
            List<String> pntString = new List<string>();
            foreach (Point pnt in points)
            {
                pntString.Add(pnt.ToString());
            }
            String result = String.Join("/", pntString.ToArray());
            File.WriteAllText(Filename, result);
            return result;
        }

        public List<Point> strpntTolist(string points)
        {
            List<Point> listepnt = new List<Point>();
            //string readText = File.ReadAllText(Filename);  // Read the contents of the file
            string[] tokens = points.Split('/');
            foreach (String str in tokens)
            {
                string[] coords = str.Split(';');
                Point pnt = new Point(Convert.ToDouble(coords[0]), Convert.ToDouble(coords[1]));
                listepnt.Add(pnt);

            }
            return listepnt;

        }
        public String savepolyselected(String Filename, MyPolygon SelectedMyPolygon)
        {
            String nomstr = SelectedMyPolygon.GetName();
            String rayonstr = SelectedMyPolygon.GetRayon().ToString();
            String centrestr = SelectedMyPolygon.GetCentre().ToString();
            String fillstr = SelectedMyPolygon.GetFill().ToString();
            String strokelstr = SelectedMyPolygon.GetStroke().ToString();
            String pointstr = pntlistTOString(SelectedMyPolygon.GetPoints(), Filename);
            List<String> elementString = new List<string>() { nomstr, rayonstr, centrestr, fillstr, strokelstr, pointstr };

            String result = String.Join("-", elementString.ToArray());
            return result;
            //File.AppendAllText(Filename, result);
            //File.WriteAllText(Filename,  result+Environment.NewLine);
        }

        public MyPolygon retorePolySelected(String readText)
        {
            // string readText = File.ReadAllText(Filename);  // Read the contents of the file
            string[] tokens = readText.Split('-');
            string name = tokens[0];
            float rayon = float.Parse(tokens[1]);
            string[] coords = tokens[2].Split(';');
            Point centre = new Point(Convert.ToDouble(coords[0]), Convert.ToDouble(coords[1]));
            List<Point> points = strpntTolist(tokens[5]);
            Color cf = (Color)ColorConverter.ConvertFromString(tokens[3]);
            SolidColorBrush fill = new SolidColorBrush(cf);
            Color cs = (Color)ColorConverter.ConvertFromString(tokens[4]);
            SolidColorBrush stroke = new SolidColorBrush(cs);

            MyPolygon p = new MyPolygon(points, rayon, points.Count, centre, fill, stroke);
            p.SetName(name);  
            return p;
            
        }

        public List<String> saveEnvirnment(List<Element> elements)
        {
            string str;
            foreach (Element element in elements)
            {
                str = savepolyselected("biblio.txt", element.p);
                BilblioListe.Add(str);
            }
            return BilblioListe;
        }

        public void restorEnvirnment(string filename)
        {
            List<string> ptab = File.ReadAllLines(filename).ToList();
            int i = 0;
            Env.Clear();
            foreach (var ptabstr in ptab)
            {
                MyPolygon p = retorePolySelected(ptabstr);
                Path obj = p.Draw();
                Env.Add(new Element(p, obj));
            }
        }





    }
}
