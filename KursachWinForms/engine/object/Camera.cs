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
            angle_rad = new Vector3();
        }

        public Camera(Vector3 angle) {
            angle_rad = new Vector3();
            this.angle = angle;
        }

    }
}
