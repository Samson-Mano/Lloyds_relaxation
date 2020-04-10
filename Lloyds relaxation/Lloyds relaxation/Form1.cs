using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Delaunay_triangulation;

namespace Lloyds_relaxation
{
    public partial class Form1 : Form
    {
        Planar_object_store delaunay_triangle;
        Random rand0 = new Random();
        int iteration = 0;

        public Form1()
        {           InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initiate_canvas_size();

            delaunay_triangle = new Planar_object_store(); // intialize the main drawing object
        }

        public void initiate_canvas_size()
        {
            // set canvas size and canvas orgin when the form loads and form size changes
            the_static_class.canvas_size = new SizeF(the_static_class.to_single(main_pic.Width * 0.8), the_static_class.to_single(main_pic.Height * 0.8));
            the_static_class.canvas_orgin = new PointF(the_static_class.to_single(main_pic.Width * 0.5), the_static_class.to_single(main_pic.Height * 0.5));
            mt_pic.Refresh(); // Refresh the paint region
        }

        private void main_pic_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; // Paint high quality
            e.Graphics.TranslateTransform(the_static_class.canvas_orgin.X, the_static_class.canvas_orgin.Y); // Translate transform to make the orgin as center
            e.Graphics.ScaleTransform(the_static_class.to_single(1.0f), the_static_class.to_single(1.0f));

            Graphics gr1 = e.Graphics;

            // Draw orgin
            e.Graphics.DrawEllipse(new Pen(Color.Black, 1), -2, -2, 4, 4);

            delaunay_triangle.paint_me(ref gr1);
        }

        private void main_pic_SizeChanged(object sender, EventArgs e)
        {
            initiate_canvas_size();
        }

        private void button_generate_surface_Click(object sender, EventArgs e)
        {
            // Generate Random points and voronoi diagram
            int pt_count = 600; //Change the number of points here
            int x_coord_limit = (int)(the_static_class.canvas_size.Width * 0.5);
            int y_coord_limit = (int)(the_static_class.canvas_size.Height * 0.5);

            iteration = 0;
            generate_random_points(pt_count, x_coord_limit, y_coord_limit); // Generate 10 Random points when the form loads

            // start of the delaunay triangulation 
            delaunay_triangle.delaunay_edges = new List<Planar_object_store.edge2d>(); // reinitialize the edge lists
            delaunay_triangle.delaunay_faces = new List<Planar_object_store.face2d>();
            delaunay_triangle.voronoi_points = new List<Planar_object_store.point2d>();
            delaunay_triangle.voronoi_edges = new List<Planar_object_store.edge2d>();
            delaunay_triangle.voronoi_polygon = new List<Planar_object_store.polygon2d>();

            List<Planar_object_store.edge2d> temp_edges = new List<Planar_object_store.edge2d>();
            List<Planar_object_store.face2d> temp_faces = new List<Planar_object_store.face2d>();

            List<Planar_object_store.point2d> temp_v_points = new List<Planar_object_store.point2d>();
            List<Planar_object_store.edge2d> temp_v_edges = new List<Planar_object_store.edge2d>();
            List<Planar_object_store.polygon2d> temp_v_polygons = new List<Planar_object_store.polygon2d>();

            // Delaunay Triangulation and Voronoi tesselation
            (new delaunay_triangulation_divide_n_conquer()).delaunay_start(delaunay_triangle.delaunay_points, x_coord_limit, y_coord_limit, ref temp_edges, ref temp_faces, ref temp_v_points, ref temp_v_edges, ref temp_v_polygons);


            delaunay_triangle.delaunay_edges = temp_edges;
            delaunay_triangle.delaunay_faces = temp_faces;

            delaunay_triangle.voronoi_points = temp_v_points;
            delaunay_triangle.voronoi_edges = temp_v_edges;
            delaunay_triangle.voronoi_polygon = temp_v_polygons;

            iteration++;

            mt_pic.Refresh();// Refresh the paint region
        }

        private void button_lloyds_Click(object sender, EventArgs e)
        {
            // Lloyd's relaxation
            if (iteration > 0)
            {
                while (iteration < 20)
                {
                    int x_coord_limit = (int)(the_static_class.canvas_size.Width * 0.5);
                    int y_coord_limit = (int)(the_static_class.canvas_size.Height * 0.5);

                    delaunay_triangle.delaunay_points = new List<Planar_object_store.point2d>();

                    // Delaunay Boundary points
                    int pt_id = delaunay_triangle.delaunay_points.Count;
                    foreach (Planar_object_store.polygon2d poly in delaunay_triangle.voronoi_polygon)
                    {
                        Planar_object_store.point2d pt = new Planar_object_store.point2d(pt_id, poly.mid_pt.x, poly.mid_pt.y);
                        delaunay_triangle.delaunay_points.Add(pt);
                        pt_id++;
                    }

                    // start of the delaunay triangulation 
                    delaunay_triangle.delaunay_edges = new List<Planar_object_store.edge2d>(); // reinitialize the edge lists
                    delaunay_triangle.delaunay_faces = new List<Planar_object_store.face2d>();
                    delaunay_triangle.voronoi_points = new List<Planar_object_store.point2d>();
                    delaunay_triangle.voronoi_edges = new List<Planar_object_store.edge2d>();
                    delaunay_triangle.voronoi_polygon = new List<Planar_object_store.polygon2d>();

                    List<Planar_object_store.edge2d> temp_edges = new List<Planar_object_store.edge2d>();
                    List<Planar_object_store.face2d> temp_faces = new List<Planar_object_store.face2d>();

                    List<Planar_object_store.point2d> temp_v_points = new List<Planar_object_store.point2d>();
                    List<Planar_object_store.edge2d> temp_v_edges = new List<Planar_object_store.edge2d>();
                    List<Planar_object_store.polygon2d> temp_v_polygons = new List<Planar_object_store.polygon2d>();

                    // Delaunay Triangulation and voronoi tesselation
                    (new delaunay_triangulation_divide_n_conquer()).delaunay_start(delaunay_triangle.delaunay_points, x_coord_limit, y_coord_limit, ref temp_edges, ref temp_faces, ref temp_v_points, ref temp_v_edges, ref temp_v_polygons);

                    delaunay_triangle.delaunay_edges = temp_edges;
                    delaunay_triangle.delaunay_faces = temp_faces;

                    delaunay_triangle.voronoi_points = temp_v_points;
                    delaunay_triangle.voronoi_edges = temp_v_edges;
                    delaunay_triangle.voronoi_polygon = temp_v_polygons;

                    iteration++;

                    mt_pic.Refresh();// Refresh the paint region
                }
                iteration = 2;
            }
        }

        public void generate_random_points(int inpt_point_count, int x_coord_limit, int y_coord_limit)
        {
            delaunay_triangle = new Planar_object_store(); // reinitialize the all the lists

            List<Planar_object_store.point2d> temp_pt_list = new List<Planar_object_store.point2d>(); // create a temporary list to store the points
                                                                                                      // !!!!!!!!!!!! Need major improvements below - very slow to generate unique n random points !!!!!!!!!!!!!!!!!!!!!!!!!!!
            int point_count = inpt_point_count;
            do
            {
                for (int i = 0; i < point_count; i++) // Loop thro' the point count
                {

                    Planar_object_store.point2d temp_pt; // temp_pt to store the intermediate random points
                    PointF rand_pt = new PointF(rand0.Next(-x_coord_limit, x_coord_limit),
                                 rand0.Next(-y_coord_limit, y_coord_limit));
                    temp_pt = new Planar_object_store.point2d(i, rand_pt.X, rand_pt.Y);
                    temp_pt_list.Add(temp_pt); // add to the temp list
                }

                temp_pt_list = temp_pt_list.Distinct(new Planar_object_store.points_equality_comparer()).ToList();


                point_count = inpt_point_count - temp_pt_list.Count;

            } while (point_count != 0);

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // copy to the main list
            delaunay_triangle.delaunay_points = temp_pt_list;
            // List<PointF> temp_rand_pts = Enumerable.Range(0,point_count).Select(obj => the_static_class.random_point(-x_coord_limit, x_coord_limit,-y_coord_limit, y_coord_limit)).ToList();
        }

        public void add_boundary_pts(int inpt_point_count, int pt_index, int x_coord_limit, int y_coord_limit)
        {

            int i, boundary_pt_count = (int)(Math.Ceiling(inpt_point_count / 100f));
            int boundary_pt_count2;

            List<Planar_object_store.point2d> temp_pt_list = new List<Planar_object_store.point2d>(); // create a temporary list to store the points

            if (x_coord_limit > y_coord_limit)
            {
                // Width is higher
                boundary_pt_count2 = (int)boundary_pt_count * (x_coord_limit / y_coord_limit);
                int width_iter = (x_coord_limit) / boundary_pt_count2;
                for (i = -boundary_pt_count2 + 1; i < boundary_pt_count2 - 1; i++)
                {
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, width_iter * i, +y_coord_limit));
                    pt_index++;
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, width_iter * i, -y_coord_limit));
                    pt_index++;
                }


                int height_iter = (y_coord_limit) / boundary_pt_count;
                for (i = -boundary_pt_count + 1; i < boundary_pt_count - 1; i++)
                {
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, +x_coord_limit, height_iter * i));
                    pt_index++;
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, -x_coord_limit, height_iter * i));
                    pt_index++;
                }
            }
            else
            {
                // Width is less  
                int width_iter = x_coord_limit / boundary_pt_count;

                for (i = 0; i < boundary_pt_count; i++)
                {
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, width_iter * i, +y_coord_limit));
                    pt_index++;
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, width_iter * i, -y_coord_limit));
                    pt_index++;
                }

                boundary_pt_count2 = (int)boundary_pt_count * (x_coord_limit / y_coord_limit);
                int height_iter = y_coord_limit / boundary_pt_count2;
                for (i = 0; i < boundary_pt_count2; i++)
                {
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, +x_coord_limit, height_iter * i));
                    pt_index++;
                    temp_pt_list.Add(new Planar_object_store.point2d(pt_index, -x_coord_limit, height_iter * i));
                    pt_index++;
                }
            }

            foreach (Planar_object_store.point2d pt in temp_pt_list)
            {
                if (delaunay_triangle.delaunay_points.Exists(obj => obj.Equals(pt)) == false)
                    delaunay_triangle.delaunay_points.Add(pt);
            }

        }

        public class the_static_class // this class stores all the useful functions and static variables
        {
            public static SizeF canvas_size; // size of the canvas (main_pic) or external bounds
            public static PointF canvas_orgin; // orgin of the canvas (mainpic) or centre point of external bounds

            /// <summary>
                        /// Function to Check the valid of Numerical text from textbox.text
                        /// </summary>
                        /// <param name="tB_txt">Textbox.text value</param>
                        /// <param name="Negative_check">Is negative number Not allowed (True) or allowed (False)</param>
                        /// <param name="zero_check">Is zero Not allowed (True) or allowed (False)</param>
                        /// <returns>Return the validity (True means its valid) </returns>
                        /// <remarks></remarks>
            public static bool test_a_textboxvalue_validity_int(string tb_txt, bool n_chk, bool z_chk)
            {
                bool is_valid = false;
                //This function returns false if the textbox doesn't contains number 
                if (Int32.TryParse(tb_txt, out Int32 number) == true)
                {
                    is_valid = true;

                    if (n_chk == true) // check for negative number
                    {
                        if (Convert.ToInt32(tb_txt) < 0)
                        {
                            is_valid = false;
                        }
                    }

                    if (z_chk == true) // check for zero number
                    {
                        if (Convert.ToInt32(tb_txt) == 0)
                        {
                            is_valid = false;
                        }
                    }
                }
                return is_valid;
            }

            /// <summary>
                        /// Function to convert double to single (mostly used in System.Drawing functions)
                        /// </summary>
                        /// <param name="value"></param>
                        /// <returns></returns>
            public static float to_single(double value)
            {
                return (float)value;
            }

            /// <summary>
                        /// Funtion to check NAN or Infinity value
                        /// </summary>
                        /// <param name="chkval"></param>
                        /// <returns></returns>
            public static bool Isval_NAN_or_Infinity(double chkval)
            {
                return (double.IsNaN(chkval) || double.IsInfinity(chkval));
            }
        }

    }
}
