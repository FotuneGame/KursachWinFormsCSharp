using Engine.Math;

namespace Engine.Object
{
    internal interface ICamera
    {
        Vector3 position { get; set; }
        double zoom { get; set; }
        Vector3 angle { get; set; }
    }
}
