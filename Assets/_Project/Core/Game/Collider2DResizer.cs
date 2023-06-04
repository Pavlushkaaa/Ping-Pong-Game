using NaughtyAttributes;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    public class Collider2DResizer : MonoBehaviour
    {
        [Button("RESIZE")]
        private void CalculateColliderSize()
        {
            if (TryGetComponent<Collider2D>(out var target))
            {
                var child = GetComponentInChildren<SpriteRenderer>();

                if (target is BoxCollider2D)
                {
                    var collider = (BoxCollider2D)target;
                    BoxCollider2D temp = child.AddComponent<BoxCollider2D>();
                    collider.size = temp.size * temp.transform.localScale;
                    DestroyImmediate(temp);
                }
                else if (target is PolygonCollider2D)
                {
                    var collider = (PolygonCollider2D)target;
                    PolygonCollider2D temp = child.AddComponent<PolygonCollider2D>();

                    var scale = temp.transform.localScale;
                    collider.pathCount = temp.pathCount;

                    for (int i = 0; i < temp.pathCount; i++)
                    {
                        Vector2[] points = temp.GetPath(i);

                        for (int j = 0; j < points.Length; j++)
                            points[j] = points[j] * scale;

                        collider.SetPath(i, points);
                    }

                    DestroyImmediate(temp);
                }
                else if (target is CircleCollider2D)
                {
                    var collider = (CircleCollider2D)target;
                    CircleCollider2D temp = child.AddComponent<CircleCollider2D>();
                    collider.radius = temp.radius * Mathf.Max(temp.transform.localScale.x, temp.transform.localScale.y);
                    DestroyImmediate(temp);
                }
                else
                    throw new ArgumentNullException("Collider should be: BoxCollider2D, PolygonCollider2D, CircleCollider2D");
            }
            else
                throw new NullReferenceException("Add a collider to the object!");
        }
    }
}
