using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformDrawer : MonoBehaviour
{
    [SerializeField] float minPointDistance = 0.1f;
    [SerializeField] Platform platformPrototype;

    bool _isDrawing = false;
    List<Vector2> _points = new();
    Platform _currentPlatform;

    // Get the mouse position in the WORLD COORDINATES
    Vector2 MousePoint => Camera.main.ScreenToWorldPoint(Input.mousePosition);


    void Update()
    {   
        
        // When the left mouse button is JUST GOT PREESED
        if (Input.GetMouseButtonDown(0)) // (0 = Left, 1 = Right, 2 = Middle)
        {
            _points.Clear();
            _isDrawing = true;
            Vector2 startPoint = MousePoint;
            _points.Add(startPoint);
            _currentPlatform = Instantiate(platformPrototype, startPoint, Quaternion.identity);
        }

        if (!_isDrawing) return;

        if (Input.GetMouseButton(0))     // When the left mouse button is HELD DOWN
            UpdatePoint();

        if (Input.GetMouseButtonUp(0))   // When the left mouse button is JUST GOT RELEASED
        {
            _isDrawing = false;
            Destroy(_currentPlatform.gameObject);
        }
    }

    void UpdatePoint()
    {
        Vector2 currentPoint = MousePoint;
        float distance = Vector2.Distance(_points.Last(), currentPoint);
        if (distance > minPointDistance)
        {
            _points.Add(MousePoint);

            if (_points.Count > 3)
            {
                float distanceToStart = Vector2.Distance(_points.Last(), _points.First());
                if (distanceToStart < minPointDistance)
                    _isDrawing = false;

                Vector2 p1 = _points[^1];   // Last point
                Vector2 p2 = _points[^2];   // Second to last point
                for (int i = 0; i < _points.Count - 3; i++)
                {
                    Vector2 q1 = _points[i];
                    Vector2 q2 = _points[i + 1];
                    if (Utility.DoSegmentsIntersect(p1, p2, q1, q2))
                    {
                        _points.RemoveRange(0, i + 1);
                        _points.RemoveAt(_points.Count - 1);
                        _isDrawing = false;
                    }
                }
            }

            _currentPlatform.SetShape(_points);
        }
    }
     
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < _points.Count - 1; i++)
            Gizmos.DrawLine(_points[i], _points[i + 1]);
    }
}

