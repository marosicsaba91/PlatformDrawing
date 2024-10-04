using System;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class Platform_SpriteShape : Platform
{
    [SerializeField] SpriteShapeRenderer spriteShapeRenderer;
    [SerializeField] SpriteShapeController spriteShapeController;
    [SerializeField] TMP_Text areaText;

    protected override void UpdateVisual(Vector2[] shape, Mesh mesh)
    {
        spriteShapeController.autoUpdateCollider = false;
        spriteShapeRenderer.enabled = true;

        if (!Utility.IsClockwise(Vector2.zero, shape))
            Array.Reverse(shape);

        spriteShapeController.spline.Clear();
        for (int i = 0; i < shape.Length; i++)
            spriteShapeController.spline.InsertPointAt(i, shape[i]);

        areaText.enabled = true;
        areaText.text = Area.ToString("0.0");
    }
}


