using System.Collections.Generic;
using Godot;

namespace VeritaPlinko.Scripts;

public static class GodotStatics
{
    public static IEnumerable<Node> GetChildrenRecursively(this Node node)
    {
        foreach (Node child in node.GetChildren())
        {
            yield return child;
            foreach (Node grandchild in GetChildrenRecursively(child))
            {
                yield return grandchild;
            }
        }
    }
}