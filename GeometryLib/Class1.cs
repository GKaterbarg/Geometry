using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib
{
    public class Geometry
    {
        public static bool isPointOnLine(Int32 lineX1, Int32 lineY1, Int32 lineX2,
                                       Int32 lineY2, Int32 pointX, Int32 pointY)
        {
            Double e = 0.005;
            if (lineX1 == lineX2) return (lineX1 == pointX);
            Double k = Convert.ToDouble(lineY1 - lineY2) / Convert.ToDouble(lineX1 - lineX2);
            Double y = lineY2 + (pointX - lineX2) * k;
            return (Math.Abs(Convert.ToDouble(pointY)) - y) < e;
        }
    }
}