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
        

        public void polygon(int nVertice, float radius) {
            radius = radius * SF;
            PointF[] pointsPoligonArr = new PointF[(int)nVertice];
            float angle = 45;
            float radianes;
            for (int i = 0; i < nVertice; i++){
                
                radianes = angle * ((float)Math.PI / 180);
                float x = centerX + (radius) * (float)(Math.Cos(radianes));
                float y= centerY + (radius) * (float)(Math.Sin(radianes));
                pointsPoligonArr[i] = new PointF(x, y);
                angle += (360 / nVertice);

            }
            
            bufferGraphics.DrawPolygon(Pens.Blue, pointsPoligonArr);
        }


    }
}
