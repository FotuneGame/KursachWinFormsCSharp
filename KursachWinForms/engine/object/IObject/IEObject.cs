using Engine.Component;

namespace Engine.Object
{
    internal interface IEObject
    {
        Transform transform { get; set; }
        Renderer renderer { get; set; }
        string name { get; set; }
    }
}
