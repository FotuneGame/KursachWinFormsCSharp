using Engine.Component;

namespace Engine.Object
{
    public class EObject : IEObject
    {
        public Transform transform { get; set; }
        public Renderer renderer { get; set; }
        public string name { get; set; }

        public EObject() {
            transform = new Transform();
            renderer = new Renderer(transform);
            name = "UnName";
        }

        public EObject(Transform transform)
        {
            this.transform = transform;
            renderer = new Renderer(transform);
            name = "UnName";
        }

        public EObject(string name)
        {
            transform = new Transform();
            renderer = new Renderer(transform);
            this.name = name;
        }

        public EObject(Renderer renderer)
        {
            transform = new Transform();
            this.renderer = renderer;
            name = "UnName";
        }

        public EObject(Transform transform,Renderer renderer)
        {
            this.transform = transform;
            this.renderer = renderer;
            name = "UnName";
        }

        public EObject(Transform transform, Renderer renderer,string name)
        {
            this.transform = transform;
            this.renderer = renderer;
            this.name = name;
        }


    }
}
