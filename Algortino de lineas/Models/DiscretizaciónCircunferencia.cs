using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algortino_de_lineas
{
    internal class DiscretizaciónCircunferencia
    {
        
        public float P;
        public float cx;
        public float cy;
        public float radius;
        public const float SF = 10;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public PictureBox canvas;
        public List<PointF> points;
        public PointF point;
        public int currentStep;
        public DiscretizaciónCircunferencia(PictureBox picCanvas, float radius)
        {
            canvas = picCanvas;
            this.radius = radius;
            this.points = new List<PointF>();
            this.point = new PointF();
            this.cx = 0;
            this.cy = 0;
            this.currentStep= 0;
        }
        public void initBuffer()
        {
            bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            bufferGraphics = Graphics.FromImage(bufferBitmap);
            bufferGraphics.Clear(Color.White);
            cx = (bufferBitmap.Width/2);
            cy = (bufferBitmap.Height/2);

        }
        public void calculaterCircunferencia() {
           
            SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 255));
            points.Clear();
            point.X = 0;
            point.Y = radius;
            P = 1 - radius;
            points.Add(point);
            while (point.X < point.Y)
            {
                point.X ++;
                if (P < 0)
                {
                    P = P + (2 * point.X) + 1;
                }
                else
                {
                    point.Y--;
                    P = P + (2 * (point.X - point.Y)) + 1;
                }
                points.Add(point);
            }
        }


        public void drawPointCircle(float x, float y)
        {

            bufferGraphics.FillRectangle(Brushes.Blue, cx + (x * SF), (cy + (-y * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx - (x * SF), (cy - (-y * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx + (x * SF), (cy - (-y * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx - (x * SF), (cy + (-y * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx + (y * SF), (cy + (-x * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx - (y * SF), (cy + (-x * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx - (y * SF), (cy - (-x * SF)), 5, 5);
            bufferGraphics.FillRectangle(Brushes.Blue, cx + (y * SF), (cy - (-x * SF)), 5, 5);
        }
        public void DrawNextStep()
        {
            SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 255));
            if (currentStep >= points.Count)
                return;

            point = points[currentStep];
            drawPointCircle(point.X, point.Y);
            canvas.Image = bufferBitmap;
            currentStep++;
        }



    }
}
