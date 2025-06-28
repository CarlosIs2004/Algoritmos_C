using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algortino_de_lineas
{
    internal class LineAlgoritm
    {
        public PointF startPoint;
        public PointF endPoint;
        public float P;
        public float varx;
        public float vary;
        public const float SF = 10/2f;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public PictureBox canvas;
        public List<PointF> puntos;
        float slope;
        public int currentStep;
        public PointF p;
        public LineAlgoritm(PictureBox picCanvas, PointF startPoint, PointF endPoint)
        {
            canvas = picCanvas;
            this.startPoint = startPoint;
            this.endPoint = endPoint; 
            this.puntos = new List<PointF>();
        }


        public void initBuffer()
        {
            bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            bufferGraphics = Graphics.FromImage(bufferBitmap);
            bufferGraphics.Clear(Color.White);
            varx = 0;
            vary = 0;
            currentStep = 0;
        }


        public void ddaCalculator() {
            puntos.Clear();
            float kIteraciones;
            varx = endPoint.X - startPoint.X;
            vary = endPoint.Y - startPoint.Y;
            slope = Math.Abs(vary / varx);
            kIteraciones = Math.Max(varx, vary);
            float x = startPoint.X;
            float y = startPoint.Y;
            puntos.Add(startPoint);

            for (int i = 0; i < kIteraciones; i++) {
                if (slope < 1)
                {
                    y = (float)Math.Round(y + slope, 2);
                    startPoint.Y = (float)Math.Round( (double)(y) );
                    startPoint.X++;
                 
                }
                else if (slope >= 1) {
                    x = x + (1 / slope);
                    startPoint.X = (float)Math.Round((double)(x));
                    startPoint.Y++;
                }
                puntos.Add(startPoint);      
            }
        }


        public void bresenhamCalculator()
        {
            puntos.Clear();
            float kIteraciones; 
            varx = endPoint.X - startPoint.X;
            vary = endPoint.Y - startPoint.Y;
            slope = Math.Abs(vary / varx);
            if (slope > 1){ P = (2 * varx) - vary; }
            else if (0 < slope && slope < 1){ P = (2 * vary) - varx;}
            kIteraciones = Math.Max(varx, vary);

            puntos.Add(startPoint);
            pointsCalculator(kIteraciones, startPoint, endPoint, P);

        }

        public void pointsCalculator(float i, PointF startPoint, PointF endPoint, float P)
        {
            if (i < 0) { return; }
            if (P >= 0){
                startPoint.X++;
                startPoint.Y++;
                P = P + (2 * vary) - (2 * varx);
            }
            else if (P < 0){
                startPoint.X++;
                P = P + (2 * vary);
            }
            puntos.Add(startPoint);
            i--;
            pointsCalculator(i, startPoint, endPoint, P);
        }

        // Metodo para poder mostrar cada punto Paso a Paso
        public void DrawNextStep()
        {
            if (currentStep >= puntos.Count)
                return;

            p = puntos[currentStep];

            bufferGraphics.FillRectangle(Brushes.Black, (p.X * SF)+(bufferBitmap.Width/2), (-p.Y * SF) + (bufferBitmap.Height / 2), 3, 3);
            canvas.Image = bufferBitmap;

            currentStep++;
        }

    }
}
