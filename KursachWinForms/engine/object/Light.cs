using Engine.Math;

namespace Engine.Object
{
    public class Light : ILight
    {
        public Vector3 vector {  get; set; }

        public Light() {
            vector = new Vector3(0, 0, -1);
        }

        public Light(Vector3 vector)
        {
            this.vector = vector;
        }

    }
}
