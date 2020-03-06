using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        System.Drawing.Graphics graphicsObj;
        List<Point> customers;
        List<Point> suppliers;

        public Form1()
        {
            InitializeComponent();
        }

        public void ShowGraph()
        {
            
            CreateRectangle();
            customers = PopulateCustomers();
            suppliers = PopulateSuppliers();
            MapCustomerSupplier();
            btnShow.Hide();
            btnShow.Enabled=false;
        }

        private List<Point> PopulateSuppliers()
        {
            return new List<Point>()
            {
                new Point(200, 50),
                new Point(250, 250),
                new Point(325, 325),
                new Point(450, 50),
                new Point(425, 475)
            };
        }

        private void CreateRectangle()
        {
            Pen pen = new Pen(Color.Black);
            Rectangle rectangle = new Rectangle(0, 0, 600, 600);
            graphicsObj.DrawRectangle(pen, rectangle);     
        }

        private void MapCustomerSupplier()
        {
            foreach (var cus in customers)
            {
                Point start = cus;
                Point end = suppliers.First();

                double minLength = GetLength(start, end);

                foreach (var sup in suppliers)
                {
                    double length = GetLength(cus, sup);
                    if (length < minLength)
                    {
                        end = sup;
                        minLength = length;
                    }
                }
                graphicsObj.DrawLine(GetPen(suppliers.IndexOf(end)), start, end);
            }
        }

        private Pen GetPen(int index)
        {
            List<Color> colors = new List<Color>()
            {
                Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Gray
            };

            return new Pen(colors[index]);
        }

        private double GetLength(Point cus, Point sup)
        {
            return Math.Sqrt((SquareOf(sup.X - cus.X)) + SquareOf(sup.Y - cus.Y));
        }

        private int SquareOf(int a)
        {
            return a * a;
        }

        private List<Point> PopulateCustomers()
        {
            List<Point> points = new List<Point>();
            Random random = new Random();

            for (int i = 1; i <= 100; i++)
            {
                points.Add(new Point(random.Next(0, 600), random.Next(0, 600)));
            }
            return points;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graphicsObj = this.CreateGraphics();
            ShowGraph();
        }
    }
}
