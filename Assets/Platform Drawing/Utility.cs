using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector2[] NormalizeShape(List<Vector2> shape, out Vector2 center)
    {
        center = Vector2.zero;

        foreach (Vector2 point in shape)
            center += point;

        center /= shape.Count;

        Vector2[] normalizedShape = new Vector2[shape.Count];
        for (int i = 0; i < shape.Count; i++)
            normalizedShape[i] = shape[i] - center;

        return normalizedShape;
    }

    public static bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        float dx1 = p2.x - p1.x;
        float dy1 = p2.y - p1.y;
        float dx2 = q2.x - q1.x;
        float dy2 = q2.y - q1.y;

        float d = dx1 * dy2 - dy1 * dx2;
        if (d == 0)
            return false;

        float t = ((q1.x - p1.x) * dy2 - (q1.y - p1.y) * dx2) / d;
        float u = ((q1.x - p1.x) * dy1 - (q1.y - p1.y) * dx1) / d;

        return t >= 0 && t <= 1 && u >= 0 && u <= 1;
    }

    public static bool IsClockwise(Vector2 center, Vector2[] shape)
    {
        float sum = 0;

        Vector2 last = shape[^1] - center;
        for (int i = 0; i < shape.Length; i++)
        {
            Vector2 point = shape[i] - center; 
            sum += (point.x - last.x) * (point.y + last.y);
            last = point;
        }

        return sum > 0;
    }
    public static float CalculateMeshArea(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        float area = 0;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 a = vertices[triangles[i]];
            Vector3 b = vertices[triangles[i + 1]];
            Vector3 c = vertices[triangles[i + 2]];

            area += CalculateTriangleArea(a, b, c);
        }

        return area;
    }

    public static float CalculateTriangleArea(Vector3 a, Vector3 b, Vector3 c)
    {
        float ab = Vector3.Distance(a, b);
        float bc = Vector3.Distance(b, c);
        float ca = Vector3.Distance(c, a);

        float s = (ab + bc + ca) / 2;
        return Mathf.Sqrt(s * (s - ab) * (s - bc) * (s - ca));
    }


}


