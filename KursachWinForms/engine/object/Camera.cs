using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Component;
using Engine.Math;

namespace Engine.Object
{
    // смотрит на начало координат под углом
    public class Camera
    {
        public Vector3 position;
        // коэфицент приближения
        private double zoom_size;
        public double zoom { 
            get { return zoom_size; }
            set {
                if (value >= 0.1)
                {
                    zoom_size = value;
                }
            }
        }
        //угол в радианах (для св-ва преобразуем в градусы)
        private Vector3 angle_rad;
        public Vector3 angle
        {
            get
            {
                return new Vector3((angle_rad.x * 180 / System.Math.PI), (angle_rad.y * 180 / System.Math.PI), (angle_rad.z * 180 / System.Math.PI));
            }
            set
            {
                angle_rad.set((value.x * System.Math.PI) / 180, (value.y * System.Math.PI) / 180, (value.z * System.Math.PI) / 180);
            }
        }

        public Camera() {
            zoom_size = 1;
            angle_rad = new Vector3();
            position = new Vector3();
        }

        public Camera(Vector3 angle) {
            zoom_size = 1;
            angle_rad = new Vector3();
            this.angle = angle;
            position = new Vector3();
        }
        public Camera(Vector3 angle,Vector3 position)
        {
            zoom_size = 1;
            angle_rad = new Vector3();
            this.angle = angle;
            this.position = position;
        }

    }
}
