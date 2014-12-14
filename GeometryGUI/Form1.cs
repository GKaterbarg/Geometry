using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometryLib;

namespace GeometryGUI
{
    public partial class Form1 : Form
    {
        Int32 xMax; Int32 xMin; Int32 yMax;  Int32 yMin;
        Point A; Point M; Point N;
        Pen penLine; SolidBrush brushPaint; Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        private void Start(object sender, EventArgs e)
        {
            xMax = pictureBox1.Width - 10;
            xMin = 10;
            yMax = pictureBox1.Width - 10;
            yMin = 10;
            A = new Point(0, 0);
            M = new Point(0, 0);
            N = new Point(0, 0);
            g = Graphics.FromHwnd(pictureBox1.Handle);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            penLine = new Pen(Color.Black, 3);
            brushPaint = new SolidBrush(System.Drawing.Color.Red);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start(this, null);
            Random rand = new Random();
            Int32 x = (xMax - xMin) / 2;
            Int32 y = (yMax - yMin) / 2;
            A = new Point(rand.Next(x - 5, x + 5), rand.Next(y - 5, y + 5));
            do
            {
                M = new Point(rand.Next(x - 5, x + 5), rand.Next(y - 5, y + 5));
                N = new Point(rand.Next(x - 5, x + 5), rand.Next(y - 5, y + 5));
            } while ((M.X == N.X) && (M.Y == N.Y));
            Painting(this, null);
            ViewCoordinates();
            Result();
        }
        private void ViewCoordinates()
        {
            String pointM = "(" + Convert.ToString(M.X) + "," + Convert.ToString(M.Y) + ")";
            String pointN = "(" + Convert.ToString(N.X) + "," + Convert.ToString(N.Y) + ")";
            String pointA = "(" + Convert.ToString(A.X) + "," + Convert.ToString(A.Y) + ")";
            label1.Text = pointM;
            label2.Text = pointN;
            label3.Text = pointA;
        }
       
        private void Result()
        {
            bool solution = Geometry.isPointOnLine(M.X, M.Y, N.X, N.Y, A.X, A.Y);
            if (solution)
                label5.Text = "Принадлежит";
            else
                label5.Text = "Не принадлежит";
        }
        private Point normalize(Point X)
        {
            Point Y = new Point(X.X, yMax - X.Y);
            return Y;
        }
        private Double f(int x)
        {
            Int32 kx = M.X - N.X;
            Int32 ky = M.Y - N.Y;
            Double k = Convert.ToDouble(ky) / Convert.ToDouble(kx);
            return N.Y + (x - N.X) * k;
        }
        

        private void Painting(object sender, PaintEventArgs e)
        {
            g.Clear(Color.White);
            if ((A == M) && (M == N))
                return;
            Point M2 = normalize(M);
            Point N2 = normalize(N);
            Point A2 = normalize(A);
            if (M2.X == N2.X)
            {
                g.DrawLine(penLine, new Point(M2.X, yMin), new Point(M2.X, yMax));
            }
            else
            {
                for (int i = xMin; i <= xMax; i++)
                {
                    Double yd = yMax - f(i);
                    if ((yd >= yMin) && (yd <= yMax))
                        M2 = new Point(i, Convert.ToInt32(Math.Round(yd)));
                }
                for (int i = xMax; i >= xMin; i--)
                {
                    Double yd = yMax - f(i);
                    if ((yd <= yMax) && (yd >= yMin))
                        N2 = new Point(i, Convert.ToInt32(Math.Round(yd)));
                }
                g.DrawLine(penLine, M2, N2);
            }
            g.FillEllipse(brushPaint, new System.Drawing.Rectangle(A2.X - 2, A2.Y - 2, 4, 4));
        }
    }
}
