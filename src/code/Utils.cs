static class Utils
{
    public static float Lerp(float a, float b, float t) => (1-t) * a + t * b;
    public static float InverseLerp(float a, float b, float v) => (v-a) / (b-a);
}

static class NodeExtenstions
{
    public static void RemoveChildren(this Node node)
    {
        foreach(Node child in node.GetChildren())
        {
            node.RemoveChild(child);
            child.QueueFree();
        }
    }

    public static void AddChildren(this Node node, IEnumerable<Node> children)
    {
        foreach(Node child in children)
            node.AddChild(child);
    }
}

static class EnumerableExtensions
{
    public static void Iterate<T>(this IEnumerable<T> source, Action<T> func)
    {
        foreach(T value in source)
            func(value);
    }
}