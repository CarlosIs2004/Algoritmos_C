using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Algortino_de_lineas
{
    
    public class Relleno
    {
        public PointF startPoint;
        public float centerX;
        public float centerY;
        public PointF pointPoligon;
        public const float SF = 10 / 2f;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public PictureBox canvas;
        public SolidBrush fadeBrush;
        public List<PointF> poinstPoligon;
        public PointF point;
        public Color color;
        public Timer timer;


        public Relleno(PictureBox picCanvas)
        {
            canvas = picCanvas;
            this.bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            this.bufferGraphics = Graphics.FromImage(bufferBitmap);
            this.point = new PointF();
            this.centerX = bufferBitmap.Width / 2;
            this.centerY = bufferBitmap.Height / 2;
        }
           

        public static Point[] Recursive_Flood_Fill(Bitmap canvas, int x, int y, Color color)
        {
            List<Point> points = new List<Point>();
            Color colorComp = canvas.GetPixel(x, y);
            if (color.ToArgb() != colorComp.ToArgb() )
            {
                canvas.SetPixel(x, y, color);  // Paint pixel
                
                points.Add(new Point(x, y));

                points.AddRange(Recursive_Flood_Fill(canvas, x, y - 1, color));  // Up
                points.AddRange(Recursive_Flood_Fill(canvas, x + 1, y, color));  // Right
                points.AddRange(Recursive_Flood_Fill(canvas, x, y + 1, color));  // Down
                points.AddRange(Recursive_Flood_Fill(canvas, x - 1, y, color));  // Left
            }

            return points.ToArray();
        }
        public static Point[] Iterative_Flood_Fill(Bitmap canvas, int x, int y, Color color)
        {
            Stack<Point> stack = new Stack<Point>();
            List<Point> points = new List<Point>();
            if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height)
                return new Point[0];

            Color targetColor = canvas.GetPixel(x, y);
            if (color.ToArgb() == targetColor.ToArgb())
                return new Point[0];

           
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                // Verifica límites
                if (p.X < 0 || p.X >= canvas.Width || p.Y < 0 || p.Y >= canvas.Height)
                    continue;

                Color currentColor = canvas.GetPixel(p.X, p.Y);
                if (currentColor.ToArgb() != targetColor.ToArgb())
                    continue;

                canvas.SetPixel(p.X, p.Y, color);
                points.Add(p);

                stack.Push(new Point(p.X + 1, p.Y)); // Derecha
                stack.Push(new Point(p.X - 1, p.Y)); // Izquierda
                stack.Push(new Point(p.X, p.Y + 1)); // Abajo
                stack.Push(new Point(p.X, p.Y - 1)); // Arriba
            }

            return points.ToArray();
        }
        

        public static PointF[] polygonPoints(PointF center,int nVertice, float radius) {
            radius = radius * SF;
            PointF[] pointsPoligonArr = new PointF[(int)nVertice];
            float angle = 45;
            float radianes;
            for (int i = 0; i < nVertice; i++){
                
                radianes = angle * ((float)Math.PI / 180);
                float x = center.X + (radius) * (float)(Math.Cos(radianes));
                float y= center.Y + (radius) * (float)(Math.Sin(radianes));
                pointsPoligonArr[i] = new PointF(x, y);
                angle += (360 / nVertice);

            }
            return pointsPoligonArr;
        }

       public static Point[] ScanlineFillPoints(Bitmap canvas, PointF[] polygon)
        {
            List<Point> fillPoints = new List<Point>();

            if (polygon == null || polygon.Length < 3)
                return fillPoints.ToArray();

            int minY = (int)polygon.Min(p => p.Y);
            int maxY = (int)polygon.Max(p => p.Y);

            for (int y = minY; y <= maxY; y++)
            {
                List<int> nodes = new List<int>();
                int j = polygon.Length - 1;
                for (int i = 0; i < polygon.Length; i++)
                {
                    if ((polygon[i].Y < y && polygon[j].Y >= y) ||
                        (polygon[j].Y < y && polygon[i].Y >= y))
                    {
                        int x = (int)(polygon[i].X + (float)(y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X));
                        nodes.Add(x);
                    }
                    j = i;
                }
                nodes.Sort();
                for (int k = 0; k < nodes.Count; k += 2)
                {
                    if (k + 1 >= nodes.Count) break;
                    for (int x = nodes[k]; x < nodes[k + 1]; x++)
                    {
                        fillPoints.Add(new Point(x, y));
                        canvas.SetPixel(x, y, Color.Blue);

                    }
                }
            }

            return fillPoints.ToArray();
        }


    }
}
