using Engine.Math;

namespace Engine.Component
{
    internal interface ITransform
    {
        Vector3 position { get; set; }
        Vector3 angle { get; set; }
        Vector3 size { get; set; }
        void Rotate(Vector3 point, Vector3 angle = null);

    }
}
