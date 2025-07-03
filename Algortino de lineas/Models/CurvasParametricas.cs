using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algortino_de_lineas.Models
{
    internal class CurvasParametricas
    {
        public static PointF CurvaBezierCuadratica(double t, PointF p0, PointF p1, PointF p2)
        {
            float x = (float)(Math.Pow(1 - t, 2) * p0.X +
                              2 * (1 - t) * t * p1.X +
                              Math.Pow(t, 2) * p2.X);

            float y = (float)(Math.Pow(1 - t, 2) * p0.Y +
                              2 * (1 - t) * t * p1.Y +
                              Math.Pow(t, 2) * p2.Y);

            return new PointF(x, y);
        }

    }


}
