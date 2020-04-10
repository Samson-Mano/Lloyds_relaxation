using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lloyds_relaxation
{
    class Planar_object_store// This is a sattelite class to store and control all the drawing ojects
    {
        List<point2d> _delaunay_points = new List<point2d>(); // List of point object to store all the points in the drawing area
        List<edge2d> _delaunay_edges = new List<edge2d>(); // List of edge object to store all the edges created from Delaunay triangulation
        List<face2d> _delaunay_faces = new List<face2d>(); // List of face object to store all the faces created from Delaunay triangulation

        List<point2d> _voronoi_points = new List<point2d>(); // List of point object to store all the points in the drawing area
        List<edge2d> _voronoi_edges = new List<edge2d>(); // List of edge object to store all the edges created from voronoi 
        List<polygon2d> _voronoi_poly = new List<polygon2d>(); // List of polygon object to store all the polygons created from voronoi

        public List<point2d> delaunay_points
        {
            set { this._delaunay_points = value; }
            get { return this._delaunay_points; }
        }

        public List<edge2d> delaunay_edges
        {
            set { this._delaunay_edges = value; }
            get { return this._delaunay_edges; }
        }

        public List<face2d> delaunay_faces
        {
            set { this._delaunay_faces = value; }
            get { return this._delaunay_faces; }
        }


        public List<point2d> voronoi_points
        {
            set { this._voronoi_points = value; }
            get { return this._voronoi_points; }
        }

        public List<edge2d> voronoi_edges
        {
            set { this._voronoi_edges = value; }
            get { return this._voronoi_edges; }
        }

        public List<polygon2d> voronoi_polygon
        {
            set { this._voronoi_poly = value; }
            get { return this._voronoi_poly; }
        }

        public Planar_object_store()
        {
            // Empty constructor used to initialize and re-intialize
            this._delaunay_points = new List<point2d>();
            this._delaunay_edges = new List<edge2d>();

        }

        public void paint_me(ref Graphics gr1)
        {
            Graphics gr0 = gr1;

            //Pen delaunay_edge_pen = new Pen(Color.DarkOrange, 1);
            //delaunay_edges.ForEach(obj => obj.paint_me(ref gr0, ref delaunay_edge_pen)); // Paint the edges
            //Pen triangle_pen = new Pen(Color.LightGreen, 1);
            //delaunay_faces.ForEach(obj => obj.paint_me(ref gr0, ref triangle_pen)); // Paint the faces

            Pen delaunay_pt_pen = new Pen(Color.BlueViolet, 2);
            delaunay_points.ForEach(obj => obj.paint_me(ref gr0, ref delaunay_pt_pen)); // Paint the points

            Pen voronoi_edge_pen = new Pen(Color.DarkGreen, 1);
            voronoi_edges.ForEach(obj => obj.paint_me(ref gr0, ref voronoi_edge_pen)); // Paint the edges

            Pen voronoi_pt_pen = new Pen(Color.Crimson, 2);
            voronoi_points.ForEach(obj => obj.paint_me(ref gr0, ref voronoi_pt_pen)); // Paint the points

            Pen voronoi_poly_pen = new Pen(Color.LightSkyBlue, 2);
            voronoi_polygon.ForEach(obj => obj.paint_me(ref gr0, ref voronoi_poly_pen)); // Paint the polygon
        }

        public class point2d // class to store the points
        {
            int _id;
            double _x;
            double _y;

            public int id
            {
                get { return this._id; }
            }

            public double x
            {
                get { return this._x; }
            }
            public double y
            {
                get { return this._y; }
            }
            public point2d(int i_id, double i_x, double i_y)
            {
                // constructor 1
                this._id = i_id;
                this._x = i_x;
                this._y = i_y;
            }

            public void paint_me(ref Graphics gr0, ref Pen pt_pen) // this function is used to paint the points
            {
                gr0.FillEllipse(pt_pen.Brush, new RectangleF(get_point_for_ellipse(), new SizeF(4, 4)));

                //if (false)
                //{
                //    string my_string = (this.id + 1).ToString() + "(" + this._x.ToString("F2") + ", " + this._y.ToString("F2") + ")";
                //    SizeF str_size = gr0.MeasureString(my_string, new Font("Cambria", 6)); // Measure string size to position the dimension

                //    gr0.DrawString(my_string, new Font("Cambria", 6),
                //                                                       new Pen(Color.DarkBlue, 2).Brush,
                //                                                       get_point_for_ellipse().X + 3 + Form1.the_static_class.to_single(-str_size.Width * 0.5),
                //                                                       Form1.the_static_class.to_single(str_size.Height * 0.5) + get_point_for_ellipse().Y + 3);
                //}
            }

            public PointF get_point_for_ellipse()
            {
                // y axis is flipped here
                return (new PointF(Form1.the_static_class.to_single(this._x) - 2,
               Form1.the_static_class.to_single((-1 * this._y) - 2))); // return the point as PointF as edge of an ellipse
            }

            public PointF get_point()
            {
                // y axis is flipped here
                return (new PointF(Form1.the_static_class.to_single(this._x),
               Form1.the_static_class.to_single((-1 * this._y)))); // return the point as PointF as edge of an ellipse
            }

            public bool Equals(point2d other)
            {
                return (this._x == other.x && this._y == other.y); // Equal function is used to check the uniqueness of the points added
            }
        }

        public class points_equality_comparer : IEqualityComparer<point2d>
        {
            public bool Equals(point2d a, point2d b)
            {
                return (a.Equals(b));
            }

            public int GetHashCode(point2d other)
            {
                return (other.x.GetHashCode() * 17 + other.y.GetHashCode() * 19);
                // 17,19 are just ranfom prime numbers
            }
        }

        public class edge2d
        {
            int _edge_id;
            point2d _start_pt;
            point2d _end_pt;
            point2d _mid_pt; // not stored in point list

            public point2d start_pt
            {
                get { return this._start_pt; }
            }

            public point2d end_pt
            {
                get { return this._end_pt; }
            }

            public point2d mid_pt
            {
                get { return this._mid_pt; }
            }

            public edge2d(int i_edge_id, point2d i_start_pt, point2d i_end_pt)
            {
                // constructor 1
                this._edge_id = i_edge_id;
                this._start_pt = i_start_pt;
                this._end_pt = i_end_pt;
                this._mid_pt = new point2d(-1, (i_start_pt.x + i_end_pt.x) * 0.5, (i_start_pt.y + i_end_pt.y) * 0.5);
            }

            public void paint_me(ref Graphics gr0, ref Pen edge_pen) // this function is used to paint the points
            {
                //Pen edge_pen = new Pen(Color.DarkOrange, 1);

                gr0.DrawLine(edge_pen, start_pt.get_point(), end_pt.get_point());

                //System.Drawing.Drawing2D.AdjustableArrowCap bigArrow = new System.Drawing.Drawing2D.AdjustableArrowCap(3, 3);
                //edge_pen.CustomEndCap = bigArrow;
                //gr0.DrawLine(edge_pen, start_pt.get_point(), mid_pt.get_point());
            }

            public bool Equals(edge2d other)
            {
                return (other.start_pt.Equals(this._start_pt) && other.end_pt.Equals(this._end_pt));
            }

        }

        public class face2d
        {
            int _face_id;
            point2d _p1;
            point2d _p2;
            point2d _p3;
            point2d _mid_pt;
            double shrink_factor = 0.6f; // in Circle

            point2d _circle_center;
            double _circle_radius;
            point2d _ellipse_edge;

            public int face_id
            {
                get { return this._face_id; }
            }

            public PointF get_p1
            {
                get
                {
                    return new PointF(Form1.the_static_class.to_single(_mid_pt.get_point().X * (1 - shrink_factor) + (_p1.get_point().X * shrink_factor)),
                                       Form1.the_static_class.to_single(_mid_pt.get_point().Y * (1 - shrink_factor) + (_p1.get_point().Y * shrink_factor)));
                }
            }

            public PointF get_p2
            {
                get
                {
                    return new PointF(Form1.the_static_class.to_single(_mid_pt.get_point().X * (1 - shrink_factor) + (_p2.get_point().X * shrink_factor)),
                                      Form1.the_static_class.to_single(_mid_pt.get_point().Y * (1 - shrink_factor) + (_p2.get_point().Y * shrink_factor)));
                }
            }

            public PointF get_p3
            {
                get
                {
                    return new PointF(Form1.the_static_class.to_single(_mid_pt.get_point().X * (1 - shrink_factor) + (_p3.get_point().X * shrink_factor)),
                                      Form1.the_static_class.to_single(_mid_pt.get_point().Y * (1 - shrink_factor) + (_p3.get_point().Y * shrink_factor)));
                }
            }

            public face2d(int i_face_id, point2d i_p1, point2d i_p2, point2d i_p3, point2d i_cpt, double i_radius)
            {
                this._face_id = i_face_id;
                this._p1 = i_p1;
                this._p2 = i_p2;
                this._p3 = i_p3;
                this._mid_pt = new point2d(-1, (i_p1.x + i_p2.x + i_p3.x) / 3, (i_p1.y + i_p2.y + i_p3.y) / 3);

                // set circle
                this._circle_center = new point2d(-1, i_cpt.x, i_cpt.y);
                this._circle_radius = i_radius;
                this._ellipse_edge = new point2d(-1, i_cpt.x - this._circle_radius, i_cpt.y + this._circle_radius);
            }

            public void paint_me(ref Graphics gr0, ref Pen triangle_pen) // this function is used to paint the points
            {

                if (true)
                {
                    PointF[] curve_pts = { get_p1, get_p2, get_p3 };
                    gr0.FillPolygon(triangle_pen.Brush, curve_pts); // Fill the polygon

                    //if (false)
                    //{
                    //    string my_string = this._face_id.ToString();
                    //    SizeF str_size = gr0.MeasureString(my_string, new Font("Cambria", 6)); // Measure string size to position the dimension

                    //    gr0.DrawString(my_string, new Font("Cambria", 6), new Pen(Color.DeepPink, 2).Brush, this._mid_pt.get_point());

                    //}
                }


                // gr0.FillEllipse(triangle_pen.Brush, new RectangleF(_circle_center.get_point_for_ellipse(), new SizeF(4, 4)));


            }
        }

        public class polygon2d
        {
            int _poly_id;
            List<point2d> _poly_pts = new List<point2d>();
            private List<PointF> get_poly_pts = new List<PointF>();
            point2d _mid_pt;
            double shrink_factor = 0.6f;

            public int poly_id
            {
                get { return this._poly_id; }
            }

            public point2d mid_pt
            {
                get { return this._mid_pt; }
            }

            public polygon2d(int i_poly_id, List<point2d> i_poly_pt)
            {
                this._poly_id = i_poly_id;  // assign the id
                this._mid_pt = findCentroid(i_poly_pt);
                this._poly_pts = i_poly_pt.OrderBy(obj => Math.Atan2(obj.x - this.mid_pt.x, obj.y - this.mid_pt.y)).ToList();

                foreach (point2d pt in this._poly_pts)
                {
                    get_poly_pts.Add(new PointF(Form1.the_static_class.to_single(_mid_pt.get_point().X * (1 - shrink_factor) + (pt.get_point().X * shrink_factor)),
                                                Form1.the_static_class.to_single(_mid_pt.get_point().Y * (1 - shrink_factor) + (pt.get_point().Y * shrink_factor))));
                }
            }

            public point2d findCentroid(List<point2d> points)
            {
                double x = 0;
                double y = 0;
                foreach (point2d p in points)
                {
                    x += p.x;
                    y += p.y;
                }

                double center_x = x / points.Count();
                double center_y = y / points.Count();

                return new point2d(-1, center_x, center_y);
            }

            public void paint_me(ref Graphics gr0, ref Pen triangle_pen) // this function is used to paint the points
            {
                //gr0.FillPolygon(triangle_pen.Brush, get_poly_pts.ToArray()); // Fill the polygon
                gr0.DrawPolygon(triangle_pen, get_poly_pts.ToArray());
            }
        }


    }
}
