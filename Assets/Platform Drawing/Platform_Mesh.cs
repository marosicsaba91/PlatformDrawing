using UnityEngine;

public class Platform_Mesh : Platform
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;

    protected override void UpdateVisual(Vector2[] shape, Mesh mesh)
    {
        meshFilter.mesh = mesh;
    }
}


