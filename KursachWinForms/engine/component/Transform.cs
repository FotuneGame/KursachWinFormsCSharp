using Engine.Math;

namespace Engine.Component
{
    public class Transform : ITransform
    {
        private Vector3 pos_y_invert;
        public Vector3 position { 
            get {
                return pos_y_invert; 
            } 
            set { 
                pos_y_invert = value;
                pos_y_invert.y = -value.y;
            } 
        }
        public Vector3 size { get; set; }

        //угол в радианах (для св-ва преобразуем в градусы)
        private Vector3 angle_rad;
        public Vector3 angle {
            get
            {
                return new Vector3((angle_rad.x * 180/ System.Math.PI), (angle_rad.y * 180 / System.Math.PI), (angle_rad.z * 180 / System.Math.PI));
            }
            set
            {
                angle_rad.set((value.x * System.Math.PI) / 180, (value.y * System.Math.PI) / 180, (value.z * System.Math.PI) / 180);
            }
        }

        public Transform()
        {
            pos_y_invert = new Vector3(0, 0, 0);
            position = new Vector3(0,0,0);
            size = new Vector3(1,1,1);
            angle_rad = new Vector3(0,0,0);
        }

        public Transform(Vector3 position)
        {
            pos_y_invert = new Vector3(0, 0, 0);
            this.position = new Vector3(position.x,position.y,position.z);
            size = new Vector3(1,1,1);
            angle_rad = new Vector3(0,0,0);
        }

        public Transform(Vector3 position,Vector3 size)
        {
            pos_y_invert = new Vector3(0, 0, 0);
            this.position = new Vector3(position.x, position.y, position.z);
            this.size = new Vector3(size);
            angle_rad = new Vector3(0, 0, 0);
        }

        public Transform(Vector3 position, Vector3 size, Vector3 angle)
        {
            pos_y_invert = new Vector3(0, 0, 0);
            this.position = new Vector3(position.x, position.y, position.z);
            this.size = new Vector3(size);
            angle_rad = new Vector3();
            this.angle = new Vector3(angle);
        }

        // вращение вершины по осям эйлера 
        public void Rotate(Vector3 point, Vector3 angle =null)
        {
                if (angle == null) angle = angle_rad;
                else angle = new Vector3((angle.x * System.Math.PI) / 180, (angle.y * System.Math.PI) / 180, (angle.z * System.Math.PI) / 180);
                Vector3 tmp = new Vector3(point);
                
                point.x = tmp.x * System.Math.Cos(angle.z) - tmp.y * System.Math.Sin(angle.z);
                point.y = tmp.x * System.Math.Sin(angle.z) + tmp.y * System.Math.Cos(angle.z);

                tmp.set(point.x,point.y,point.z);

                point.x  = tmp.x * System.Math.Cos(angle.y) - tmp.z * System.Math.Sin(angle.y);
                point.z  = tmp.x * System.Math.Sin(angle.y) + tmp.z * System.Math.Cos(angle.y);

                tmp.set(point.x, point.y, point.z);

                point.y  = tmp.y * System.Math.Cos(angle.x) - tmp.z * System.Math.Sin(angle.x);
                point.z  = tmp.y * System.Math.Sin(angle.x) + tmp.z * System.Math.Cos(angle.x);
                
        }

    }
}
