using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Math;

namespace Engine.Object
{
    public class Light
    {
        public Vector3 vector;

        public Light() {
            vector = new Vector3(0, 0, -1);
        }

        public Light(Vector3 vector)
        {
            this.vector = vector;
        }

    }
}
