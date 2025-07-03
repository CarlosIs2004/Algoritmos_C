using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algortino_de_lineas.Models;

namespace Algortino_de_lineas.Controllers
{

    internal class GraficarFigurasController
    {
        public PictureBox canvas;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public int cx;
        public int cy;
        public Point[] puntos;
        public Point punto;
        public int iterador;
        public const int SF = 10;

        public GraficarFigurasController(PictureBox canvas) {
            this.canvas = canvas;
            this.bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            this.bufferGraphics = Graphics.FromImage(bufferBitmap);
            this.bufferGraphics.Clear(Color.White);
            this.cx = (bufferBitmap.Width / 2);
            this.cy = (bufferBitmap.Height / 2);

        }


        public void DrawNextStep() {
            punto = puntos[iterador];
            bufferGraphics.FillRectangle(Brushes.Black, cx + (punto.X * SF), (cy) + (-punto.Y * SF), 5, 5);
            canvas.Image = bufferBitmap;
            iterador++;
        }
        public void DrawNextStep2()
        {
            punto = puntos[iterador];
            bufferGraphics.FillRectangle(Brushes.Blue, punto.X , punto.Y , 5, 5);
            canvas.Image = bufferBitmap;
            iterador++;
        }

        public void DibujarDDa(Point puntoInicio, Point puntoFinal) {
            iterador = 0;
            bufferGraphics.FillRectangle(Brushes.Blue, puntoInicio.X, -puntoInicio.Y, 5, 5);
            canvas.Image = bufferBitmap;
            puntos = LineAlgoritm.DDA_Line(puntoInicio, puntoFinal);

        }



        public void ReadDataCircunferencia(TextBox[] textBoxNames)
        {
            iterador = 0;
            int radio = int.Parse(textBoxNames[0].Text);
            puntos = DiscretizacionCircunferencia.Bresenham_Circle(new Point(0, 0), radio);
     
        }
        public void ReadDataElipse(TextBox[] textBoxNames)
        {
            iterador = 0;
            int rx = int.Parse(textBoxNames[0].Text);
            int ry = int.Parse(textBoxNames[1].Text);
            puntos = DiscretizacionCircunferencia.Bresenham_Ellipse(new Point(0, 0), rx,  ry);

        }

        public void ReadDataRellenoScanline(TextBox[] textBoxNames) {


            Point center = new Point(cx, cy);
            PointF[] pointsPolygon = Relleno.polygonPoints(center, int.Parse(textBoxNames[0].Text), int.Parse(textBoxNames[1].Text));
            puntos = Relleno.ScanlineFillPoints(bufferBitmap, pointsPolygon);
            canvas.Image = bufferBitmap;

        }
        public void ReadDataRellenoFlood(TextBox[] textBoxNames)
        {
            Point center = new Point(cx, cy);
            PointF[] pointsPolygon = Relleno.polygonPoints(center, int.Parse(textBoxNames[0].Text), int.Parse(textBoxNames[1].Text));
            bufferGraphics.DrawPolygon(Pens.Blue,pointsPolygon);

            canvas.Image = bufferBitmap;

        }

        public PointF[] CalcularAreaVisible(int radio) {
            Point center = new Point(cx, cy);
            PointF[] pointsPolygon = Relleno.polygonPoints(center, 4, radio);
            bufferGraphics.Clear(Color.White);
            bufferGraphics.DrawPolygon(Pens.Blue, pointsPolygon);
            canvas.Image = bufferBitmap;
            return pointsPolygon;
        }
        public PointF[] Graficarlinea(List<PointF> points, PointF[] pointsCanvas) {

            return RecorteGeometrico.CohenSutherlandClipping(points, pointsCanvas);
        
        }

        public static bool validateInput(TextBox[] textBoxNames)
        {
            try
            {

                foreach (TextBox textBoxName in textBoxNames)
                {
                    if (string.IsNullOrWhiteSpace(textBoxName.Text))
                    {
                        MessageBox.Show("Algun dato está vacio, revisa tus campos de entrada");
                        return false;
                    }

                    if (!float.TryParse(textBoxName.Text, out float number))
                    {
                        MessageBox.Show($"El valor {textBoxName.Text} no es un numero, revisa tus campos de entrada");
                        return false;
                    }

                    if (float.TryParse(textBoxName.Text, out float number1))
                    {
                        if (number1 <= 0)
                        {
                            MessageBox.Show("No se permite numeros negativos o cero");
                            return false;
                        }
                    }
                }
            }
            catch {
                
                MessageBox.Show("Ingreso no valido....", "Mensaje de error");
                return false;
            }
            return true;
        }

        public static void inicilizarTablaPuntos(DataGridView dataGridView1)
        {

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "#";
            dataGridView1.Columns[0].Width = 25;

            int widthColumn = (dataGridView1.Width - dataGridView1.Columns[0].Width) / 2;

            dataGridView1.Columns[1].Name = "X";
            dataGridView1.Columns[1].Width = widthColumn;
            dataGridView1.Columns[2].Name = "Y";
            dataGridView1.Columns[2].Width = widthColumn;

        }
        public void DrawGrid()
        {
            using (Graphics g = Graphics.FromImage(bufferBitmap))
            {
                g.Clear(Color.White); // Fondo blanco

                int cellSize = 50; // Tamaño de cada celda
                int rows = canvas.Height / cellSize; // Número de filas
                int cols = canvas.Width / cellSize;  // Número de columnas

                // Dibujar líneas verticales
                for (int x = 0; x <= canvas.Width; x += cellSize)
                {
                    g.DrawLine(Pens.LightGray, x, 0, x, canvas.Height);
                }

                // Dibujar líneas horizontales
                for (int y = 0; y <= canvas.Height; y += cellSize)
                {
                    g.DrawLine(Pens.LightGray, 0, y, canvas.Width, y);
                }

                // Dibujar ejes de coordenadas
                g.DrawLine(Pens.Black, 0, 0, 0, canvas.Height); // Eje Y
                g.DrawLine(Pens.Black, 0, canvas.Height - 1, canvas.Width, canvas.Height - 1); // Eje X

                // Dibujar etiquetas de coordenadas en el eje X (comienza desde 0)
                for (int x = 0; x <= cols; x++)
                {
                    g.DrawString(x.ToString(), new Font("Arial", 8), Brushes.Black, x * cellSize + 5, canvas.Height - 20);
                }

                // Dibujar etiquetas de coordenadas en el eje Y (comienza desde 0)
                for (int y = 0; y <= rows; y++)
                {
                    g.DrawString(y.ToString(), new Font("Arial", 8), Brushes.Black, 5, canvas.Height - y * cellSize - 20);
                }
            }
            canvas.Image = bufferBitmap;
            canvas.Refresh(); // Actualizar la vista
            
        }
         


    }
}
