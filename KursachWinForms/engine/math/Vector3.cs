

namespace Engine.Math
{
    public sealed class Vector3
    {
        public double x, y, z;

        public Vector3() {
            x = 0; y=0; z=0;
        }
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(Vector3 a)
        {
            x = a.x; y = a.y; z = a.z;
        }

        public void set(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static Vector3 operator+(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector3 operator *(Vector3 a, double numb)
        {
            return new Vector3(a.x * numb, a.y * numb, a.z * numb);
        }

        // векторная произведение векторов 
        public static Vector3 operator ^(Vector3 a, Vector3 b)
        {
            return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public override string ToString()
        {
            return "(" + System.Math.Round(x, 1) +" "+ System.Math.Round(y, 1) + " "+ System.Math.Round(z, 1) + ")";
        }

        public Vector3 normalize()
        {
            double length = System.Math.Sqrt(x*x+y*y+z*z);
            if(length!=0)
                return new Vector3(x / length, y / length, z / length);
            else
                return new Vector3(x,y,z);
        }

        public double getLength()
        {
            return System.Math.Sqrt(x * x + y * y + z * z);
        }

    }

}
