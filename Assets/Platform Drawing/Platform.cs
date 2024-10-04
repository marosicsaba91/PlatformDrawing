using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] PolygonCollider2D polygonCollider;
    [SerializeField] Rigidbody2D rigidBody2D;

    public float Area { get; private set; }

    public void SetShape(List<Vector2> shape)
    {
        polygonCollider.enabled = true;
        Vector2[] normalizedShape = Utility.NormalizeShape(shape, out Vector2 center);
        polygonCollider.points = normalizedShape;
        transform.position = center;
        rigidBody2D.position = center;
        Mesh mesh = polygonCollider.CreateMesh(false, false);

        Area = mesh == null ? 0 : Utility.CalculateMeshArea(mesh);

        UpdateVisual(normalizedShape, mesh);
    }

    protected abstract void UpdateVisual(Vector2[] shape, Mesh mesh);
}









