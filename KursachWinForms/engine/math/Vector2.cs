using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Math
{
    public sealed class Vector2
    {
        public double x, y;

        public Vector2()
        {
            x = 0; y = 0;
        }
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(Vector2 a)
        {
            x = a.x; y = a.y;
        }

        public void set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator *(Vector2 a, double numb)
        {
            return new Vector2(a.x * numb, a.y * numb);
        }
        public Vector2 normalize()
        {
            double length = System.Math.Sqrt(x * x + y * y);
            return new Vector2(x / length, y / length);
        }

        public double getLength()
        {
            return System.Math.Sqrt(x * x + y * y);
        }

        public override string ToString()
        {
            return "(" + x + " " + y  + ")";
        }

    }

}
